////////////////////////////////////////////////////////////////////////////////
// File:	    ZT1428VXI_T.cpp
//
// Date:	    05APR06
//
// Purpose:	    ATXMLW Instrument Driver for ZT1428VXI
//
// Instrument:	ZT1428VXI  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     AtXmlWrapper.lib       ..\..\Common\lib  (EADS Wrapper support functions)
//
//
// Revision History
// Rev	     Date                  Reason					  AUTHOR
// =======  =======  =======================================  =================
// 1.0.0.0  05APR06  Baseline Release						  D. Bubenik, EADS North America Defense
// 1.2.0.1  04JAN07	 Added 200mS delay in initScope()		  M. Hendricksen, EADS North America Defense
//					  before the capture of the signal
//					  and after the capture of signal
//					  which is before the READ.  Customer
//					  recommended.
// 1.2.1.1 23FEB07  Corrected code in scope setup to change   M. Hendricksen EADS North America Defense 
//                      the amplitude to full-scale, before 
//                      setting the vertical parameters.
//                  Corrected code in scope_setup() to treat
//                   a pulsed dc measurment same as dc measurment
//                   when setting the amplitude.
// 1.2.1.2 12Mar07  Corrected code in scope_setup(), for DC	  M. Hendricksen, EADS
//						and Pulsed DC to only not the voltage to
//						the m_Amplitude.Real and not the offset.
// 1.2.1.3 12Mar07  Corrected code in scope_setup()           M.Hendricksen, EADS
//                      to only use offset for amplitude if
//                      the signal is DC.
// 1.2.1.4 23Mar07  Added code to set the max-time in         M.Hendricksen, EADS
//                      function SetupZT1428VXI, without it being
//                      set digitizing the waveform would time-out.
// 1.4.0.1 02Jul07  Increased maximum return data array       D. Bubenik, EADS
//                      size to 8000. PCR 167
// 1.4.0.2 06Sep07  Changed the scope reference from center   D. Bubenik, EADS
//                      to left. PCR 167
// 1.4.0.3 23MAR11  Corrected retrieval of maxtime value      E. Larson, EADS
// 1.4.0.4 14MAY11  Corrected sample time for waveform        E. Larson, EADS
// 1.4.0.5 17MAY11  Corrected for floating point error        E. Larson, EADS
// 1.4.0.6 18MAY11  Corrected format for invalid variable     E. Larson, EADS
////////////////////////////////////////////////////////////////////////////////
// Includes
#include "visa.h"
#include "AtxmlWrapper.h"
#include "AtxmlZT1428VXI_T.h"
#include "zt1428.h"

//from legacy
ViString	Out_Buf, Mea_Buf;
ViStatus	err;
char		fstring[80]; 

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024
#define SQRT_2         1.414213562373 //As defined
#define SQRT_3         1.73205080808 //As defined

int	DE_BUG = 0;
FILE *debugfp = 0;
//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

/*********************************************************************************/
/*  					Scope Setup					 							 */
/*********************************************************************************/
void CZT1428VXI_T::scope_setup(double sweeptime=.001, ATXMLW_INTF_RESPONSE* Response=NULL, int BufferSize=0)
{
	
	ISDODEBUG(dodebug(0, "scope_setup", "(%f)", sweeptime, (char*) NULL));

	int chan = ZT1428_CHAN1;
	int	coupling = ZT1428_VERT_COUP_DC;
	int	lowpass=ZT1428_VERT_FILT_OFF;
	double amplitude = 4.0;
	double interval = 0.000002;
	double offset = 0.0;
	double probe = 1.0;
	int triggerMode = ZT1428_ACQ_AUTO;
	int acquireType = ZT1428_ACQ_NORM;
	int trigport = 0;
	double sampletime = 0.0;
	int points = 8000;
	int Status = 0;

	// Channel setup
	if(m_nChannel.Int==2)
	{
		chan = ZT1428_CHAN2;
	}

	ISDODEBUG(dodebug(0, "scope_setup", "Found Chan [%d]", chan, (char*) NULL));

	// test assign number of points
	if (m_dSamples.Exists)
	{
		points = (int)m_dSamples.Real;
	}

	IFNSIM(Status=zt1428_query_vertical(m_Handle, chan, &coupling, &lowpass, &probe, &amplitude, &offset));

	ATXMLW_DEBUG(5,atxmlw_FmtMsg("%d = zt1428_query_vertical(%x, %d, %d, %d, %.3lf, %.3lf, %.3lf);",
		Status, m_Handle, chan, coupling, lowpass, probe, amplitude, offset), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "scope_setup", "%d = zt1428_query_vertical(%x, %d, %d, %d, %.3lf, %.3lf, %.3lf);",
		Status, m_Handle, chan, coupling, lowpass, probe, amplitude, offset, (char*) NULL));
	
	if(Status)
	{
		ISDODEBUG(dodebug(0, "scope_setup", "Status (%d)", Status, (char*) NULL));
		ErrorZT1428VXI(Status, Response, BufferSize);
		ISDODEBUG(dodebug(0, "scope_setup", "Response (%s)", Response, (char*) NULL));
	}

	Sleep(1000);
	
	// Coupling setup
	if (m_nAcCoupling.Exists && m_nAcCoupling.Int == 1)
	{
		coupling = ZT1428_VERT_COUP_AC;
		ISDODEBUG(dodebug(0, "scope_setup", "Found Coupling [AC]", (char*) NULL));
	}
	else
	{
		coupling = ZT1428_VERT_COUP_DC;
		ISDODEBUG(dodebug(0, "scope_setup", "Found Coupling [DC]", (char*) NULL));
	}

	// Impedance
	if (m_nImpedance.Exists && m_nImpedance.Int == 50 && m_nAcCoupling.Int == 0)
	{
		coupling = ZT1428_VERT_COUP_DCF;
		ISDODEBUG(dodebug(0, "scope_setup", "Found Impedance [%d]", m_nImpedance.Int, (char*) NULL));
	}

	// Freq window Min
	if (m_dBanwidthMin.Exists && m_dBanwidthMin.Real > 450)
	{
		coupling = ZT1428_VERT_COUP_ACLFR;
		ISDODEBUG(dodebug(0, "scope_setup", "Found Bandwidth Min [%f]", m_dBanwidthMin.Real, (char*) NULL));
	}
	
	// Freq Window Max
	if(m_dBanwidthMax.Exists && m_dBanwidthMax.Real < 30000000)
	{
		lowpass = ZT1428_VERT_FILT_30MHZ;
		ISDODEBUG(dodebug(0, "scope_setup", "Found Bandwidth Max [%f]", m_dBanwidthMax.Real, (char*) NULL));
	}

	// Attenuation
	if (m_nGain.Exists && m_nGain.Int==10)
	{
		probe = 10.0;
		ISDODEBUG(dodebug(0, "scope_setup", "Found Attenuation [%d]", m_nGain.Int, (char*) NULL));
	}
	
	// dc offset
	if (m_dDc_offset.Exists)
	{
		offset = m_dDc_offset.Real;
		ISDODEBUG(dodebug(0, "scope_setup", "Found DC Offset [%f]", m_dDc_offset.Real, (char*) NULL));
	}

	// Sweep-time (linked to freq)
	//if (m_dFrequency.Exists || m_dPeriod.Exists || m_dSampleTime.Exists || m_RiseTime.Exists || m_FallTime.Exists || m_PulseWidth.Exists)
	if (m_dFrequency.Exists || m_dPeriod.Exists || m_RiseTime.Exists || m_FallTime.Exists || m_PulseWidth.Exists)
	{
		if(sweeptime<=.0000005)  //based on old inst with 8000 samples
			interval=.000000001;
		else if(sweeptime>.0000005 && sweeptime<=.000001)
			interval=.000000002;
		else if(sweeptime>.000001 && sweeptime<=.000002)
			interval=.000000004;
		else if(sweeptime>.000002 && sweeptime<=.000005)
			interval=.00000001;
		else if(sweeptime>.000005 && sweeptime<=.00001)
			interval=.00000002;
		else if(sweeptime>.00001 && sweeptime<=.00002)
			interval=.00000004;
		else if(sweeptime>.00002 && sweeptime<=.00005)
			interval=.0000001;
		else if(sweeptime>.00005 && sweeptime<=.0001)
			interval=.0000002;
		else if(sweeptime>.0001 && sweeptime<=.0002)
			interval=.0000004;
		else if(sweeptime>.0002 && sweeptime<=.0005)
			interval=.000001;
		else if(sweeptime>.0005 && sweeptime<=.001)
			interval=.000002;		
		else if(sweeptime>.001 && sweeptime<=.002)
			interval=.000004;		
		else if(sweeptime>.002 && sweeptime<=.005)
			interval=.00001;		
		else if(sweeptime>.005 && sweeptime<=.01)
			interval=.00002;		
		else if(sweeptime>.01 && sweeptime<=.02)
			interval=.00004;
		else if(sweeptime>.02 && sweeptime<=.05)
			interval=.0001;
		else if(sweeptime>.05 && sweeptime<=.1)
			interval=.0002;
		else if(sweeptime>.1 && sweeptime<=.2)
			interval=.0004;
		else if(sweeptime>.2 && sweeptime<=.5)
			interval=.001;
		else if(sweeptime>.5 && sweeptime<=1)
			interval=.002;
		else if(sweeptime>1 && sweeptime<=2)
			interval=.004;
		else if(sweeptime>2 && sweeptime<=5)
			interval=.01;
		else if(sweeptime>5 && sweeptime<=10)
			interval=.02;
		else if(sweeptime>10 && sweeptime<=20)
			interval=.05;
		else
			interval=.1;

		ISDODEBUG(dodebug(0, "scope_setup", "Interval Set to [%f]", interval, (char*) NULL));

	}
	// Test section - allow variable samples and control over interval
	// Note: This bypasses the divide by 16 of the setup_sample() function
	//       and directly configures the sample interval based on the 
	//       ATLAS sample time and number of samples.
	else if (m_dSampleTime.Exists)
	{
		sampletime = m_dSampleTime.Real / m_dSamples.Real;

		if (sampletime <= 20.0000001e-12)
			interval = 20.0e-12;
		else if (sampletime <= 40.0000001e-12)
			interval = 40.0e-12;
		else if (sampletime <= 100.0000001e-12)
			interval = 100.0e-12;
		else if (sampletime <= 200.0000001e-12)
			interval = 200.0e-12;
		else if (sampletime <= 400.0000001e-12)
			interval = 400.0e-12;
		else if (sampletime <= 1.0000001e-9)
			interval = 1.0e-9;
		else if (sampletime <= 2.0000001e-9)
			interval = 2.0e-9;
		else if (sampletime <= 4.0000001e-9)
			interval = 4.0e-9;
		else if (sampletime <= 10.0000001e-9)
			interval = 10.0e-9;
		else if (sampletime <= 20.0000001e-9)
			interval = 20.0e-9;
		else if (sampletime <= 40.0000001e-9)
			interval = 40.0e-9;
		else if (sampletime <= 100.0000001e-9)
			interval = 100.0e-9;
		else if (sampletime <= 200.0000001e-9)
			interval = 200.0e-9;
		else if (sampletime <= 400.0000001e-9)
			interval = 400.0e-9;
		else if (sampletime <= 1.0000001e-6)
			interval = 1.0e-6;
		else if (sampletime <= 2.0000001e-6)
			interval = 2.0e-6;
		else if (sampletime <= 4.0000001e-6)
			interval = 4.0e-6;
		else if (sampletime <= 10.0000001e-6)
			interval = 10.0e-6;
		else if (sampletime <= 20.0000001e-6)
			interval = 20.0e-6;
		else if (sampletime <= 40.0000001e-6)
			interval = 40.0e-6;
		else if (sampletime <= 100.0000001e-6)
			interval = 100.0e-6;
		else if (sampletime <= 200.0000001e-6)
			interval = 200.0e-6;
		else if (sampletime <= 400.0000001e-6)
			interval = 400.0e-6;
		else if (sampletime <= 1.0000001e-3)
			interval = 1.0e-3;
		else if (sampletime <= 2.0000001e-3)
			interval = 2.0e-3;
		else if (sampletime <= 4.0000001e-3)
			interval = 4.0e-3;
		else if (sampletime <= 10.0000001e-3)
			interval = 10.0e-3;
		else if (sampletime <= 20.0000001e-3)
			interval = 20.0e-3;
		else if (sampletime <= 40.0000001e-3)
			interval = 40.0e-3;
		else if (sampletime <= 100.0000001e-3)
			interval = 100.0e-3;
		else if (sampletime <= 200.0000001e-3)
			interval = 200.0e-3;
		else if (sampletime <= 400.0000001e-3)
			interval = 400.0e-3;
		else
			interval = 1.0;

		ISDODEBUG(dodebug(0, "scope_setup", "Interval Set to [%f]", interval, (char*) NULL));

	}

	if(m_nMeasChar.Int == 7) //dc measurment ONLY
	{
		interval=.000004;
	}
	
	if (m_dAmplitude.Exists)
	{
		amplitude = m_dAmplitude.Real * 1.5; // was multiplied by 2....but why exactly??
		
		// increase scale to account for dc offset
		amplitude = amplitude + offset;

		if (amplitude < 0)
		{
			amplitude *= -1.0;
		}

		if (amplitude > 40.0)
		{
			amplitude = 40.0;
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Amplitude exceeded 40 volts.  Will be set to 40 volts.\n", Response, BufferSize);
		}		
	}
	else
	{
		amplitude = 4.0;
	}

	IFNSIM(Status=zt1428_vertical(m_Handle, chan, coupling, lowpass, probe, amplitude, offset));

	if(Status)
	{
		ErrorZT1428VXI(Status, Response, BufferSize);
	}

	ATXMLW_DEBUG(5,atxmlw_FmtMsg("%d = zt1428_vertical(%x, %d, %d, %d, %f, %f, %f)",Status,
		m_Handle, chan, coupling, lowpass, probe, amplitude, offset), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "scope_setup", "%d = zt1428_vertical(Hndl [%x], Ch [%d], Cpl [%d], Fltr [%d], Prb [%f], Amp [%f], Offs [%f])",Status,
		m_Handle, chan, coupling, lowpass, probe, amplitude, offset, (char*) NULL));

	// trigger m_TrigInfo
	if(m_TrigInfo.TrigExists)// (m_dTrigLevel.Exists)
	{
		ISDODEBUG(dodebug(0, "scope_setup", "In if(m_TrigInfo.TrigExists)", (char*) NULL));
	
		if(strcmp(m_TrigInfo.TrigPort, "CHA")==0)
		{
			ISDODEBUG(dodebug(0, "scope_setup", "In if(m_TrigInfo.TrigExists) and if(strcmp(m_TrigInfo.TrigPort, CHA)==0)", (char*) NULL));
			trigport=ZT1428_TRG_CHAN1;
		}
		else if(strcmp(m_TrigInfo.TrigPort, "CHB")==0)
		{
			ISDODEBUG(dodebug(0, "scope_setup", "In if(m_TrigInfo.TrigExists) and else if(strcmp(m_TrigInfo.TrigPort, CHB)==0)", (char*) NULL));
			trigport=ZT1428_TRG_CHAN2;
		}
		else if(strcmp(m_TrigInfo.TrigPort, "EXT")==0)
		{
			ISDODEBUG(dodebug(0, "scope_setup", "In if(m_TrigInfo.TrigExists) and else if(strcmp(m_TrigInfo.TrigPort, EXT)==0)", (char*) NULL));
			trigport=ZT1428_TRG_EXT;
		}
		else
		{
			trigport=m_nChannel.Int;
		}

		IFNSIM(Status=zt1428_edge_trigger(m_Handle, trigport,m_TrigInfo.TrigLevel, m_TrigInfo.TrigSlopePos, ZT1428_TRG_SENS_NORM));
		ISDODEBUG(dodebug(0, "scope_setup", "Status[%d] =zt1428_edge_trigger(m_Handle[%d], trigport[%d],m_TrigInfo.TrigLevel[%f], m_TrigInfo.TrigSlopePos, ZT1428_TRG_SENS_NORM)",Status, m_Handle, m_TrigInfo.TrigLevel, (char*) NULL));

		if(Status)
		{
			ErrorZT1428VXI(Status, Response, BufferSize);
		}

		IFNSIM(Status=zt1428_trigger_holdoff(m_Handle, ZT1428_TRG_HOLD_TIME, 1, m_TrigInfo.TrigDelay));
		if(Status)
		{
			ErrorZT1428VXI(Status, Response, BufferSize);
		}
	}
	
	// Sample count for average measurements
	if (m_nSampleCount.Exists && m_nSampleCount.Int > 1)
	{
		if(m_TrigInfo.TrigExists)
		{
			triggerMode=ZT1428_ACQ_TRIG;
		}
		else
		{
			triggerMode=ZT1428_ACQ_AUTO;
		}

		acquireType=ZT1428_ACQ_AVER;
	}
	else
	{
		m_nSampleCount.Int=1;
		
		if(m_TrigInfo.TrigExists)
		{
			triggerMode=ZT1428_ACQ_TRIG;
		}
		else
		{
			triggerMode=ZT1428_ACQ_AUTO;
		}

	    acquireType=ZT1428_ACQ_NORM;
	}

	// Changed param 2 from fixed 8000 to variable may be set for waveform
	IFNSIM(Status=zt1428_acquisition(m_Handle, points, interval, ZT1428_ACQ_LEFT, 0, triggerMode, acquireType, m_nSampleCount.Int));
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("%d = zt1428_acquisition(%x, %d, %g, ZT1428_ACQ_LEFT, 0, %d, %d, %d)",
		Status, m_Handle, points, interval, triggerMode, acquireType, m_nSampleCount.Int), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "scope_setup", "%d = zt1428_acquisition(%x, %d, %g, ZT1428_ACQ_LEFT, 0, %d, %d, %d)",
		Status, m_Handle, points, interval, triggerMode, acquireType, m_nSampleCount.Int, (char*) NULL));

	if(Status)
	{
		ErrorZT1428VXI(Status, Response, BufferSize);
	}
	
	ISDODEBUG(dodebug(0, "scope_setup", "Leaving function", (char*) NULL));
}

