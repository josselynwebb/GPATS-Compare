# Non-ATLAS Module (NAM) Description

### Name:  NetConfigNAM

### Category ( System / APS ):  System

### System / APS Nomenclature:  GPATS

#### Prepared by Jeremy Wiley

#### Date: Jan 21, 2021

***
**Purpose:**<br/><br/>
Allows for configuration of network ports via MS Windows command line interface and/or ATLAS Test Program Software (TPS)<br/><br/>
**Dependencies:**<br/><br/>
.NET 4.5 Run-time (Microsoft)<br/>
PAWS Studio Run Time System (RTS) v1.40.1 with appropriate dynamic link library - nam.dll (Teradyne)<br/>
GPATSUtils Windows Service - \\Program Files (x86)\ATS\GPATSUtils.exe<br/><br/>
**Syntax**<br/><br/>
NetConfigNAM <span style="color:red;"><ACTION\></span><span style="color:blue;"><Options\></span> <br/><br/>
**Arguments**<br/><br/>

<span style="color:red;"><ACTION\></span><br/>
Description - Action to perform<br/>
Type - String<br/>

- <span style="color:red;">STATICIP</span><br/>
Description - Configure given network interface with a static IP address<br/><br/>
<span style="color:blue;"><Options\></span><br/>
Options relevant to given action<br/><br/>
  - <span style="color:blue;">/INTFC:</span><br/>
Description - network interface name<br/>
Type - String<br/>
Range - N/A<br/>

  - <span style="color:blue;">/ADDR:</span><br/>
  Description - Desired IP address<br/>
  Type - String<br/>
  Range - 0.0.0.0 thru 255.255.255.255<br/>

  - <span style="color:blue;">/MASK:</span><br/>
  Description - Desired SubNet Mask<br/>
  Type - String<br/>
  Range - 0.0.0.0 thru 255.255.255.255<br/>

  - <span style="color:blue;">/GW:</span><br/>
  Description - Desired Gateway Address<br/>
  Type - String<br/>
  Range - 0.0.0.0 thru 255.255.255.255<br/>

  - <span style="color:green;">/RTN_ARG</span> <span style="color:cyan;">(ATLAS only)</span><br/>
  Description - Return Status<br/>
  Type - Integer<br/>
  Range - -1 or 0

 - <span style="color:red;">DHCP</span><br/>
 Description - Configure given network interface for DHCP<br/><br/>
 <span style="color:blue;"><Options\></span><br/>
 Options relevant to given action<br/><br/>
   - <span style="color:blue;">/INTFC:</span><br/>
Description - network interface name<br/>
Type - String<br/>
Range - N/A<br/>

   - <span style="color:green;">/RTN_ARG</span> <span style="color:cyan;">(ATLAS only)</span><br/>
  Description - Return Status<br/>
  Type - Integer<br/>
  Range - -1 or 0

- <span style="color:red;">RESET</span><br/>
Description - Reset given network interface to default configuration<br/>
   - <span style="color:blue;">/INTFC:</span><br/>
Description - network interface name<br/>
Type - String<br/>
Range - N/A<br/>

   - <span style="color:green;">/RTN_ARG</span> <span style="color:cyan;">(ATLAS only)</span><br/>
  Description - Return Status<br/>
  Type - Integer<br/>
  Range - -1 or 0

<br/><br/>

**Example (Command LIne Interface)**<br/><br/>
NetConfigNAM <span style="color:red;">DHCP</span> <span style="color:blue;">Gigabit1</span><br/>
 <span style="color:green;">0</span><br/>
***

#### Additional Info

See GPATSUtils markdown for more info









