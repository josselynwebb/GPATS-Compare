// ***************************************************************************************************************
// ***************************************************************************************************************
//
//                                C E M _ I R W I N _ I N T E R F A C E . H
//
//                                                12 Feb 04
//
//
// This is the header file for the SBIR CEM Program Library
//
// ***************************************************************************************************************
// ***************************************************************************************************************
//
//
// ***************************************************************************************************************
// ***************************************************************************************************************
//
//                                            function prototypes
//
// ***************************************************************************************************************
// ***************************************************************************************************************
//
#ifndef         _IRWIN_API
#define         _IRWIN_API

#ifdef __cplusplus
extern "C" {
#endif

			void __stdcall BORESIGHT_ALTA_IR_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					int nTecDrive,
					int nBeamSplitterPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					float fIntensityRatio,
					float fAlignmentErrorX,
					float fAlignmentErrorY);
			void __stdcall BORESIGHT_ALTA_IR_INITIATE();
			void __stdcall BORESIGHT_ALTA_IR_FETCH(
					float *fBoresightX,
					float *fBoresightY,
					int *nStatus);

			void __stdcall BORESIGHT_IR_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fDiffTemp,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					float fIntensityRatio);
			void __stdcall BORESIGHT_IR_INITIATE();
			void __stdcall BORESIGHT_IR_FETCH(
					float *fBoresightX,
					float *fBoresightY,
					int *nStatus);

			void __stdcall CALCULATED_MRTD_IR_SETUP(
					int nFrameRate,
					float fSensorRate,
					float fDetLineSpacing,
					float fIFOV,
					int nNumFreqPts);
			void __stdcall CALCULATED_MRTD_IR_INITIATE();
			void __stdcall CALCULATED_MRTD_IR_FETCH(
					float* fMRTD,
					int *nStatus);

			void __stdcall CHANNEL_CROSSTALK_IR_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fDiffTemp,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
	                float fLinesPerChan,
	                float fLinesFirstChan);
			void __stdcall CHANNEL_CROSSTALK_IR_INITIATE();
			void __stdcall CHANNEL_CROSSTALK_IR_FETCH(
					int *nChanList,
					int *nStatus);

			void __stdcall CHANNEL_INTEGRITY_ALIGNMENT_IR_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fDiffTemp,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection);
			void __stdcall CHANNEL_INTEGRITY_ALIGNMENT_IR_INITIATE();
			void __stdcall CHANNEL_INTEGRITY_ALIGNMENT_IR_FETCH(
					float *fLinesPerChan,
					float *fLinesFirstChan,
					int *nStatus);

			void __stdcall CHANNEL_INTEGRITY_IR_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fDiffTemp,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