///////////////////////////////////////////////////////////////////////////////
// Function: CZT1428VXI_T
//
// Purpose: Initialize the instrument driver
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Instno           int             System assigned instrument number
// ResourceType     int             Type of Resource Base, Physical, Virtual
// ResourceName     char*           Station resource name
// Sim              int             Simulation flag value (0/1)
// Dbglvl           int             Debug flag value
// AddressInfoPtr   InstAddPtr*     Contains all addressing information available
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response         ATXMLW_INTF_RESPONSE* Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////

CZT1428VXI_T::CZT1428VXI_T(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
    char LclMsg[1024];
    int Status = 0;
	
	ISDODEBUG(dodebug(0, "CZT1428VXI_T", "Entering function", (char*) NULL));
	memset(LclMsg, '\0', sizeof(LclMsg));

    // Save Device Info
    m_InstNo = Instno;
    m_ResourceType = ResourceType;
    m_ResourceName[0] = '\0';

    if(ResourceName)
    {
        strnzcpy(m_ResourceName,ResourceName,ATXMLW_MAX_NAME);
    }

    m_Sim = Sim;
    m_Dbg = Dbglvl;

    // Save Address Information
    if(AddressInfoPtr)
    {
        m_AddressInfo.ResourceAddress = AddressInfoPtr->ResourceAddress;

        if(AddressInfoPtr->InstrumentQueryID)
        {
            strnzcpy(m_AddressInfo.InstrumentQueryID, AddressInfoPtr->InstrumentQueryID, ATXMLW_MAX_NAME);
        }

        m_AddressInfo.InstrumentTypeNumber = AddressInfoPtr->InstrumentTypeNumber;

        if(AddressInfoPtr->ControllerType)
        {
            strnzcpy(m_AddressInfo.ControllerType, AddressInfoPtr->ControllerType, ATXMLW_MAX_NAME);
        }

        m_AddressInfo.ControllerNumber = AddressInfoPtr->ControllerNumber;
        m_AddressInfo.PrimaryAddress = AddressInfoPtr->PrimaryAddress;
        m_AddressInfo.SecondaryAddress = AddressInfoPtr->SecondaryAddress;
        m_AddressInfo.SubModuleAddress = AddressInfoPtr->SubModuleAddress;
    }

    m_Handle= NULL;

	if (m_Sim)
	{
		m_Handle = 1;
	}

    m_InitString[0] = '\0';

    InitPrivateZT1428VXI();
	NullCalDataZT1428VXI();

    // The Form Init String
    sprintf(m_InitString,"%s%d::%d::INSTR", m_AddressInfo.ControllerType,m_AddressInfo.ControllerNumber, m_AddressInfo.PrimaryAddress);
    sprintf(LclMsg,"Wrap-CZT1428VXI Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);
	ISDODEBUG(dodebug(0, "CZT1428VXI_T", "Wrap-CZT1428VXI Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg, (char*) NULL));
    
	// Initialize the ZT1428VXI
	IFNSIM(Status=zt1428_init_with_options(m_InitString, false, true, &m_Handle));
	ATXMLW_DEBUG(9,atxmlw_FmtMsg("zt1428_init_with_options(%s, false, true, %d)", m_InitString, m_Handle), Response, Buffersize);
	
	ISDODEBUG(dodebug(0, "CZT1428VXI_T", "zt1428_init_with_options(%s, false, true, %d)", m_InitString, m_Handle, (char*) NULL));
    
	if(Status && ErrorZT1428VXI(Status, Response, Buffersize))
	{
        return;
	}

	SanitizeZT1428VXI();
	
	ISDODEBUG(dodebug(0, "CZT1428VXI_T", "Leaving function", (char*) NULL));
    
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: ~CZT1428VXI_T()
//
// Purpose: Destroy the instrument driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//    Class instance destroyed.
//
///////////////////////////////////////////////////////////////////////////////
CZT1428VXI_T::~CZT1428VXI_T()
{
    char Dummy[1024];
	
	ISDODEBUG(dodebug(0, "~CZT1428VXI_T", "Entering function", (char*) NULL));
    // Reset the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CZT1428VXI Class Destructor called "),Dummy,1024);
	
    // Reset the ZT1428VXI
    IFNSIM(zt1428_reset(m_Handle));
	// Release ZT1428VXI
	IFNSIM(zt1428_close(m_Handle));
	
	ISDODEBUG(dodebug(0, "~CZT1428VXI_T", "Leaving function", (char*) NULL));
    
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusZT1428VXI
//
// Purpose: Perform the Status action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::StatusZT1428VXI(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
	
	ISDODEBUG(dodebug(0, "StatusZT1428VXI", "Entering function", (char*) NULL));
    // Status action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusZT1428VXI called "), Response, BufferSize);
    // Check for any pending error messages
    
	Status = ErrorZT1428VXI(0, Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "StatusZT1428VXI", "Wrap-StatusZT1428VXI called with Response [%s] and Status [%d]", Response, Status, (char*) NULL));
	ISDODEBUG(dodebug(0, "StatusZT1428VXI", "Leaving function", (char*) NULL));
    
	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalZT1428VXI
//
// Purpose: Perform the IEEE 1641 / Action action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// SignalDescription  ATXMLW_INTF_SIGDESC*   The Allocated FNC code
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::IssueSignalZT1428VXI(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	
	ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Entering function Response is [%s]", Response,  (char*) NULL));
    // IEEE 1641 Issue Signal action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalZT1428VXI Signal: "), Response, BufferSize);

	if((Status = GetStmtInfoZT1428VXI(SignalDescription, Response, BufferSize)) != 0)
	{
		ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling GetStmtInfoZT1428VXI() with Status [%d]", Status, (char*) NULL));
        
		return(Status);
	}

    switch(m_Action)
    {
		case ATXMLW_SA_APPLY:
			if((Status = SetupZT1428VXI(Response, BufferSize)) != 0)
			{			
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling SetupZT1428VXI() with Status [%d]", Status, (char*) NULL));
				return(Status);
			}
        
			if((Status = EnableZT1428VXI(Response, BufferSize)) != 0)
			{		
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling EnableZT1428VXI() with Status [%d]", Status, (char*) NULL));
				return(Status);
			}

			break;

		case ATXMLW_SA_REMOVE:
			if((Status = DisableZT1428VXI(Response, BufferSize)) != 0)
			{
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling DisableZT1428VXI() with Status [%d]", Status, (char*) NULL));
				return(Status);
			}

			if((Status = ResetZT1428VXI(Response, BufferSize)) != 0)
			{
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling ResetZT1428VXI() with Status [%d]", Status, (char*) NULL));
				return(Status);
			}

			break;

		case ATXMLW_SA_MEASURE:
			if((Status = SetupZT1428VXI(Response, BufferSize)) != 0)
			{
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling SetupZT1428VXI() with Status [%d]", Status, (char*) NULL));
				return(Status);
			}
        
			break;

		case ATXMLW_SA_READ:
			if((Status = EnableZT1428VXI(Response, BufferSize)) != 0)
			{
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling EnableZT1428VXI() with Status [%d]", Status, (char*) NULL));
				return(Status);
			}

			if((Status = FetchZT1428VXI(Response, BufferSize)) != 0)
			{
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling FetchZT1428VXI() with Status [%d]", Status, (char*) NULL));
				return(Status);
			}

			break;

		case ATXMLW_SA_RESET:
		    if((Status = ResetZT1428VXI(Response, BufferSize)) != 0)
			{
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling ResetZT1428VXI() with Status [%d]", Status, (char*) NULL));
		        return(Status);
			}

		    break;

		case ATXMLW_SA_SETUP:
		    if((Status = SetupZT1428VXI(Response, BufferSize)) != 0)
			{
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling SetupZT1428VXI() with Status [%d]", Status, (char*) NULL));
		        return(Status);
			}

		    break;

		case ATXMLW_SA_CONNECT:
		    break;

		case ATXMLW_SA_ENABLE:
		    if((Status = EnableZT1428VXI(Response, BufferSize)) != 0)
			{
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling EnableZT1428VXI() with Status [%d]", Status, (char*) NULL));
		        return(Status);
			}

		    break;

		case ATXMLW_SA_DISABLE:
		    if((Status = DisableZT1428VXI(Response, BufferSize)) != 0)
			{
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling DisableZT1428VXI() with Status [%d]", Status, (char*) NULL));
		        return(Status);
			}

		    break;

		case ATXMLW_SA_FETCH:
		    if((Status = FetchZT1428VXI(Response, BufferSize)) != 0)
			{
				ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Calling FetchZT1428VXI() with Status [%d]", Status, (char*) NULL));
		        return(Status);
			}

		    break;

		case ATXMLW_SA_DISCONNECT:
			break;
    }
	
	ISDODEBUG(dodebug(0, "IssueSignalZT1428VXI", "Leaving function", (char*) NULL));
    
	return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalZT1428VXI
//
// Purpose: Register/Provide the Calibration data for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// CalData            ATXMLW_INTF_CALDATA*   Xml description of the calibration data
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::RegCalZT1428VXI(ATXMLW_INTF_CALDATA* CalData)
{
    int       Status = 0;
    char      Dummy[1024];
	
	ISDODEBUG(dodebug(0, "RegCalZT1428VXI", "Entering function", (char*) NULL));
    // Setup action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalZT1428VXI CalData: %s", CalData),Dummy,1024);
	
	ISDODEBUG(dodebug(0, "RegCalZT1428VXI", "Leaving function", (char*) NULL));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetZT1428VXI
//
// Purpose: Perform the Reset action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::ResetZT1428VXI(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	
	ISDODEBUG(dodebug(0, "ResetZT1428VXI", "Entering function", (char*) NULL));
    // Reset action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetZT1428VXI called "), Response, BufferSize);
    
	// Reset the ZT1428VXI
    IFNSIM((Status = zt1428_reset (m_Handle)));
	ISDODEBUG(dodebug(0, "ResetZT1428VXI", "Status after reset is [%d]",Status, (char*) NULL));
    
	if(Status)
	{
        ErrorZT1428VXI(Status, Response, BufferSize);
	}

    InitPrivateZT1428VXI();

	SanitizeZT1428VXI();
	
	ISDODEBUG(dodebug(0, "ResetZT1428VXI", "Entering function", (char*) NULL));
    
	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstZT1428VXI
//
// Purpose: Perform the SelfTest Action action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Level              ATXMLW_INTF_STLEVEL    Indicates the Instrument Level To Be Performed
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::IstZT1428VXI(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	
	ISDODEBUG(dodebug(0, "IstZT1428VXI", "Entering function", (char*) NULL));
    // Reset action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstZT1428VXI called Level %d", Level), Response, BufferSize);
    
	// Reset the ZT1428VXI
    switch(Level)
    {
		case ATXMLW_IST_LVL_PST:
			Status = StatusZT1428VXI(Response,BufferSize);
		
			ISDODEBUG(dodebug(0, "IstZT1428VXI", "ATXMLW_IST_LVL_PST", (char*) NULL));
			break;
		case ATXMLW_IST_LVL_IST:
		
			ISDODEBUG(dodebug(0, "IstZT1428VXI", "ATXMLW_IST_LVL_IST", (char*) NULL));
			break;
		case ATXMLW_IST_LVL_CNF:
			Status = StatusZT1428VXI(Response,BufferSize);
		
			ISDODEBUG(dodebug(0, "IstZT1428VXI", "ATXMLW_IST_LVL_CNF", (char*) NULL));
			break;
		default: // Hopefully BIT 1-9
		
			ISDODEBUG(dodebug(0, "IstZT1428VXI", "default", (char*) NULL));
			break;
    }

    if(Status)
	{
        ErrorZT1428VXI(Status, Response, BufferSize);
	}

    InitPrivateZT1428VXI();
	
	ISDODEBUG(dodebug(0, "IstZT1428VXI", "Leaving function", (char*) NULL));
    
	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsZT1428VXI
//
// Purpose: Issue Native nstrument commands for to this instrument
//          Return in the response values in Response
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// InstrumentCmds     ATXMLW_INTF_INSTCMD*   Xml description of the Native Instrument commands
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::IssueNativeCmdsZT1428VXI(ATXMLW_INTF_INSTCMD* InstrumentCmds, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
	
	ISDODEBUG(dodebug(0, "IssueNativeCmdsZT1428VXI", "Entering function", (char*) NULL));
    // Setup action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsZT1428VXI "), Response, BufferSize);

    // Retrieve the CalData
    if((Status = atxmlw_InstrumentCommands(m_Handle, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
    {
		ISDODEBUG(dodebug(0, "IssueNativeCmdsZT1428VXI", "Status = atxmlw_InstrumentCommands() [%d]", Status,(char*) NULL));
        
		return(Status);
    }

	if (strstr(InstrumentCmds, "*RST") != NULL)
	{
		SanitizeZT1428VXI(); 
	}
	
	ISDODEBUG(dodebug(0, "IssueNativeCmdsZT1428VXI", "Leaving function", (char*) NULL));
    
	return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallZT1428VXI
//
// Purpose: Issue Instrument Driver function calls for to this instrument
//          Return in the response values in Response
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// DriverFunction     ATXMLW_INTF_DRVRFNC*   Xml description of the IVI Instrument commands
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::IssueDriverFunctionCallZT1428VXI(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    ATXMLW_DF_VAL RetVal;
    ATXMLW_XML_HANDLE DfHandle=NULL;
    char Name[ATXMLW_MAX_NAME];
	
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallZT1428VXI", "Entering function", (char*) NULL));
/*
	<AtXmlDriverFunctionCall>
		<SignalResourceName>DEVX_1</SignalResourceName>
		<SignalChannelName>Chan1</SignalChannelName>
		<DriverFunctionCall>
			<FunctionName>TestParam</FunctionName>
			<ReturnType>RetInt32</ReturnType>
			<Parameter   ParamNumber="1" ParamType="Handle" Value=""/>
			<Parameter   ParamNumber="2" ParamType="SrcUInt16" Value="0x3B05"/>
			<Parameter   ParamNumber="3" ParamType="SrcDblPtr" Value="35.e+3 36.0 37.0e5"/>
			<Parameter   ParamNumber="4" ParamType="SrcStrPtr" Value="xxx yyy zzz"/>
			<Parameter   ParamNumber="5" ParamType="RetInt8"/>
			<Parameter   ParamNumber="6" ParamType="RetStrPtr" Size="52"/>
		</DriverFunctionCall>
	</AtXmlDriverFunctionCall>
*/
    if((atxmlw_ParseDriverFunction(m_Handle, DriverFunction, &DfHandle, Response, BufferSize)) || (DfHandle == NULL))
	{
        return(0);
	}

    atxmlw_GetDFName(DfHandle,Name);
    RetVal.Int32 = 0;
    
	///////// Implement Supported Driver Function Calls ///////////////////////
	if(ISDFNAME("zt1428_read_waveform"))
	{
		RetVal.Int32 = zt1428_read_waveform(SrcInt32(1),SrcInt32(2),SrcInt32(3),RetDblPtr(4),(int *)RetUInt32(5),(int *)RetUInt32(6),RetDbl(7),RetDbl(8),(int *)RetUInt32(9),RetDbl(10),RetDbl(11),(int *)RetUInt32(12));
	}
    else if((strncmp("vi",Name,2)==0) && atxmlw_doViDrvrFunc(DfHandle,Name,&RetVal))
	{
        int x = 0;
	}
    else
    {
        atxmlw_ErrorResponse("", Response, BufferSize, "IssueDriverFunction ", ATXMLW_WRAPPER_ERRCD_INVALID_ACTION, atxmlw_FmtMsg(" Invalid/Unimplemented Function [%s]",Name));
    }

    // return "Ret..." values and RetVal value to caller
    atxmlw_ReturnDFResponse(DfHandle,RetVal,Response,BufferSize);
    atxmlw_CloseDFXml(DfHandle);
	
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallZT1428VXI", "Leaving function", (char*) NULL));
    
	return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: SetupZT1428VXI
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::SetupZT1428VXI(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int		Status = 0;
	double	sweeptime = 0.001;
	char sTemp[256];
	int trigport = 0;
    double  MaxTime = 0;
	
	ISDODEBUG(dodebug(0, "SetupZT1428VXI", "Entered function", (char*) NULL));

	memset(sTemp, '\0', sizeof(sTemp));

    // Setup action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupZT1428VXI called "), Response, BufferSize);
	
	// Check MaxTime Modifier
    if(m_MaxTime.Exists)
	{
        MaxTime = m_MaxTime.Real * 1000.0;
	}
    else
	{
        MaxTime = 10 * 1000.0;
	}

    // set the visa time-out using above MaxTime	
	IFNSIM(Status = viSetAttribute(m_Handle, VI_ATTR_TMO_VALUE,(unsigned long) MaxTime));
	ISDODEBUG(dodebug(0, "SetupZT1428VXI", "MAX-TIME set to [%f]", MaxTime, (char*) NULL));

	if(Status)
	{
		ErrorZT1428VXI(Status, Response, BufferSize);	
	}

	if(m_TrigInfo.TrigExists)// (m_dTrigLevel.Exists)
	{
		if(strcmp(m_TrigInfo.TrigPort, "CHA")==0)
		{
			trigport=ZT1428_TRG_CHAN1;
		}
		else if(strcmp(m_TrigInfo.TrigPort, "CHB")==0)
		{
			trigport=ZT1428_TRG_CHAN2;
		}
		else if(strcmp(m_TrigInfo.TrigPort, "EXT")==0)
		{
			trigport=ZT1428_TRG_EXT;
		}
		else
		{
			trigport=m_nChannel.Int;
		}

		IFNSIM(Status=zt1428_edge_trigger(m_Handle, trigport, m_TrigInfo.TrigLevel, m_TrigInfo.TrigSlopePos, ZT1428_TRG_SENS_NORM));
		ISDODEBUG(dodebug(0, "SetupZT1428VXI", "(Status=zt1428_edge_trigger()",Status, (char*) NULL));
		
		if(Status)
		{
			ErrorZT1428VXI(Status, Response, BufferSize);
		}
	}

	// AC Signal
	{
		// Freq measurement
		if (m_nMeasChar.Int == 1)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Frequency() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			Setup_Frequency(m_nMeasChar.Int, Response, BufferSize);
		}
		// Period measurement
		else if (m_nMeasChar.Int == 2)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Period() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Period() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Period(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-PP measurement
		else if (m_nMeasChar.Int == 3)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_pp() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI","scope debug: Entering Setup_Voltage_pp() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_pp(m_nMeasChar.Int, Response, BufferSize);
		}
		// Av-Voltage measurement
		else if (m_nMeasChar.Int == 4)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Av_Voltage() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI","scope debug: Entering Setup_Av_Voltage() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Av_Voltage(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P measurement
		// New
		else if (m_nMeasChar.Int == 5)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI","scope debug: Entering Setup_Voltage_p() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage measurement
		else if (m_nMeasChar.Int == 6)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_AC(m_nMeasChar.Int, Response, BufferSize);
		}
	}

	// DC SIGNAL
	{
		// Voltage measurement
		if (m_nMeasChar.Int == 7)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_DC() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_DC() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_DC(m_nMeasChar.Int, Response, BufferSize);
		}
	}

	// SQUARE WAVE
	{
		if (m_dAmplitude.Int == STD_PERIODIC_SQUAREWAVE)
		{
			if (m_dPeriod.Exists)
			{
				m_dFrequency.Exists = true;

				if (m_dPeriod.Real != 0)
				{
					m_dFrequency.Real = 1/m_dPeriod.Real;
				}
			}
		}
		// Voltage-PP measurement
		if (m_nMeasChar.Int == 32)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_pp() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_pp() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_pp(m_nMeasChar.Int, Response, BufferSize);
		}
		// Freq measurement
		else if (m_nMeasChar.Int == 33)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Frequency() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Frequency() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Frequency(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Rise-Time measurement
		else if (m_nMeasChar.Int == 34)
		{
			sprintf(sTemp, "scope debug: Entering Setup_RiseTime() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI","scope debug: Entering Setup_RiseTime() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_RiseTime(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Fall-Time measurement
		else if (m_nMeasChar.Int == 35)
		{
			sprintf(sTemp, "scope debug: Entering Setup_FallTime() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI",  "scope debug: Entering Setup_FallTime() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_FallTime(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Duty-cycle measurement
		else if (m_nMeasChar.Int == 36)
		{
			sprintf(sTemp, "scope debug: Entering Setup_DutyCycle() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI",  "scope debug: Entering Setup_DutyCycle() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_DutyCycle(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P measurement
		// New
		else if (m_nMeasChar.Int == 37)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI","scope debug: Entering Setup_Voltage_p() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P-Pos measurement
		// New
		else if (m_nMeasChar.Int == 38)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p_pos() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			Setup_Voltage_p_pos(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P-Neg measurement
		// New
		else if (m_nMeasChar.Int == 39)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p_neg() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_p_neg() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p_neg(m_nMeasChar.Int, Response, BufferSize);
		}
		// Period measurement
		// New
		else if (m_nMeasChar.Int == 40)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Period() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Period() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Period(m_nMeasChar.Int, Response, BufferSize);
		}
	}

	// RAMP SIGNAL
	{
		if (m_dAmplitude.Int == STD_PERIODIC_RAMP)
		{
			if (m_dPeriod.Exists)
			{
				m_dFrequency.Exists = true;

				if (m_dPeriod.Real != 0)
				{
					m_dFrequency.Real = 1/m_dPeriod.Real;
				}
			}
		}

		// Voltage-PP measurement
		if (m_nMeasChar.Int == 22)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_pp() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_pp() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_pp(m_nMeasChar.Int, Response, BufferSize);
		}
		// Freq measurement
		else if (m_nMeasChar.Int == 23)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Frequency() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI",  "scope debug: Entering Setup_Frequency() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Frequency(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Rise-Time measurement
		else if (m_nMeasChar.Int == 24)
		{
			sprintf(sTemp, "scope debug: Entering Setup_RiseTime() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_RiseTime() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_RiseTime(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Fall-Time measurement
		else if (m_nMeasChar.Int == 25)
		{
			sprintf(sTemp, "scope debug: Entering Setup_FallTime() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI","scope debug: Entering Setup_FallTime() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_FallTime(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P measurement
		// New
		else if (m_nMeasChar.Int == 26)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_p() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P-Pos measurement
		// New
		else if (m_nMeasChar.Int == 27)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p_pos() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_p_pos() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p_pos(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P-Neg measurement
		// New
		else if (m_nMeasChar.Int == 28)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p_neg() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_p_neg() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p_neg(m_nMeasChar.Int, Response, BufferSize);
		}
		// Period measurement
		// New
		else if (m_nMeasChar.Int == 29)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Period() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Period() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Period(m_nMeasChar.Int, Response, BufferSize);
		}
	}

	// TRIANGULAR WAVE SIGNAL
	{
		if (m_dAmplitude.Int == STD_PERIODIC_TRIANGLE)
		{
			if (m_dPeriod.Exists)
			{
				m_dFrequency.Exists = true;

				if (m_dPeriod.Real != 0)
				{
					m_dFrequency.Real = 1/m_dPeriod.Real;
				}
			}
		}

		// Voltage-PP measurement
		if (m_nMeasChar.Int == 42)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_pp() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_pp() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_pp(m_nMeasChar.Int, Response, BufferSize);
		}
		// Freq measurement
		else if (m_nMeasChar.Int == 43)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Frequency() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Frequency() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Frequency(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Rise-Time measurement
		else if (m_nMeasChar.Int == 44)
		{
			sprintf(sTemp, "scope debug: Entering Setup_RiseTime() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_RiseTime() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_RiseTime(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Fall-Time measurement
		else if (m_nMeasChar.Int == 45)
		{
			sprintf(sTemp, "scope debug: Entering Setup_FallTime() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_FallTime() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_FallTime(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Duty-cycle measurement
		else if (m_nMeasChar.Int == 46)
		{
			sprintf(sTemp, "scope debug: Entering Setup_DutyCycle() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI",  "scope debug: Entering Setup_DutyCycle() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_DutyCycle(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P measurement
		// New
		else if (m_nMeasChar.Int == 47)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_p() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P-Pos measurement
		// New
		else if (m_nMeasChar.Int == 48)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p_pos() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_p_pos() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p_pos(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P-Neg measurement
		// New
		else if (m_nMeasChar.Int == 49)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p_neg() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_p_neg() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p_neg(m_nMeasChar.Int, Response, BufferSize);
		}
		// Period measurement
		// New
		else if (m_nMeasChar.Int == 50)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Period() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "IssueDriverFunctionCallZT1428VXI", "scope debug: Entering Setup_Period() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Period(m_nMeasChar.Int, Response, BufferSize);
		}
	}

	// PULSED DC
	{
		if (m_dAmplitude.Int == STD_PERIODIC_TRAPEZOID)
		{
			if (m_dPeriod.Exists)
			{
				m_dFrequency.Exists = true;

				if (m_dPeriod.Real != 0)
				{
					m_dFrequency.Real = 1/m_dPeriod.Real;
				}
			}
		}

		// Voltage-P-Pos measurement
		// New
		if (m_nMeasChar.Int == 8)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p_pos() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_p_pos() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p_pos(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-PP measurement
		else if (m_nMeasChar.Int == 9)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_pp() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_pp() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_pp(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P-Neg measurement
		// New
		else if (m_nMeasChar.Int == 10)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p_neg() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Voltage_p_neg() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p_neg(m_nMeasChar.Int, Response, BufferSize);
		}
		// Prf measurement
		else if (m_nMeasChar.Int == 11)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Frequency() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Frequency() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Frequency(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Rise-Time measurement
		else if (m_nMeasChar.Int == 12)
		{
			sprintf(sTemp, "scope debug: Entering Setup_RiseTime() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_RiseTime() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_RiseTime(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Fall-Time measurement
		else if (m_nMeasChar.Int == 13)
		{
			sprintf(sTemp, "scope debug: Entering Setup_FallTime() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI",  "scope debug: Entering Setup_FallTime() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_FallTime(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Pulse-width measurement
		else if (m_nMeasChar.Int == 14)
		{
			sprintf(sTemp, "scope debug: Entering Setup_PulseWidth() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_PulseWidth() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_PulseWidth(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Duty-cycle measurement
		else if (m_nMeasChar.Int == 15)
		{
			sprintf(sTemp, "scope debug: Entering Setup_DutyCycle() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_DutyCycle() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_DutyCycle(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Overshoot measurement
		else if (m_nMeasChar.Int == 16)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Preshoot() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Preshoot() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Preshoot(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Preshoot measurement
		else if (m_nMeasChar.Int == 17)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Overshoot() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "IssueDriverFunctionCallZT1428VXI", "scope debug: Entering Setup_Overshoot() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Overshoot(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Neg-Pulse-Width measurement
		else if (m_nMeasChar.Int == 18)
		{
			sprintf(sTemp, "scope debug: Entering Setup_NegPulseWidth() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_NegPulseWidth() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_NegPulseWidth(m_nMeasChar.Int, Response, BufferSize);
		}
		//  Pos-Pulse-Width measurement
		else if (m_nMeasChar.Int == 19)
		{
			sprintf(sTemp, "scope debug: Entering Setup_PosPulseWidth() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_PosPulseWidth() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_PosPulseWidth(m_nMeasChar.Int, Response, BufferSize);
		}
		// Voltage-P measurement
		// New
		else if (m_nMeasChar.Int == 20)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Voltage_p() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "IssueDriverFunctionCallZT1428VXI", "scope debug: Entering Setup_Voltage_p() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Voltage_p(m_nMeasChar.Int, Response, BufferSize);
		}
		// Period measurement
		// New
		else if (m_nMeasChar.Int == 21)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Period() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Period() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Period(m_nMeasChar.Int, Response, BufferSize);
		}
	}

	//time-interval
	{
		if(m_nMeasChar.Int==63) //time-interval
		{
			sprintf(sTemp, "scope debug: Entering Setup_TimeInterval() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_TimeInterval() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_TimeInterval(m_nMeasChar.Int, Response, BufferSize);
		}		
	}

	//waveform
	{
		// Sample Waveform measurement
		if (m_nMeasChar.Int == 60)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Sample() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "SetupZT1428VXI", "scope debug: Entering Setup_Sample() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Sample(m_nMeasChar.Int, Response, BufferSize);
		}
		// Save Waveform measurement
		else if (m_nMeasChar.Int == 52)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Save() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "IssueDriverFunctionCallZT1428VXI", "scope debug: Entering Setup_Save() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Save(m_nMeasChar.Int, Response, BufferSize);
		}
		// Load Waveform measurement
		else if (m_nMeasChar.Int == 53)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Load() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "IssueDriverFunctionCallZT1428VXI", "scope debug: Entering Setup_Load() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Load(m_nMeasChar.Int, Response, BufferSize);
		}
		// Compare Waveform measurement
		else if (m_nMeasChar.Int == 54)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Compare() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "IssueDriverFunctionCallZT1428VXI", "scope debug: Entering Setup_Compare() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Compare(m_nMeasChar.Int, Response, BufferSize);
		}
		// Math Waveform measurement
		else if (m_nMeasChar.Int == 55)
		{
			sprintf(sTemp, "scope debug: Entering Setup_Math() fnc %d)", m_nMeasChar.Int);
			ATXMLW_DEBUG(9,atxmlw_FmtMsg(sTemp), Response, BufferSize);
			ISDODEBUG(dodebug(0, "IssueDriverFunctionCallZT1428VXI", "scope debug: Entering Setup_Math() fnc %d)", m_nMeasChar.Int, (char*) NULL));
			Setup_Math(m_nMeasChar.Int, Response, BufferSize);
		}
	}

    if(Status)
	{
        ErrorZT1428VXI(Status, Response, BufferSize);
	}
	
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallZT1428VXI", "Leaving function", (char*) NULL));
    
	return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchZT1428VXI
//
// Purpose: Perform the Fetch action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::FetchZT1428VXI(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    ViStatus Status = 0;
	double   MeasValue = 0.0;
    double   MaxTime = 5000;
    char *   ErrMsg = NULL;
	int      source = 1;
	int      measurement = 6;
	char     Unit[5] = "V";
	ViChar	 readBuffer[1024];
	ViInt32	 BytesRead = 25;
	double   measurementValue = 0.0;
	int      time1 = 1, time2 = 2;
	int      digComplete = 0, msec = 0;
	
	ISDODEBUG(dodebug(0, "FetchZT1428VXI", "Leaving function", (char*) NULL));
	memset(readBuffer, '\0', sizeof(readBuffer));

    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
	{
        MaxTime = m_MaxTime.Real * 1000;   
	}
	else
	{
		MaxTime = 10 * 1000;
	}

    // Fetch action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchZT1428VXI called "), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "FetchZT1428VXI", "Response [%s]",Response, (char*) NULL));
	
	if(m_TrigInfo.TrigExists)
	{
		
		while(!digComplete && msec <= MaxTime)
		{
			IFNSIM(Status=zt1428_dig_complete(m_Handle, &digComplete));
		
			if(Status)
			{
				ErrorZT1428VXI(Status, Response, BufferSize);
			}

			Sleep(100);
			msec+=100;
		}

		// PCR 10030
		time(&m_StopTime);

		if(msec > MaxTime && !m_Sim)
		{
			ATXMLW_ERROR(Status, "DSO Error", "No trigger found", Response, BufferSize);
			atxmlw_ScalerDoubleReturn(m_nMeasChar.Text, "V", 0,
                             Response, BufferSize);
			return 0;
		}
	}

	// PCR 10030
	//if (difftime(m_StopTime, m_StartTime) > MaxTime)
	if (difftime(m_StopTime, m_StartTime) > MaxTime)
	{
		m_TimeOut = true;
		IFNSIM(atxmlw_ScalerIntegerReturn("timeout", "", 1, Response, BufferSize));
		
		return 0;
	}

	if(m_nMeasChar.Int==60) //waveform measurment
	{
		send_digital_data(m_nChannel.Int, Response, BufferSize);
	
		return 0;
	}
	else if(m_nMeasChar.Int==63) //time
	{
		if(strcmp(m_GateInfo.GateStartPort, "CHB")==0)
		{
			time1=2;
		}

		if(strcmp(m_GateInfo.GateStopPort, "CHA")==0)
		{
			time2=1;
		}

		IFNSIM(Status=zt1428_measurement(m_Handle, time1, time2, ZT1428_MEAS_DEL, &measurementValue));
		strcpy(Unit, "s");			
	}
	else if(m_nMeasChar.Int==52)  //save
	{
		if(m_sSaveTo.Exists)
		{
			if(strcmp(m_sSaveTo.Text, "WMEM1")==0)
			{
				source=ZT1428_WMEM1;
			}
			else if(strcmp(m_sSaveTo.Text, "WMEM2")==0)
			{
				source=ZT1428_WMEM2;
			}
			else if(strcmp(m_sSaveTo.Text, "WMEM3")==0)
			{
				source=ZT1428_WMEM3;
			}
			else if(strcmp(m_sSaveTo.Text, "WMEM4")==0)
			{
				source=ZT1428_WMEM4;
			}
		}
		else if(m_sSaveFrom.Exists)
		{
			if(strcmp(m_sSaveFrom.Text, "WMEM1")==0)
			{
				source=ZT1428_WMEM1;
			}
			else if(strcmp(m_sSaveFrom.Text, "WMEM2")==0)
			{
				source=ZT1428_WMEM2;
			}
			else if(strcmp(m_sSaveFrom.Text, "WMEM3")==0)
			{
				source=ZT1428_WMEM3;
			}
			else if(strcmp(m_sSaveFrom.Text, "WMEM4")==0)
			{
				source=ZT1428_WMEM4;
			}
			else if(strcmp(m_sSaveFrom.Text, "CHAN1")==0)
			{
				source=ZT1428_CHAN1;
			}
			else if(strcmp(m_sSaveFrom.Text, "CHAN2")==0)
			{
				source=ZT1428_CHAN2;
			}
			else if(strcmp(m_sSaveFrom.Text, "FUNC1")==0)
			{
				source=ZT1428_FUNC1;
			}
			else if(strcmp(m_sSaveFrom.Text, "FUNC2")==0)
			{
				source=ZT1428_FUNC2;
			}
		}
		else
		{
			source=m_nChannel.Int;
		}

		send_digital_data(source, Response, BufferSize);
		
		return 0;
	}
	else if(m_nMeasChar.Int==53)  //load
	{
		if(m_sLoadFrom.Exists)
		{
			if(strcmp(m_sLoadFrom.Text, "WMEM1")==0)
			{
				source=ZT1428_WMEM1;
			}
			else if(strcmp(m_sLoadFrom.Text, "WMEM2")==0)
			{
				source=ZT1428_WMEM2;
			}
			else if(strcmp(m_sLoadFrom.Text, "WMEM3")==0)
			{
				source=ZT1428_WMEM3;
			}
			else if(strcmp(m_sLoadFrom.Text, "WMEM4")==0)
			{
				source=ZT1428_WMEM4;
			}
			else if(strcmp(m_sLoadFrom.Text, "CHAN1")==0)
			{
				source=ZT1428_CHAN1;
			}
			else if(strcmp(m_sLoadFrom.Text, "CHAN2")==0)
			{
				source=ZT1428_CHAN2;
			}
			else if(strcmp(m_sLoadFrom.Text, "FUNC1")==0)
			{
				source=ZT1428_FUNC1;
			}
			else if(strcmp(m_sLoadFrom.Text, "FUNC2")==0)
			{
				source=ZT1428_FUNC2;
			}
		}
		else
		{
			source=ZT1428_WMEM1;
		}

		send_digital_data(source, Response, BufferSize);
		
		return 0;
	}
	else if(m_nMeasChar.Int==54) //compare
	{
		send_compare_data(Response, BufferSize);
	}
	else if(m_nMeasChar.Int==55) // math fncs
	{
		if(m_sDestination.Text)
		{
			if(strcmp(m_sDestination.Text, "FUNC1")==0)
			{
				source=ZT1428_FUNC1;
			}
			else if(strcmp(m_sDestination.Text, "FUNC2")==0)
			{
				source=ZT1428_FUNC2;
			}
		}
		else
		{
			source=ZT1428_FUNC1;
		}

		send_digital_data(source, Response, BufferSize);
		
		return 0;
	}
	else //regular fetch
	{
		switch(m_nMeasChar.Int)
		{
			case 6: //acs_volt
				measurement = ZT1428_MEAS_VACR;
				strcpy(Unit, "V");
				break;
					
			case 3:  //acs_vlpp
			case 9:  //pdc_vlpp
			case 32: //sqw_vlpp
			case 22: //rps_vlpp
			case 42: //tri_vlpp
				measurement = ZT1428_MEAS_VPP;
				strcpy(Unit, "V");
				break;

			case 1:  //acs_freq
			case 11: //pdc_prfr
			case 33: //sqw_freq
			case 23: //rps_freq
			case 43: //tri_freq
				measurement=ZT1428_MEAS_FREQ;
				strcpy(Unit, "Hz");
				break;

			case 2:  //acs_peri
			case 21: //pdc_peri
			case 40: //sqw_peri
			case 29: //rps_peri
			case 50: //tri_peri
				measurement=ZT1428_MEAS_PER;
				strcpy(Unit, "s");
				break;

			case 7: //dcs_volt
				//mh	measurement=ZT1428_MEAS_VDCR;
				// vdcr is rms of dc volt always give positive
				measurement = ZT1428_MEAS_VAVG;
				strcpy(Unit, "V");
				break;

			case 12: //pdc_rise
			case 34: //sqw_rise
			case 24: //rps_rise
			case 44: //tri_rise
				measurement=ZT1428_MEAS_RISE;
				strcpy(Unit, "s");
				break;

			case 13: //pdc_fall
			case 35: //sqw_fall
			case 25: //rps_fall
			case 45: //tri_fall
				measurement=ZT1428_MEAS_FALL;
				strcpy(Unit, "s");
				break;

			case 14: //pdc_plwd
			case 19: //pdc_ppwt
				measurement = ZT1428_MEAS_PWID;
				strcpy(Unit, "s");
				break;

			case 15: //pdc_duty
			case 36: //sqw_duty
			case 46: //tri_duty
				measurement = ZT1428_MEAS_DUTY;
				strcpy(Unit, "pc");
				break;

			case 5:  //acs_vlpk
			case 8:  //pdc_vpkp
			case 20: //pdc_vlpk
			case 26: //rps_vlpk
			case 27: //rps_vpkp
			case 37: //sqw_vlpk
			case 38: //sqw_vpkp
			case 47: //tri_vlpk
			case 48: //tri_vpkp
				measurement = ZT1428_MEAS_VMAX;
				strcpy(Unit, "V");
				break;

			case 10: //pdc_vpkn
			case 39: //sqw_vpkn
			case 28: //rps_vpkn
			case 49: //tri_vpkn
				measurement = ZT1428_MEAS_VMIN;
				strcpy(Unit, "V");
				break;

			case 18: //pdc_npwt
				measurement = ZT1428_MEAS_NWID;
				strcpy(Unit, "s");
				break;

			case 16: //overshoot
				measurement = ZT1428_MEAS_OVER;
				strcpy(Unit, "V");
				break;
			
			case 17: //preshoot
				measurement = ZT1428_MEAS_PRE;
				strcpy(Unit, "V");
				break;
			
			case 4:
				measurement = ZT1428_MEAS_VACR;
				strcpy(Unit, "V");
				break;
		}

		//fetch_scope
		IFNSIM(Status = zt1428_measurement(m_Handle, m_nChannel.Int, ZT1428_NONE, measurement, &measurementValue));

		if(Status)
		{
			ErrorZT1428VXI(Status, Response, BufferSize);
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("%x=zt1428_measurement(%x, %d, ZT1428_NONE, %d, %f)",Status,
			m_Handle, m_nChannel.Int, measurement, measurementValue), Response, BufferSize);
		ISDODEBUG(dodebug(0, "FetchZT1428VXI", "%x=zt1428_measurement(%x, %d, ZT1428_NONE, %d, %f)",Status,
			m_Handle, m_nChannel.Int, measurement, measurementValue, (char*) NULL));

		// ***********************************************************
		// This logic below will have to be removed in the near future
		// ***********************************************************
		if (measurementValue > 9.9e37)  // invalid reading, try autoscale 
		{
			IFNSIM(Status=zt1428_auto_setup(m_Handle));
			
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("%x=zt1428_auto_setup(%x)",Status,	m_Handle), Response, BufferSize);
			ISDODEBUG(dodebug(0, "FetchZT1428VXI","%x=zt1428_auto_setup(%x)",Status, m_Handle, (char*) NULL));
			
			if(Status)
			{
				ErrorZT1428VXI(Status, Response, BufferSize);
			}

			//fetch_scope
			IFNSIM(Status=zt1428_measurement(m_Handle, m_nChannel.Int, ZT1428_NONE, measurement, &measurementValue));
			
			if(Status)
			{
				ErrorZT1428VXI(Status, Response, BufferSize);
			}

			ATXMLW_DEBUG(5,atxmlw_FmtMsg("%x=zt1428_measurement(%x, %d, ZT1428_NONE, %d, %f)",Status,
				m_Handle, m_nChannel.Int, measurement, measurementValue), Response, BufferSize);

			ISDODEBUG(dodebug(0, "FetchZT1428VXI", "%x=zt1428_measurement(%x, %d, ZT1428_NONE, %d, %f)",Status,
				m_Handle, m_nChannel.Int, measurement, measurementValue, (char*) NULL));
		}

		switch(m_nMeasChar.Int) //some measurments require additional calculations
		{
			/*case 8: //pdc_vlpp
				measurementValue = measurementValue * 2; 
				break;*/
				
			case 5: //acs_vlpk
			case 37: //sqw_vlpk
			case 26: //rps_vlpk
			case 47: //tri_vlpk
				if (measurementValue != 0)
				{
					// what is this for ??  did we not measure peak voltage??
					//measurementValue = measurementValue / 2;
				}
				break;

			case 16: 
			case 17:
				// and why????
				measurementValue=measurementValue * 100; //done in legacy
				break;
		}
	}

	if(Status)
	{
		measurementValue = FLT_MAX;
		ErrorZT1428VXI(Status, Response, BufferSize);
	}
	//else
	//{
	//	sscanf(ErrMsg,"%E",&MeasValue);
	//}

	if(Response && (BufferSize > (int)(strlen(Response)+200)))
	{
		atxmlw_ScalerDoubleReturn(m_nMeasChar.Text, Unit, measurementValue, Response, BufferSize);
	}
	
	ISDODEBUG(dodebug(0, "FetchZT1428VXI", "Leaving function", (char*) NULL));
	
	return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: DisableZT1428VXI
//
// Purpose: Perform the Open action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::DisableZT1428VXI(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    // Open action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenZT1428VXI called "), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "DisableZT1428VXI", "Entering function", (char*) NULL));
	
	IFNSIM(Status=zt1428_run_stop(m_Handle, ZT1428_STOP));
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("zt1428_run_stop(%d, ZT1428_STOP)", m_Handle), Response, BufferSize);
	
	if(Status)
	{
		ErrorZT1428VXI(Status, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "DisableZT1428VXI", "Leaving function", (char*) NULL));
    
	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableZT1428VXI
//
// Purpose: Perform the Close action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::EnableZT1428VXI(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int functionNumber=ZT1428_FUNC1;
    int operation=ZT1428_FUNC_ADD; 
	int source1=ZT1428_CHAN1; 
	int source2=ZT1428_WMEM1;
	
	ISDODEBUG(dodebug(0, "EnableZT1428VXI", "Entering function", (char*) NULL));
    // Close action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableZT1428VXI called "), Response, BufferSize);

	// STR10030 Process MAX-TIME info
	if (m_MeasInfo.TimeOut > 0.0)
	{

	}

	if(m_nMeasChar.Int > 52 && m_nMeasChar.Int <= 55) //waveform and math fncs
	{
		if(m_sCompareCh.Exists)
		{
			if(strcmp(m_sCompareCh.Text,"CHAN2")==0)
			{
				source1=ZT1428_CHAN2;
			}

			if(m_sCompareTo.Exists)
			{
				if(strcmp(m_sCompareCh.Text,"WMEM2")==0)
				{
					source2=ZT1428_WMEM2;
				}
				else if(strcmp(m_sCompareCh.Text,"WMEM3")==0)
				{
					source2=ZT1428_WMEM3;
				}
				else if(strcmp(m_sCompareCh.Text,"WMEM4")==0)
				{
					source2=ZT1428_WMEM4;
				}
			}

			operation=ZT1428_FUNC_SUB;
		}

		if(m_sMultiplyFrom.Exists)
		{
			if(strcmp(m_sMultiplyFrom.Text,"CHAN2")==0)
			{
				source1=ZT1428_CHAN2;
			}
			else if(strcmp(m_sMultiplyFrom.Text,"WMEM1")==0)
			{
				source1=ZT1428_WMEM1;
			}
			else if(strcmp(m_sMultiplyFrom.Text,"WMEM2")==0)
			{
				source1=ZT1428_WMEM2;
			}
			else if(strcmp(m_sMultiplyFrom.Text,"WMEM3")==0)
			{
				source1=ZT1428_WMEM3;
			}
			else if(strcmp(m_sMultiplyFrom.Text,"WMEM4")==0)
			{
				source1=ZT1428_WMEM4;
			}
			if(strcmp(m_sMultiplyTo.Text,"CHAN1")==0)
			{
				source2=ZT1428_CHAN1;
			}
			else if(strcmp(m_sMultiplyTo.Text,"CHAN2")==0)
			{
				source2=ZT1428_CHAN2;
			}
			else if(strcmp(m_sMultiplyTo.Text,"WMEM2")==0)
			{
				source2=ZT1428_WMEM2;
			}
			else if(strcmp(m_sMultiplyTo.Text,"WMEM3")==0)
			{
				source2=ZT1428_WMEM3;
			}
			else if(strcmp(m_sMultiplyTo.Text,"WMEM4")==0)
			{
				source2=ZT1428_WMEM4;
			}

			operation=ZT1428_FUNC_MULT;
			
			if(strcmp(m_sDestination.Text,"FUNC2")==0)
			{
				functionNumber=ZT1428_FUNC2;
			}
		}

		if(m_sAddFrom.Exists)
		{
			if(strcmp(m_sAddFrom.Text,"CHAN2")==0)
				source1=ZT1428_CHAN2;
			else if(strcmp(m_sAddFrom.Text,"WMEM1")==0)
				source1=ZT1428_WMEM1;
			else if(strcmp(m_sAddFrom.Text,"WMEM2")==0)
				source1=ZT1428_WMEM2;
			else if(strcmp(m_sAddFrom.Text,"WMEM3")==0)
				source1=ZT1428_WMEM3;
			else if(strcmp(m_sAddFrom.Text,"WMEM4")==0)
				source1=ZT1428_WMEM4;
			if(strcmp(m_sAddTo.Text,"CHAN1")==0)
				source2=ZT1428_CHAN1;
			else if(strcmp(m_sAddTo.Text,"CHAN2")==0)
				source2=ZT1428_CHAN2;
			else if(strcmp(m_sAddTo.Text,"WMEM2")==0)
				source2=ZT1428_WMEM2;
			else if(strcmp(m_sAddTo.Text,"WMEM3")==0)
				source2=ZT1428_WMEM3;
			else if(strcmp(m_sAddTo.Text,"WMEM4")==0)
				source2=ZT1428_WMEM4;

			operation=ZT1428_FUNC_ADD;

			if(strcmp(m_sDestination.Text,"FUNC2")==0)
				functionNumber=ZT1428_FUNC2;
		}

		if(m_sSubtractFrom.Exists)
		{
			if(strcmp(m_sSubtractFrom.Text,"CHAN2")==0)
				source1=ZT1428_CHAN2;
			else if(strcmp(m_sSubtractFrom.Text,"WMEM1")==0)
				source1=ZT1428_WMEM1;
			else if(strcmp(m_sSubtractFrom.Text,"WMEM2")==0)
				source1=ZT1428_WMEM2;
			else if(strcmp(m_sSubtractFrom.Text,"WMEM3")==0)
				source1=ZT1428_WMEM3;
			else if(strcmp(m_sSubtractFrom.Text,"WMEM4")==0)
				source1=ZT1428_WMEM4;
			if(strcmp(m_sSubtractTo.Text,"CHAN1")==0)
				source2=ZT1428_CHAN1;
			else if(strcmp(m_sSubtractTo.Text,"CHAN2")==0)
				source2=ZT1428_CHAN2;
			else if(strcmp(m_sSubtractTo.Text,"WMEM2")==0)
				source2=ZT1428_WMEM2;
			else if(strcmp(m_sSubtractTo.Text,"WMEM3")==0)
				source2=ZT1428_WMEM3;
			else if(strcmp(m_sSubtractTo.Text,"WMEM4")==0)
				source2=ZT1428_WMEM4;

			operation=ZT1428_FUNC_SUB;

			if(strcmp(m_sDestination.Text,"FUNC2")==0)
				functionNumber=ZT1428_FUNC2;
		}

		if(m_sDifferentiate.Exists)
		{
			if(strcmp(m_sDifferentiate.Text,"CHAN2")==0)
				source1=ZT1428_CHAN2;
			else if(strcmp(m_sDifferentiate.Text,"WMEM1")==0)
				source1=ZT1428_WMEM1;
			else if(strcmp(m_sDifferentiate.Text,"WMEM2")==0)
				source1=ZT1428_WMEM2;
			else if(strcmp(m_sDifferentiate.Text,"WMEM3")==0)
				source1=ZT1428_WMEM3;
			else if(strcmp(m_sDifferentiate.Text,"WMEM4")==0)
				source1=ZT1428_WMEM4;
			
			operation=ZT1428_FUNC_DIFF;

			if(strcmp(m_sDestination.Text,"FUNC2")==0)
				functionNumber=ZT1428_FUNC2;
		}

		if(m_sIntegrate.Exists)
		{
			if(strcmp(m_sIntegrate.Text,"CHAN2")==0)
				source1=ZT1428_CHAN2;
			else if(strcmp(m_sIntegrate.Text,"WMEM1")==0)
				source1=ZT1428_WMEM1;
			else if(strcmp(m_sIntegrate.Text,"WMEM2")==0)
				source1=ZT1428_WMEM2;
			else if(strcmp(m_sIntegrate.Text,"WMEM3")==0)
				source1=ZT1428_WMEM3;
			else if(strcmp(m_sIntegrate.Text,"WMEM4")==0)
				source1=ZT1428_WMEM4;
			
			operation=ZT1428_FUNC_INT;

			if(strcmp(m_sDestination.Text,"FUNC2")==0)
				functionNumber=ZT1428_FUNC2;
		}
		
		if(m_nMeasChar.Int!=53)
		{
			IFNSIM(Status=zt1428_function(m_Handle, functionNumber, operation, source1, source2, ZT1428_FUNC_ON, 0, 0));
			
			if(Status)
			{
				ErrorZT1428VXI(Status, Response, BufferSize);
			}
		}

		init_scope(Response, BufferSize);
	}
	else if(m_nMeasChar.Int==52)//save-wave
	{
		save_waveform(Response, BufferSize);
	}
	else 
	{
		init_scope(Response, BufferSize);
	}
    
	ISDODEBUG(dodebug(0, "EnableZT1428VXI", "Leaving function", (char*) NULL));
  
	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorZT1428VXI
//
// Purpose: Query ZT1428VXI for the error text and send to WRTS
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Status           int             Error code returned from driver
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int  CZT1428VXI_T::ErrorZT1428VXI(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      retval;
    int      Err = 0;
    char     Msg[MAX_MSG_SIZE];
	ViInt32	 BytesRead = 25;
	ViChar   errorMessage[512] = "";
	int      errorCode = 0;
	char     hp1428_string[256]="";

    retval = Status;
    Msg[0] = '\0';
	
	ISDODEBUG(dodebug(0, "ErrorZT1428VXI", "Entering function", (char*) NULL));

    if(Status)
    {
		IFNSIM(Err = zt1428_error(m_Handle, &errorCode));
		ATXMLW_DEBUG(9,atxmlw_FmtMsg("zt1428_error(%d, %x)",m_Handle, errorCode), Response, BufferSize); 
	
		ISDODEBUG(dodebug(0, "ErrorZT1428VXI", "zt1428_error(%d, %x)",m_Handle, errorCode, (char*) NULL));
        
		if(Err)
		{
            sprintf(Msg,"Plug 'n' Play Error: Unable to get error text [%X]!", Err);
		}

		// Timeout occurred before operation could complete
        if(Status == 0xBFFF0015)
		{
            Status = ATXMLW_WRAPPER_ERRCD_MAX_TIME;
			m_TimeOut = true;
		}

		if (errorCode == -303)
		{
			Status = ATXMLW_WRAPPER_ERRCD_MAX_TIME;
			m_TimeOut = true;
		}
	}
		
    IFNSIM(errorCode = 1);

    // Retrieve any pending errors in the device
    while(errorCode)
    {
		IFNSIM(Err = zt1428_error(m_Handle, &errorCode));
		ATXMLW_DEBUG(9,atxmlw_FmtMsg("zt1428_error(%d, %x)",m_Handle, errorCode), Response, BufferSize); 
	
		ISDODEBUG(dodebug(0, "ErrorZT1428VXI","zt1428_error(%d, %x)",m_Handle, errorCode, (char*) NULL));

        if(Err != 0)
		{
            break;
		}

		if (errorCode == -303)
		{
			m_TimeOut = true;
		}

        if(errorCode < 0)
		{
			ATXMLW_ERROR(Status, "DSO 1428 Error", errorMessage, Response, BufferSize);
		}
		else if (errorCode > 0)
		{
			ATXMLW_ERROR(Status, "DSO 1428 Warning", errorMessage, Response, BufferSize);
		}
    }
	
	
	ISDODEBUG(dodebug(0, "ErrorZT1428VXI", "Leaving function", (char*) NULL));
    
	return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoZT1428VXI
//
// Purpose: Get the Modifier values from the ATLAS Statement
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CZT1428VXI_T::GetStmtInfoZT1428VXI(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int    Status = 0;
	char   Name[ATXMLW_MAX_NAME];
	char  *NextName;
	char   Element[ATXMLW_MAX_NAME];
	bool   RetVal = true;
	char  *InputNames;
	
	ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "Entering function", (char*) NULL));

    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription, Response, BufferSize)))
	{
         return(Status);
	}

    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription, Response, BufferSize);
    
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action), Response, BufferSize);
	ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found Action %d", m_Action, (char*) NULL));

    if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement, (char*) NULL));
	}

	strncpy(Name, m_SignalName, ATXMLW_MAX_NAME);

	if(atxmlw_Get1641SignalIn(m_SignalDescription, &InputNames))
    {
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, InputNames), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found %s In [%s]", Name, InputNames, (char*) NULL));
        //------ Possibly validate InputNames --------
    }

	if (m_Action == ATXMLW_SA_RESET)
	{
		RetVal = 0; //execute reset
	}

	//  Get Measurement Type
	if((atxmlw_Get1641StdMeasChar(m_SignalDescription, Name, &m_MeasInfo)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s MeasChar %d", Name, m_MeasInfo.StdType),	Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found %s MeasChar %d", Name, m_MeasInfo.StdType, (char*) NULL));
		
		m_MaxTime.Exists=true;
		m_MaxTime.Real=m_MeasInfo.TimeOut;

		if(m_MaxTime.Real == 0.0)
		{
			m_MaxTime.Real = 10.0;
		}
	}

	//  Get Trigger Info from SYNC on Measure
	if(ISSTRATTR("Sync",&NextName))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Sync [%s]", Name, NextName), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found %s Sync [%s]", Name, NextName, (char*) NULL));
		
		if((atxmlw_Get1641StdTrigChar(m_SignalDescription, NextName, InputNames,  &m_TrigInfo)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Trig True for [%s]", NextName), Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found Trig True for [%s]", NextName, (char*) NULL));
		}
	}

	//  Get Gate Info from GATE on Measure
	if(ISSTRATTR("Gate",&NextName))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Gate [%s]", Name, NextName), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found %s Gate [%s]", Name, NextName, (char*) NULL));
		
		m_GateInfo.GateStartDelay=0;
		
		if((atxmlw_Get1641StdGateChar(m_SignalDescription, NextName, InputNames, &m_GateInfo)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Gate True for [%s]", NextName), Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found Gate True for [%s]", NextName, (char*) NULL));
		}
	}

	// Get Signal Characteristics from As chain
	if(ISSTRATTR("As",&NextName))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]", Name, NextName), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found %s As [%s]", Name, NextName, (char*) NULL));

		if((GetSignalChar(NextName, Response, BufferSize)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", NextName), Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found Signal Chars for [%s]", NextName, (char*) NULL));
		}
	}

	// Get Signal Conditioning from the In chain
	if(ISSTRATTR("In",&NextName))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]", Name, NextName), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found %s As [%s]", Name, NextName, (char*) NULL));

		if((GetSignalCond(NextName, InputNames, Response, BufferSize)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", NextName), Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "WrapGSI-Found Signal Chars for [%s]", NextName, (char*) NULL));
		}
	}

	if((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	{
		char* sTemp;

		if(ISELEMENT("Measure"))
		{
			if(ISDBLATTR("samples", &(m_nSampleCount.Real), (char*)m_nSampleCount.Dim))
			{
				m_nSampleCount.Exists = true;
				m_nSampleCount.Int = (int)(m_nSampleCount.Real + 0.5);
			}
	
			if(ISSTRATTR("saveTo", &sTemp))
			{
				m_sSaveTo.Exists = true;
				sprintf(m_sSaveTo.Text, sTemp);
			}
		
			if(ISSTRATTR("saveFrom", &sTemp))
			{
				m_sSaveFrom.Exists = true;
				sprintf(m_sSaveFrom.Text, sTemp);
			}
			
			if(ISSTRATTR("loadFrom", &sTemp))
			{
				m_sLoadFrom.Exists = true;
				sprintf(m_sLoadFrom.Text, sTemp);
			}
			
			if(ISSTRATTR("compareCh", &sTemp))
			{
				m_sCompareCh.Exists = true;
				sprintf(m_sCompareCh.Text, sTemp);
			}
			
			if(ISSTRATTR("compareTo", &sTemp))
			{
				m_sCompareTo.Exists = true;
				sprintf(m_sCompareTo.Text, sTemp);
			}
		
			if(ISSTRATTR("multiplyFrom", &sTemp))
			{
				m_sMultiplyFrom.Exists = true;
				sprintf(m_sMultiplyFrom.Text, sTemp);
			}
			
			if(ISSTRATTR("multiplyTo", &sTemp))
			{
				m_sMultiplyTo.Exists = true;
				sprintf(m_sMultiplyTo.Text, sTemp);
			}
			
			if(ISSTRATTR("addFrom", &sTemp))
			{
				m_sAddFrom.Exists = true;
				sprintf(m_sAddFrom.Text, sTemp);
			}
			
			if(ISSTRATTR("addTo", &sTemp))
			{
				m_sAddTo.Exists = true;
				sprintf(m_sAddTo.Text, sTemp);
			}
			
			if(ISSTRATTR("subtractFrom", &sTemp))
			{
				m_sSubtractFrom.Exists = true;
				sprintf(m_sSubtractFrom.Text, sTemp);
			}
			
			if(ISSTRATTR("subtractTo", &sTemp))
			{
				m_sSubtractTo.Exists = true;
				sprintf(m_sSubtractTo.Text, sTemp);
			}
			
			if(ISSTRATTR("differentiate", &sTemp))
			{
				m_sDifferentiate.Exists = true;
				sprintf(m_sDifferentiate.Text, sTemp);
			}
			
			if(ISSTRATTR("integrate", &sTemp))
			{
				m_sIntegrate.Exists = true;
				sprintf(m_sIntegrate.Text, sTemp);
			}
			
			if(ISSTRATTR("destination", &sTemp))
			{
				m_sDestination.Exists = true;
				sprintf(m_sDestination.Text, sTemp);
			}
			
			if(ISDBLATTR("allowance", &(m_nAllowance.Real), (char*)m_nAllowance.Dim))
			{
				m_nAllowance.Exists = true;
				m_nAllowance.Int = (int)(m_nSampleCount.Real);
				sprintf(m_nAllowance.Text, sTemp);
			}
			
			if (ISSTRATTR("attribute", &sTemp))
			{
    			sprintf(m_nMeasChar.Text, sTemp);
				m_nMeasChar.Exists = true;

				// AC SIGNAL
				if (strcmp(sTemp, "acs_volt")==0)
				{
					m_nMeasChar.Int = 6;
				}
				else if (strcmp(sTemp, "acs_vlpk")==0)
				{
					m_nMeasChar.Int = 5;
				}
				else if (strcmp(sTemp, "acs_vlpp")==0)
				{
					m_nMeasChar.Int = 3;
				}
				else if (strcmp(sTemp, "acs_freq")==0)
				{
					m_nMeasChar.Int = 1;
				}
				else if (strcmp(sTemp, "acs_peri")==0)
				{
					m_nMeasChar.Int = 2;
				}
				else if (strcmp(sTemp, "acs_valv")==0)
				{
					m_nMeasChar.Int = 4;
				}
				// DC SIGNAL
				else if (strcmp(sTemp, "dcs_volt")==0)
				{
					m_nMeasChar.Int = 7;
				}
				// PULSED DC
				else if (strcmp(sTemp, "pdc_vlpk")==0)
					m_nMeasChar.Int = 20;
				else if (strcmp(sTemp, "pdc_prfr")==0)
					m_nMeasChar.Int = 11;
				else if (strcmp(sTemp, "pdc_peri")==0)
					m_nMeasChar.Int = 21;
				else if (strcmp(sTemp, "pdc_rise")==0)
					m_nMeasChar.Int = 12;
				else if (strcmp(sTemp, "pdc_fall")==0)
					m_nMeasChar.Int = 13;
				else if (strcmp(sTemp, "pdc_plwd")==0)
					m_nMeasChar.Int = 14;
				else if (strcmp(sTemp, "pdc_duty")==0)
					m_nMeasChar.Int = 15;
				else if (strcmp(sTemp, "pdc_psht")==0)
					m_nMeasChar.Int = 17;
				else if (strcmp(sTemp, "pdc_over")==0)
					m_nMeasChar.Int = 16;
				else if (strcmp(sTemp, "pdc_vpkp")==0)
					m_nMeasChar.Int = 8;
				else if (strcmp(sTemp, "pdc_vlpp")==0)
					m_nMeasChar.Int = 9;
				else if (strcmp(sTemp, "pdc_vpkn")==0)
					m_nMeasChar.Int = 10;
				else if (strcmp(sTemp, "pdc_ppwt")==0)
					m_nMeasChar.Int = 19;
				else if (strcmp(sTemp, "pdc_npwt")==0)
					m_nMeasChar.Int = 18;
				// SQUARE WAVE
				else if (strcmp(sTemp, "sqw_vlpk")==0)
					m_nMeasChar.Int = 37;
				else if (strcmp(sTemp, "sqw_vlpp")==0)
					m_nMeasChar.Int = 32;
				else if (strcmp(sTemp, "sqw_vpkp")==0)
					m_nMeasChar.Int = 38;
				else if (strcmp(sTemp, "sqw_vpkn")==0)
					m_nMeasChar.Int = 39;
				else if (strcmp(sTemp, "sqw_freq")==0)
					m_nMeasChar.Int = 33;
				else if (strcmp(sTemp, "sqw_peri")==0)
					m_nMeasChar.Int = 40;
				else if (strcmp(sTemp, "sqw_rise")==0)
					m_nMeasChar.Int = 34;
				else if (strcmp(sTemp, "sqw_fall")==0)
					m_nMeasChar.Int = 35;
				else if (strcmp(sTemp, "sqw_duty")==0)
					m_nMeasChar.Int = 36;
				// RAMP SIGNAL
				else if (strcmp(sTemp, "rps_vlpk")==0)
					m_nMeasChar.Int = 26;
				else if (strcmp(sTemp, "rps_vlpp")==0)
					m_nMeasChar.Int = 22;
				else if (strcmp(sTemp, "rps_vpkp")==0)
					m_nMeasChar.Int = 27;
				else if (strcmp(sTemp, "rps_vpkn")==0)
					m_nMeasChar.Int = 28;
				else if (strcmp(sTemp, "rps_freq")==0)
					m_nMeasChar.Int = 23;
				else if (strcmp(sTemp, "rps_peri")==0)
					m_nMeasChar.Int = 29;
				else if (strcmp(sTemp, "rps_rise")==0)
					m_nMeasChar.Int = 24;
				else if (strcmp(sTemp, "rps_fall")==0)
					m_nMeasChar.Int = 25;
				// TRIANGULAR WAVE SIGNAL
				else if (strcmp(sTemp, "tri_vlpk")==0)
					m_nMeasChar.Int = 47;
				else if (strcmp(sTemp, "tri_vlpp")==0)
					m_nMeasChar.Int = 42;
				else if (strcmp(sTemp, "tri_vpkp")==0)
					m_nMeasChar.Int = 48;
				else if (strcmp(sTemp, "tri_vpkn")==0)
					m_nMeasChar.Int = 49;
				else if (strcmp(sTemp, "tri_freq")==0)
					m_nMeasChar.Int = 43;
				else if (strcmp(sTemp, "tri_peri")==0)
					m_nMeasChar.Int = 50;
				else if (strcmp(sTemp, "tri_rise")==0)
					m_nMeasChar.Int = 44;
				else if (strcmp(sTemp, "tri_fall")==0)
					m_nMeasChar.Int = 45;
				else if (strcmp(sTemp, "tri_duty")==0)
					m_nMeasChar.Int = 46;
				// WAVEFORM
				else if (strcmp(sTemp, "wav_smpl")==0)
					m_nMeasChar.Int = 60;
				else if (strcmp(sTemp, "wav_svmv")==0)
					m_nMeasChar.Int = 52;
				else if (strcmp(sTemp, "wav_ldvm")==0)
					m_nMeasChar.Int = 53;
				else if (strcmp(sTemp, "wav_cmwv")==0)
					m_nMeasChar.Int = 54;
				else if (strcmp(sTemp, "wav_math")==0)
					m_nMeasChar.Int = 55;
				// TIME
				else if (strcmp(sTemp, "tmi_time")==0)
					m_nMeasChar.Int = 63;
				else
					// No match to known string
					m_nMeasChar.Exists = false;
			}
		}

		RetVal = 0;
	}
	if (m_Action == ATXMLW_SA_FETCH)
	{

		if((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
		{
			char* sTemp;

			if(ISELEMENT("Instantaneous"))
			{
				if (ISSTRATTR("attribute", &sTemp))
				{
					sprintf(m_nMeasChar.Text, sTemp);
				}
			}
		}
		RetVal = 0;
	}

    atxmlw_Close1641Xml(&m_SignalDescription);
	
	ISDODEBUG(dodebug(0, "GetStmtInfoZT1428VXI", "leaving function", (char*) NULL));
    
	return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateZT1428VXI
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::InitPrivateZT1428VXI(void)
{
	ISDODEBUG(dodebug(0, "InitPrivateZT1428VXI", "Entering function", (char*) NULL));
	m_dDc_offset.Real = 0.0;
	m_dAmplitude.Real = 0.0;
	m_dFrequency.Real = 0.0;
	m_dPeriod.Real = 1000;
	m_nAcCoupling.Int = 0;
	m_dBanwidthMin.Real = 0.0;
	m_dBanwidthMax.Real = 20.0e+9;
	m_nGain.Int = 1;
	m_nImpedance.Real = 1000000;
	m_nChannel.Int = 1;
	m_nSampleCount.Int = 1;
	m_nMeasChar.Int = 1;
	//
	m_dTrigLevel.Real = 0.0;
	m_nTrigSlope.Int = 1;
	m_nTrigChannel.Int = 1;
	//
	m_dGateStartLevel.Real = 0.0;
	m_dGateStartSlope.Real = 1;
	m_dGateStartChannel.Real = 1;
	m_dGateStopLevel.Real = 0.0;
	m_dGateStopSlope.Real = 1;
	m_dGateStopChannel.Real = 1;
	//
	m_dDc_offset.Exists = false;
	m_dAmplitude.Exists = false;
	m_dFrequency.Exists = false;
	m_MaxTime.Exists=false;
	m_dPeriod.Exists = false;
	m_nAcCoupling.Exists = false;
	m_dBanwidthMin.Exists = false;
	m_dBanwidthMax.Exists = false;
	m_nGain.Exists = false;
	m_nImpedance.Exists = false;
	m_nChannel.Exists = false;
	m_nSampleCount.Exists = false;
	m_nMeasChar.Exists = false;
	//
	m_dTrigLevel.Exists = false;
	m_nTrigSlope.Exists= false;
	m_nTrigChannel.Exists = false;
	m_dGateStartLevel.Exists = false;
	m_dGateStartSlope.Exists = false;
	m_dGateStartChannel.Exists = false;
	m_dGateStopLevel.Exists = false;
	m_dGateStopSlope.Exists = false;
	m_dGateStopChannel.Exists = false;
	m_sSaveTo.Exists = false;
	m_sSaveFrom.Exists = false;
	m_sLoadFrom.Exists = false;
	m_sCompareCh.Exists = false;
	m_sCompareTo.Exists = false;
	m_nAllowance.Exists = false;
	m_sDifferentiate.Exists = false;
	m_sIntegrate.Exists = false;
	m_sMultiplyFrom.Exists = false;
	m_sMultiplyTo.Exists = false;
	m_sAddFrom.Exists = false;
	m_sAddTo.Exists = false;
    m_sSubtractFrom.Exists = false;
	m_sSubtractTo.Exists = false;
	m_sDestination.Exists = false;

	m_TrigInfo.TrigExists=false;
	m_TrigInfo.TrigDelay=0;
	m_TrigInfo.TrigExt=false;
	m_TrigInfo.TrigLevel=1.5;
	m_TrigInfo.TrigPort[0]='\0';
	m_TrigInfo.TrigSlopePos=true;

	m_PulseWidth.Exists=false;
	m_RiseTime.Exists=false;
	m_FallTime.Exists=false;

	m_dSampleTime.Exists = false;

	m_dSamples.Exists = false;
	m_dSamples.Real = 0.0;
	m_dSamples.Int = 0;

	// PCR10030
	m_TimeOut = false;
	
	ISDODEBUG(dodebug(0, "InitPrivateZT1428VXI", "Leaving function", (char*) NULL));
   
	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataZT1428VXI
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::NullCalDataZT1428VXI(void)
{
	ISDODEBUG(dodebug(0, "NullCalDataZT1428VXI", "Entering function", (char*) NULL));
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
	ISDODEBUG(dodebug(0, "NullCalDataZT1428VXI", "Leaving function", (char*) NULL));
    
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: GetSignalChar
//
// Purpose: Parse the 1641 for signal charistics starting with Name
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return: 
//      0 - OK
//      - - Failure code
//   m_Signal char filled in accordingly
//
///////////////////////////////////////////////////////////////////////////////
bool CZT1428VXI_T::GetSignalChar(char *Name, char *Response, int BufferSize)
{
    bool   RetVal = true;
    char   Element[ATXMLW_MAX_NAME];
    char   Unit[ATXMLW_MAX_NAME];
    char  *TempPtr;
    int    SumCount;
    char   SumName1[ATXMLW_MAX_NAME], SumName2[ATXMLW_MAX_NAME];
	
	ISDODEBUG(dodebug(0, "GetSignalChar", "Entering function", (char*) NULL));
    Element[0] = '\0';
    Unit[0] = '\0';

    if((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	{
		if(ISELEMENT("Constant"))
		{
			if (m_dAmplitude.Int != STD_PERIODIC_SINUSOID  && m_dAmplitude.Int != STD_PERIODIC_SQUAREWAVE  &&
				m_dAmplitude.Int != STD_PERIODIC_RAMP      && m_dAmplitude.Int != STD_PERIODIC_TRIANGLE    &&
				m_dAmplitude.Int != STD_PERIODIC_TRAPEZOID && m_dAmplitude.Int != STD_PERIODIC_WAVEFORMSTEP)
			{
				m_dAmplitude.Int = JUST_DC_SIGNAL;
			}

			// This value will be redefined if any of the others are found
            if(ISDBLATTR("amplitude", &(m_dDc_offset.Real), (char*)m_dDc_offset.Dim))
			{
                m_dDc_offset.Exists = true;
			}
		}
		else if(ISELEMENT("Sinusoid"))
		{
			m_dAmplitude.Int = STD_PERIODIC_SINUSOID;
			
			if(ISDBLATTR("amplitude", &(m_dAmplitude.Real), (char*)m_dAmplitude.Dim))
			{
				m_dAmplitude.Exists = true;
			}

			if(ISDBLATTR("frequency", &(m_dFrequency.Real), (char*)m_dFrequency.Dim))
			{
				m_dFrequency.Exists = true;
			}
		}
		else if(ISELEMENT("SquareWave"))
		{
			m_dAmplitude.Int = STD_PERIODIC_SQUAREWAVE;
			
			if(ISDBLATTR("amplitude", &(m_dAmplitude.Real), (char*)m_dAmplitude.Dim))
			{
				m_dAmplitude.Exists = true;
			}

			if(ISDBLATTR("period", &(m_dPeriod.Real), (char*)m_dPeriod.Dim))
			{
				m_dPeriod.Exists = true;
			}
		}
		else if(ISELEMENT("Ramp"))
		{
			m_dAmplitude.Int = STD_PERIODIC_RAMP;
			
			if(ISDBLATTR("amplitude", &(m_dAmplitude.Real), (char*)m_dAmplitude.Dim))
			{
				m_dAmplitude.Exists = true;
			}

			if(ISDBLATTR("period", &(m_dPeriod.Real), (char*)m_dPeriod.Dim))
			{
				m_dPeriod.Exists = true;
			}

			if(ISDBLATTR("riseTime", &(m_RiseTime.Real), (char*)m_RiseTime.Dim))
			{
				m_RiseTime.Exists = true;
			
				if(m_dPeriod.Exists)
				{
					m_FallTime.Exists=true;
					m_FallTime.Real=m_dPeriod.Real-m_RiseTime.Real;
				}
			}
		}
		else if(ISELEMENT("Triangle"))
		{
			m_dAmplitude.Int = STD_PERIODIC_TRIANGLE;

			if(ISDBLATTR("amplitude", &(m_dAmplitude.Real), (char*)m_dAmplitude.Dim))
			{
				m_dAmplitude.Exists = true;
			}

			if(ISDBLATTR("period", &(m_dPeriod.Real), (char*)m_dPeriod.Dim))
			{
				m_dPeriod.Exists = true;
			}

			if(ISDBLATTR("riseTime", &(m_RiseTime.Real), (char*)m_RiseTime.Dim))
			{
				m_RiseTime.Exists = true;
			}

			if(ISDBLATTR("fallTime", &(m_FallTime.Real), (char*)m_FallTime.Dim))
			{
				m_FallTime.Exists = true;
			}
		}
		else if(ISELEMENT("Trapezoid"))
		{
			m_dAmplitude.Int = STD_PERIODIC_TRAPEZOID;
			
			if(ISDBLATTR("amplitude", &(m_dAmplitude.Real), (char*)m_dAmplitude.Dim))
			{
				m_dAmplitude.Exists = true;
			}

			if(ISDBLATTR("period", &(m_dPeriod.Real), (char*)m_dPeriod.Dim))
			{
				m_dPeriod.Exists = true;
			}

			if(ISDBLATTR("pulseWidth", &(m_PulseWidth.Real), (char*)m_PulseWidth.Dim))
			{
				m_PulseWidth.Exists = true;
			}

			if(ISDBLATTR("riseTime", &(m_RiseTime.Real), (char*)m_RiseTime.Dim))
			{
				m_RiseTime.Exists = true;
			}

			if(ISDBLATTR("fallTime", &(m_FallTime.Real), (char*)m_FallTime.Dim))
			{
				m_FallTime.Exists = true;
			}
		}
		else if(ISELEMENT("Instantaneous"))
		{
			m_dAmplitude.Int = STD_PERIODIC_WAVEFORMSTEP;
			
			if(ISDBLATTR("nominal", &(m_dAmplitude.Real), (char*)m_dAmplitude.Dim))
			{
				m_dAmplitude.Exists = true;
			}

			if(ISDBLATTR("samplingInterval", &(m_dSampleTime.Real), (char*)m_dSampleTime.Dim))
			{
				m_dSampleTime.Exists = true;
			}

			// 10MAY11 Added processing to parse out samples
			if (ISDBLATTR("samples", &(m_dSamples.Real), (char*)m_dSamples.Dim))
			{
				m_dSamples.Int = (long)m_dSamples.Real;
				m_dSamples.Exists = true;
			}
		}
		else if(ISELEMENT("Sum"))
		{
            if(ISSTRATTR("In",&TempPtr))
            {
                SumCount = sscanf(TempPtr,"%s %s",SumName1,SumName2);
            
				if(SumCount > 0)
				{
                    GetSignalChar(SumName1,Response,BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", SumName1), Response, BufferSize);
					ISDODEBUG(dodebug(0, "GetSignalChar", "WrapGSI-Found Signal Chars for [%s]", SumName1, (char*) NULL));
				}
                if(SumCount > 1)
				{
                    GetSignalChar(SumName2,Response,BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", SumName2), Response, BufferSize);
					ISDODEBUG(dodebug(0, "GetSignalChar", "WrapGSI-Found Signal Chars for [%s]", SumName2, (char*) NULL));
				}
            }
        } 
    }
    else
    {
        // Unknown name ??
        RetVal=false;
    }

	ISDODEBUG(dodebug(0, "GetSignalChar", "Leaving function", (char*) NULL));
    
	return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: GetSignalCond
//
// Purpose: Parse the 1641 for signal conditionars starting with Name
//          FILTER-ON, AC-COUPLE, etc.
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return: 
//      0 - OK
//      - - Failure code
//   m_Signal char filled in accordingly
//
///////////////////////////////////////////////////////////////////////////////
bool CZT1428VXI_T::GetSignalCond(char *Name, char *InputNames, char *Response, int BufferSize)
{
    bool RetVal = true;
    char   Element[ATXMLW_MAX_NAME];
    char   Unit[ATXMLW_MAX_NAME];
    char  *TempPtr;
	
	ISDODEBUG(dodebug(0, "GetSignalCond", "Entering function", (char*) NULL));
    Element[0] = '\0';
    Unit[0] = '\0';

    if((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	{
		if (m_dAmplitude.Int == STD_PERIODIC_SINUSOID || m_dAmplitude.Int == STD_PERIODIC_SQUAREWAVE ||
			m_dAmplitude.Int == STD_PERIODIC_RAMP     || m_dAmplitude.Int == STD_PERIODIC_TRIANGLE   ||
			m_dAmplitude.Int == JUST_DC_SIGNAL        || m_dAmplitude.Int == STD_PERIODIC_TRAPEZOID  ||
			m_dAmplitude.Int == STD_PERIODIC_WAVEFORMSTEP)
		{
			if(ISELEMENT("LowPass"))
			{
				if(ISDBLATTR("cutoff", &(m_dBanwidthMax.Real), (char*)m_dBanwidthMax.Dim))
				{
					m_dBanwidthMax.Exists = true;
				}
			}
			else if(ISELEMENT("HighPass"))
			{
				if(ISDBLATTR("cutoff", &(m_dBanwidthMin.Real), (char*)m_dBanwidthMin.Dim))
				{
					if (m_dBanwidthMin.Real > 0)
					{
						m_dBanwidthMin.Exists = true;
					}
					else if (m_dBanwidthMin.Real == 0)
					{
						// If HighPass and cutoff value is 0 then it's for AC coupling
						m_nAcCoupling.Exists = true;
						m_nAcCoupling.Int = 1;
					}
				}
			}
			else if(ISELEMENT("Attenuator"))
			{
				if(ISDBLATTR("gain", &(m_nGain.Real), (char*)m_nGain.Dim))
				{
					m_nGain.Exists = true;

					if (m_nGain.Real > 5)
					{
						m_nGain.Int = 10;
					}
					else
					{
						m_nGain.Int = 1;
					}
				}
			}
			else if(ISELEMENT("Load"))
			{
				if(ISDBLATTR("resistance", &(m_nImpedance.Real), (char*)m_nImpedance.Dim))
				{
					m_nImpedance.Exists = true;

					if (m_nImpedance.Real < 51)
					{
						m_nImpedance.Int = 50;
					}
					else 
					{
						m_nImpedance.Int = 1000000;
					}
				}	
			}
			// Find the next signal name or port name
			if(ISSTRATTR("In",&TempPtr))
			{
				if(atxmlw_IsPortName(InputNames,TempPtr))
				{
					//Found the Input Port Name
					strnzcpy(m_InputChannel,TempPtr,ATXMLW_MAX_NAME);

					if (strcmp(m_InputChannel, "CHA")==0)
					{
						m_nChannel.Exists = true;
						m_nChannel.Int = 1;
					}
					else if (strcmp(m_InputChannel, "CHB")==0)
					{
						m_nChannel.Exists = true;
						m_nChannel.Int = 2;
					}
				}
				else
				{
					GetSignalCond(TempPtr,InputNames, Response, BufferSize);
				}
			} 
		}
    }
    else
    {
        // Unknown name ??
        RetVal=false;
    }
	
	ISDODEBUG(dodebug(0, "GetSignalCond", "Leaving function", (char*) NULL));
    
	return(RetVal);
}



//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Voltage_pp
//
// Purpose: Sets up the instrument to measure Voltage-PP
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Voltage_pp(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Voltage_pp", "Entering function", (char*) NULL));
	// Fnc:
	// 3: AC Vpp
	// 32 SQW Vpp
	// 22: RAMP Vpp
	// 42: TRI Vpp
	// 8: PDC Vpp
	
	if (nFunction == 3 || nFunction == 32 || nFunction == 22 || nFunction == 42 || nFunction == 9)
	{
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2.5; // was 4 - JLW
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 4 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Voltage_pp", "Leaving function", (char*) NULL));
	
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Voltage_p
//
// Purpose: Sets up the instrument to measure Voltage-P
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Voltage_p(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Voltage_p", "Entering function", (char*) NULL));
	// Fnc:
	// 5: AC Vp NEW
	// 37 SQW Vp NEW
	// 26: RAMP NEW
	// 47: TRI NEW
	// 20: PDC NEW
	
	if (nFunction == 5 || nFunction == 37 || nFunction == 26 || nFunction == 47 || nFunction == 20)
	{
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2.5; // was 4 - JLW
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5; // was div by 4 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Voltage_p", "Leaving function", (char*) NULL));
	
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Frequency
//
// Purpose: Sets up the instrument to measure Frequency
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Frequency(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Frequency", "Entering function", (char*) NULL));
	// Fnc:
	// 1: AC Freq 
	// 33: SQW Freq
	// 23: RAMP Freq
	// 43: TRI Freq
	// 11: PDC Prf
	
	if (nFunction == 1 || nFunction == 33 || nFunction == 23 || nFunction == 43 || nFunction == 11)
	{
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2.5; // was 4 - JLW
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5; // was div by 4 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Frequency", "Leaving function", (char*) NULL));
	
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Period
//
// Purpose: Sets up the instrument to measure Period
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Period(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Period", "Entering function", (char*) NULL));
	// Fnc:
	// 2: AC Period 
	// 40: SQW Period NEW
	// 29: RAMP Period NEW
	// 50: TRI Period NEW
	// 21: PDC Period NEW
	
	if (nFunction == 2 || nFunction == 40 || nFunction == 29 || nFunction == 50 || nFunction == 21)
	{
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2.5; // was 4 - JLW
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 4 ??? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}
	
	ISDODEBUG(dodebug(0, "Setup_Period", "Leaving function", (char*) NULL));
	
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Voltage_AC
//
// Purpose: Sets up the instrument to measure RMS Voltage
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Voltage_AC(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Voltage_AC", "Entering function", (char*) NULL));
	
	// Fnc:
	// 6: AC Voltage New
	if (nFunction == 6)
	{
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2.5; // was 4 - JLW
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5; // was div by 4 ??? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Voltage_AC", "Leaving function", (char*) NULL));
	
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Av_Voltage
//
// Purpose: Sets up the instrument to measure an averaged RMS voltage
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Av_Voltage(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	int		sample_cnt = 8;
	
	ISDODEBUG(dodebug(0, "Setup_Av_Voltage", "Entering function", (char*) NULL));
	
	// Fnc:
	// 2: AC Av-Voltage 
	if (nFunction == 4)
	{
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2.5; // was 4 - JLW
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 4 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		if (!m_nSampleCount.Exists)
		{
			m_nSampleCount.Exists = true;
			m_nSampleCount.Int = 8;	// Default
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Av_Voltage", "Leaving function", (char*) NULL));
	
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Voltage_DC
//
// Purpose: Sets up the instrument to measure a DC Voltage
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Voltage_DC(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	double amplitude = 4.0;
	double offset = 0.0;  //amplitude/2;
	
	ISDODEBUG(dodebug(0, "Setup_Voltage_DC", "Entering function", (char*) NULL));
	
	// Fnc:
	// 7: DC Voltage 
	if (nFunction == 7)
	{

		// this appears to be done in scope_setup()
		
		/*if (m_dDc_offset.Exists)
			{
				amplitude = m_dDc_offset.Real;
				if (amplitude > 40)
					amplitude = 40;
			}
			else
				amplitude = 4;
			offset = 0.0;  //amplitude / 2;
			if (amplitude < 0)
				amplitude *= -1;*/

		double sweeptime=.001;	// Test reduced from .0000005
		
		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Voltage_DC", "Leaving function", (char*) NULL));
	
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Voltage_p_pos
//
// Purpose: Sets up the instrument to measure Positive Peak Voltage
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Voltage_p_pos(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Voltage_p_pos", "Entering function", (char*) NULL));
	// Fnc:
	// 38: SQW Voltage-p-pos NEW
	// 27: RAMP Voltage-p-pos NEW
	// 48: TRI Voltage-p-pos NEW
	// 9: PDC Voltage-p-pos
	
	if (nFunction == 38 || nFunction == 27 || nFunction == 48 || nFunction == 8)
	{	
		if(m_PulseWidth.Exists==true && m_PulseWidth.Real !=0)
		{
			sweeptime = m_PulseWidth.Real * 1.5;  // was 3 - JLW
		}
		else if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2.5; // was 4 - JLW
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was 4 - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Voltage_p_pos", "Leaving function", (char*) NULL));
	
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Voltage_p_neg
//
// Purpose: Sets up the instrument to measure Negative Peak Voltage
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Voltage_p_neg(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Voltage_p_neg", "Entering function", (char*) NULL));
	
	// Fnc:
	// 38: SQW Voltage-p-neg NEW
	// 28: RAMP Voltage-p-neg NEW
	// 49: TRI Voltage-p-neg NEW
	// 9: PDC Voltage-p-neg
	if (nFunction == 39 || nFunction == 28 || nFunction == 49 || nFunction == 9)
	{
		if(m_PulseWidth.Exists==true && m_PulseWidth.Real !=0)
		{
			sweeptime = m_PulseWidth.Real * 1.5;  // was 3 - JLW
		}
		else if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2.5; // was 4 - JLW
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 4 ??? - JLW
		}
		else
			sweeptime = .001;  // Default

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}
	
		ISDODEBUG(dodebug(0, "Setup_Voltage_p_neg", "Leaving function", (char*) NULL));
	
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_RiseTime
//
// Purpose: Sets up the instrument to measure Rise Time
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_RiseTime(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_RiseTime", "Entering function", (char*) NULL));
	// Fnc:
	// 34: SQW Rise-Time
	// 24: RAMP Rise-Time
	// 34: TRI Rise-Time
	// 12: PDC Rise-Time
	// No Rise-Time information. Sweetime can only be set on ATML period
	if (nFunction == 34 || nFunction == 24  || nFunction == 44 || nFunction == 12)
	{
		if(m_RiseTime.Exists ==true && m_RiseTime.Real!=0)
		{
			sweeptime = m_RiseTime.Real * 2.5;
		}
		else if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;  // was 2 - JLW
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 2 ??? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_RiseTime", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_FallTime
//
// Purpose: Sets up the instrument to measure Fall Time
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_FallTime(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_FallTime", "Entering function", (char*) NULL));
	// Fnc:
	// 35: SQW Fall-Time
	// 25: RAMP Fall-Time
	// 45: TRI Fall-Time
	// 13: PDC Fall-Time
	// No Fall-Time information. Sweetime can only be set on ATML period
	if (nFunction == 35 || nFunction == 25 || nFunction == 45 || nFunction == 13)
	{	
		if(m_FallTime.Exists ==true && m_FallTime.Real!=0)
		{
			sweeptime = m_FallTime.Real * 2.5;
		}
		else if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 2 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_FallTime", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_DutyCycle
//
// Purpose: Sets up the instrument to measure Duty Cycle
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_DutyCycle(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_DutyCycle", "Entering function", (char*) NULL));
	// Fnc:
	// 36: SQW DutyCycle
	// 46: TRI DutyCycle
	// 15: PDC DutyCycle
	if (nFunction == 36 || nFunction == 46 || nFunction == 15)
	{		
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5; // was div by 2 ??? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_DutyCycle", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_PulseWidth
//
// Purpose: Sets up the instrument to measure Pulse Width
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_PulseWidth(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_PulseWidth", "Entering function", (char*) NULL));
	// Fnc:
	// 14: PDC Pulse-Width
	if (nFunction == 14)
	{		
		if(m_PulseWidth.Exists==true && m_PulseWidth.Real !=0)
		{
			sweeptime = m_PulseWidth.Real * 1.5;  // was 3 - JLW
		}
		else if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 1.5 ??? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		//=>scope_setup();
		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_PulseWidth", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Preshoot
//
// Purpose: Sets up the instrument to measure Preshoot
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Preshoot(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Preshoot", "Entering function", (char*) NULL));

	// Fnc:
	// 16: PDC Preshoot
	if (nFunction == 16)
	{		
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 2 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Preshoot", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Overshoot
//
// Purpose: Sets up the instrument to measure Overshoot
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Overshoot(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Overshoot", "Entering function", (char*) NULL));

	// Fnc:
	// 17: PDC Overshoot
	if (nFunction == 17)
	{		
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5; // was div by 2 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Overshoot", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_NegPulseWidth
//
// Purpose: Sets up the instrument to measure Negative Pulse Width
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_NegPulseWidth(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_NegPulseWidth", "Entering function", (char*) NULL));
	
	// Fnc:
	// 18: PDC NegPulseWidth NEW
	if (nFunction == 18)
	{		
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5; // was div by 1.5 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_NegPulseWidth", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_PosPulseWidth
//
// Purpose: Sets up the instrument to measure Positive Pulse Width
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_PosPulseWidth(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_PosPulseWidth", "Entering function", (char*) NULL));

	// Fnc:
	// 19: PDC PosPulseWidth NEW
	if (nFunction == 19)
	{		
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 1.5 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}
	
	ISDODEBUG(dodebug(0, "Setup_PosPulseWidth", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Sample
//
// Purpose: Sets up the instrument to measure several data points 
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Sample(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Sample", "Entering function", (char*) NULL));

	// Fnc:
	// 60: WAV Sample
	if (nFunction == 60)
	{		
		if(m_dSampleTime.Exists)
		{
			sweeptime=m_dSampleTime.Real/16;
		}
		else if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 1.5 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Sample", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Save
//
// Purpose: Sets up the instrument to save several data points to memory
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Save(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Save", "Entering function", (char*) NULL));

	// Fnc:
	// 52: WAV Save
	if (nFunction == 52)
	{		
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 1.5 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Save", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Load
//
// Purpose: Sets up the instrument to Load several data points from memory
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Load(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Load", "Entering function", (char*) NULL));
	
	// Fnc:
	// 53: WAV Load
	if (nFunction == 53)
	{		
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5; // was div by 1.5 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Load", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Compare
//
// Purpose: Sets up the instrument to compare two waveforms
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Compare(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	char*   sTemp = "";
	
	ISDODEBUG(dodebug(0, "Setup_Compare", "Enterimg function", (char*) NULL));
	
	// Fnc:
	// 54: WAV Compare
	if (nFunction == 54)
	{		
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 1.5 ??? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Compare", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_Math
//
// Purpose: Sets up the instrument to perform a math function on one or two waveforms
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_Math(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	
	ISDODEBUG(dodebug(0, "Setup_Math", "Entering function", (char*) NULL));

	// Fnc:
	// 55-59: WAV Math
	if (nFunction == 55 || nFunction == 56 || nFunction == 57 || nFunction == 58 ||nFunction == 59)
	{		
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 1.5 ??? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "Setup_Math", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_TimeInterval
//
// Purpose: Sets up the instrument to measure the time delay between two events
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::Setup_TimeInterval(int nFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		Status = 0;
	double	sweeptime = 0.001;
	int startSlope=ZT1428_DEL_SLOP_POS;
    int startEdge=1;
	int startLevel=ZT1428_DEL_LEV_UPP;
	int stopSlope=ZT1428_DEL_SLOP_POS;
    int stopEdge=1;
	int stopLevel=ZT1428_DEL_LEV_UPP;
	
	ISDODEBUG(dodebug(0, "Setup_TimeInterval", "Entering function", (char*) NULL));

	// Fnc:
	// 63: TIM Time Interval
	if (nFunction == 63)
	{		
		if (m_dFrequency.Exists == true && m_dFrequency.Real != 0)
		{
			sweeptime = (1/m_dFrequency.Real) * 2;
		}
		else if (m_dPeriod.Exists == true && m_dPeriod.Real != 0)
		{
			sweeptime = m_dPeriod.Real * 1.5;  // was div by 1.5 ???? - JLW
		}
		else
		{
			sweeptime = .001;  // Default
		}

		if(sweeptime > 50)
		{
			/* Display message indicating that value has been clipped */
			ATXMLW_ERROR(0, "DSO 1428 Warning", "Sweeptime exceeded 50 hz.  Will be set to 50 hz.\n", Response, BufferSize);
			sweeptime = 50;
		}

		scope_setup(sweeptime, Response, BufferSize);

		if (m_GateInfo.GateExists)
		{
			if(!m_GateInfo.GateStartSlopePos)
			{
				startSlope=ZT1428_DEL_SLOP_NEG;
				startLevel=ZT1428_DEL_LEV_LOW;
			}
		
			if(!m_GateInfo.GateStopSlopePos)
			{
				stopSlope=ZT1428_DEL_SLOP_NEG;
				stopLevel=ZT1428_DEL_LEV_LOW;
			}
		}

		IFNSIM(Status=zt1428_delay_parameters(m_Handle, startSlope, 1, startLevel, stopSlope, 2, stopLevel));
		
		if(Status)
		{
			ErrorZT1428VXI(Status, Response, BufferSize);
		}
	}

	ISDODEBUG(dodebug(0, "Setup_TimeInterval", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: init_scope
//
// Purpose: Initiates the scope measurement
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::init_scope(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	ISDODEBUG(dodebug(0, "init_scope", "Entering function", (char*) NULL));
	IFNSIM(Status=zt1428_run_stop(m_Handle, ZT1428_STOP));
	
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("zt1428_run_stop(%d, ZT1428_STOP)", m_Handle), Response, BufferSize);
	ISDODEBUG(dodebug(0, "init_scope", "zt1428_run_stop(%d, ZT1428_STOP)", m_Handle, (char*) NULL));
	
	if(Status)
	{
		ErrorZT1428VXI(Status, Response, BufferSize);
	}

	// PCR 10030 - start timer prior to start of digitizing waveform
	time(&m_StartTime);

    /* If setup for external trigger and using single-action verbs, the remaining
       code in this function will cause the RTS to hang up during INIT before the 
       supposed next ATLAS statement that generates the actual external trigger can
       execute. Therefore, the RTS MONITOR display persistance fix below cannot
       be used in this case.
    */
	if(m_TrigInfo.TrigExists)//strcmp(m_TrigInfo.TrigPort, "EXT")==0)
	{
		IFNSIM(Status = zt1428_digitize_waveform(m_Handle, m_nChannel.Int, ZT1428_DIG_ASYN));
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("zt1428_digitize_waveform(%d, %d, ZT1428_DIG_ASYN)", m_Handle, m_nChannel.Int), Response, BufferSize);
		ISDODEBUG(dodebug(0, "init_scope", "zt1428_digitize_waveform(%d, %d, ZT1428_DIG_ASYN)", m_Handle, m_nChannel.Int, (char*) NULL));
		
		if(Status)
		{
			ErrorZT1428VXI(Status, Response, BufferSize);
		}

		Sleep(2000);   // Allow scope to digest before trigger can happen
	}
	else
    /*	These commands cause this function to wait until the measurement is complete 
		or the VISA timeout at read. This is necessary so the 
		persistance of the RTS-displayed value during a MONITOR statement is readable.
    	Otherwise, the time spent waiting for the Scope to complete measurement is 
		during the FETCH part, when the RTS has already blanked-out the measurement 
		display.
	*/
	// set VISA TMO_VALUE, if max-time is specified
	{

		IFNSIM(Status = zt1428_digitize_waveform(m_Handle,10, ZT1428_DIG_NORM));
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("zt1428_digitize_waveform(%d, %d, ZT1428_DIG_NORM)", m_Handle, m_nChannel.Int), Response, BufferSize);
		
		if(Status)
		{
			ErrorZT1428VXI(Status, Response, BufferSize);
		}

		time(&m_StopTime);

		// Allow scope to digest customer recommend delay before read to give signal time to settle
		Sleep(200);   
	}

	ISDODEBUG(dodebug(0, "init_scope", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: send_digital_data
//
// Purpose: Gets several data points from a channel or memory, and sends it back to user
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::send_digital_data(int chan, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		  Status = 0;
	int number_ofPoints, acquisitionCount;
    double sampleInterval, timeOffset, Results[8000];
    int xReference, yReference;
    double voltOffset, voltIncrement;
	
	ISDODEBUG(dodebug(0, "send_digital_data", "Entering function", (char*) NULL));
	IFNSIM(Status=zt1428_read_waveform(m_Handle, chan,
				ZT1428_TRAN_SER, Results,
                &number_ofPoints, &acquisitionCount,
                &sampleInterval, &timeOffset,
                &xReference, &voltIncrement,
                &voltOffset, &yReference));
	if(Status)
	{
		ErrorZT1428VXI(Status, Response, BufferSize);
	}

	if(number_ofPoints>8000)
	{
		number_ofPoints=8000;
	}

	if(m_Sim)
	{
		number_ofPoints=8000;
	
		for(int i=0; i<8000; i++)
		{
			Results[i]=i;
		}
	}

	atxmlw_DoubleArrayReturn(m_nMeasChar.Text, "V", Results, number_ofPoints, Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "send_digital_data", "Leaving function", (char*) NULL));

	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: send_compare_data
//
// Purpose: Sends compare data back to user
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::send_compare_data(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int number_ofPoints, acquisitionCount, i;
    double sampleInterval, timeOffset, tempResults[8000];
    int xReference, yReference;
    double voltOffset, voltIncrement;
	double Results[8000];
	
	ISDODEBUG(dodebug(0, "send_compare_data", "Entering function", (char*) NULL));
	IFNSIM(Status=zt1428_read_waveform(m_Handle, ZT1428_FUNC1,
				ZT1428_TRAN_SER, tempResults,
                &number_ofPoints, &acquisitionCount,
                &sampleInterval, &timeOffset,
                &xReference, &voltIncrement,
                &voltOffset, &yReference));
	if(Status)
	{
		ErrorZT1428VXI(Status, Response, BufferSize);
	}

	for(i=0; i< number_ofPoints && i<8000; i++)
	{
		if(tempResults[i]<0)
		{
			tempResults[i]=tempResults[i]*-1;
		}

		if(tempResults[i] > m_nAllowance.Real)
		{
			Results[i]=0.0;
		}
		else
		{
			Results[i]=1.0;
		}
	}

	if(number_ofPoints>8000)
	{
		number_ofPoints=8000;
	}

	atxmlw_DoubleArrayReturn(m_nMeasChar.Text, "V", Results, number_ofPoints, Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "send_compare_data", "Leaving function", (char*) NULL));

	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: save_waveform
//
// Purpose: Saves waveform to memory
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// nFunction          int                  Verifys correct measurement type is calling function
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::save_waveform(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int string1, string2, channel, memory;
	int Status=0;
	int mode=ZT1428_DIG_NORM;
	
	ISDODEBUG(dodebug(0, "save_waveform", "Entering function", (char*) NULL));
	
	Sleep(2000);

	if(m_TrigInfo.TrigExists)
	{
		mode=ZT1428_DIG_ASYN;
	}

	//src_datum = GetDatum(M_SVFM,K_SET);
	if(m_sSaveFrom.Exists)
	{
		/* Digitize the waveform only if it's an active channel(1,2)*/
		string1 = strcmp(m_sSaveFrom.Text,"CHAN1");
		string2 = strcmp(m_sSaveFrom.Text,"CHAN2");

		if(string2)
		{
			channel=ZT1428_CHAN1;
		}
		else
		{
			channel=ZT1428_CHAN2;
		}

		if (string1==0||string2==0)
		{
			IFNSIM(Status=zt1428_digitize_waveform(m_Handle, channel, mode));

			if(Status)
			{
				ErrorZT1428VXI(Status, Response, BufferSize);
			}

			Sleep(2000);
		}
	}
	else
	{
		IFNSIM(Status=zt1428_digitize_waveform(m_Handle, ZT1428_CHAN1, mode));

		if(Status)
		{
			ErrorZT1428VXI(Status, Response, BufferSize);
		}

		Sleep(2000);
		//CheckHPe1428ABErrors();
	}

	if(m_sSaveTo.Exists)
	{
		if(strcmp(m_sSaveTo.Text, "WMEM1")==0)
		{
			memory=ZT1428_WMEM1;
		}
		else if(strcmp(m_sSaveTo.Text, "WMEM2")==0)
		{
			memory=ZT1428_WMEM2;
		}
		else if(strcmp(m_sSaveTo.Text, "WMEM3")==0)
		{
			memory=ZT1428_WMEM3;
		}
		else if(strcmp(m_sSaveTo.Text, "WMEM4")==0)
		{
			memory=ZT1428_WMEM4;
		}
	}
	else
	{
		memory=ZT1428_WMEM1;
	}

	IFNSIM(Status=zt1428_store_waveform(m_Handle, channel, memory));

	if(Status)
	{
		ErrorZT1428VXI(Status, Response, BufferSize);
	}

	ATXMLW_DEBUG(5,atxmlw_FmtMsg("%x=zt1428_store_waveform(%x, %d, %s)",Status, m_Handle, channel, m_sSaveTo.Text), Response, BufferSize);
	ISDODEBUG(dodebug(0, "save_waveform", "%x=zt1428_store_waveform(%x, %d, %s)",Status, m_Handle, channel, m_sSaveTo.Text, (char*) NULL));

	Sleep(5000); /* Give the scope long enough time to save the waveform */
	
	ISDODEBUG(dodebug(0, "save_waveform", "Leaving function", (char*) NULL));
	
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: SanitizeZT1428
//
// Sets instrument to lowest stim and highest impedance settings. Opens all output relays
//
//
///////////////////////////////////////////////////////////////////////////////
void CZT1428VXI_T::SanitizeZT1428VXI(void)
{
	ISDODEBUG(dodebug(0, "SanitizeZT1428VXI", "Entering function", (char*) NULL));
	IFNSIM(zt1428_outputs(m_Handle, ZT1428_OUT_OFF, 0.0, ZT1428_OUT_OFF, ZT1428_OUT_OFF));
	
	ISDODEBUG(dodebug(0, "SanitizeZT1428VXI", "Leaving function", (char*) NULL));
}

////////////////////////////////////////////////////////////////////////////////
// Function : dodebug(int, char*, char* format, ...);                         //
// Purpose  : Print the message to a debug file.                              //
// Return   : None if it don't work o well                                    //
////////////////////////////////////////////////////////////////////////////////
void dodebug(int code, char *function_name, char *format, ...)
{
	static int FirstRun = 0;
	char TmpBuf[_MAX_PATH];

	if (DE_BUG == 1 && FirstRun == 0) 
	{

		sprintf(TmpBuf, "%s", DEBUGIT_ZT1428);
		if ((debugfp = fopen(TmpBuf, "w+b")) == NULL) 
		{
			DE_BUG = 0;
			return;
		}
		FirstRun++;
	}

	va_list	arglist;

	if (DE_BUG) 
	{

		if (code != 0) 
		{
			fprintf(debugfp, "%s in %s\r\n", strerror(code), function_name);
		}

		else 
		{
			va_start(arglist, format);
			fprintf(debugfp, "In function %s ", function_name);
			vfprintf(debugfp, format, arglist);
			fprintf(debugfp, "\r\n");
			va_end(arglist);
		}

		fflush(debugfp);
	}

	return;
}