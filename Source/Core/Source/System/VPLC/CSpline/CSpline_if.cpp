///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// History
///////////////////////////////////////////////////////////////////////////////
// Date      Eng  PCR  Description
// --------  ---  ---  --------------------------------------------------------
// 07/22/05  AJP       Initial creation
///////////////////////////////////////////////////////////////////////////////

#include <stdlib.h>
#include "CSpline.h"

CSpline spline;

int initSpline(long *handle, double *xArray, double *yArray, int size){

	return (spline.init(handle, xArray,yArray,size));
}

int evalSpline(long handle, double x, double *result){

	return (spline.getValue(handle,x,result));
}

int freeSpline(long handle){

	return spline.freeSpline(handle);
}