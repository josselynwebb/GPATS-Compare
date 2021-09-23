# Non-ATLAS Module (NAM) Description

### Name:  FirewallUpdateNAM

### Category ( System / APS ):  System

### System / APS Nomenclature:  GPATS

#### Prepared by Jeremy Wiley

#### Date: Feb 22, 2021

***
**Purpose:**<br/><br/>
Allows for temporary configuration of Windows firewall via MS Windows command line interface and/or ATLAS Test Program Software (TPS)<br/><br/>
**Dependencies:**<br/><br/>
.NET 4.6.1 Run-time (Microsoft)<br/>
PAWS Studio Run Time System (RTS) v1.40.1 with appropriate dynamic link library - nam.dll (Teradyne)<br/>
GPATSUtils Windows Service v1.1.0 - \\Program Files (x86)\ATS\GPATSUtils.exe<br/><br/>
**Syntax**<br/><br/>
FirewallUpdateNAM <span style="color:red;"><ACTION\></span><span style="color:blue;"><Options\></span> <br/><br/>
**Arguments**<br/><br/>

<span style="color:red;"><ACTION\></span><br/>
Description - Action to perform<br/>
Type - String<br/>

- <span style="color:red;">OPEN</span><br/>
Description - Create TPS firewall rule allowing network traffic on given port<br/><br/>
<span style="color:blue;"><Options\></span><br/>
Options relevant to given action<br/><br/>
  - <span style="color:blue;">/DIR:</span><br/>
Description - Direction of traffic<br/>
Type - String<br/>
Range - (IN / OUT / MAX)<br/>

  - <span style="color:blue;">/PROTO:</span><br/>
  Description - Desired Protocol(s)<br/>
  Type - String<br/>
  Range - (TCP / UDP / BOTH)<br/>

  - <span style="color:blue;">/PORT:</span><br/>
  Description - Desired Port<br/>
  Type - String<br/>
  Range - 1 thru 65353<br/>

  - <span style="color:green;">/RTN_ARG</span> <span style="color:cyan;">(ATLAS only)</span><br/>
  Description - Return Status<br/>
  Type - Integer<br/>
  Range - -1 or 0

 - <span style="color:red;">CLOSE</span><br/>
 Description - Remove TPS Firewall Rule by name (based on input parameters)<br/><br/>
 <span style="color:blue;"><Options\></span><br/>
 Options relevant to given action<br/><br/>
  - <span style="color:blue;">/DIR:</span><br/>
Description - Direction of traffic<br/>
Type - String<br/>
Range - (IN / OUT / MAX)<br/>

  - <span style="color:blue;">/PROTO:</span><br/>
  Description - Desired Protocol(s)<br/>
  Type - String<br/>
  Range - (TCP / UDP / BOTH)<br/>

  - <span style="color:blue;">/PORT:</span><br/>
  Description - Desired Port<br/>
  Type - String<br/>
  Range - 1 thru 65353<br/>

   - <span style="color:green;">/RTN_ARG</span> <span style="color:cyan;">(ATLAS only)</span><br/>
  Description - Return Status<br/>
  Type - Integer<br/>
  Range - -1 or 0

<br/><br/>

**Example (Command LIne Interface)**<br/><br/>
FirewallUpdateNAM <span style="color:red;">OPEN</span> <span style="color:blue;">/DIR:IN /PROTO:TCP /PORT:453</span><br/>
 <span style="color:green;">0</span><br/>
***

#### Additional Info

See GPATSUtils markdown for more info









