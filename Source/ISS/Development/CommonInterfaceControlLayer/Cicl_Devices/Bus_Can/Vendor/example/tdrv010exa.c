/* $Id: tdrv010exa.c 27851 2020-07-06 20:01:53Z wileyj $ */
/******************************************************************************
*******************************************************************************
**                                                                           **
**                                                                           **
**                          @@@@@@@@@@@@@@@@@@@@@@@                          **
**                          @                     @                          **
**                          @ T D R V 0 1 0 E X A @                          **
**                          @                     @                          **
**                          @@@@@@@@@@@@@@@@@@@@@@@                          **
**                                                                           **
**                                                                           **
**    Project          Windows WDM - TDRV010 Device Driver                   **
**                                                                           **
**                                                                           **
**    File             tdrv010exa.c                                          **
**                                                                           **
**                                                                           **
**    Function         TDRV010 Example Application                           **
**                                                                           **
**                                                                           **
**                                                                           **
**    Owner            TEWS TECHNOLOGIES GmbH                                **
**                     Am Bahnhof 7                                          **
**                     D-25469 Halstenbek                                    **
**                     Germany                                               **
**                                                                           **
**                                                                           **
**                     Tel.: +49 / (0)4101 / 4058-0                          **
**                     Fax.: +49 / (0)4101 / 4058-19                         **
**                     e-mail: support@tews.com                              **
**                                                                           **
**                                                                           **
**                     Copyright (c) 2005-2011                               **
**                     TEWS TECHNOLOGIES GmbH                                **
**                                                                           **
**                                                                           **
*******************************************************************************
*******************************************************************************/
#define _CRT_SECURE_NO_WARNINGS 1

#include <windows.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <winioctl.h>

#include "../tdrv010.h"
#include "../api/tdrv010api.h"


#define TDRV010EXA_VERSION  "2.0.1"
#define DEVICE_NAME         "TDRV010"
#define MAX_DEVICES         8
#define MAX_LEN             100


TDRV010_HANDLE hDevices[MAX_DEVICES];

static VOID PrintErrorMessage(int errVal);
static VOID PrintIOFlags(UCHAR IOFlags);

static USHORT TIMING_CONST[] = {TDRV010_1MBIT, TDRV010_500KBIT, TDRV010_250KBIT, TDRV010_125KBIT, TDRV010_100KBIT};


