/////////////////////////////////////////////////////////////////////////////
//	File:	PingInfo.cpp													/
//																			/
//	Creation Date:	24 June 2016											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0	Assigned it a version number.								/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include "Version.h"
#pragma warning (disable : 4035 4068)
#include <winsock2.h>
#pragma warning (default : 4035 4068)
#include <iphlpapi.h>
#include <icmpapi.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <malloc.h>
#pragma warning (disable : 4035 4068)
#include <windows.h>
#pragma warning (default : 4035 4068)
#include "gpnam.h"

/////////////////////////////////////////////////////////////////////////////
//	Local Constants															/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Modules																	/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// DoPing:			This function will find out what ping capability the	/
//						programmer wants to do. There are two methods that	/
//						are allowed. The first is just a standard one ping.	/
//						The second will allow for multiple pings detecting	/
//						droped pings, Max, Min, and Avg time fort he pings.	/
//																			/
// Parameters:																/
//		argc:		The argument count value main was started with.			/
//		argv:		The agrument list main was started with.				/
//																			/
// Returns:																	/
//		none:		This function is a void function.						/
//																			/
/////////////////////////////////////////////////////////////////////////////

void DoPing(int argc, char* argv[])
{
    
	int				PingNumber = 1, LoopCnt = 1, BadRead = 0;
	int				MaxTime = 0, MinTime = 0, AvgTime = 0, TmpTime = 0, TotalTime = 0;
    unsigned long	ipaddr = INADDR_NONE;
    HANDLE			hIcmpFile;
    char			SendData[32] = "Data Buffer";
	char			OutPutFileName[_MAX_PATH];
    DWORD			dwRetVal = 0;
    DWORD			ReplySize = 0;
    LPVOID			ReplyBuffer = NULL;
	FILE			*OutPutfp;
	dodebug(0, "DoPIng()", "Entering function", (char*)NULL);
	sprintf(OutPutFileName, "%s%s", gp_info.log_location, PINGOUTPUTNAME);
	
		//dodebug(0, "DoPing()", "Argv is [%s] [%s] [%s] [%s] [%s] [%s] [%s] [%s] [%s]", argv[0],argv[1],argv[2],argv[3],argv[4],argv[5],argv[6],argv[7],argv[8]);
	if ((OutPutfp = fopen(OutPutFileName, "w+b")) == NULL) {
		gp_info.return_value = GP_ERROR;
		return;
	}
	int dataType;
	if(ATLAS == 1)
	{
		dataType = ATLAS_CHAR;
	}
	else 
	{
		dataType = CMD_LINE_CHAR;
	}
	dodebug(0, "DoPing", "Data type is %d and argv[ADDRESS + ATLAS{%d}] is %s and address is %s", dataType, ATLAS,argv[ADDRESS + ATLAS], gp_info.address, (char*)NULL);
	if (setArgumentValue(dataType, argv[ADDRESS + ATLAS], gp_info.address, sizeof(gp_info.address), NULL)) {
	
		dodebug(0, "DoPIng()", "Failed to get option value Ping Address", (char *)NULL);
		gp_info.return_value = GP_ERROR;
		return;
	}

	dodebug(0, "DoPIng()", "Going into if ((argc[%d] > PINGNUMBER[%d] && !ATLAS[%d]) || (argc[%d] > PINGNUMBER[%d] + ATLAS[%d] + ARGCOFFSET[%d]))", argc, PINGNUMBER, ATLAS,argc, PINGNUMBER, ATLAS,ARGCOFFSET, (char *)NULL);
	if ((argc > PINGNUMBER && !ATLAS) || (argc > PINGNUMBER + ATLAS + ARGCOFFSET)) {

		dodebug(0, "DoPIng()", "Calling Setargumentvalue argc = %d Ping+Atlas = %d", argc, PINGNUMBER + ATLAS, (char *)NULL);
		if (setArgumentValue(ATLAS == 1 ? ATLAS_INT : CMD_LINE_INT, 
							  argv[PINGNUMBER + ATLAS], NULL, 0, &PingNumber)) {
		
			dodebug(0, "DoPIng()", "Failed to get option value Ping Number", (char *)NULL);
			gp_info.return_value = GP_ERROR;
			return;
		}
		LoopCnt = PingNumber;
	}

    ipaddr = inet_addr(gp_info.address);
	dodebug(0, "DoPing()", "IP ADDRESS is %s", gp_info.address, (char*)NULL);
    if (ipaddr == INADDR_NONE) {
		dodebug(0, "DoPIng()", "Invalid Internet address format - %s",
				gp_info.address, (char*)NULL);
		gp_info.return_value = GP_ERROR;
		return;
    }
    
    hIcmpFile = IcmpCreateFile();

    if (hIcmpFile == INVALID_HANDLE_VALUE) {
		dodebug(0, "DoPIng()", "Unable to open handle", (char*)NULL);
		gp_info.return_value = GP_ERROR;
		return;
    }    

    ReplySize = sizeof(ICMP_ECHO_REPLY) + sizeof(SendData);

	if ((ReplyBuffer = (char *)calloc((size_t)ReplySize, sizeof(char))) == NULL) {
		dodebug(0, "DoPIng()", "Unable to allocate memory", (char*)NULL);
		gp_info.return_value = GP_ERROR;
		return;
    }    
    
	while (PingNumber) {
    
		dwRetVal = IcmpSendEcho(hIcmpFile, ipaddr, SendData, sizeof(SendData), 
								NULL, ReplyBuffer, ReplySize, 1000);

		if (dwRetVal != 0) {

			PICMP_ECHO_REPLY pEchoReply = (PICMP_ECHO_REPLY)ReplyBuffer;
			struct in_addr ReplyAddr;

			ReplyAddr.S_un.S_addr = pEchoReply->Address;
			dodebug(0, "DoPIng()", "Sent icmp message to %s", gp_info.address, (char*)NULL);

			if (dwRetVal > 1) {
				dodebug(0, "DoPIng()", "Received %ld icmp message responses dwRetVal > 1", dwRetVal, (char*)NULL);
			}    
			else {    
				dodebug(0, "DoPIng()", "Received %ld icmp message responses dwRetVal = 1", dwRetVal, (char*)NULL);
			}    

			dodebug(0, "DoPIng()", "Received from %s", inet_ntoa(ReplyAddr), (char*)NULL);
			dodebug(0, "DoPIng()", "Status = %ld", pEchoReply->Status, (char*)NULL);
			dodebug(0, "DoPIng()", "Roundtrip time = %ld milliseconds", pEchoReply->RoundTripTime, (char*)NULL);
			TmpTime = (int)pEchoReply->RoundTripTime;

//			if (DE_BUG) {
//				if (PingNumber == 3) {
//					TmpTime = 24;
//				}
//				if (PingNumber == 5) {
//					TmpTime = 56;
//				}
//				if (PingNumber == 7) {
//					TmpTime = 12;
//				}
//				if (PingNumber == 9) {
//					TmpTime = 41;
//				}
//				
//				if (TmpTime < MinTime) {
//					MinTime = TmpTime;
//				}
//				if (TmpTime > MaxTime) {
//					MaxTime = TmpTime;
//				}
//			}

			TotalTime += TmpTime;
		}
		else {
			BadRead++;
			LoopCnt--;
		}

		PingNumber--;
		memset(ReplyBuffer, '\0', ReplySize);
	}

	if (LoopCnt != 0) {
		AvgTime = TotalTime / LoopCnt;
	}
	else {
		MaxTime = -1;
		MinTime = -1;
		AvgTime = -1;
		gp_info.return_value = GP_ERROR;
	}

	dodebug(0, "DoPIng()", "MaxTime = %d milliseconds", MaxTime, (char*)NULL);
	dodebug(0, "DoPIng()", "MinTime = %d milliseconds", MinTime, (char*)NULL);
	dodebug(0, "DoPIng()", "AvgTime = %d milliseconds", AvgTime, (char*)NULL);
	dodebug(0, "DoPIng()", "Number of drop packets = %d", BadRead, (char*)NULL);
	dodebug(0, "DoPIng()", "Total packets recieved = %d", LoopCnt, (char*)NULL);

	dodebug(0, "DoPIng()", "Calling returnArgumentValue", (char*)NULL);
		dodebug(0, "DoPIng()", "Calling Setargumentvalue argc = %d Ping+Atlas = %d", argc, PINGNUMBER + ATLAS, (char *)NULL);

	if ((argc == PINGNUMBER + ATLAS + ARGCOFFSET && ATLAS || argc == PINGNUMBER)) {
		dodebug(0, "DoPIng()", "Calling Setargumentvalue argc = %d Ping+Atlas = %d", argc, PINGNUMBER + ATLAS, (char *)NULL);
		returnArgumentValue (ATLAS == 1 ? ATLAS_INT : CMD_LINE_INT, 
							 ATLAS == 1 ? argv[PINGNUMBER + ATLAS] : NULL, NULL, 0, MaxTime, OutPutfp);
	}
	
	else {
		returnArgumentValue(ATLAS == 1 ? ATLAS_INT : CMD_LINE_INT,
							 ATLAS == 1 ? argv[MAXTIME + ATLAS] : NULL, NULL, 0, MaxTime, OutPutfp);
		returnArgumentValue(ATLAS == 1 ? ATLAS_INT : CMD_LINE_INT,
							 ATLAS == 1 ? argv[MINTIME + ATLAS] : NULL, NULL, 0, MinTime, OutPutfp);
		returnArgumentValue(ATLAS == 1 ? ATLAS_INT : CMD_LINE_INT,
							 ATLAS == 1 ? argv[AVGTIME + ATLAS] : NULL, NULL, 0, AvgTime, OutPutfp);
		returnArgumentValue(ATLAS == 1 ? ATLAS_INT : CMD_LINE_INT,
							 ATLAS == 1 ? argv[DROPEDPACK + ATLAS] : NULL, NULL, 0, BadRead, OutPutfp);
		returnArgumentValue(ATLAS == 1 ? ATLAS_INT : CMD_LINE_INT,
							 ATLAS == 1 ? argv[RECVDPACK + ATLAS] : NULL, NULL, 0, LoopCnt, OutPutfp);
	}
	
	dodebug(0, "DoPIng()", "After returnArgumentValue", (char*)NULL);
	if (argc > ADDRESS + ATLAS) {
		dodebug(0, "DoPIng()", "INif (argc > ADDRESS + ATLAS) {", (char*)NULL);
		;
	}

	if (ReplyBuffer != NULL) {
		dodebug(0, "DoPIng()", " In if (ReplyBuffer != NULL) {", (char*)NULL);
		free(ReplyBuffer);
	}

	if (OutPutfp != NULL) {
		dodebug(0, "DoPIng()", "In if (OutPutfp != NULL) {", (char*)NULL);
		fclose(OutPutfp);
	}
	
	dodebug(0, "DoPIng()", "before return", (char*)NULL);
	return;
}

