///////////////////////////////////////////////////////////////////////////////
// File    : TETS_Switch.cpp
//
// Purpose : Perform actual general purpose switch control.
//
//
// Date    : 05/12/04
//
// Functions
// Name						Purpose
// =======================  ===================================================
// InitSwitch()             Initialize all the switch cards
// ReleaseSwitch()			Reset all switches and release all allocated memory.
// CloseSwitches()          Operate the specified switches to the "Closed" position.
// OpenSwitches()           Operate the specified switches to the "Open" position.
// ResetSwitch()            Reset all switch cards to their quiesent state.
// GetErrorMessage()        Query the appropriate card(s) for error message
//
// Revision History
//           Revisions tracked in SwxSrvr.cpp file
//
///////////////////////////////////////////////////////////////////////////////
#include <windows.h>
#include <math.h>
#include "cem.h"
#include "swxsrvr.h"
#include "SwxSrvrGlbl.h"
#include "AtXmlInterfaceApiC.h"

// switch card constants
#define CEMOUTPUT (WM_USER + 211)

// Local Static Variables

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////////////////
// Function: InitSwitch()
// Purpose : This function should be called from the switch WCEM project in the
//			 DoLoad() or DoOpen() function. It should be used to open a session
//			 to the switch cards. An alternative would be to get the handles in 
//			 DllMain in SwxSrvr.cpp
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int InitSwitch(int Dbg, int Sim, char *Response, int BufferSize)
{
    g_Sim = Sim;
    g_Dbg = Dbg;

    CEMDEBUG(g_Dbg, 5, "SWXSRVR", "TETS Switch Server DLL");
    GetUniqCalAvail("PAWS_SWITCH", 36000, NULL, 0,
                    Response, BufferSize, g_Sim);
//    ResetSwitch();

	return 0;
}

////////////////////////////////////////////////////////////////////////////////
// Function: ReleaseSwitch()
// Purpose : This function should be called from the switch WCEM project in the
//			 DoUnload() or DoClose() function. It should be used to open a session
//			 to the switch cards An alternative would be to release the handles in 
//			 DllMain in SwxSrvr.cpp
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int ReleaseSwitch()
{
	int  Status = 0;

	CEMDEBUG(g_Dbg, 5, "SWXSRVR", "TETS Switch Server Closing");

    IFNSIM(g_Sim, (Status = atxml_Close()));

	return 0;
}

////////////////////////////////////////////////////////////////////////////////
// Function: CloseSwitches()
// Purpose : This function is called by cemsupport lib function cs_DoSwitch().
//			 It is passed a vector of triplets. 
//
//			 The number of triplets can be determined by using the function
//				int size = paths.size()
//
//			 Each Triplet is accessed by using an index to the path vector.
//				paths[0].blk
//				paths[0].mod
//				paths[0].pth
//
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int CloseSwitches(PathData & paths, char *Response, int BufferSize)
{
    int   Status = 0;
	int   Idx;
    int   XmlBufLen;
    char *XmlBuf = NULL;

	for (Idx = 0; Idx < (int)paths.size(); Idx++)
    {
        // Issue startup Connection XML String
        if(Idx == 0)
        {
            XmlBufLen = ((int)paths.size() * 12) + 1024;
            XmlBuf = new char[XmlBufLen];
/*
		"<AtXmlSignalDescription xmlns:atxml=\"ATXML_TSF\">\n"
		"	<SignalAction>Connect</SignalAction>\n"
        "   <SignalResourceName>PAWS_SWITCH</SignalResourceName>\n"
		"	<SignalSnippet>\n"
	    "       <Signal Out=\"SHORTx\">\n"
		"           <atxml:Connection name=\"SHORTx\" path=\"1,1000,0\"/>\n"
	    "       </Signal>\n"
        "   </SignalSnippet>\n"
		"</AtXmlSignalDescription>\n"
*/
            strcpy(XmlBuf,
	            "<AtXmlSignalDescription xmlns:atxml=\"ATXML_TSF\">\n"
		        "    <SignalAction>Connect</SignalAction>\n"
		        "    <SignalResourceName>PAWS_SWITCH</SignalResourceName>\n"
		        "       <SignalSnippet>\n"
			    "           <Signal Out=\"SHORTx\">\n"
                "               <atxml:Connection name=\"SHORTx\" path=\"");
        }
        sprintf(&XmlBuf[strlen(XmlBuf)],"%d,%d,%d;",
                 paths[Idx].blk, paths[Idx].mod, paths[Idx].pth);
    }// End Path For Loop
    if(XmlBuf != NULL)
    {
        // Get rid of trailing simicolon
        if(XmlBuf[strlen(XmlBuf)-1] == ';')
            XmlBuf[strlen(XmlBuf)-1] = '\0';
        strcat(XmlBuf,
                "\"/>\n"
			    "           </Signal>\n"
		        "       </SignalSnippet>\n"
	            "</AtXmlSignalDescription>\n"
                );
        IFNSIM(g_Sim, (Status = atxml_IssueSignal(XmlBuf, Response, BufferSize)));
        //FIX Process Response buffer
        delete(XmlBuf);
    }
    
	return 0;
}

