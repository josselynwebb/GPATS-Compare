

                      USING THE BTI1553 VB.NET EXAMPLES

                                May 14, 2015

                 Copyright 2002-2015 by Ballard Technology, Inc.


                                ------------


          This folder contains the following folders:

          Folder Name          Description

          <EXAMP01>      Example 1:   Simulating the BC - Unscheduled Messages
          <EXAMP02>      Example 2:   Simulating the BC - Scheduled Messages
          <EXAMP03>      Example 3:   Simulating an RT
          <EXAMP04>      Example 4:   Monitoring the bus
          <EXAMP05>      Example 5:   Simultaneous BC, RT, and monitor simulation
          <EXAMP06>      Example 6:   Errors in BC Transmissions - Unscheduled Messages
          <EXAMP07>      Example 7:   Errors in BC Transmissions - Scheduled Messages
          <EXAMP08>      Example 8:   Errors in RT Transmissions
          <EXAMP10>      Example 10:  Polling the Event Log List
          <EXAMP11>      Example 11:  BC Transmission of Ramping Data Using a Circular Buffer
          <EXAMP12>      Example 12:  BC Transmission of Ramping Data Using a FIFO
          <EXAMP13>      Example 13:  BC Transmission of Data Using a Ping-Pong Buffer
          <EXAMP14>      Example 14:  Multiple Cores
          <EXAMP15>      Example 15:  IRIG Timer
          <EXAMP16>      Example 16:  RT Response Time
          <EXAMP17>      Example 17:  Parametrics
          <EXAMP18>      Example 18:  Engineering Unit

          README.TXT     This document

          Refer to the end of this document for corrections,
          warnings, and additional information.


     INTRODUCTION

          This folder contains Visual Basic.NET project
          folders.  These projects are Visual Basic.NET
          versions of the C/C++ examples.  Refer to the
          source comments for information on each of the
          examples.



     BREAKING CHANGES

          6/25/13
          1. Card/Core handles are converted from UInt32 to 
          IntPtr for 64-bit compatibility. All handles need 
          to be converted to this new type.
          2. Array parameters (such as msgaddr in the SchedBuild
          function) now utilize the array type directly in order
          to support .NET Framework versions > 2.0. The array
          index is no longer passed to the function (e.g.
          "msgaddr(0)" becomes "msgaddr"). The type used is
          unaffected.



     CORRECTIONS, WARNINGS, AND ADDITIONAL INFORMATION

          There is no additional information.


                                ------------


          SAFETY WARNING: Ballard products are not intended,
          warranted or certified for any particular use or
          application or for use in any application where
          failure of the products could lead directly to
          death, personal injury, or damage to property.
          Customers, licensees, and/or users are responsible
          for establishing and assuring suitability and proper
          use of Ballard products for their particular use or
          application.


                                ------------


     Ballard Technology would appreciate hearing your questions,
     comments, and suggestions concerning this product.

     Ballard Technology, Inc.
     www.ballardtech.com
     support@ballardtech.com
