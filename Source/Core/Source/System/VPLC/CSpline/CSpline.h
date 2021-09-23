//-----------------------------------------------------------------------------
// File:     CSpline.h
// Contains: This is the main interface file for the CSpline.dll which exist only
//  to put a wrapper around the gsl CSpline functions. This wrapper was required
//  because I was unable to include this gsl feature in the RFMS project without
//  linker problems. There may be a way to overcome the linker problems, but I
//  ran out of time to deal with it.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// History
//-----------------------------------------------------------------------------
// Date      Eng  PCR  Description
// --------  ---  ---  --------------------------------------------------------
// 04/10/06  AJP       Initial creation
//-----------------------------------------------------------------------------

#pragma warning( disable : 4786 ) // disable warning that debug cannot support the vector class
#include <vector>
#include <stdlib.h>
#include <gsl\gsl_errno.h>
#include <gsl\gsl_spline.h>

#ifdef CSPLINE_EXPORTS

#define CSPLINE_API __declspec(dllexport)
#else
#define CSPLINE_API __declspec(dllimport)
#endif

#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif
int CSPLINE_API initSpline(long *handle, double *xArray, double *yArray, int size);
int CSPLINE_API evalSpline(long handle, double x, double *result);
int CSPLINE_API freeSpline(long handle);

#if defined(__cplusplus) || defined(__cplusplus__)
}

class CSpline{

	public:
		CSpline();
		~CSpline();
		
		int init(long *handle, double *xArray, double *yArray, int size);
		int getValue(long handle, double x, double *result);
		int freeSpline(long handle);
	
	private:
		struct st_SplineVec {
			gsl_spline *Spline;
			gsl_interp_accel  *Accel;
			long handle;			// index into vector of structs		
		};
		
		std::vector<st_SplineVec> m_SplineVec;
};

#endif