////////////////////////////////////////////////////////////////////////////////
// Function: OpenSwitches()
// Purpose : This function is called by cemsupport lib function cs_DoSwitch().
//			 It is passed a vector of triplets. 
//
//			 The number of triplets can be determined by using the function
//				int size = paths.size()
//
//			 Each Triplet is accessed by using an index to the path vector.
//				paths[0].blk
//				paths[0].mod
//				paths[0].pth
//
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int OpenSwitches(PathData & paths, char *Response, int BufferSize)
{
    int   Status = 0;
	int   Idx;
    int   XmlBufLen;
    char *XmlBuf = NULL;

	for (Idx = 0; Idx < (int)paths.size(); Idx++)
    {
        // Issue startup Connection XML String
        if(Idx == 0)
        {
            XmlBufLen = ((int)paths.size() * 12) + 1024;
            XmlBuf = new char[XmlBufLen];
            strcpy(XmlBuf,
	            "<AtXmlSignalDescription xmlns:atxml=\"ATXML_TSF\">\n"
		        "    <SignalAction>Disconnect</SignalAction>\n"
		        "    <SignalResourceName>PAWS_SWITCH</SignalResourceName>\n"
		        "       <SignalSnippet> \n"
			    "           <Signal Out=\"SHORTx\">\n"
                "               <atxml:Connection name=\"SHORTx\" path=\"");
        }
        sprintf(&XmlBuf[strlen(XmlBuf)],"%d,%d,%d;",
                 paths[Idx].blk, paths[Idx].mod, paths[Idx].pth);
    }// End Path For Loop
    if(XmlBuf != NULL)
    {
        // Get rid of trailing simicolon
        if(XmlBuf[strlen(XmlBuf)-1] == ';')
            XmlBuf[strlen(XmlBuf)-1] = '\0';
        strcat(XmlBuf,
                "\"/>\n"
			    "           </Signal>\n"
		        "       </SignalSnippet>\n"
	            "</AtXmlSignalDescription>\n"
                );
        IFNSIM(g_Sim, (Status = atxml_IssueSignal(XmlBuf, Response, BufferSize)));
        //FIX Process Response buffer
        delete(XmlBuf);
    }
    

	return 0;
}

////////////////////////////////////////////////////////////////////////////////
// Function: ResetSwitch()
// Purpose : This function should be called by the switch WCEM DLL DoIfc() 
//			 function. It should be used to open all switches in the ATE.
//
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API int ResetSwitch()
{
	int  Status = 0;
    int  RemAllStatus = 0;
    char Response[1024];

	CEMDEBUG(g_Dbg, 5, "SWXSRVR", "TETS Switch Server Reset");

    IFNSIM(g_Sim,(Status = atxml_InvokeRemoveAllSequence(Response, 1024)));

    if(Status || (RemAllStatus == ATXML_REMALL_FAILED))
        return(0);
	return 0;
}


////////////////////////////////////////////////////////////////////////////////
// Function: GetErrorMessage()
// Purpose : This function take a key code as an argument and returns a visa 
//           error code and error message
////////////////////////////////////////////////////////////////////////////////
extern "C" SWXSRVR_API void GetErrorMessage(int & returnCode, char * card, char * msg)
{

    // Place System Specific Switch Stuff Here!

}

//+++++////////////////////////////////////////////////////////////////////////
// Local Fnctions
///////////////////////////////////////////////////////////////////////////////

