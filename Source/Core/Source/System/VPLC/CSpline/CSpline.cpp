///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// History
///////////////////////////////////////////////////////////////////////////////
// Date      Eng  PCR  Description
// --------  ---  ---  --------------------------------------------------------
// 07/22/05  AJP       Initial creation
///////////////////////////////////////////////////////////////////////////////

#include "CSpline.h"

CSpline::CSpline(){


}

CSpline::~CSpline(){

	long SplineVecSize = m_SplineVec.size();
		
	for (long i=0; i<SplineVecSize; i++){

		if (m_SplineVec[i].Spline != 0){
			gsl_spline_free(m_SplineVec[i].Spline);
		}
		if (m_SplineVec[i].Accel != 0){
			gsl_interp_accel_free(m_SplineVec[i].Accel);
		}
	}
}
		
//-----------------------------------------------------------------------------
// init
//  Initializes the spline and stores the spline pointer data into m_SplineVec
//   vector. A handle to the newly created spline is passed back via the arg
//   handle.
// Returns: -1 if GSL throws any exceptions, otherwise 0.
//-----------------------------------------------------------------------------
int CSpline::init(long *handle, double *xArray, double *yArray, int size){

	st_SplineVec tmpVec;
	long VecSize;

	try {
		tmpVec.Spline = gsl_spline_alloc (gsl_interp_cspline, size);
		tmpVec.Accel = gsl_interp_accel_alloc ();
		gsl_spline_init (tmpVec.Spline, xArray, yArray, size);
	}catch(...){
		return -1;
	}	
	
	// determine this vector size so we can obtain the handle
	VecSize = m_SplineVec.size();
	tmpVec.handle = VecSize;
	*handle = VecSize;
	m_SplineVec.push_back(tmpVec);
	return 0;
}

//-----------------------------------------------------------------------------
// evalSpline
// Returns: -1 if GSL throws any exceptions, otherwise 0.
//-----------------------------------------------------------------------------
int CSpline::getValue(long handle, double x, double *result){

	try{	
		*result = gsl_spline_eval (m_SplineVec[handle].Spline, x, m_SplineVec[handle].Accel);
	}catch(...){
		return -1;
	}
	return 0;
}

//-----------------------------------------------------------------------------
// freeSpline
// Returns: -1 if GSL throws any exceptions, otherwise 0.
//-----------------------------------------------------------------------------
int CSpline::freeSpline(long handle){

	try{
		if (m_SplineVec[handle].Spline != 0){
			gsl_spline_free(m_SplineVec[handle].Spline);
			m_SplineVec[handle].Spline = 0;
		}
		
		if (m_SplineVec[handle].Accel != 0){
			gsl_interp_accel_free(m_SplineVec[handle].Accel);
			m_SplineVec[handle].Accel = 0;
		}
	}catch(...){
		return -1;
	}
	return 0;
}