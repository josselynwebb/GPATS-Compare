//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Cbts.h
//
// Date:	    23-Mar-06
//
// Purpose:	    This file contains data structures and constants common to 
//				all CBTS classes. 
//
// Revision History described in Cbts.cpp
//
///////////////////////////////////////////////////////////////////////////////
#ifndef CBTS_INCLUDED
#define CBTS_INCLUDED

typedef int COMM_HANDLE;

typedef struct AttStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;
	char Address[50];

}AttributeStruct;

#endif