//////////////////////////////////////////////////////////////////////////////
//  Function:
//      main
//
//  Description:
//      Example Application for the TDRV010 device driver
//
//  Arguments:
//      none
//
//  Return Value:
//      none
//////////////////////////////////////////////////////////////////////////////
int main(void)
{
    int                 i;
    int                 selnum;
    char                Path[100];
    char                select[MAX_LEN];
    char                devName[20];
    TDRV010_HANDLE      hCurrent;
    BOOLEAN             found;
    FILE                *fp;
    int                 errVal;
    BOOLEAN             SetIOFlags;
    UCHAR               channel = 1;
    BOOLEAN             noWait = FALSE;
    ULONG               timeout = 10;
    ULONG               identifier = 0;
    UCHAR               IOFlags = TDRV010_STANDARD;
    UCHAR               msgStatus;
    int                 dataLen;
    UCHAR               dataBuf[8];
    int                 value;
    USHORT              timingValue = TDRV010_125KBIT;
    BOOLEAN             useThreeSamples = FALSE;
    BOOLEAN             useSingleFilter = FALSE;
    ULONG               acceptanceCode = 0x123;
    ULONG               acceptanceMask = 0xFFFFFFFF;
    UCHAR               warnLimit = 0xFF;
    TDRV010_STATUS      statusBuf;
    int                 numberOfMessages = 20;


    printf("\n\n*******************************************\n");
    printf("***                                     ***\n");
    printf("***            T D R V 0 1 0            ***\n");
    printf("***          WDM Device Driver          ***\n");
    printf("***         Example Application         ***\n");
    printf("***                                     ***\n");
    printf("***                    Version %8s ***\n", TDRV010EXA_VERSION);
    printf("*******************************************\n\n");

    printf("Searching for %s Devices...\n\n", DEVICE_NAME);

    found = FALSE;
    for (i = 0; i < MAX_DEVICES; i++)
    {
        //
        //  Remember Win32 device names ending in a one-based number
        //  ( TDRV010_1, TDRV010_2, ... )
        //
        sprintf_s(Path, 100, "\\\\.\\%s_%d", DEVICE_NAME, i+1);


        //
        //  Open a TDRV010 device
        //
        hDevices[i] = tdrv010Open(Path);
        if ( hDevices[i] != INVALID_HANDLE_VALUE )
        {
            found = TRUE;
            printf("    %s Device found --> %s\n", DEVICE_NAME, Path);
        }
    }

    if (!found)
    {
        printf("\n\nNo %s Devices found. Exit Example Application...\n", DEVICE_NAME);
        fgets(select, MAX_LEN, stdin);
        exit(1);
    }


    //
    //  Select the first TDRV010 device we found as current I/O device
    //
    for (i = 0; i < MAX_DEVICES; i++ )
    {
        if ( hDevices[i] != INVALID_HANDLE_VALUE )
        {
            hCurrent = hDevices[i];
            sprintf_s(devName, 20, "%s_%d", DEVICE_NAME, i+1);
            printf("\n\nOpen %s for I/O...\n\n", devName);
            break;
        }
    }

    //
    //  Simple example code to show the usage of all implemented
    //  TDRV010 device driver functions
    //
    do
    {
        do
        {
            printf("\nPlease select function\n\n");
            printf("        1  --  Receive a CAN Message\n");
            printf("        2  --  Send a CAN Message\n");
		    printf("        3  --  Define Bit Timing\n");
    		printf("        4  --  Set Acceptance Filter Masks\n");
            printf("        5  --  Enter Bus On mode\n");
            printf("        6  --  Enter Bus Off mode\n");
            printf("        7  --  Flush receive FIFO's\n");
            printf("        8  --  Enable/Disable Self Test Mode\n");
            printf("        9  --  Enable/Disable Listen Only Mode\n");
            printf("       10  --  Set Error Warning Limit\n");
            printf("       11  --  Get CAN controller status register\n");
            printf("       12  --  Setup CAN channel for testing\n");
            printf("       13  --  Send multiple CAN Messages\n");
            printf("       14  --  Receive and store CAN messages until timeout\n");
            printf("       15  --  Reset/Operating Mode\n");
            printf("       16  --  Silent/Operating Mode\n");
            printf("       17  --  Interrupt Enabled/Disabled\n");
            printf("       50  --  Change Current Device [act: %s]\n", devName);
            printf("       51  --  Change Current Channel [act: %d]\n", channel);
            printf("       99  --  Quit\n\n");
            printf("select ");

            fgets(select, MAX_LEN, stdin);
            selnum = atoi(select);

        } while ((selnum !=  1) &&
                 (selnum !=  2) &&
                 (selnum !=  3) &&
                 (selnum !=  4) &&
                 (selnum !=  5) &&
                 (selnum !=  6) &&
                 (selnum !=  7) &&
                 (selnum !=  8) &&
                 (selnum !=  9) &&
                 (selnum != 10) &&
                 (selnum != 11) &&
                 (selnum != 12) &&
                 (selnum != 13) &&
                 (selnum != 14) &&
                 (selnum != 15) &&
                 (selnum != 16) &&
                 (selnum != 17) &&
                 (selnum != 50) &&
                 (selnum != 51) &&
                 (selnum != 99));

        switch(selnum)
        {
        case 1:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Read a CAN Message
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            do
            {
                printf("Wait for Data [y|n]: [%c]", (noWait) ? 'n' : 'y');
                fgets(select, MAX_LEN, stdin);
            } while ((select[0] != 'y') &&
                     (select[0] != 'Y') &&
                     (select[0] != 'n') &&
                     (select[0] != 'N') &&
                     (select[0] != '\n'));
            switch(select[0])
            {
            case 'n':
            case 'N':
                noWait = TRUE;
                break;

            case 'y':
            case 'Y':
                noWait = FALSE;
                break;
            }

            if (!noWait)
            {
                printf("Timeout in seconds [%d] ", timeout);
                fgets(select, MAX_LEN, stdin);
                if (select[0] != '\n')
                {
                    timeout = atoi(select);
                }
            }

            if(noWait)
            {
                errVal = tdrv010ReadNoWait(hCurrent, channel, &identifier, &IOFlags, &msgStatus, &dataLen, dataBuf);
            }
            else
            {
                errVal = tdrv010Read(hCurrent, channel, timeout, &identifier, &IOFlags, &msgStatus, &dataLen, dataBuf);
            }

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nRead Message successful completed!\n");
                printf("%s %s Identifier = %ld (0x%lx)\n",
                       (IOFlags & TDRV010_EXTENDED) ? "Extended" : "Standard",
                       (IOFlags & TDRV010_REMOTE_FRAME) ? "Remote Frame Message - " : "Message - ",
                       identifier,
                       identifier);
                printf("%d data bytes received\n", dataLen);
                for( i = 0; i < dataLen; i++ )
                {
                    printf("%02X ", dataBuf[i]);
                }
                printf("  ");
                for( i = 0; i < dataLen; i++ )
                {
                    if((dataBuf[i] >= ' ') &&
                       (dataBuf[i] <= '~'))
                    {
                        printf("%c", dataBuf[i]);
                    }
                    else
                    {
                        printf(".");
                    }
                }
                printf("\nMessage status field = %d\n", msgStatus );
            }
            else
            {
                printf("\nRead Message failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 2:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Write a CAN Message
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            printf("Message Identifier (decimal) [%ld] ", identifier);
            fgets(select, MAX_LEN, stdin);
            if (select[0] != '\n')
            {
                identifier = atoi(select);
            }

            PrintIOFlags(IOFlags);

            do
            {
                printf("Change Flags [y|n]: [n]");
                fgets(select, MAX_LEN, stdin);
                if (select[0] == '\n')
                {
                    select[0] = 'n';
                }
            } while ((select[0] != 'y') &&
                     (select[0] != 'Y') &&
                     (select[0] != 'n') &&
                     (select[0] != 'N'));
            switch(select[0])
            {
            case 'n':
            case 'N':
                SetIOFlags = FALSE;
                break;

            case 'y':
            case 'Y':
                SetIOFlags = TRUE;
                break;
            }

            if (SetIOFlags)
            {    
                IOFlags = TDRV010_STANDARD;    /* set default flags */
                
                do
                {
                    printf("Extended Identifier [y|n]: [%c]", (IOFlags & TDRV010_EXTENDED) ? 'y' : 'n');
                    fgets(select, MAX_LEN, stdin);
                } while ((select[0] != 'y') &&
                         (select[0] != 'Y') &&
                         (select[0] != 'n') &&
                         (select[0] != 'N') &&
                         (select[0] != '\n'));
                switch(select[0])
                {
                case 'n':
                case 'N':
                    break;

                case 'y':
                case 'Y':
                    IOFlags |= TDRV010_EXTENDED;
                    break;
                }

                do
                {
                    printf("Send Remote Frame Request [y|n]: [%c]", (IOFlags & TDRV010_REMOTE_FRAME) ? 'y' : 'n');
                    fgets(select, MAX_LEN, stdin);
                } while ((select[0] != 'y') &&
                         (select[0] != 'Y') &&
                         (select[0] != 'n') &&
                         (select[0] != 'N') &&
                         (select[0] != '\n'));
                switch(select[0])
                {
                case 'n':
                case 'N':
                    break;

                case 'y':
                case 'Y':
                    IOFlags |= TDRV010_REMOTE_FRAME;
                    break;
                }

                do
                {
                    printf("No re-transmission on error (single-shot) [y|n]: [%c]", (IOFlags & TDRV010_SINGLE_SHOT) ? 'y' : 'n');
                    fgets(select, MAX_LEN, stdin);
                } while ((select[0] != 'y') &&
                         (select[0] != 'Y') &&
                         (select[0] != 'n') &&
                         (select[0] != 'N') &&
                         (select[0] != '\n'));
                switch(select[0])
                {
                case 'n':
                case 'N':
                    break;

                case 'y':
                case 'Y':
                    IOFlags |= TDRV010_SINGLE_SHOT;
                    break;
                }

                do
                {
                    printf("Receive message simultaneously (self-reception) [y|n]: [%c]", (IOFlags & TDRV010_SELF_RECEPTION) ? 'y' : 'n');
                    fgets(select, MAX_LEN, stdin);
                } while ((select[0] != 'y') &&
                         (select[0] != 'Y') &&
                         (select[0] != 'n') &&
                         (select[0] != 'N') &&
                         (select[0] != '\n'));
                switch(select[0])
                {
                case 'n':
                case 'N':
                    break;

                case 'y':
                case 'Y':
                    IOFlags |= TDRV010_SELF_RECEPTION;
                    break;
                }

                PrintIOFlags(IOFlags);
            }

            printf("Message Text (max. 8 Bytes) ");
            fgets(select, MAX_LEN, stdin);
            dataLen = (int)strlen(select) - 1;
            if (dataLen > 8)
            {
                dataLen = 8;
            }
            memcpy(dataBuf, select, dataLen);
            
            do
            {
                printf("Wait for Acknowledge [y|n]: [%c]", noWait ? 'n' : 'y');
                fgets(select, MAX_LEN, stdin);
            } while ((select[0] != 'y') &&
                     (select[0] != 'Y') &&
                     (select[0] != 'n') &&
                     (select[0] != 'N') &&
                     (select[0] != '\n'));
            switch(select[0])
            {
            case 'n':
            case 'N':
                noWait = TRUE;
                break;

            case 'y':
            case 'Y':
                noWait = FALSE;
                break;
            }

            printf("Timeout in seconds [%d] ", timeout);
            fgets(select, MAX_LEN, stdin);
            if (select[0] != '\n')
            {
                timeout = atoi(select);
            }

            if (noWait)
            {
                errVal = tdrv010WriteNoWait(hCurrent, channel, identifier, IOFlags, dataLen, dataBuf);
            }
            else
            {
                errVal = tdrv010Write(hCurrent, channel, timeout, identifier, IOFlags, dataLen, dataBuf);
            }

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nSend message successful completed!\n");
            }
            else
            {
                printf( "\nSend message failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 3:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Define Bit Timing 
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            do 
            {
                printf("\n\n Please select Bit Timing\n\n");
                printf("        0  --     1.0 Mbps\n");
                printf("        1  --     500 Kbps\n");
                printf("        2  --     250 Kbps\n");
                printf("        3  --     125 Kbps\n");
                printf("        4  --     100 Kbps\n");
                printf("        5  --         other\n");
                printf(" select ");
                fgets(select, MAX_LEN, stdin);
                i = atoi(select);
            } while (i > 5);
            
            if (i == 5)
            {
                do
                {
                    printf("Bit Timing Register 0 (Hex)  : ");
                    fgets(select, MAX_LEN, stdin);
                    sscanf_s(select, "%lx", &value);  
                } while (value > 0xff);
                
                timingValue = value;
                
                do {
                    printf("Bit Timing Register 1 (Hex)  : ");
                    fgets(select, MAX_LEN, stdin);
                    sscanf_s(select, "%lx", &value);  
                } while (value > 0xff);
                
                timingValue |= (value & 0xff) << 8;
            }
            else
            {
                timingValue = TIMING_CONST[i];
            }
                        
            do
            {
                printf("Use three samples per bit time [y|n]: [%c]", useThreeSamples ? 'y' : 'n');
                fgets(select, MAX_LEN, stdin);
            } while ((select[0] != 'y') &&
                     (select[0] != 'Y') &&
                     (select[0] != 'n') &&
                     (select[0] != 'N') &&
                     (select[0] != '\n'));
            switch(select[0])
            {
            case 'n':
            case 'N':
                useThreeSamples = FALSE;
                break;

            case 'y':
            case 'Y':
                useThreeSamples = TRUE;
                break;
            }

            errVal = tdrv010SetBitTiming(hCurrent, channel, timingValue, useThreeSamples);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nSet Bittiming successful completed!\n");
            }
            else
            {
                printf( "\nSet Bittiming failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 4:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Set Acceptance Filter Masks
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            printf("Acceptance Code Register (Hex) [0x%lx] ", acceptanceCode);
            fgets(select, MAX_LEN, stdin);
            if (select[0] != '\n')
            {
                sscanf_s(select, "%lx", &acceptanceCode);
            }
            
            printf("Acceptance Mask Register (Hex) [0x%lx] ", acceptanceMask);
            fgets(select, MAX_LEN, stdin);
            if (select[0] != '\n')
            {
                sscanf_s(select, "%lx", &acceptanceMask);
            }
            
            do
            {
                printf("Use single acceptance filter mode [y|n]: [%c]", useSingleFilter ? 'y' : 'n');
                fgets(select, MAX_LEN, stdin);
            } while ((select[0] != 'y') &&
                     (select[0] != 'Y') &&
                     (select[0] != 'n') &&
                     (select[0] != 'N') &&
                     (select[0] != '\n'));
            switch(select[0])
            {
            case 'n':
            case 'N':
                useSingleFilter = FALSE;
                break;

            case 'y':
            case 'Y':
                useSingleFilter = TRUE;
                break;
            }

            errVal = tdrv010SetFilter(hCurrent, channel, useSingleFilter, acceptanceCode, acceptanceMask);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nSet Acceptance Filter successful completed!\n");
            }
            else
            {
                printf( "\nSet Acceptance Filter failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;


        case 5:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Enter bus on mode
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

            errVal = tdrv010Start(hCurrent, channel);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nEnter Bus On successful completed!\n");
            }
            else
            {
                printf("\nEnter Bus On failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 6:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Enter bus off mode
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

            errVal = tdrv010Stop(hCurrent, channel);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nEnter Bus Off successful completed!\n");
            }
            else
            {
                printf("\nEnter Bus Off failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;




        case 7:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Flush RxFifo
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

            errVal = tdrv010FlushReceiveFifo(hCurrent, channel);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nFlush FIFO successful completed!\n");
            }
            else
            {
                printf( "\nFlush FIFO failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;




        case 8:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Enable/Disable controller self test mode
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            do
            {
                printf("(E)nable or (D)isable Self Test Mode [D]:");
                fgets(select, MAX_LEN, stdin);
            } while ((select[0] != 'e') &&
                     (select[0] != 'E') &&
                     (select[0] != 'd') &&
                     (select[0] != 'D'));
            switch(select[0])
            {
            case 'e':
            case 'E':
                errVal = tdrv010SelftestEnable(hCurrent, channel);
                break;

            case 'd':
            case 'D':
                errVal = tdrv010SelftestDisable(hCurrent, channel);
                break;
            }

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nChange Selftest Mode successful completed!\n");
            }
            else
            {
                printf( "\nChange Selftest Mode failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 9:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Enable/Disable controller listen only mode
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            do
            {
                printf("(E)nable or (D)isable Listen Only Mode [D]:");
                fgets(select, MAX_LEN, stdin);
            } while ((select[0] != 'e') &&
                     (select[0] != 'E') &&
                     (select[0] != 'd') &&
                     (select[0] != 'D'));
            switch(select[0])
            {
            case 'e':
            case 'E':
                errVal = tdrv010ListenOnlyEnable(hCurrent, channel);
                break;

            case 'd':
            case 'D':
                errVal = tdrv010ListenOnlyDisable(hCurrent, channel);
                break;
            }

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nChange Listen Only Mode successful completed!\n");
            }
            else
            {
                printf( "\nChange Listen Only Mode failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 10:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Set error warning limit
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

            printf("New Error Warning Limit (decimal) [%d] ", warnLimit);
            fgets(select, MAX_LEN, stdin);

            if (atoi(select) > 255) {
                printf("New limit out of range (0..255)\n");
                break;
            }

            if (select[0] != '\n')
            {
                warnLimit = atoi(select);
            }

            errVal = tdrv010SetLimit(hCurrent, channel, warnLimit);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nSet Warning Limit successful completed!\n");
            }
            else
            {
                printf( "\nSet Warning Limit failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 11:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Get CAN controller status information
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

            errVal = tdrv010GetControllerStatus(hCurrent, channel, &statusBuf);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nRead Status successful completed!\n");

                printf("\nArbitration lost capture register   = 0x%02X\n", statusBuf.ArbitrationLostCapture);
                printf("Error code capture register         = 0x%02X\n", statusBuf.ErrorCodeCapture);
                printf("TX error counter register           = 0x%02X\n", statusBuf.TxErrorCounter);
                printf("RX error counter register           = 0x%02X\n", statusBuf.RxErrorCounter);
                printf("Error warning limit register        = 0x%02X\n", statusBuf.ErrorWarningLimit);
                printf("Status register                     = 0x%02X\n", statusBuf.StatusRegister);
                printf("Mode register                       = 0x%02X\n", statusBuf.ModeRegister);
                printf("Peak value RX message counter       = %d\n", statusBuf.RxMessageCounterMax);

            }
            else
            {
                printf( "\nRead Status failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 12:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Make Default Channel Setup 
            ++      - 100 KBit
            ++      - except all messages
            ++      - Bus On
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++  Enter bus off mode
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            errVal = tdrv010Stop(hCurrent, channel);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nEnter Bus Off successful completed!\n");
            }
            else
            {
                printf("\nEnter Bus Off failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }

            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++  Define Bit Timing 
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            timingValue = TDRV010_100KBIT;
            useThreeSamples = FALSE;

            errVal = tdrv010SetBitTiming(hCurrent, channel, timingValue, useThreeSamples);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nSet Bittiming successful completed!\n");
            }
            else
            {
                printf( "\nSet Bittiming failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }

            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++  Set Acceptance Filter Masks
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            acceptanceCode = 0;
            acceptanceMask = 0xFFFFFFFF;
            useSingleFilter = TRUE;

            errVal = tdrv010SetFilter(hCurrent, channel, useSingleFilter, acceptanceCode, acceptanceMask);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nSet Acceptance Filter successful completed!\n");
            }
            else
            {
                printf( "\nSet Acceptance Filter failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }

            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++  Enter bus on mode
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            errVal = tdrv010Start(hCurrent, channel);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nEnter Bus On successful completed!\n");
            }
            else
            {
                printf("\nEnter Bus On failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 13:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Write a specified number of CAN Messages
            ++      - identifiers: 1, 2, 3, ...
            ++      - extended
            ++      - timeout = 2 seconds
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            printf("Message Text (max. 8 Bytes) ");
            fgets(select, MAX_LEN, stdin);
            dataLen = (int)strlen(select) - 1;
            if (dataLen > 8)
            {
                dataLen = 8;
            }
            memcpy(dataBuf, select, dataLen);
            
            IOFlags = TDRV010_EXTENDED;
            timeout = 2;

            printf("Number of messages (decimal) [%d] ", numberOfMessages);
            fgets(select, MAX_LEN, stdin);
            if (select[0] != '\n')
            {
                numberOfMessages = atoi(select);
            }

            for (i = 0; i < numberOfMessages; i++)
            {
                identifier = i + 1;

                errVal = tdrv010Write(hCurrent, channel, timeout, identifier, IOFlags, dataLen, dataBuf);

                //  Check the result of the last device I/O control operation
                if(errVal == 0)
                {
                    // printf("\nSend message successful completed!\n");
                }
                else
                {
                    printf( "\nSend message failed [msgNum %d] -->  Error = %d\n", i, errVal); 
                    PrintErrorMessage(errVal);
                }
            }
            break;


        case 14:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Read a CAN Messages and store to file
            ++      - File: msg.txt
            ++      - Timeout 5 sec.
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
            i = 0;
            timeout = 5;

            if ((fp = fopen("msg.txt", "w")) != NULL)
            {
                errVal = tdrv010Read(hCurrent, channel, timeout, &identifier, &IOFlags, &msgStatus, &dataLen, dataBuf);

                while (errVal == 0)
                {
                    i++;    /* count succesful messages */

                    printf("ID=%ld len=%d status=%d data= %d,%d,%d,%d,%d,%d,%d,%d\n", identifier, dataLen, msgStatus,
                           dataBuf[0], dataBuf[1], dataBuf[2], dataBuf[3], dataBuf[4], dataBuf[5], dataBuf[6], dataBuf[7]);
                    fprintf(fp, "ID=%ld len=%d status=%d data= %d,%d,%d,%d,%d,%d,%d,%d\n", identifier, dataLen, msgStatus,
                            dataBuf[0], dataBuf[1], dataBuf[2], dataBuf[3], dataBuf[4], dataBuf[5], dataBuf[6], dataBuf[7]);

                    errVal = tdrv010Read(hCurrent, channel, timeout, &identifier, &IOFlags, &msgStatus, &dataLen, dataBuf);
                }

                printf("\n\nMessage read terminated after %d successful reads\n", i);
                printf("    last read failed -->  Error = %d\n", errVal);
                PrintErrorMessage(errVal);

                fprintf_s(fp, "\n\nMessage read terminated after %d successful reads\n", i);
                fprintf_s(fp, "    last read failed -->  Error = %d\n", errVal);

                if (fclose(fp))
                {
                    printf("closing filehandle failed\n", errVal);
                }
            }
            else
            {
                printf("Open file for CAN messages failed\n");
            }
            break;


        case 15:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Setup Reset/Operating Mode
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
    	  	printf("Enter:  0 = Reset Mode, 1 = Operating Mode :");
            fgets(select, MAX_LEN, stdin);
            
            errVal = tdrv010CanReset(hCurrent, channel, (atoi(select) == 0) ? FALSE : TRUE);

            //  Check the result of the last device I/O control operation
            if(errVal == 0)
            {
                printf("\nSetup Reset/Operating Mode successful completed!\n");
            }
            else
            {
                printf("\nSetup Reset/Operating Mode failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 16:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Setup Silent/Operating Mode
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
    	  	printf("Enter:  0 = Silent Mode, 1 = Operating Mode :");
            fgets(select, MAX_LEN, stdin);

            errVal = tdrv010CanSel(hCurrent, channel, (atoi(select) == 0) ? FALSE : TRUE);

            //  Check the result of the last device I/O control operation
            //
            if(errVal == 0)
            {
                printf("\nSetup Silent/Operating Mode successful completed!\n");
            }
            else
            {
                printf( "\nSetup Silent/Operating Mode failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;



        case 17:
            /*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ++
            ++  Setup Interrupt Disable/Enable
            ++
            +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
    	  	printf("Enter:  0 = Interrupt Disable, 1 = Interrupt Enable :");
            fgets(select, MAX_LEN, stdin);
            
            errVal = tdrv010CanInt(hCurrent, channel, (atoi(select) == 0) ? FALSE : TRUE);

            //
            //  Check the result of the last device I/O control operation
            //
            if(errVal == 0)
            {
                printf("\nSetup Interrupt Disable/Enable successful completed!\n");
            }
            else
            {
                printf( "\nSetup Interrupt Disable/Enable failed -->  Error = %d\n", errVal); 
                PrintErrorMessage(errVal);
            }
            break;


        case 50:
            printf("\nPlease enter valid device number [1..%d] = ", MAX_DEVICES);
            fgets(select, MAX_LEN, stdin);
            sscanf_s(select, "%lX", &selnum);

            if ((selnum > 0) &&
                (selnum <= MAX_DEVICES) &&
                (hDevices[selnum-1] != INVALID_HANDLE_VALUE) )
            {
                hCurrent = hDevices[selnum-1];
                sprintf_s(devName, 20, "%s_%d", DEVICE_NAME, selnum);
                printf("\n\nOpen %s for I/O...\n\n", devName);
            }
            else
            {
                printf("\nUnknown TDRV010 device\n\n");
            }
            break;


        case 51:        /* Change channel number */
            channel ++;
            if (channel > 2)
            {
                channel = 1;
            }
            break;


        case 99:
            break;
        }
    } while(selnum != 99);

    //
    //  Close all currently open TDRV010 device and exit
    //
    for (i = 0; i < MAX_DEVICES; i++ )
    {
        if (hDevices[i] != INVALID_HANDLE_VALUE)
        {
            errVal = tdrv010Close(hDevices[i]);
        }
    }
}




//////////////////////////////////////////////////////////////////////////////
//  Function:
//      PrintErrorMessage
//
//  Description:
//      Formats a message string for the last error code
//
//  Arguments:
//      none
//
//  Return Value:
//      none
//////////////////////////////////////////////////////////////////////////////

static VOID PrintErrorMessage(int errVal)

{
    LPVOID lpMsgBuf;

    if (errVal < 0) errVal = -errVal;

    FormatMessage( 
        FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
        NULL,
        errVal,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
        (LPTSTR) &lpMsgBuf,
        0,
        NULL 
        );

    printf("%s\n", lpMsgBuf);
}

/******************************************************************************
**  Function:
**      PrintIOFlags
**
**  Description:
**      Printout text for every set I/O flag
**
**  Arguments:
**      IOFlags     Set of I/O flags
**
**  Return Value:
**      none
*******************************************************************************/
static VOID PrintIOFlags(UCHAR IOFlags)

{
    if (IOFlags & TDRV010_EXTENDED)
    {
        printf("I/O Flags :  <EXTENDED>");
    }
    else
    {
        printf("I/O Flags :  <STANDARD>");
    }

    if (IOFlags & TDRV010_REMOTE_FRAME)
    {
        printf(" | <REMOTE>");
    }
    if (IOFlags & TDRV010_SINGLE_SHOT)
    {
        printf(" | <SINGLE_SHOT>");
    }
    if (IOFlags & TDRV010_SELF_RECEPTION)
    {
        printf(" | <SELF_RECEPTION>");
    }
    printf("\n");
}