//					float fNoiseLimit,
					float fLinesPerChan,
					float fLinesFirstChan,
					float fNoiseCriteria);
			void __stdcall CHANNEL_INTEGRITY_IR_INITIATE();
			void __stdcall CHANNEL_INTEGRITY_IR_FETCH(
					int nChanList[],
					int *nStatus);

			void __stdcall GEOMETRIC_FIDELITY_DISTORTION_IR_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fDiffTemp,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection);
			void __stdcall GEOMETRIC_FIDELITY_DISTORTION_IR_INITIATE();
			void __stdcall GEOMETRIC_FIDELITY_DISTORTION_IR_FETCH(
					float* fDistortion, 
					float* fTilt, 
					int *nStatus);

			void __stdcall IMAGE_UNIFORMITY_IR_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fDiffTemp,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection);
			void __stdcall IMAGE_UNIFORMITY_IR_INITIATE();
			void __stdcall IMAGE_UNIFORMITY_IR_FETCH(
					float *fImgUniformity, 
					int *nStatus);

			void __stdcall MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_SETUP(
					int nNumSteps,
					float fStartDiffTemps[],
					int nTargetPos[],
					int nTargetFeatures[]);
			void __stdcall MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_INITIATE();
			void __stdcall MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_FETCH(
					float* fMRTD, 
					int *nStatus);

			void __stdcall MODULATION_TRANSFER_FUNCTION_IR_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fDiffTemp,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					int nOrientation,
					int nPedestalFilter,
					int nSmoothing,
					int nNumFreqPts,
					int nCorrectionCurve	);
			void __stdcall MODULATION_TRANSFER_FUNCTION_IR_INITIATE();
			void __stdcall MODULATION_TRANSFER_FUNCTION_IR_FETCH(
					float* fMTFList, 
					int *nStatus);

			void __stdcall NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fDiffTemp,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					float fBeginTemp,
					float fEndTemp,
					float fTempInterval,
					int nABlockTopLeftX,
					int nABlockTopLeftY,
					int nABlockBotRightX,
					int nABlockBotRightY);
			void __stdcall NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_INITIATE();
			void __stdcall NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH(
					float *fNETD, 
					int *nStatus);
			//
			// VISISBLE
			//
			void __stdcall BORESIGHT_TV_ALTA_VIS_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					int nLedDrive,
					int nBeamSplitterPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					float fIntensityRatio,
					float fAlignmentErrorX,
					float fAlignmentErrorY);
			void __stdcall BORESIGHT_TV_ALTA_VIS_INITIATE();
			void __stdcall BORESIGHT_TV_ALTA_VIS_FETCH(
					float *fBoresightX,
					float *fBoresightY,
					int *nStatus);

			void __stdcall BORESIGHT_TV_VIS_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fRadiance,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					float fColorTemp,
					float fIntensityRatio);
			void __stdcall BORESIGHT_TV_VIS_INITIATE();
			void __stdcall BORESIGHT_TV_VIS_FETCH(
					float *fBoresightX,
					float *fBoresightY,
					int *nStatus);

			void __stdcall CAMERA_UNIFORMITY_VIS_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fRadiance,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					float fColorTemp);
			void __stdcall CAMERA_UNIFORMITY_VIS_INITIATE();
			void __stdcall CAMERA_UNIFORMITY_VIS_FETCH(
					float *fCameraUniformity, 
					int *nStatus);

			void __stdcall GAIN_VIS_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fRadiance,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					float fColorTemp,
					float fBeginRad,
					float fEndRad,
					float fRadInterval);
			void __stdcall GAIN_VIS_INITIATE();
			void __stdcall GAIN_VIS_FETCH(
					float* fGain, 
					float* fDynamicRange,
					int *nStatus);

			void __stdcall GEOMETRIC_FIDELITY_DISTORTION_VIS_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fRadiance,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					float fColorTemp);
			void __stdcall GEOMETRIC_FIDELITY_DISTORTION_VIS_INITIATE();
			void __stdcall GEOMETRIC_FIDELITY_DISTORTION_VIS_FETCH(
					float* fDistortion, 
					float* fTilt, 
					int *nStatus);

			void __stdcall MINIMUM_RESOLVABLE_CONTRAST_VIS_SETUP(
					int nNumSteps,
					float fStartRadiance[],
					int nTargetPos[],
					int nTargetFeatures[]);
			void __stdcall MINIMUM_RESOLVABLE_CONTRAST_VIS_INITIATE();
			void __stdcall MINIMUM_RESOLVABLE_CONTRAST_VIS_FETCH(
					float* fMRC, 
					int *nStatus);

			void __stdcall MODULATION_TRANSFER_FUNCTION_VIS_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fRadiance,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					float fColorTemp,
					int nOrientation,
					int nPedestalFilter,
					int nSmoothing,
					int nNumFreqPts,
					int nCorrectionCurve);
			void __stdcall MODULATION_TRANSFER_FUNCTION_VIS_INITIATE();
			void __stdcall MODULATION_TRANSFER_FUNCTION_VIS_FETCH(
					float* fMTFList, 
					int *nStatus);

			void __stdcall SHADES_OF_GRAY_VIS_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					float fRadiance,
					int nTargetPos,
					int nCenterX,
					int nCenterY,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					int nCameraSelection,
					float fColorTemp);
			void __stdcall SHADES_OF_GRAY_VIS_INITIATE();
			void __stdcall SHADES_OF_GRAY_VIS_FETCH(
					int* nGrayScaleCount,
					int *nStatus);
			//
			// Laser
			//
			void __stdcall AUTOCOLLIMATE_LASER_SETUP(
					int     nImgNumFrames,
					float	fHFieldOfView,
					float	fVFieldOfView,
					int     nSBlockTopLeftX,
					int     nSBlockTopLeftY,
					int     nSBlockBotRightX,
					int     nSBlockBotRightY,
					float	fCameraIntegTime,
					int		nBeamSplitPos,
					float	fCamDelayTime,
					int		nCamTrigger,
					int		nCamGain,
					int		nCamOffset,
					int		nARMTriggerMode,
					int		nARMDiodeSelection,
					float	fARMDiodeAmplitude,
					float	fARMPulseWidth,
					float   fIntensityRatio);
			void __stdcall AUTOCOLLIMATE_LASER_INITIATE();
			void __stdcall AUTOCOLLIMATE_LASER_FETCH(
					float *ErrorX, 
					float *ErrorY, 
					int *nStatus);

			void __stdcall BEAM_DIVERGENCE_LASER_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					float fCamIntegrationTime,
					int nBeamSplitterPos,
					float fCamDelayTime,
					int nCamTrigger,
					int nCamGain,
					int nCamOffset);
			void __stdcall BEAM_DIVERGENCE_LASER_INITIATE();
			void __stdcall BEAM_DIVERGENCE_LASER_FETCH(
					float *fDiameter, 
					int *nStatus);

			void __stdcall BORESIGHT_LASER_SETUP(
					int nImgNumFrames,
					float fHFieldOfView,
					float fVFieldOfView,
					int nSBlockTopLeftX,
					int nSBlockTopLeftY,
					int nSBlockBotRightX,
					int nSBlockBotRightY,
					float fCamIntegrationTime,
					int nBeamSplitterPos,
					float fCamDelayTime,
					int nCamTrigger,
					int nCamGain,
					int nCamOffset,
					float fIntensityRatio);
			void __stdcall BORESIGHT_LASER_INITIATE();
			void __stdcall BORESIGHT_LASER_FETCH(
					float *LBeamAlignX, 
					float *LBeamAlignY, 
					int *nStatus);

			void __stdcall PULSE_ENERGY_MEASUREMENTS_LASER_SETUP(
					int nSCNumberMeasurements,
					int nSCInputChannel,
					int nSCInputRange,
					int nSCTimebase,
					int nSCTriggerSource,
					int nSCTriggerSlope,
					float fSCTriggerLevel,
					int nBeamSplitterPos,
					int nLaserFreq);
			void __stdcall PULSE_ENERGY_MEASUREMENTS_LASER_INITIATE();
			void __stdcall PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(
					float *fEnergy_Instability,
					float *fAverage_Energy,
					float *fAmplitude_Instability,
					float *fAverage_Amplitude,
					float *fPulse_Width_Instability,
					float *fAverage_Pulse_Width,
					int *nStatus); 

			void __stdcall PULSE_REPETITION_FREQUENCY_LASER_SETUP(
					int nNumberMeasurements,
					int nTriggerSlope,
					float fTriggerLevel,
					int nDelay,
					int nBeamSplitterPos);
			void __stdcall PULSE_REPETITION_FREQUENCY_LASER_INITIATE();
			void __stdcall PULSE_REPETITION_FREQUENCY_LASER_FETCH(
					float *fPeriod_Instability,
					float *fAverage_Period,
					int *nStatus);

			void __stdcall RANGE_FINDER_ACCURACY_LASER_SETUP(
					int nBeamSplitterPos,
					int nARMTriggerMode,
					int nARMDiodeSelection,
					float fARMFirstPulseRange,
					float fARMDiodeAmplitude,
					float fARMPulseWidth,
					float fARMSecondPulseRange);
			void __stdcall RANGE_FINDER_ACCURACY_LASER_INITIATE();
			void __stdcall RANGE_FINDER_ACCURACY_LASER_FETCH(
					float *fRange, 
					int *nStatus);

			void __stdcall RECEIVER_SENSITIVITY_LASER_SETUP(
					int nBeamSplitterPos,
					int nARMTriggerMode,
					int nARMDiodeSelection,
					float fARMFirstPulseRange,
					float fARMDiodeAmplitude,
					float fARMPulseWidth);
			void __stdcall RECEIVER_SENSITIVITY_LASER_INITIATE();
			void __stdcall RECEIVER_SENSITIVITY_LASER_FETCH(
					float *fRange, 
					int *nStatus);

			void __stdcall SATELLITE_BEAMS_LASER_SETUP(
					int		nImgNumFrames,
					float	fHFieldOfView,
					float	fVFieldOfView,
					int		nSBlockTopLeftX,
					int		nSBlockTopLeftY,
					int		nSBlockBotRightX,
					int		nSBlockBotRightY,
					float	fCameraIntegTime,
					int		nBeamSplitPos,
					float	fCamDelayTime,
					int		nCamTrigger,
					int		nCamGain,
					int		nCamOffset,
					float	fThresholddB,
					float	fAttenMain,
					float	fAttenSat);
			void __stdcall SATELLITE_BEAMS_LASER_INITIATE();
			void __stdcall SATELLITE_BEAMS_LASER_FETCH(
					int *nBeams, 
					int *nStatus);
			//
			// Hardware Controls
			//
			void __stdcall RESET_MODULE_INITIATE();

			void __stdcall GET_BIT_DATA_IR_INITIATE();
			void __stdcall GET_BIT_DATA_IR_FETCH(int *nErrorNumber);

			void __stdcall GET_MODULE_ID_IR_INITIATE();
			void __stdcall GET_MODULE_ID_IR_FETCH(int *nID);

			void __stdcall GET_STATUS_BYTE_MESSAGE_IR_INITIATE();
			void __stdcall GET_STATUS_BYTE_MESSAGE_IR_FETCH(int *nStatusByte);

			void __stdcall GET_TEMP_TARGET_IR_INITIATE();
			void __stdcall GET_TEMP_TARGET_IR_FETCH(float *fTgtTemp);

			void __stdcall SET_RDY_WINDOW_IR_INITIATE(float fRdyWindowSetting);
			void __stdcall SET_RDY_WINDOW_IR_FETCH(float *fRdyWindowRdg);

			void __stdcall SET_TARGET_POSITION_IR_INITIATE(int nTgtPosition);
			void __stdcall SET_TARGET_POSITION_IR_FETCH(int *nTgtPosition);

			void __stdcall SET_TEMP_ABSOLUTE_IR_INITIATE(float fTempSetting);
			void __stdcall SET_TEMP_ABSOLUTE_IR_FETCH(float *fTempRdg);

			void __stdcall SET_TEMP_DIFFERENTIAL_IR_INITIATE(float fTempSetting);
			void __stdcall SET_TEMP_DIFFERENTIAL_IR_FETCH(float *fTempRdg);

			void __stdcall GET_BIT_DATA_LASER_INITIATE();
			void __stdcall GET_BIT_DATA_LASER_FETCH(int *nErrorNumber);

			void __stdcall GET_MODULE_ID_LASER_INITIATE();
			void __stdcall GET_MODULE_ID_LASER_FETCH(int *nID);

			void __stdcall GET_STATUS_BYTE_MESSAGE_LASER_INITIATE();
			void __stdcall GET_STATUS_BYTE_MESSAGE_LASER_FETCH(int *nStatusByte);

			void __stdcall SET_CAMERA_TRIGGER_LASER_INITIATE(int nTrgSrc);
			void __stdcall SET_CAMERA_TRIGGER_LASER_FETCH(int *nTrgSrcRdg);

			void __stdcall SET_CAMERA_INTEGRATION_LASER_INITIATE(float fIntTime);
			void __stdcall SET_CAMERA_INTEGRATION_LASER_FETCH(float *fIntTimeRdg);

			void __stdcall SET_CAMERA_GAIN_LASER_INITIATE(int nPosition);
			void __stdcall SET_CAMERA_GAIN_LASER_FETCH(int *nPositionRdg);

			void __stdcall SET_CAMERA_OFFSET_LASER_INITIATE(int nPosition);
			void __stdcall SET_CAMERA_OFFSET_LASER_FETCH(int *nPositionRdg);

			void __stdcall SET_STAGE_POSITION_LASER_INITIATE(int nStgPosition);
			void __stdcall SET_STAGE_POSITION_LASER_FETCH(int *nStgPositionRdg);

			void __stdcall SET_CAMERA_DELAY_LASER_INITIATE(int nDelay);
			void __stdcall SET_CAMERA_DELAY_LASER_FETCH(int *nDelay);

			void __stdcall SELECT_DIODE_LASER_INITIATE(int nDiode);
			void __stdcall SELECT_DIODE_LASER_FETCH(int *nDiode);

			void __stdcall SET_LED_DRIVE_LASER_INITIATE(int nDrive);
			void __stdcall SET_LED_DRIVE_LASER_FETCH(int *nDrive);

			void __stdcall SET_POWER_LASER_INITIATE(int nEnable);
			void __stdcall SET_POWER_LASER_FETCH(int *nEnable);

			void __stdcall SET_PULSE_AMPLITUDE_LASER_INITIATE(float fAmplitude);
			void __stdcall SET_PULSE_AMPLITUDE_LASER_FETCH(float *fAmplitude);

			void __stdcall SET_TEC_DRIVE_LASER_INITIATE(int nDrive);
			void __stdcall SET_TEC_DRIVE_LASER_FETCH(int *nDrive);

			void __stdcall SET_PULSE_RANGE_LASER_INITIATE(float fRange);
			void __stdcall SET_PULSE_RANGE_LASER_FETCH(float *fRange);

			void __stdcall SET_PULSE_RANGE2_LASER_INITIATE(float fRange);
			void __stdcall SET_PULSE_RANGE2_LASER_FETCH(float *fRange);

			void __stdcall SET_PULSE_PERIOD_LASER_INITIATE(float fPeriod);
			void __stdcall SET_PULSE_PERIOD_LASER_FETCH(float *fPeriod);

			void __stdcall SET_PULSE_WIDTH_LASER_INITIATE(int nWidth);
			void __stdcall SET_PULSE_WIDTH_LASER_FETCH(int *nWidth);

			void __stdcall SET_TRIGGER_SOURCE_LASER_INITIATE(int nSource);
			void __stdcall SET_TRIGGER_SOURCE_LASER_FETCH(int *nSource);

			void __stdcall GET_CONVERTION_FACTOR_1064_LASER_INITIATE();
			void __stdcall GET_CONVERTION_FACTOR_1064_LASER_FETCH(float *fLaserCal);

			void __stdcall GET_CONVERTION_FACTOR_15XX_LASER_INITIATE();
			void __stdcall GET_CONVERTION_FACTOR_15XX_LASER_FETCH(float *fLaserCal);

			void __stdcall GET_BIT_DATA_MOD_SRC_INITIATE();
			void __stdcall GET_BIT_DATA_MOD_SRC_FETCH(int *nErrorNumber);

			void __stdcall GET_MODULE_ID_MOD_SRC_INITIATE();
			void __stdcall GET_MODULE_ID_MOD_SRC_FETCH(int *ID);

			void __stdcall GET_STATUS_BYTE_MESSAGE_MOD_SRC_INITIATE();
			void __stdcall GET_STATUS_BYTE_MESSAGE_MOD_SRC_FETCH(int *nStatus_Byte);

			void __stdcall SET_MODULATION_SOURCE_POSITION_MOD_SRC_INITIATE(int nPosition);
			void __stdcall SET_MODULATION_SOURCE_POSITION_MOD_SRC_FETCH(int *nPosition_Rdg);

			void __stdcall SET_MODULATION_SOURCE_FREQUENCY_MOD_SRC_INITIATE(int nFrequency);
			void __stdcall SET_MODULATION_SOURCE_FREQUENCY_MOD_SRC_FETCH(int *nFrequency_Rdg);

			void __stdcall SET_MODULATION_SOURCE_LAMP_MOD_SRC_INITIATE(int nLamp_State);
			void __stdcall SET_MODULATION_SOURCE_LAMP_MOD_SRC_FETCH(int *nLamp_State_Rdg);

			void __stdcall GET_BIT_DATA_VIS_INITIATE();
			void __stdcall GET_BIT_DATA_VIS_FETCH(int *nErrorNumber);

			void __stdcall GET_MODULE_ID_VIS_INITIATE();
			void __stdcall GET_MODULE_ID_VIS_FETCH(int *nID);

			void __stdcall GET_STATUS_BYTE_MESSAGE_VIS_INITIATE();
			void __stdcall GET_STATUS_BYTE_MESSAGE_VIS_FETCH(int *nStatusByte);

			void __stdcall SET_ANGULAR_RATE_VIS_INITIATE(float fRate);
			void __stdcall SET_ANGULAR_RATE_VIS_FETCH(float *fRateRdg);

			void __stdcall SET_RADIANCE_VIS_INITIATE(float fRadiance);
			void __stdcall SET_RADIANCE_VIS_FETCH(float *fRadianceRdg);

			void __stdcall SET_TARGET_POSITION_VIS_INITIATE(int nTgtPosition);
			void __stdcall SET_TARGET_POSITION_VIS_FETCH(int *nTgtPositionRdg);

			
#ifdef __cplusplus
}
#endif


#endif
//
// ***************************************************************************************************************
// ***************************************************************************************************************
//
//                          E N D    O F    C E M _ I R W I N _ I N T E R F A C E . H
//
// ***************************************************************************************************************
// ***************************************************************************************************************
