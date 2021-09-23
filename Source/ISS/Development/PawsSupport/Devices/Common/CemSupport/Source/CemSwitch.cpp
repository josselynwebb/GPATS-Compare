///////////////////////////////////////////////////////////////////////////////
// File    : cemswitch.cpp
//
// Purpose : Provides function for closing all switches in system.. 
//
//
//
// Date:	11OCT05
//
// Functions
// Name						Purpose
// =======================  ===================================================
// cs_DoSwitch()			Retrieves switch triplets and calls switch server
//
// Revision History
// Rev	  Date                  Reason							Author
// ===  ========  =======================================  ====================
// 1.0  11OCT05   Initial baseline release.                TGM EADS
//
///////////////////////////////////////////////////////////////////////////////

#include <vector>
#include "cem.h"
#include "cemsupport.h"
#include "swxsrvr.h"

using std::vector;

// Local static variables
//typedef struct PathData {
//	long	blk;
//	long	mod;
//	long	pth;
//}	Triplet;

typedef vector<Triplet> PathData;

// Local static functions

// prototypes for switch calls - resolved at link time with WCEM
//extern "C" int CloseSwitches(PathData &, char *Response, int BufferSize);
//extern "C" int OpenSwitches(PathData &, char *Response, int BufferSize);
//extern "C" int GetErrorMessage(int &, char *, char *);

int cs_DoSwitching(int key)
{
	DATUM * pDatum;
	PathData paths;
	int nCount = 0;
	int nTripletCnt = 0;
	int nIdx = 0;
	int nPathIdx = 0;
	char sDevName[8] = "";
    char Response[4096] = "";
    int  RetVal = 0;

	// test for CONNECT data
	if ((pDatum = RetrieveDatum(key, K_CON)) != NULL) {
		nCount = DatCnt(pDatum);
		nTripletCnt = nCount / 3;

		paths.resize(nTripletCnt);

		for (nIdx = 0; nIdx < nTripletCnt; nIdx++) {
			paths[nIdx].blk = INTDatVal(pDatum, nPathIdx++);
			paths[nIdx].mod = INTDatVal(pDatum, nPathIdx++);
			paths[nIdx].pth = INTDatVal(pDatum, nPathIdx++);
		}

		CloseSwitches(paths,Response,4096);
		FreeDatum(pDatum);
        if(Response[0] != '\0')
        {
            RetVal = cs_ParseResponseError(Response, NULL, 0);
        }
	}
	else if ((pDatum = RetrieveDatum(key, K_DIS)) != NULL) {
		nCount = DatCnt(pDatum);
		nTripletCnt = nCount / 3;

		paths.resize(nTripletCnt);

		for (nIdx = 0; nIdx < nTripletCnt; nIdx++) {
			paths[nIdx].blk = INTDatVal(pDatum, nPathIdx++);
			paths[nIdx].mod = INTDatVal(pDatum, nPathIdx++);
			paths[nIdx].pth = INTDatVal(pDatum, nPathIdx++);
		}

		OpenSwitches(paths,Response,4096);
		FreeDatum(pDatum);
        if(Response[0] != '\0')
        {
            RetVal = cs_ParseResponseError(Response, NULL, 0);
        }
	}

	return(RetVal);
}

