# Non-ATLAS Module (NAM) Description

### Name:  canLoopBackNAM

### Category ( System / APS ):  System

### System / APS Nomenclature:	  GPATS

#### Prepared by Jeremy Wiley

#### Date: Oct 30, 2020

***
**Purpose:**<br/><br/>
Allows for loopback testing of CAN bus via MS Windows command line interface and/or ATLAS Test Program Software (TPS)<br/><br/>
**Dependencies:**<br/><br/>
.NET 4.5 Run-time (Microsoft)<br/>
PAWS Studio Run Time System (RTS) v1.40.1 with appropriate dynamic link library - nam.dll (Teradyne)<br/>
GPATS ATXML dynamic link library - AtXmlApi.dll<br/><br/>
**Syntax**<br/><br/>
canLoopBackNAM <span style="color:red;"><TX Chan\> <RX Chan\> <Timing\> <3Samples\> <Filter\> <AcceptanceCode\> <AcceptanceMask\> <TimeOut\> <Data\> </span> <br/><br/>
**Arguments**<br/><br/>
<span style="color:red;">TX Chan</span><br/>
Description:  Transmit Channel<br/>
Data Type:  Integer<br/>
Units - N/A<br/>
Range: 1 thru 2<br/>

<span style="color:red;">RX Chan</span><br/>
Description - Receive Channel<br/>
Data Type - Integer<br/>
Units - N/A<br/>
Range - 1 thru 2<br/>

<span style="color:red;">Timing</span><br/>
Description - Timing<br/>
Data Type: Integer<br/>
Units - Hz<br/>
Range: 20,000 thru 1,000,000<br/>

<span style="color:red;">3Samples</span><br/>
Description - 3 Samples<br/>
Data Type - Integer<br/>
Units - N/A<br/>
Range - 0 thru 1<br/>

<span style="color:red;">Filter</span><br/>
Description - Filter<br/>
Data Type - Integer<br/>
Units - N/A<br/>
Range - 0 thru 1<br/>

<span style="color:red;">AcceptanceCode</span><br/>
Description - Acceptance Code<br/>
Data Type - Unsigned Integer<br/>
Units - N/A<br/>
Range - 0 thru 4294967295<br/>
	
<span style="color:red;">AcceptanceMask</span><br/>
Description - Acceptance Mask<br/>
Data Type - Unsigned Integer<br/>
Units - N/A<br/>
Range - 0 thru 4294967295<br/>

<span style="color:red;">TimeOut</span><br/>
Description - Time-Out<br/>
Data Type - Integer<br/>
Units - mSec<br/>
Range - 0 thru 60,000<br/>

<span style="color:red;">Data</span><br/>
Description - Data to transmit<br/>
Data Type - String<br/>
Units - N/A<br/>

<span style="color:green;"><RETURN\></span><br/>
	Description - Pass/Fail Status<br/>
	Data Type - String<br/>
	Units - N/A<br/>
	Range - "Passed"/"Failed"<br/>

<br/><br/>

**Example (Command LIne Interface)**<br/><br/>
canLoopBackNAM <span style="color:red;">1 2 20000 </span> <span style="color:blue;">9</span><br/>
 <span style="color:green;">Passed</span><br/>
***
####Additional Info
See SBIR IRWindows 2001 software manual for more info









