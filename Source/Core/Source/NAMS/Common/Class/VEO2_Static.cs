// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2021-07-02 10:35#$: Date of last commit
//    $Rev:: 28275            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NAM;

namespace VEO2_Library
{
    public static class VEO2
    {

        #region DLLImports

        #region Hardware Control

        // BIT
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int GET_BIT_DATA_FETCH(ref int Error_Number);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GET_BIT_DATA_INITIATE();

        // Status
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int GET_STATUS_BYTE_MESSAGE_FETCH(ref int Status_Byte);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GET_STATUS_BYTE_MESSAGE_INITIATE();

        // Reset Module
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void RESET_MODULE_INITIATE();

        // Get Module ID
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GET_MODULE_ID_INITIATE();
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GET_MODULE_ID_FETCH(ref int ID);

        // Close IRWindows
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void IRWIN_SHUTDOWN();

        // Laser Diode
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SELECT_DIODE_LASER_FETCH(ref int Select_Rdg);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SELECT_DIODE_LASER_INITIATE(int Select);

        // Camera Power
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_CAMERA_POWER_FETCH(ref int Power_Rdg);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_CAMERA_POWER_INITIATE(int Power);

        // LARRS Azimuth
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_LARRS_AZ_LASER_FETCH(ref int Position);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_LARRS_AZ_LASER_INITIATE(int Position);

        // LARRS Elevation
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_LARRS_EL_LASER_FETCH(ref int Position);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_LARRS_EL_LASER_INITIATE(int Position);

        // LARRS Polarizer
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_LARRS_POLARIZE_LASER_FETCH(ref int Angle);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_LARRS_POLARIZE_LASER_INITIATE(int Angle);

        // Laser Test Mode
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_LASER_TEST_FETCH(ref int Operation_Rdg);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_LASER_TEST_INITIATE(int Operation);

        // Radiance
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_RADIANCE_VIS_FETCH(ref float Radiance_Rdg);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_RADIANCE_VIS_INITIATE(float Radiance);

        // Sensor Stage
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_SENSOR_STAGE_LASER_FETCH(ref int Sensor_Stage_Position);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_SENSOR_STAGE_LASER_INITIATE(int Sensor_Stage_Position);

        // Source Stage
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_SOURCE_STAGE_LASER_FETCH(ref int Source_Stage_Position);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_SOURCE_STAGE_LASER_INITIATE(int Source_Stage_Position);

        // System Config
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_SYSTEM_CONFIGURATION_FETCH(ref int SysConfig_Rdg);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_SYSTEM_CONFIGURATION_INITIATE(int SysConfig);

        // Target Position
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_TARGET_POSITION_FETCH(ref int Target_Position);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_TARGET_POSITION_INITIATE(int Target_Position);

        // Target Temp
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GET_TEMP_TARGET_IR_INITIATE();
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int GET_TEMP_TARGET_IR_FETCH(ref float Target_Temp);

        // Ready Window
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_RDY_WINDOW_IR_INITIATE(float Rdy_Window_Setting);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_RDY_WINDOW_IR_FETCH(ref float Rdy_Window_Rdg);

        // Absolute Temperature
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_TEMP_ABSOLUTE_IR_INITIATE(float Temp_Setting);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_TEMP_ABSOLUTE_IR_FETCH(ref float Temp_Rdg);

        // Differential Temperature
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_TEMP_DIFFERENTIAL_IR_INITIATE(float Temp_Setting);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_TEMP_DIFFERENTIAL_IR_FETCH(ref float Temp_Rdg);

        // Camera Trigger
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_CAMERA_TRIGGER_LASER_INITIATE(int Trigger);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_CAMERA_TRIGGER_LASER_FETCH(ref int Trigger);

        // Camera Delay
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_CAMERA_DELAY_LASER_INITIATE(float Delay);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_CAMERA_DELAY_LASER_FETCH(ref float Delay);

        // Laser Trigger Source
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_TRIGGER_SOURCE_LASER_INITIATE(int setting);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_TRIGGER_SOURCE_LASER_FETCH(ref int setting);

        // Laser Operation
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_OPERATION_LASER_INITIATE(int Select);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_OPERATION_LASER_FETCH(ref int Select_Rdg);

        /// <summary>
        /// Set Laser Attenuation (Coarse)
        /// 0 to 4095
        /// </summary>
        /// <param name="PA"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_COARSE_EVOA_LASER_INITIATE(float PA);

        /// <summary>
        /// Get Laser Attenuation (Coarse)
        /// </summary>
        /// <param name="PA_Rdg"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_COARSE_EVOA_LASER_FETCH(ref float PA_Rdg);

        /// <summary>
        /// Set Laser Attenuation (Fine)
        /// 0 to 4095
        /// </summary>
        /// <param name="PA"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_FINE_EVOA_LASER_INITIATE(float PA);

        /// <summary>
        /// Get Laser Attenuation (Fine)
        /// </summary>
        /// <param name="PA_Rdg"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_FINE_EVOA_LASER_FETCH(ref float PA_Rdg);

        /// <summary>
        /// Set Laser Pulse Amplitude
        /// 0 to 3000
        /// </summary>
        /// <param name="PA"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_PULSE_AMPLITUDE_LASER_INITIATE(float PA);

        /// <summary>
        /// Get Laser Pulse Amplitude
        /// 0 to 3000
        /// </summary>
        /// <param name="PA_Rdg"></param>
        /// <returns></returns>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_PULSE_AMPLITUDE_LASER_FETCH(ref float PA_Rdg);

        /// <summary>
        /// Set Laser Pulse Period
        /// 50 to 125 ms
        /// </summary>
        /// <param name="PP"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_PULSE_PERIOD_LASER_INITIATE(float PP);

        /// <summary>
        /// Get Laser Pulse Period
        /// 50 to 125 ms
        /// </summary>
        /// <param name="PP_Rdg"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_PULSE_PERIOD_LASER_FETCH(ref float PP_Rdg);

        /// <summary>
        /// Set Timed Delay Between First and Second Return Pulses
        /// 0 - No Second Pulse
        /// 60 - 2000 ns
        /// 20 ns increments
        /// </summary>
        /// <param name="PR2"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_PULSE2_DELAY_LASER_INITIATE(float PR2);

        /// <summary>
        /// Get Timed Delay Between First and Second Return Pulses
        /// </summary>
        /// <param name="PR2_Rdg"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_PULSE2_DELAY_LASER_FETCH(ref float PR2_Rdg);

        /// <summary>
        /// Select Larger Pulse
        /// 0 to 1
        /// </summary>
        /// <param name="Select"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SELECT_LARGER_PULSE_LASER_INITIATE(int Select);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SELECT_LARGER_PULSE_LASER_FETCH(ref int Select_Rdg);

        /// <summary>
        /// Set Pulse Amplitude of Smaller Pulse relative to the Larger Pulse
        /// 10 to 100
        /// </summary>
        /// <param name="PA"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_PULSE_PERCENTAGE_LASER_INITIATE(float PA);

        /// <summary>
        /// Get Pulse Amplitude of Smaller Pulse relative to the Larger Pulse
        /// 10 to 100
        /// </summary>
        /// <param name="PA_Rdg"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SET_PULSE_PERCENTAGE_LASER_FETCH(ref float PA_Rdg);

        /// <summary>
        /// Set Range Emulation Distance
        /// 500 to 60,000 m
        /// </summary>
        /// <param name="PR"></param>
        /// <returns></returns>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_RANGE_EMULATION_LASER_INITIATE(float PR);

        /// <summary>
        /// Get Range Emulation Distance
        /// 500 to 60,000 m
        /// </summary>
        /// <param name="PR_Rdg"></param>
        /// <returns></returns>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int SET_RANGE_EMULATION_LASER_FETCH(ref float PR_Rdg);

        // Laser Calibration Value
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GET_CALIBRATION_VALUE_INITIATE(int CAL_ID);
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern int GET_CALIBRATION_VALUE_FETCH(ref float CAL_Value);

        #endregion

        #region Test Measurement Calls

        #region Infrared

        //BORESIGHT
        /// <summary>
        /// Setup for IR Boresight Measurement
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Image_Num_Frames"></param>
        /// <param name="H_Field_of_View"></param>
        /// <param name="V_Field_of_View"></param>
        /// <param name="Diff_Temp"></param>
        /// <param name="Target_Position"></param>
        /// <param name="Center_X"></param>
        /// <param name="Center_Y"></param>
        /// <param name="Signal_Block_Top_Left_X"></param>
        /// <param name="Signal_Block_Top_Left_Y"></param>
        /// <param name="Signal_Block_Bot_Right_X"></param>
        /// <param name="Signal_Block_Bot_Right_Y"></param>
        /// <param name="Camera_Selection"></param>
        /// <param name="Intensity_Ratio"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BORESIGHT_IR_SETUP(int Source, int Image_Num_Frames, float H_Field_of_View, float V_Field_of_View, float Diff_Temp, int Target_Position, int Center_X, int Center_Y, int Signal_Block_Top_Left_X, int Signal_Block_Top_Left_Y, int Signal_Block_Bot_Right_X, int Signal_Block_Bot_Right_Y, int Camera_Selection, float Intensity_Ratio);

        /// <summary>
        /// Send Request for an IR Boresight Measurement
        /// </summary>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BORESIGHT_IR_INITIATE();

        /// <summary>
        /// Get IR Boresight Measurement Readings
        /// </summary>
        /// <param name="Boresight_X_Coord"></param>
        /// <param name="Boresight_Y_Coord"></param>
        /// <param name="Status"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BORESIGHT_IR_FETCH(ref float Boresight_X_Coord, ref float Boresight_Y_Coord, ref int Status);

        //CHANNEL INTEGRITY
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void CHANNEL_INTEGRITY_IR_SETUP(int Source, int Image_Num_Frames, float H_Field_of_View, float V_Field_of_View, float Diff_Temp, int Target_Position, int Center_X, int Center_Y, int Signal_Block_Top_Left_X, int Signal_Block_Top_Left_Y, int Signal_Block_Bot_Right_X, int Signal_Block_Bot_Right_Y, int Camera_Selection, float Lines_Per_Channel, float Lines_First_Channel, float Noise_Criteria);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void CHANNEL_INTEGRITY_IR_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void CHANNEL_INTEGRITY_IR_FETCH(ref List<int> Channel_List, ref int Status);
        //public static extern void CHANNEL_INTEGRITY_IR_FETCH(ref int[] Channel_List, ref int Status);

        //GEOMETRIC FIDELITY DISTORTION
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GEOMETRIC_FIDELITY_DISTORTION_IR_SETUP(int Source, int Image_Num_Frames, float H_Field_of_View, float V_Field_of_View, float Diff_Temp, int Target_Position, int Center_X, int Center_Y, int Signal_Block_Top_Left_X, int Signal_Block_Top_Left_Y, int Signal_Block_Bot_Right_X, int Signal_Block_Bot_Right_Y, int Camera_Selection, int Number_of_Positions, List<float> Distortion_Positions); // float[] Distortion_Positions);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GEOMETRIC_FIDELITY_DISTORTION_IR_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GEOMETRIC_FIDELITY_DISTORTION_IR_FETCH(ref float Distortion, ref int Status);

        //IMAGE UNIFORMITY
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void IMAGE_UNIFORMITY_IR_SETUP(int Source, int Image_Num_Frames, float H_Field_of_View, float V_Field_of_View, float Diff_Temp, int Target_Position, int Center_X, int Center_Y, int Signal_Block_Top_Left_X, int Signal_Block_Top_Left_Y, int Signal_Block_Bot_Right_X, int Signal_Block_Bot_Right_Y, int Camera_Selection);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void IMAGE_UNIFORMITY_IR_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void IMAGE_UNIFORMITY_IR_FETCH(ref float Image_Uniformity, ref int Status);

        //MINIMUM RESOLVABLE TEMPERATURE DIFFERENCE
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_SETUP(int Source, int Number_of_Steps, List<float> Starting_Diff_Temps, List<int> Target_Position, List<int> Target_Features);
        //public static extern void MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_SETUP(int Source, int Number_of_Steps, float[] Starting_Diff_Temps, int[] Target_Position, int[] Target_Features);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_FETCH(ref List<float> MRTD, ref int Status);
        //public static extern void MINIMUM_RESOLVABLE_TEMPERATURE_DIFFERENCE_IR_FETCH(ref float[] MRTD, ref int Status);

        //MODULATION TRANSFER FUNCTION
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MODULATION_TRANSFER_FUNCTION_IR_SETUP(int Source, int Image_Num_Frames, float H_Field_of_View, float V_Field_of_View, float Diff_Temp, int Target_Position, int Center_X, int Center_Y, int Signal_Block_Top_Left_X, int Signal_Block_Top_Left_Y, int Signal_Block_Bot_Right_X, int Signal_Block_Bot_Right_Y, int Camera_Selection, int Orientation, int Pedestal_Filter, int Smoothing, int Num_Freq_Points, int Correction_Curve_Index);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MODULATION_TRANSFER_FUNCTION_IR_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MODULATION_TRANSFER_FUNCTION_IR_FETCH(ref List<float> MTF_List, ref int Status);

        //NOISE EQUIVALENT DIFFERENTIAL TEMPERATURE
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP(int Source, int Image_Num_Frames, float H_Field_of_View, float V_Field_of_View, float Diff_Temp, int Target_Position, int Center_X, int Center_Y, int Signal_Block_Top_Left_X, int Signal_Block_Top_Left_Y, int Signal_Block_Bot_Right_X, int Signal_Block_Bot_Right_Y, int Camera_Selection, float Begin_Temp, float End_Temp, float Temp_Interval, int Amb_Block_Top_Left_X, int Amb_Block_Top_Left_Y, int Amb_Block_Bot_Right_X, int Amb_Block_Bot_Right_Y);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH(ref float NETD, ref int Status);

        #endregion

        #region Laser

        //BEAM DIVERGENCE
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BEAM_DIVERGENCE_LASER_SETUP(int Image_Num_Frames, int Signal_Block_Top_Left_X, int Signal_Block_Top_Left_Y, int Signal_Block_Bot_Right_X, int Signal_Block_Bot_Right_Y, float Camera_Delay_Time, int Camera_Trigger);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BEAM_DIVERGENCE_LASER_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BEAM_DIVERGENCE_LASER_FETCH(ref float Diameter, ref int Status);

        //LASER BORESIGHT
        /// <summary>
        /// Setup for Laser BoreSight Measurement 
        /// </summary>
        /// <param name="Image_Num_Frames"></param>
        /// <param name="Signal_Block_Top_Left_X"></param>
        /// <param name="Signal_Block_Top_Left_Y"></param>
        /// <param name="Signal_Block_Bot_Right_X"></param>
        /// <param name="Signal_Block_Bot_Right_Y"></param>
        /// <param name="Camera_Delay_Time"></param>
        /// <param name="Camera_Trigger"></param>
        /// <param name="Intensity_Ratio"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BORESIGHT_LASER_SETUP(int Image_Num_Frames, int Signal_Block_Top_Left_X, int Signal_Block_Top_Left_Y, int Signal_Block_Bot_Right_X, int Signal_Block_Bot_Right_Y, float Camera_Delay_Time, int Camera_Trigger, float Intensity_Ratio);

        /// <summary>
        /// Send Request for a Laser BoreSight Measurement
        /// </summary>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BORESIGHT_LASER_INITIATE();

        /// <summary>
        /// Get Laser BoreSight Measurement Readings
        /// </summary>
        /// <param name="LBeam_Align_Coord_X"></param>
        /// <param name="LBeam_Align_Coord_Y"></param>
        /// <param name="LBeam_Area"></param>
        /// <param name="Status"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BORESIGHT_LASER_FETCH(ref float LBeam_Align_Coord_X, ref float LBeam_Align_Coord_Y, ref float LBeam_Area, ref int Status);

        //RANGE FINDER ACCURACY
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void RANGE_FINDER_ACCURACY_LASER_SETUP(int Trigger_Mode, int Diode_Selection, float Pulse_Amplitude, float Pulse_Period, float Pulse_Width, int Emulated_Range, int Pulse2_Delay, int Pulse_Select, int Amplitude_Percentage, float Range_Criteria);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void RANGE_FINDER_ACCURACY_LASER_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void RANGE_FINDER_ACCURACY_LASER_FETCH(ref float Range_Error, ref int Status);

        //RECEIVER SENSITIVITY
        /// <summary>
        /// Setup for Laser Range Finder Receiver Sensitivity Measurement
        /// </summary>
        /// <param name="Trigger_Mode"></param>
        /// <param name="Diode_Selection"></param>
        /// <param name="Pulse_Amplitude"></param>
        /// <param name="Pulse_Period"></param>
        /// <param name="Pulse_Width"></param>
        /// <param name="Emulated_Range"></param>
        /// <param name="Pulse2_Delay"></param>
        /// <param name="Pulse_Select"></param>
        /// <param name="Amplitude_Percentage"></param>
        /// <param name="Range_Criteria"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void RECEIVER_SENSITIVITY_LASER_SETUP(int Trigger_Mode, int Diode_Selection, float Pulse_Amplitude, float Pulse_Period, float Pulse_Width, int Emulated_Range, int Pulse2_Delay, int Pulse_Select, int Amplitude_Percentage, float Range_Criteria);

        /// <summary>
        /// Send Request for Lser Range Finder Receiver Sensitivity Measurement
        /// </summary>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void RECEIVER_SENSITIVITY_LASER_INITIATE();

        /// <summary>
        /// Get Laser Range Finder Receiver Sensitivity Measurement Readings
        /// </summary>
        /// <param name="Range_Error"></param>
        /// <param name="Status"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void RECEIVER_SENSITIVITY_LASER_FETCH(ref float Range_Error, ref int Status);

        //PULSE REPETITION FREQUENCY
        /// <summary>
        /// Setup for Laser Pulse Repetition Frequency Measurement
        /// </summary>
        /// <param name="TC_Number_Measurements"></param>
        /// <param name="TC_Trigger_Slope"></param>
        /// <param name="TC_Trigger_Level"></param>
        /// <param name="TC_Delay"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void PULSE_REPETITION_FREQUENCY_LASER_SETUP(int TC_Number_Measurements, int TC_Trigger_Slope, float TC_Trigger_Level, float TC_Delay);

        /// <summary>
        /// Send Request for Laser Pulse Repetition Frequency Measurement
        /// </summary>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void PULSE_REPETITION_FREQUENCY_LASER_INITIATE();

        /// <summary>
        /// Get Laser Pulse Repetition Frequency Measurement Readings
        /// </summary>
        /// <param name="Period_Instability"></param>
        /// <param name="Average_Period"></param>
        /// <param name="Status"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void PULSE_REPETITION_FREQUENCY_LASER_FETCH(ref float Period_Instability, ref float Average_Period, ref int Status);

        //PULSE WIDTH
        /// <summary>
        /// Setup for Laser Pulse Width Measurement
        /// </summary>
        /// <param name="Number_Measurements"></param>
        /// <param name="SC_Input_Channel"></param>
        /// <param name="SC_Input_Range"></param>
        /// <param name="SC_Timebase"></param>
        /// <param name="SC_Trigger_Source"></param>
        /// <param name="SC_Trigger_Slope"></param>
        /// <param name="SC_Trigger_Level"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void PULSE_WIDTH_LASER_SETUP(int Number_Measurements, int SC_Input_Channel, int SC_Input_Range, int SC_Timebase, int SC_Trigger_Source, int SC_Trigger_Slope, float SC_Trigger_Level);

        /// <summary>
        /// Send Request for a Laser Pulse Width Measurement
        /// </summary>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void PULSE_WIDTH_LASER_INITIATE();

        /// <summary>
        /// Get Laser Pulse Width Measurement Readings
        /// </summary>
        /// <param name="Average_Pulse_Width"></param>
        /// <param name="Pulse_Width_Instability"></param>
        /// <param name="Status"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void PULSE_WIDTH_LASER_FETCH(ref float Average_Pulse_Width, ref float Pulse_Width_Instability, ref int Status);

        //PULSE ENERGY
        /// <summary>
        /// Setup for Laser Pulse Energy Measurement
        /// </summary>
        /// <param name="Number_Measurements"></param>
        /// <param name="SC_Input_Channel"></param>
        /// <param name="SC_Input_Range"></param>
        /// <param name="SC_Timebase"></param>
        /// <param name="SC_Trigger_Source"></param>
        /// <param name="SC_Trigger_Slope"></param>
        /// <param name="SC_Trigger_Level"></param>
        /// <param name="Frequency"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void PULSE_ENERGY_MEASUREMENTS_LASER_SETUP(int Number_Measurements, int SC_Input_Channel, int SC_Input_Range, int SC_Timebase, int SC_Trigger_Source, int SC_Trigger_Slope, float SC_Trigger_Level, int Frequency);

        /// <summary>
        /// Send Request for a Laser Pulse Energy Measurement
        /// </summary>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void PULSE_ENERGY_MEASUREMENTS_LASER_INITIATE();

        /// <summary>
        /// Get Laser Pulse Energy Measurement Readings
        /// </summary>
        /// <param name="Energy_Instability"></param>
        /// <param name="Average_Energy"></param>
        /// <param name="Status"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(ref float Energy_Instability, ref float Average_Energy, ref int Status);

        #endregion

        #region Visible

        //BORESIGHT
        /// <summary>
        /// Setup for Visible BoreSight Measurement
        /// </summary>
        /// <param name="lSource"></param>
        /// <param name="lNumFrames"></param>
        /// <param name="sHFieldOfView"></param>
        /// <param name="sVFieldOfView"></param>
        /// <param name="sRadiance"></param>
        /// <param name="lTargetPos"></param>
        /// <param name="lCenterX"></param>
        /// <param name="lCenterY"></param>
        /// <param name="lSBlockTopLeftX"></param>
        /// <param name="lSBlockTopLeftY"></param>
        /// <param name="lSBlockBotRightX"></param>
        /// <param name="lSBlockBotRightY"></param>
        /// <param name="lCameraSelection"></param>
        /// <param name="sColorTemp"></param>
        /// <param name="sIntensityRatio"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BORESIGHT_TV_VIS_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sRadiance, int lTargetPos, int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection, float sColorTemp, float sIntensityRatio);

        /// <summary>
        /// Send Request for a Visible BoreSight Measurement
        /// </summary>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BORESIGHT_TV_VIS_INITIATE();

        /// <summary>
        /// Get Visible BoreSight Measurement Readings
        /// </summary>
        /// <param name="I1"></param>
        /// <param name="I2"></param>
        /// <param name="I3"></param>
        /// <param name="I4"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void BORESIGHT_TV_VIS_FETCH(ref float Boresight_X_Coord, ref float Boresight_Y_Coord, ref float LBeam_Area, ref int Status);

        //GEOMETRIC FIDELITY DISTORTION
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GEOMETRIC_FIDELITY_DISTORTION_VIS_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sRadiance, int lTargetPos, int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection, float sColorTemp, int Number_of_Positions, List<float> Distortion_Positions);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GEOMETRIC_FIDELITY_DISTORTION_VIS_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GEOMETRIC_FIDELITY_DISTORTION_VIS_FETCH(ref float Distortion, ref int Status);

        //CAMERA UNIFORMITY
        /// <summary>
        /// Setup Camera Uniformity Measurement
        /// (sigma / mean * 100)
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Image_Num_Frames"></param>
        /// <param name="H_Field_of_View"></param>
        /// <param name="V_Field_of_View"></param>
        /// <param name="Radiance"></param>
        /// <param name="Target_Position"></param>
        /// <param name="Center_X"></param>
        /// <param name="Center_Y"></param>
        /// <param name="Signal_Block_Top_Left_X"></param>
        /// <param name="Signal_Block_Top_Left_Y"></param>
        /// <param name="Signal_Block_Bot_Right_X"></param>
        /// <param name="Signal_Block_Bot_Right_Y"></param>
        /// <param name="Camera_Selection"></param>
        /// <param name="Color_Temperature"></param>
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void CAMERA_UNIFORMITY_VIS_SETUP(int Source, int Image_Num_Frames, float H_Field_of_View, float V_Field_of_View, float Radiance, int Target_Position, int Center_X, int Center_Y, int Signal_Block_Top_Left_X, int Signal_Block_Top_Left_Y, int Signal_Block_Bot_Right_X, int Signal_Block_Bot_Right_Y, int Camera_Selection, float Color_Temperature);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void CAMERA_UNIFORMITY_VIS_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void CAMERA_UNIFORMITY_VIS_FETCH(ref float Camera_Uniformity, ref int Status);

        //GAIN
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GAIN_VIS_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sRadiance, int lTargetPos, int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection, float sColorTemp, float Begin_Rad, float End_Rad, float Rad_Interval);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GAIN_VIS_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void GAIN_VIS_FETCH(ref float Gain, ref float Dynamic_Range, ref int Status);

        //MINIMUM RESOLVABLE CONTRAST
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MINIMUM_RESOLVABLE_CONTRAST_VIS_SETUP(int Source, int Number_of_Steps, List<float> Starting_Radiance, List<int> Target_Position, List<int> Target_Features);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MINIMUM_RESOLVABLE_CONTRAST_VIS_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MINIMUM_RESOLVABLE_CONTRAST_VIS_FETCH(ref List<float> MRC, ref int Status);

        //MODULATION TRANSFER FUNCTION
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MODULATION_TRANSFER_FUNCTION_VIS_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sRadiance, int lTargetPos, int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection, float sColorTemp, int Orientation, int Pedestal_Filter, int Smoothing, int Num_Freq_Points, int Correction_Curve_Index);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MODULATION_TRANSFER_FUNCTION_VIS_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void MODULATION_TRANSFER_FUNCTION_VIS_FETCH(List<float> MTF_List, ref int Status);

        //SHADES OF GRAY
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SHADES_OF_GRAY_VIS_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sRadiance, int lTargetPos, int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection, float sColorTemp);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SHADES_OF_GRAY_VIS_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void SHADES_OF_GRAY_VIS_FETCH(ref int Grayscale_Count, ref int Status);

        //NOISE EQUIVALENT INPUT
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void NEI_VIS_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sRadiance, int lTargetPos, int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection, float sColorTemp, float Begin_Rad, float End_Rad, float Rad_Interval, int Amb_Block_Top_Left_X, int Amb_Block_Top_Left_Y, int Amb_Block_Bot_Right_X, int Amb_Block_Bot_Right_Y);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void NEI_VIS_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void NEI_VIS_FETCH(ref float NEI, ref int Status);

        //AUTOCOLLIMATE
        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void AUTOCOLLIMATE_VIS_SETUP(int Source, int Image_Num_Frames, float H_Field_of_View, float V_Field_of_View, float Radiance, int Target_Position, int Center_X, int Center_Y, int Signal_Block_Top_Left_X, int Signal_Block_Top_Left_Y, int Signal_Block_Bot_Right_X, int Signal_Block_Bot_Right_Y, int Camera_Selection, float Color_Temperature, float Intensity_Ratio);

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void AUTOCOLLIMATE_VIS_INITIATE();

        [DllImport(@"C:\IRWin2001\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern void AUTOCOLLIMATE_VIS_FETCH(ref float Boresight_X_Coord, ref float Boresight_Y_Coord, ref int Status);

        #endregion

        #endregion

        #endregion

        #region Constants

        public const int OFF = 0;
        public const int ON = 1;

        // Mode Constants
        public const int NONE = 1;
        public const int BBODY_IR = 2;
        public const int VISIBLE = 3;
        public const int VISIBLEALIGN = 4;
        public const int LASER = 5;
        public const int LASERALIGN = 6;

        // Sensor Constants
        public const int ENERGY = 2;
        public const int CAMERA = 3;

        // Source Constants
        public const int IR = 1;
        public const int LAS = 2;

        // Targetwheel Constants
        public const int OPEN = 0;
        public const int PINHOLE = 1;
        public const int PIE = 2;
        public const int BAR_5 = 3;
        public const int BAR_3_8 = 4;
        public const int BAR_2_6 = 5;
        public const int BAR_1_5 = 6;
        public const int BAR_0_3 = 7;
        public const int DIAGONAL = 8;
        public const int MULTI_PINHOLE = 9;
        public const int CROSS = 10;
        public const int RESOLUTION = 11;
        public const int BAR_1_0 = 12;
        public const int BAR_0_6 = 13;
        public const int SQUARE = 14;

        // LaserDiode Constants
        public const int LASER1570 = 0;
        public const int LASER1540 = 1;
        public const int LASER1064 = 2;

        // LaserCal Factor Constants
        public const int CAL1064 = 10;
        public const int CAL1540 = 11;
        public const int CAL1570 = 12;
        
        // LaserTrigger Constants
        public const int ALIGNMENT = 0;
        public const int FREERUN   = 1;
        public const int OPTICAL   = 2;
        public const int EXTERNAL  = 3;
        public const int CALIBRATE = 4;

        // LaserPulseEnergy Constants
        private const int PE1064 = 0;
        private const int PE1540 = 1;
        private const int PE1570 = 2;

        #endregion

        #region Variables
       
        private static readonly float laserEnergyCal1064 = CalFactor_Get(CAL1064);
        private static readonly float laserEnergyCal1540 = CalFactor_Get(CAL1540);
        private static readonly float laserEnergyCal1570 = CalFactor_Get(CAL1570);
        private static string veo2Config;
        private static string veo2Operation;
        private static string veo2Query;
        private static string veo2Status;
        private static int veo2Progress;
        private static int ireturnValue;

        #endregion

        #region Structures

        /// <summary>
        /// Resulting values from a laser pulse-width measurement
        /// </summary>
        public struct PulseWidthAttribues
        {
            /// <summary>
            /// The expected range is 4 to 70 ns.  The actual measurement could be above or below the stated range.
            /// </summary>
            public float AveragePulseWidth;
            /// <summary>
            /// Standard deviation of the pulse-width measurments divided by the AveragePulseWidth
            /// </summary>
            public float PulseWidthInstability;
            /// <summary>
            /// 16bit word indicating type of failure that has occurred
            /// </summary>
            public int Status;
        }

        /// <summary>
        /// Resulting values from a laser pulse-energy measurement
        /// </summary>
        public struct PulseEnergyAttributes
        {
            /// <summary>
            /// Standard deviation of the pulse-energy measurements divided by the AveragePulseEnergy
            /// </summary>
            public float PulseEnergyInstability;
            /// <summary>
            /// The expected range is 3 to 300 mJ.  The actual measurement could be above or below the stated range.
            /// </summary>
            public float AveragePulseEnergy;
            /// <summary>
            /// 16bit word indicating type of failure that has occurred
            /// </summary>
            public int Status;
        }

        #endregion

        #region Properties

        public static string VEO2Config
        {
            get
            {
                return veo2Config;
            }
            set
            {
                if (value != null)
                {
                    veo2Config = value;
                }
            }
        }

        public static string VEO2Operation
        {
            get
            {
                return veo2Operation;
            }
            set
            {
                if (value != null)
                {
                    veo2Operation = value;
                }
            }
        }

        public static string VEO2Query
        {
            get
            {
                return veo2Query;
            }
            set
            {
                if (value != null)
                {
                    veo2Query = value;
                }
            }
        }

        public static string VEO2Status
        {
            get
            {
                return veo2Status;
            }
            set
            {
                if (value != null)
                {
                    veo2Status = value;
                }
            }
        }

        public static int VEO2Progress
        {
            get
            {
                return veo2Progress;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    veo2Progress = value;
                }
                System.Diagnostics.Debug.Print("Progress = " + veo2Progress);
            }
        }

        public static int iReturnValue
        {
            get
            {
                return ireturnValue;
            }
            set
            {
                ireturnValue = value;
            }
        }

        public static double LaserEnergyCal1064
        {
            get
            {
                return laserEnergyCal1064;
            }
        }

        public static double LaserEnergyCal1540
        {
            get
            {
                return laserEnergyCal1540;
            }
        }

        public static double LaserEnergyCal1570
        {
            get
            {
                return laserEnergyCal1570;
            }
        }

        public struct Laser
        {
            public struct PulseWidth
            {
                public const int COUNT_MIN = 1;
                public const int COUNT_MAX = 128;
            }

            public struct PulseEnergy
            {
                public struct WaveLength
                {
                    public const int _1064 = 0;
                    public const int _1540 = 1;
                    public const int _1570 = 2;
                    public const int MIN = _1064;
                    public const int MAX = _1570;
                }
            }
        }
        #endregion


        /// <summary>
        /// Get Laser Energy Probe Cal Factors
        /// <para>10 - 1064nm</para>
        /// <para>11 - 1540nm</para>
        /// <para>12 - 1570nm</para>
        /// </summary>
        /// <param name="WaveLngth"></param>
        /// <returns></returns>
        private static float CalFactor_Get(int WaveLngth)
        {
            int iReturnValue = -1;
            float fFactor = -1;
            string vFactor = "";

            switch (WaveLngth)
            {
                case CAL1064:
                    vFactor = "1064";
                    break;

                case CAL1540:
                    vFactor = "1540";
                    break;

                case CAL1570:
                    vFactor = "1570";
                    break;

                default:
                    break;
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("Getting Cal Factor for " + vFactor + "nm"); }

            try
            {
                GET_CALIBRATION_VALUE_INITIATE(WaveLngth);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (System.AccessViolationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("IrWindows 2001 is busy");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("CalFactor_Get() - GET_CALIBRATION_VALUE_INITIATE Exception: \n" + EEx.Message); }
            }

            try
            {
                GET_CALIBRATION_VALUE_FETCH(ref fFactor);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("CalFactor_Get() - GET_CALIBRATION_VALUE_FETCH Exception: \n" + EEx.Message); }
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("CalFactor_Get() - Calibration Value found " + fFactor.ToString()); }
            return fFactor;
        }

        /// <summary>
        /// Get VEO2 BIT Data
        /// </summary>
        /// <returns></returns>
        public static int BIT_Data_Get()
        {
            int iReturnValue = -1;
            int iData = -1;

            try
            {
                GET_BIT_DATA_INITIATE();
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("BIT_Data_Get() -  GET_BIT_DATA_INITIATE Exception: \n" + EEx.Message); }
            }

            try
            {
                GET_BIT_DATA_FETCH(ref iData);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("BIT_Data_Get() -  GET_BIT_DATA_FETCH Exception: \n" + EEx.Message); }
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("VEO-2 BIT Data Reported As " + iData.ToString()); }

            return iData;
        }

        /// <summary>
        /// Get VEO2 Status Bit Message
        /// </summary>
        /// <returns></returns>
        public static int Status_Get()
        {
            int iReturnValue = -1;
            int iTry = 0;
            int iStatus = -1;
            string vStatus = "";

            while ((iTry < 35) && (iStatus != 641) && (iStatus != 705) && (iStatus != 577))
            {
                iTry++;

                if (Program.debugMode) { Debug.WriteDebugInfo("Checking VEO-2 Status Byte"); }
                try
                {
                    GET_STATUS_BYTE_MESSAGE_INITIATE();
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("Status_Get() - GET_STATUS_BYTE_MESSAGE_INITIATE Exception: \n" + EEx.Message); }
                }


                try
                {
                    GET_STATUS_BYTE_MESSAGE_FETCH(ref iStatus);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("Status_Get() - GET_STATUS_BYTE_MESSAGE_FETCH Exception: \n" + EEx.Message); }
                }

                if (Program.debugMode) { Debug.WriteDebugInfo("VEO-2 Status Byte Reported As " + iStatus.ToString()); }

                if (iStatus == -1)
                {
                    vStatus = "No response";
                }

                switch (iStatus)
                {
                    case 0:
                        vStatus = "Busy (Not Avail-Disabled)";
                        break;

                    case 1:
                        vStatus = "Ready (Not Avail-Disabled)";
                        break;

                    case 64:
                        vStatus = "Busy (Emulated-Not Avail-Disabled)";
                        break;

                    case 65:
                        vStatus = "Ready (Emulated-Not Avail-Disabled)";
                        break;

                    case 128:
                        vStatus = "Busy (Not Avail)";
                        break;

                    case 129:
                        vStatus = "Ready (Not Avail)";
                        break;

                    case 192:
                        vStatus = "Busy (Emulated-Not Avail)";
                        break;

                    case 193:
                        vStatus = "Ready (Emulated-Not Avail)";
                        break;

                    case 512:
                        vStatus = "Busy (Disabled)";
                        break;

                    case 513:
                        vStatus = "Ready (Disabled)";
                        break;

                    case 576:
                        vStatus = "Busy (Emulated-Disabled)";
                        break;

                    case 577:
                        vStatus = "Ready (Emulated-Disabled)";
                        break;

                    case 640:
                        vStatus = "Busy";
                        break;

                    case 641:
                        vStatus = "Ready";
                        break;

                    case 704:
                        vStatus = "Busy (Emulated)";
                        break;

                    case 705:
                        vStatus = "Ready (Emulated)";
                        break;

                    default:
                        vStatus = iStatus.ToString();
                        break;
                }

                if (Program.debugMode) { Debug.WriteDebugInfo("VEO-2 Status: " + vStatus); }
            }
            return iStatus;
        }

        private static void _Mode_Set(object Parameter)
        {
            Dictionary<int, string> dMode = new Dictionary<int, string>() { { 1, "None" },{ 2, "BlackBody" },{ 3, "Visible" },{ 4, "Visible Align" },{ 5, "Laser" },{ 6, "Laser Align" } };

            int Mode = Convert.ToInt32(Parameter);
            int iTry = 0;
            int iMode = -1;

            VEO2Config = "Operational Mode";
            VEO2Operation = "Setting Mode to " + dMode[Mode];
            VEO2Progress = 10;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting VEO-2 Operating Mode to " + dMode[Mode]); }

            // Leave if argument out of range
            if (Mode < NONE || Mode > LASERALIGN)
            {
                return;
            }

            // get current value for system config
            try
            {
                SET_SYSTEM_CONFIGURATION_FETCH(ref iMode);
                VEO2Query = "Querying Current Mode";
                VEO2Progress += 10;
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("ModeSelect() - SET_SYSTEM_CONFIGURATION_INITIATE Exception: \n" + EEx.Message); }
            }

            // if current config is not equal to the desired config
            if (iMode != Mode)
            {
                try
                {
                    SET_SYSTEM_CONFIGURATION_INITIATE(Mode);
                    VEO2Progress += 10;
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("ModeSelect() - SET_SYSTEM_CONFIGURATION_INITIATE Exception: \n" + EEx.Message); }
                }

                while ((iTry <= 30) && (iMode != Mode))
                {
                    System.Threading.Thread.Sleep(1000);
                    VEO2Progress += 1;
                    VEO2Query = "(" + iTry.ToString() + ") Querying Current Mode ...";

                    try
                    {
                        SET_SYSTEM_CONFIGURATION_FETCH(ref iMode);
                    }
                    catch (DllNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("VEO2.dll not found");
                        Console.ResetColor();
                        return;
                    }
                    catch (ExternalException EEx)
                    {
                        Console.WriteLine(EEx.Message);
                        if (Program.debugMode) { Debug.WriteDebugInfo("ModeSelect() - SET_SYSTEM_CONFIGURATION_FETCH Exception: \n" + EEx.Message); }
                    }

                    if (Program.debugMode) { Debug.WriteDebugInfo("VEO-2 Operating Mode is set to " + iMode.ToString()); }
                    VEO2Progress += 1;
                    VEO2Status = "Current Mode is " + dMode[iMode];
                    iTry++;
                }
            }

            VEO2Progress = 99;    

            if (iMode == Mode)
            {
                VEO2Status = "Success";
                iReturnValue = 0;
            }
            else
            {
                VEO2Status = "Failed";
                System.Threading.Thread.Sleep(2000);
            }

            return;
        }

        /// <summary>
        /// Set VEO2 Operation Mode
        /// <para>1 - None</para>
        /// <para>2 - Blackbody</para>
        /// <para>3 - Visible</para>
        /// <para>4 - Visible Align</para>
        /// <para>5 - Laser</para>
        /// <para>6 - Laser Align</para>
        /// </summary>
        /// <param name="Mode">Integer Value (1 thru 6)</param>
        /// <returns></returns>
        public static int Mode_Set(int Mode)
        {

            iReturnValue = -1;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(_Mode_Set));
            thread.Start(Mode);
            System.Diagnostics.Debug.Print("Thread Started");

            frmStatus statusForm = new frmStatus();
            statusForm.Show();
            System.Diagnostics.Debug.Print("Form Opened");

            while(thread.IsAlive)
            {
                //statusForm.Update();
                Application.DoEvents();
            }

            statusForm.Close();
            statusForm.Dispose();

            System.Diagnostics.Debug.Print("Returning Value");
            return iReturnValue;
        }

        private static void _Sensor_Set(object Parameter)
        {
            Dictionary<int, string> dCEPSA = new Dictionary<int, string>() { { -1, "Not Homed" }, { 0, "Unknown" }, { 1, "Open" }, { 2, "Energy Probe" }, { 3, "Camera/Beamsplitter" } };

            int iTry = 0;
            int iCEPSA = -1;
            int CEPSA = Convert.ToInt32(Parameter);

            VEO2Config = "Sensor Stage";
            VEO2Operation = "Setting Stage to " + dCEPSA[CEPSA] + " Position";
            VEO2Progress = 10;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting CEPSA to " + dCEPSA[CEPSA] + " Position"); }

            // Leave if argument out of range
            if (CEPSA < NONE || CEPSA > CAMERA)
            {
                return;
            }

            // get current value for sensor stage
            try
            {
                SET_SENSOR_STAGE_LASER_FETCH(ref iCEPSA);
                VEO2Query = "Querying Current Position";
                VEO2Progress += 10;
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("SensorSelect() - SET_SENSOR_STAGE_LASER_FETCH Exception: \n" + EEx.Message); }
            }

            // if current stage pos is not equal to the desired stage pos
            if (iCEPSA != CEPSA)
            {
                try
                {
                    SET_SENSOR_STAGE_LASER_INITIATE(CEPSA);
                    VEO2Progress += 10;
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("SensorSelect() - SET_SENSOR_STAGE_LASER_INITIATE Exception: \n" + EEx.Message); }
                }

                while ((iTry <= 20) && (iCEPSA != CEPSA))
                {
                    System.Threading.Thread.Sleep(1000);
                    VEO2Progress += 1;
                    VEO2Query = "(" + iTry.ToString() + ") Querying Current Position ...";

                    try
                    {
                        SET_SENSOR_STAGE_LASER_FETCH(ref iCEPSA);
                    }
                    catch (ExternalException EEx)
                    {
                        Console.WriteLine(EEx.Message);
                        if (Program.debugMode) { Debug.WriteDebugInfo("SensorSelect() - SET_SENSOR_STAGE_LASER_FETCH Exception: \n" + EEx.Message); }
                    }

                    if (Program.debugMode) { Debug.WriteDebugInfo("VEO-2 Sensor Stage is set to " + iCEPSA.ToString()); }
                    VEO2Progress += 2;
                    VEO2Status = "Current Position is " + dCEPSA[iCEPSA];
                    iTry++;
                }
            }

            VEO2Progress = 99;

            if (iCEPSA == CEPSA)
            {
                VEO2Status = "Success";
                iReturnValue = 0;
            }
            else
            {
                VEO2Status = "Failed";
                System.Threading.Thread.Sleep(2000);
            }

            return;
        }

        /// <summary>
        /// Set VEO2 Sensor Stage Position
        /// <para>1 - Open</para>
        /// <para>2 - Energy Probe</para> 
        /// <para>3 - Camera/Beamsplitter</para>
        /// </summary>
        /// <param name="CEPSA">Integer Value (1 thru 3)</param>
        /// <returns></returns>
        public static int Sensor_Set(int CEPSA)
        {
            iReturnValue = -1;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(_Sensor_Set));
            thread.Start(CEPSA);
            System.Diagnostics.Debug.Print("Thread Started");

            frmStatus statusForm = new frmStatus();
            statusForm.Show();
            System.Diagnostics.Debug.Print("Form Opened");

            while (thread.IsAlive)
            {
                //statusForm.Update();
                Application.DoEvents();
            }

            statusForm.Close();
            statusForm.Dispose();

            System.Diagnostics.Debug.Print("Returning Value");
            return iReturnValue;
        }

        private static void _Source_Set(object Parameter)
        {
            Dictionary<int, string> dSSA = new Dictionary<int, string>() { { -1, "Not Homed" }, { 0, "Unknown" }, { 1, "Blackbody/IR" }, { 2, "Laser Boresight" }, { 3, "Visible Source" } };

            int iTry = 0;
            int iSSA = -1;
            int SSA = Convert.ToInt32(Parameter);

            VEO2Config = "Source Stage";
            VEO2Operation = "Setting Stage to " + dSSA[SSA] + " Position";
            VEO2Progress = 10;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting SSA to " + dSSA + " Position"); }

            // Leave if argument out of range
            if (SSA < IR || SSA > VISIBLE)
            {
                return;
            }

            // Get current stage position
            try
            {
                SET_SOURCE_STAGE_LASER_FETCH(ref iSSA);
                VEO2Query = "Querying Current Position";
                VEO2Progress += 10;

            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("SourceSelect() - SET_SOURCE_STAGE_LASER_FETCH Exception: \n" + EEx.Message); }
            }

            // If current stage pos not equal to desired stage pos
            if (iSSA != SSA)
            {
                try
                {
                    SET_SOURCE_STAGE_LASER_INITIATE(SSA);
                    VEO2Progress += 10;
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("SourceSelect() - SET_SOURCE_STAGE_LASER_INITIATE Exception: \n" + EEx.Message); }
                }

                while ((iTry <= 20) && (iSSA != SSA))
                {
                    System.Threading.Thread.Sleep(1000);
                    VEO2Progress += 1;
                    VEO2Query = "(" + iTry.ToString() + ") Querying Current Position ...";

                    try
                    {
                        SET_SOURCE_STAGE_LASER_FETCH(ref iSSA);
                    }
                    catch (DllNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("VEO2.dll not found");
                        Console.ResetColor();
                        return;
                    }
                    catch (ExternalException EEx)
                    {
                        Console.WriteLine(EEx.Message);
                        if (Program.debugMode) { Debug.WriteDebugInfo("SourceSelect() - SET_SOURCE_STAGE_LASER_FETCH Exception: \n" + EEx.Message); }
                    }

                    if (Program.debugMode) { Debug.WriteDebugInfo("VEO-2 Source Stage is set to " + iSSA.ToString()); }
                    VEO2Progress += 2;
                    VEO2Status = "Current Position is " + dSSA[iSSA];
                    iTry++;
                }
            }

            VEO2Progress = 99;

            if (iSSA == SSA)
            {
                VEO2Status = "Success";
                iReturnValue = 0;
            }
            else
            {
                VEO2Status = "Failed";
                System.Threading.Thread.Sleep(2000);
            }

            return;
        }

        /// <summary>
        /// Set VEO2 Source Stage Position
        /// <para>1 - Blackbody/IR</para> 
        /// <para>2 - Laser Boresight</para>
        /// <para>3 - Visible Source</para>
        /// </summary>
        /// <param name="SSA"></param>
        /// <returns></returns>
        public static int Source_Set(int SSA)
        {
            iReturnValue = -1;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(_Source_Set));
            thread.Start(SSA);
            System.Diagnostics.Debug.Print("Thread Started");

            frmStatus statusForm = new frmStatus();
            statusForm.Show();
            System.Diagnostics.Debug.Print("Form Opened");

            while (thread.IsAlive)
            {
                //statusForm.Update();
                Application.DoEvents();
            }

            statusForm.Close();
            statusForm.Dispose();

            System.Diagnostics.Debug.Print("Returning Value");
            return iReturnValue;
        }

        private static void _Target_Set(object Parameter)
        {
            Dictionary<int, string> dTarget = new Dictionary<int, string>() { { -1, "Not Homed" }, { 0, "Open Target" }, { 1, "Pinhole" }, { 2, "Pie Sector" },
                { 3, "5.00 Cyc/mrad 4 bar" }, { 4, "3.8325 Cyc/mrad 4 bar" }, { 5, "2.665 Cyc/mrad 4 bar" }, { 6, "1.4975 Cyc/mrad 4 bar" }, { 7, "0.33 Cyc/mrad 4 bar"},
                { 8, "Diagonal Slit"}, { 9, "Multi Pinhole" }, { 10, "Alignment Cross" }, { 11, "USAF 1951, Groups 0-7" }, { 12, "0.93 Cyc/mrad 4 bar" }, { 13, "0.6122 Cyc/mrad 4 bar" },
                { 14, "Square" } };

            int iTry = 0;
            int iTarget = -1;
            int TGT = Convert.ToInt32(Parameter);

            VEO2Config = "Target Wheel";
            VEO2Operation = "Setting Target Wheel to " + dTarget[TGT] + " Position";
            VEO2Progress = 10;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting Target Wheel to " + dTarget[TGT] + " Position"); }

            // Leave if argument out of range
            if (TGT < OPEN || TGT > SQUARE)
            {
                return;
            }

            // Get current tgt position
            try
            {
                SET_TARGET_POSITION_FETCH(ref iTarget);
                VEO2Query = "Querying Current Position";
                VEO2Progress += 10;

            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("TargetWheel_Set() - SET_TARGET_POSITION_FETCH Exception: \n" + EEx.Message); }
            }

            // If current tgt position not equal to desired tgt position
            if (iTarget != TGT)
            {
                try
                {
                    SET_TARGET_POSITION_INITIATE(TGT);
                    VEO2Progress += 10;
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("TargetWheel_Set() - SET_TARGET_POSITION_INITIATE Exception: \n" + EEx.Message); }
                }

                while ((iTry <= 30) && (iTarget != TGT))
                {
                    System.Threading.Thread.Sleep(2000);
                    VEO2Progress += 1;
                    VEO2Query = "(" + iTry.ToString() + ") Querying Current Position ...";

                    try
                    {
                        SET_TARGET_POSITION_FETCH(ref iTarget);
                    }
                    catch (DllNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("VEO2.dll not found");
                        Console.ResetColor();
                        return;
                    }
                    catch (ExternalException EEx)
                    {
                        Console.WriteLine(EEx.Message);
                        if (Program.debugMode) { Debug.WriteDebugInfo("TargetWheel_Set() - SET_TARGET_POSITION_FETCH Exception: \n" + EEx.Message); }
                    }

                    if (Program.debugMode) { Debug.WriteDebugInfo("VEO-2 Target Wheel is set to " + iTarget.ToString()); }
                    VEO2Progress += 1;
                    VEO2Status = "Current Position is " + dTarget[iTarget];
                    iTry++;
                }
            }

            VEO2Progress = 99;

            if (iTarget == TGT)
            {
                VEO2Status = "Success";
                iReturnValue = 0;
            }
            else
            {
                VEO2Status = "Failed";
                System.Threading.Thread.Sleep(2000);
            }

            return;
        }

        /// <summary>
        /// Set VEO2 Target Wheel Position
        /// <para>0 - Open Target</para>
        /// <para>1 - Pinhole</para>
        /// <para>2 - Pie Sector</para>
        /// <para>3 - 4 Bar (5.00 Cyc/mRad)</para>
        /// </summary>
        /// <param name="TGT">Integer Value (0 thru 14)</param>
        /// <returns></returns>
        public static int Target_Set(int TGT)
        {
            iReturnValue = -1;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(_Target_Set));
            thread.Start(TGT);
            System.Diagnostics.Debug.Print("Thread Started");

            frmStatus statusForm = new frmStatus();
            statusForm.Show();
            System.Diagnostics.Debug.Print("Form Opened");

            while (thread.IsAlive)
            {
                //statusForm.Update();
                Application.DoEvents();
            }

            statusForm.Close();
            statusForm.Dispose();

            System.Diagnostics.Debug.Print("Returning Value");
            return iReturnValue;
        }

        /// <summary>
        /// Set PPLS Active Laser Diode
        /// <para>0 - 1570nm</para>
        /// <para>1 - 1540nm</para>
        /// <para>2 - 1064nm</para>
        /// </summary>
        /// <param name="Diode"></param>
        /// <returns></returns>
        public static int LaserDiode_Set(int Diode)
        {
            int iReturnValue = -1;
            int iTry = 0;
            int iDiode = -1;
            string vDiode = "";

            switch (Diode)
            {
                case LASER1570:
                    vDiode = "1570";
                    break;

                case LASER1540:
                    vDiode = "1540";
                    break;

                case LASER1064:
                    vDiode = "1064";
                    break;

                default:
                    vDiode = "unknown";
                    break;
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting Laser Diode to " + vDiode + "nm"); }


            // Leave if argument out of range
            if (Diode < LASER1064 || Diode > LASER1570)
            {
                return iReturnValue;
            }

            try
            {
                SELECT_DIODE_LASER_INITIATE(Diode);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("LaserDiode_Set() - SELECT_DIODE_LASER_INITIATE Exception: \n" + EEx.Message); }
            }

            while ((iTry <= 20) && (iDiode != Diode))
            {
                System.Threading.Thread.Sleep(1000);

                try
                {
                    SELECT_DIODE_LASER_FETCH(ref iDiode);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("LaserDiode_Set() - SELECT_DIODE_LASER_FETCH Exception: \n" + EEx.Message); }
                }

                iTry++;
            }

            if (iDiode == Diode)
            {
                iReturnValue = 0;
            }

            return iReturnValue;
        }

        /// <summary>
        /// Set PPLS Laser Test Mode
        /// <para>0 - Off</para>
        /// <para>1 - On</para>
        /// </summary>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public static int LaserTest_Set(int Operation)
        {
            int iReturnValue = -1;
            int iTry = 0;
            int iOp = -1;
            string vOp = "";

            switch (Operation)
            {
                case OFF:
                    vOp = "Off";
                    break;

                case ON:
                    vOp = "On";
                    break;

                default:
                    vOp = "unknown";
                    break;
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting Laser Test Mode to " + vOp); }


            // Leave if argument out of range
            if (Operation < OFF || Operation > ON)
            {
                return iReturnValue;
            }

            try
            {
                SET_LASER_TEST_INITIATE(Operation);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("LaserTest_Set() - SET_LASER_TEST_INITIATE Exception: \n" + EEx.Message); }
            }

            while ((iTry <= 20) && (iOp != Operation))
            {
                System.Threading.Thread.Sleep(1000);

                try
                {
                    SET_LASER_TEST_FETCH(ref iOp);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("LaserTest_Set() - SET_LASER_TEST_FETCH Exception: \n" + EEx.Message); }
                }

                iTry++;
            }

            if (iOp == Operation)
            {
                iReturnValue = 0;
            }

            return iReturnValue;
        }

        /// <summary>
        /// Set PPLS Laser Trigger Source
        /// <para>0 - Alignment</para>
        /// <para>1 - Free Run</para>
        /// <para>2 - Optical</para>
        /// <para>3 - External</para>
        /// <para>4 - Calibration</para>
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static int LaserTrigger_Set(int Source)
        {
            int iReturnValue = -1;
            int iTry = 0;
            int iSource = -1;
            string vSource = "";

            switch (Source)
            {
                case ALIGNMENT:
                    vSource = "Alignment";
                    break;

                case FREERUN:
                    vSource = "Free Run";
                    break;

                case OPTICAL:
                    vSource = "Optical";
                    break;

                case EXTERNAL:
                    vSource = "External";
                    break;

                case CALIBRATE:
                    vSource = "Calibrate";
                    break;

                default:
                    vSource = "unknown";
                    break;
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting Laser Trigger Source to " + vSource); }


            // Leave if argument out of range
            if (Source < ALIGNMENT || Source > CALIBRATE)
            {
                return iReturnValue;
            }

            try
            {
                SET_TRIGGER_SOURCE_LASER_INITIATE(Source);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("LaserTrigger_Set() - SET_TRIGGER_SOURCE_LASER_INITIATE Exception: \n" + EEx.Message); }
            }

            while ((iTry <= 20) && (iSource != Source))
            {
                System.Threading.Thread.Sleep(1000);

                try
                {
                    SET_TRIGGER_SOURCE_LASER_FETCH(ref iSource);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("LaserTrigger_Set() - SET_TRIGGER_SOURCE_LASER_FETCH Exception: \n" + EEx.Message); }
                }

                iTry++;
            }

            if (iSource == Source)
            {
                iReturnValue = 0;
            }

            return iReturnValue;

        }

        /// <summary>
        /// Set Laser Operation
        /// <para>0 - Off</para>
        /// <para>1 - On</para>
        /// </summary>
        /// <param name="Operation"></param>
        /// <returns></returns>
        public static int LaserOperation_Set(int Operation)
        {
            int iReturnValue = -1;
            int iTry = 0;
            int iOp = -1;
            string vOp = "";

            switch (Operation)
            {
                case OFF:
                    vOp = "Off";
                    break;

                case ON:
                    vOp = "On";
                    break;

                default:
                    vOp = "unknown";
                    break;
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting Laser Operation to " + vOp); }


            // Leave if argument out of range
            if (Operation < OFF || Operation > ON)
            {
                return iReturnValue;
            }

            try
            {
                SET_OPERATION_LASER_INITIATE(Operation);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("LaserOperation_Set() - SET_OPERATION_LASER_INITIATE Exception: \n" + EEx.Message); }
            }

            while ((iTry <= 20) && (iOp != Operation))
            {
                System.Threading.Thread.Sleep(1000);

                try
                {
                    SET_OPERATION_LASER_FETCH(ref iOp);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("LaserOperation_Set() - SET_OPERATION_LASER_FETCH Exception: \n" + EEx.Message); }
                }

                iTry++;
            }

            if (iOp == Operation)
            {
                iReturnValue = 0;
            }

            return iReturnValue;
        }

        public static PulseWidthAttribues PulseWidth_Measure(int NumberMeasurements, int SCInputChannel, int SCInputRange, int SCTimebase, int SCTriggerSource, int SCTriggerSlope, float SCTriggerLevel)
        {
            PulseWidthAttribues pulseWidthAttributes = new PulseWidthAttribues();
            pulseWidthAttributes.AveragePulseWidth = -1;
            pulseWidthAttributes.PulseWidthInstability = -1;
            pulseWidthAttributes.Status = -1;

            if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseWidth_Measure() - Configuring Laser Pulse Width Measurement"); }
            if (!Program.bATLAS) { Console.WriteLine("- Configuring Laser Pulse Width Measurement"); }

            // SETUP MEASUREMENT
            try
            {
                PULSE_WIDTH_LASER_SETUP(NumberMeasurements, SCInputChannel, SCInputRange, SCTimebase, SCTriggerSource, SCTriggerSlope, SCTriggerLevel);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return pulseWidthAttributes;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseWidth_Measure() - PULSE_WIDTH_LASER_SETUP Exception: \n" + EEx.Message); }
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseWidth_Measure() - Initiating Laser Pulse Width Measurement"); }
            if (!Program.bATLAS) { Console.WriteLine("- Initiating Laser Pulse Width Measurement"); }

            // INITIATE MEASUREMENT
            try
            {
                PULSE_WIDTH_LASER_INITIATE();
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseWidth_Measure() - PULSE_WIDTH_LASER_INITIATE Exception: \n" + EEx.Message); }
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseWidth_Measure() - Fetching Laser Pulse Width Measurement"); }
            if (!Program.bATLAS) { Console.WriteLine("- Fetching Laser Pulse Width Measurement Results"); }

            // FETCH RESULTS
            try
            {
                PULSE_WIDTH_LASER_FETCH(ref pulseWidthAttributes.AveragePulseWidth, ref pulseWidthAttributes.PulseWidthInstability, ref pulseWidthAttributes.Status);
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseWidth_Measure() - PULSE_WIDTH_LASER_FETCH Exception: \n" + EEx.Message); }
            }

            return pulseWidthAttributes;
        }

        public static PulseEnergyAttributes PulseEnergy_Measure(int NumberMeasurements, int SCInputChannel, int SCInputRange, int SCTimebase, int SCTriggerSource, int SCTriggerSlope, float SCTriggerLevel, int Frequency)
        {
            PulseEnergyAttributes pulseEnergyAttributes = new PulseEnergyAttributes();
            pulseEnergyAttributes.PulseEnergyInstability = -1;
            pulseEnergyAttributes.AveragePulseEnergy     = -1;
            pulseEnergyAttributes.Status                 = -1;

            if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseEnergy_Measure() - Configuring Laser Pulse Energy Measurement"); }
            if (!Program.bATLAS) { Console.WriteLine("- Configuring Laser Pulse Energy Measurement"); }

            // SETUP MEASUREMENT
            try
            {
                PULSE_ENERGY_MEASUREMENTS_LASER_SETUP(NumberMeasurements, SCInputChannel, SCInputRange, SCTimebase, SCTriggerSource, SCTriggerSlope, SCTriggerLevel, Frequency);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return pulseEnergyAttributes;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseEnergy_Measure() - PULSE_ENERGY_MEASUREMENTS_LASER_SETUP Exception: \n" + EEx.Message); }
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseEnergy_Measure() - Initiating Laser Pulse Energy Measurement"); }
            if (!Program.bATLAS) { Console.WriteLine("- Initiating Laser Pulse Energy Measurement"); }

            // INITIATE MEASUREMENT
            try
            {
                PULSE_ENERGY_MEASUREMENTS_LASER_INITIATE();
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseEnergy_Measure() - PULSE_ENERGY_MEASUREMENTS_LASER_INITIATE Exception: \n" + EEx.Message); }
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseEnergy_Measure() - Fetching Laser Pulse Energy Measurement"); }
            if (!Program.bATLAS) { Console.WriteLine("- Fetching Laser Pulse Energy Measurement Results"); }

            // FETCH RESULTS
            try
            {
                PULSE_ENERGY_MEASUREMENTS_LASER_FETCH(ref pulseEnergyAttributes.PulseEnergyInstability, ref pulseEnergyAttributes.AveragePulseEnergy, ref pulseEnergyAttributes.Status);
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.PulseEnergy_Measure() - PULSE_ENERGY_MEASUREMENTS_LASER_FETCH Exception: \n" + EEx.Message); }
            }

            return pulseEnergyAttributes;
        }

        /// <summary>
        /// Set LARRS AZ Stage Position
        /// </summary>
        /// <param name="Pos"></param>
        /// <returns></returns>
        public static int LARRS_AZ_Set(int Pos)
        {
            int iReturnValue = -1;
            int iTry = 0;
            int iPos = -1;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting LaRRS Azimuth Stage to " + Pos.ToString()); }

            try
            {
                SET_LARRS_AZ_LASER_INITIATE(Pos);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("LARRS_AZ_Set() - SET_LARRS_AZ_LASER_INITIATE Exception: \n" + EEx.Message); }
            }

            while ((iTry <= 20) && (iPos != Pos))
            {
                System.Threading.Thread.Sleep(1000);
                try
                {
                    SET_LARRS_AZ_LASER_FETCH(ref iPos);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("LARRS_AZ_Set() - SET_LARRS_AZ_LASER_FETCH Exception: \n" + EEx.Message); }
                }

                iTry++;
            }

            if (iPos == Pos)
            {
                iReturnValue = 0;
            }

            return iReturnValue;
        }

        /// <summary>
        /// Set LARRS EL Stage Position
        /// </summary>
        /// <param name="Pos"></param>
        /// <returns></returns>
        public static int LARRS_EL_Set(int Pos)
        {
            int iReturnValue = -1;
            int iTry = 0;
            int iPos = -1;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting LaRRS Elevation Stage to " + Pos.ToString()); }

            try
            {
                SET_LARRS_EL_LASER_INITIATE(Pos);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("LARRS_EL_Set() - SET_LARRS_EL_LASER_INITIATE Exception: \n" + EEx.Message); }
            }

            while ((iTry <= 20) && (iPos != Pos))
            {
                System.Threading.Thread.Sleep(1000);
                try
                {
                    SET_LARRS_EL_LASER_FETCH(ref iPos);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("LARRS_EL_Set() - SET_LARRS_EL_LASER_FETCH Exception: \n" + EEx.Message); }
                }

                iTry++;
            }

            if (iPos == Pos)
            {
                iReturnValue = 0;
            }

            return iReturnValue;
        }

        /// <summary>
        /// Set LARRS Polarizer Stage Position
        /// <para>0 thru 360 degrees</para>
        /// </summary>
        /// <param name="Pos">Integer Value (0 thru 360)</param>
        /// <returns></returns>
        public static int LARRS_Polarizer_Set(int Pos)
        {
            int iReturnValue = -1;
            int iTry = 0;
            int iPos = -1;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting LaRRS Polarizer Stage to " + Pos.ToString()); }

            // Leave if argument out of range
            if (Pos < 0 || Pos > 360)
            {
                return iReturnValue;
            }

            try
            {
                SET_LARRS_POLARIZE_LASER_INITIATE(Pos);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("LARRS_Polarizer_Set() - SET_LARRS_POLARIZE_LASER_INITIATE Exception: \n" + EEx.Message); }
            }

            while ((iTry <= 20) && (iPos != Pos))
            {
                System.Threading.Thread.Sleep(1000);
                try
                {
                    SET_LARRS_POLARIZE_LASER_FETCH(ref iPos);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("LARRS_Polarizer_Set() - SET_LARRS_POLARIZE_LASER_FETCH Exception: \n" + EEx.Message); }
                }
                iTry++;
            }

            if (iPos == Pos)
            {
                iReturnValue = 0;
            }

            return iReturnValue;
        }

        private static void _Radiance_Set(object Parameter)
        {

            int iTry = 0;
            float fRad = -1f;
            const float MIN = 0f;
            const float MAX = 5000f;
            const float READYWINDOW = 0.01f;
            const int MAXTRIES = 45;
            float Rad = Convert.ToSingle(Parameter);

            VEO2Config = "Radiance";
            VEO2Operation = "Setting Visible Source Radiance to " + Rad.ToString() + " uW/cm2/sr";
            VEO2Progress = 5;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting Visible Source Radiance to " + Rad.ToString() + " uW/cm2/sr"); }

            // Leave if argument out of range
            if (Rad < MIN || Rad > MAX)
            {
                return;
            }

            try
            {
                SET_RADIANCE_VIS_INITIATE(Rad);
                VEO2Progress += 5;
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("Radiance_Set() - SET_RADIANCE_VIS_INITIATE Exception: \n" + EEx.Message); }
            }

            while ((iTry <= MAXTRIES) && ((Math.Abs(fRad - Rad)) > (Rad * READYWINDOW)))
            {
                System.Threading.Thread.Sleep(3000);
                VEO2Progress += 1;
                VEO2Query = "(" + iTry.ToString() + ") Querying Current Radiance ...";

                try
                {
                    SET_RADIANCE_VIS_FETCH(ref fRad);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("Radiance_Set() - SET_RADIANCE_VIS_FETCH Exception: \n" + EEx.Message); }
                }

                if (Program.debugMode) { Debug.WriteDebugInfo("Radiance is " + fRad.ToString()); }
                VEO2Progress += 1;
                VEO2Status = "Current Radiance is " + fRad.ToString() + " uW/cm2/sr";

                iTry++;
            }

            VEO2Progress = 99;

            if ((Math.Abs(fRad - Rad)) <= (Rad * READYWINDOW))
            {
                VEO2Status = "Success";
                iReturnValue = 0;
            }
            else
            {
                VEO2Status = "Failed";
                System.Threading.Thread.Sleep(2000);
            }

            return;
        }

        /// <summary>
        /// Set Visible Source Radiance
        /// <para>0 thru 5000 uw/cm2/sr</para>
        /// </summary>
        /// <param name="Rad"></param>
        /// <returns></returns>
        public static int Radiance_Set(float Rad)
        {
            iReturnValue = -1;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(_Radiance_Set));
            thread.Start(Rad);
            System.Diagnostics.Debug.Print("Thread Started");

            frmStatus statusForm = new frmStatus();
            statusForm.Show();
            System.Diagnostics.Debug.Print("Form Opened");

            while (thread.IsAlive)
            {
                //statusForm.Update();
                Application.DoEvents();
            }

            statusForm.Close();
            statusForm.Dispose();

            System.Diagnostics.Debug.Print("Returning Value");
            return iReturnValue;
        }

        /// <summary>
        /// Get IR Source Ready Window Setting
        /// <para>0.005 thru 5.000 degrees C</para>
        /// </summary>
        /// <returns></returns>
        private static float IRReadyWindow_Get()
        {
            float fRdyWindow = -1f;

            if (Program.debugMode) { Debug.WriteDebugInfo("Getting IR Ready Window"); }

            try
            {
                SET_RDY_WINDOW_IR_FETCH(ref fRdyWindow);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return fRdyWindow;
            }
            catch (System.AccessViolationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("IrWindows 2001 is busy");
                Console.ResetColor();
                return fRdyWindow;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("IRReadyWindow_Get() - SET_RDY_WINDOW_IR_FETCH Exception: \n" + EEx.Message); }
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("IRReadyWindow_Get() - Ready Window Value found " + fRdyWindow.ToString()); }

            return fRdyWindow;
        }

        /// <summary>
        /// Set IR Source Ready Window Setting
        /// <para>0.005 thru 5.000 degrees C</para>
        /// </summary>
        /// <param name="ReadyWindow"></param>
        /// <returns></returns>
        private static int IRReadyWindow_Set(float ReadyWindow)
        {
            int iReturnValue = -1;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting IR Ready Window to " + ReadyWindow.ToString() + " degrees C"); }

            try
            {
                SET_RDY_WINDOW_IR_INITIATE(ReadyWindow);

                iReturnValue = 0;
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (System.AccessViolationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("IrWindows 2001 is busy");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("IRReadyWindow_Set() - SET_RDY_WINDOW_IR_INITIATE Exception: \n" + EEx.Message); }
            }

            return iReturnValue;
        }

        private static void _DifferentialTemp_Set(object Parameter)
        {

            int iTry = 0;
            float Temp = Convert.ToSingle(Parameter);
            decimal dTemp = Convert.ToDecimal(Temp);
            float fTemp = -1f;
            float fReadyWindow = 0.01f;
            const float MIN = -15f;
            const float MAX = 35f;
            const int MAXTRIES = 45;

            VEO2Config = "Differential Temperature";
            VEO2Operation = "Setting Blackbody Temp to " + Temp.ToString() + " degrees C";
            VEO2Progress = 5;


            // Leave if argument out of range
            if (Temp < MIN || Temp > MAX)
            {
                return;
            }

            // Set IR Ready Window Based on Commanded Temperature Value
            if (GetSignificantDigits(dTemp) > 2)
            {
                fReadyWindow = 0.001f;
            }

            iReturnValue = IRReadyWindow_Set(fReadyWindow);

            if (iReturnValue < 0)
            {
                if (Program.debugMode) { Debug.WriteDebugInfo("Error Occurred setting IR Ready Window to " + fReadyWindow.ToString() + " degrees C"); }
                return;
            }

            // Re-initialize Return Value Variable
            iReturnValue = -1;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting Differential Temperature to " + Temp.ToString() + " degrees C"); }

            try
            {
                SET_TEMP_DIFFERENTIAL_IR_INITIATE(Temp);
                VEO2Progress += 5;
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("DifferentialTemp_Set() - SET_TEMP_DIFFERENTIAL_IR_INITIATE Exception: \n" + EEx.Message); }
                return;
            }

            while ((iTry <= MAXTRIES) && ((Math.Abs(fTemp - Temp)) > fReadyWindow))
            {
                System.Threading.Thread.Sleep(5000);
                VEO2Progress += 1;
                VEO2Query = "(" + iTry.ToString() + ") Querying Current Differential Temp ...";

                try
                {
                    SET_TEMP_DIFFERENTIAL_IR_FETCH(ref fTemp);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("DifferentialTemp_Set() - SET_TEMP_DIFFERENTIAL_IR_FETCH Exception: \n" + EEx.Message); }
                }

                if (Program.debugMode) { Debug.WriteDebugInfo("Diff Temp is " + fTemp.ToString()); }
                VEO2Progress += 1;
                VEO2Status = "Current Diff Temp is " + fTemp.ToString() + " degrees C";

                iTry++;
            }

            VEO2Progress = 99;

            if ((Math.Abs(fTemp - Temp)) <= fReadyWindow)
            {
                VEO2Status = "Success";
                iReturnValue = 0;
            }
            else
            {
                VEO2Status = "Failed";
                System.Threading.Thread.Sleep(2000);
            }

            return;
        }

        /// <summary>
        /// Set Differential Temperature using IR Source
        /// <para>-15 to 35 degrees C</para>
        /// </summary>
        /// <param name="Temp"></param>
        /// <returns></returns>
        public static int DifferentialTemp_Set(float Temp)
        {
            iReturnValue = -1;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(_DifferentialTemp_Set));
            thread.Start(Temp);
            System.Diagnostics.Debug.Print("Thread Started");

            frmStatus statusForm = new frmStatus();
            statusForm.Show();
            System.Diagnostics.Debug.Print("Form Opened");

            while (thread.IsAlive)
            {
                //statusForm.Update();
                Application.DoEvents();
            }

            statusForm.Close();
            statusForm.Dispose();

            System.Diagnostics.Debug.Print("Returning Value");
            return iReturnValue;
        }

        /// <summary>
        /// Set Absolute Temperature using IR Source
        /// <para>10 thru 60 degrees C</para>
        /// </summary>
        /// <param name="Temp"></param>
        /// <returns></returns>
        public static int AbsoluteTemp_Set(float Temp)
        {
            int iReturnValue = -1;
            int iTry = 0;
            decimal dTemp = Convert.ToDecimal(Temp);
            float fTemp = -1f;
            float fReadyWindow = 0.01f;
            const float MIN = 10f;
            const float MAX = 60f;
            const int MAXTRIES = 90;

            // Set IR Ready Window Based on Commanded Temperature Value
            if (GetSignificantDigits(dTemp) > 2)
            {
                fReadyWindow = 0.001f;
            }

            iReturnValue = IRReadyWindow_Set(fReadyWindow);

            if (iReturnValue < 0)
            {
                if (Program.debugMode) { Debug.WriteDebugInfo("Error Occurred setting IR Ready Window to " + fReadyWindow.ToString() + " degrees C"); }
                return iReturnValue;
            }

            // Re-initialize Return Value Variable
            iReturnValue = -1;

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting Absolute Temperature to " + Temp.ToString() + " degrees C"); }

            // Leave if argument out of range
            if (Temp < MIN || Temp > MAX)
            {
                return iReturnValue;
            }

            try
            {
                SET_TEMP_ABSOLUTE_IR_INITIATE(Temp);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("AbsoluteTemp_Set() - SET_TEMP_ABSOLUTE_IR_INITIATE Exception: \n" + EEx.Message); }
            }

            while ((iTry <= MAXTRIES) && ((Math.Abs(fTemp - Temp)) > fReadyWindow))
            {
                System.Threading.Thread.Sleep(3000);
                try
                {
                    SET_TEMP_ABSOLUTE_IR_FETCH(ref fTemp);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("AbsoluteTemp_Set() - SET_TEMP_ABSOLUTE_IR_FETCH Exception: \n" + EEx.Message); }
                }
                iTry++;
            }

            if ((Math.Abs(fTemp - Temp)) <= fReadyWindow)
            {
                iReturnValue = 0;
            }

            return iReturnValue;

        }

        /// <summary>
        /// Set VEO2 Camera Power
        /// <para>0 - Off</para>
        /// <para>1 - On</para>
        /// </summary>
        /// <param name="Op">Integer Value (0 thru 1)</param>
        /// <returns></returns>
        public static int CameraPower_Set(int Op)
        {
            int iReturnValue = -1;
            int iTry = 0;
            int iOp = -1;
            string vOp = "";

            switch (Op)
            {
                case OFF:
                    vOp = "Off";
                    break;
                case ON:
                    vOp = "On";
                    break;

                default:
                    vOp = "";
                    break;
            }

            if (Program.debugMode) { Debug.WriteDebugInfo("Setting Camera Power to " + vOp.ToString()); }

            // Leave if argument out of range
            if (Op < OFF || Op > ON)
            {
                return iReturnValue;
            }

            // get current camera power status
            try
            {
                SET_CAMERA_POWER_FETCH(ref iOp);
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("CameraPower_Set() - SET_CAMERA_POWER_FETCH Exception: \n" + EEx.Message); }
            }

            // if current setting is not equal to desired setting
            if (iOp != Op)
            {
                try
                {
                    SET_CAMERA_POWER_INITIATE(Op);
                }
                catch (DllNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("VEO2.dll not found");
                    Console.ResetColor();
                    return iReturnValue;
                }
                catch (ExternalException EEx)
                {
                    Console.WriteLine(EEx.Message);
                    if (Program.debugMode) { Debug.WriteDebugInfo("CameraPower_Set() - SET_CAMERA_POWER_INITIATE Exception: \n" + EEx.Message); }
                }

                while ((iTry <= 20) && (iOp != Op))
                {
                    System.Threading.Thread.Sleep(1000);
                    try
                    {
                        SET_CAMERA_POWER_FETCH(ref iOp);
                    }
                    catch (DllNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("VEO2.dll not found");
                        Console.ResetColor();
                        return iReturnValue;
                    }
                    catch (ExternalException EEx)
                    {
                        Console.WriteLine(EEx.Message);
                        if (Program.debugMode) { Debug.WriteDebugInfo("CameraPower_Set() - SET_CAMERA_POWER_FETCH Exception: \n" + EEx.Message); }
                    }

                    iTry++;
                }
            }

            if (iOp == Op)
            {
                iReturnValue = 0;
            }

            return iReturnValue;
        }

        /// <summary>
        /// Close Instance of IRWindows2001
        /// </summary>
        /// <returns></returns>
        public static int ShutDown()
        {
            int iReturnValue = 0;

            if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.ShutDown() - Closing IRWin"); }
            try
            {
                IRWIN_SHUTDOWN();
            }
            catch (DllNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VEO2.dll not found");
                Console.ResetColor();
                return iReturnValue;
            }
            catch (ExternalException EEx)
            {
                Console.WriteLine(EEx.Message);
                if (Program.debugMode) { Debug.WriteDebugInfo("VEO2.ShutDown() - IRWIN_SHUTDOWN Exception: \n" + EEx.Message); }

                iReturnValue = -1;
            }

            return iReturnValue;
        }

        public static int GetSignificantDigits(decimal dNumber)
        {
            
            int idigits = 0;
            string strNum = dNumber.ToString();
            //Console.WriteLine("String Value:  " + strNum);
            int idec = strNum.IndexOf('.', 0);
            //Console.WriteLine("Decimal Position:  " + idec);
            if (idec < 0)
            {
                // whole number
            }
            else
            {
                idec = idec + 1;
                int ilength = strNum.Length;
                //Console.WriteLine("Length of Numerical String:  " + ilength);
                int iRemove = 0;

                for (int i = (ilength - 1); i > 0; i--)
                {
                    if (strNum[i].Equals('0'))
                    {
                        iRemove++;
                    }
                    else
                    {
                        break;
                    }
                }


                strNum = strNum.Remove((ilength - iRemove), iRemove);
                //Console.WriteLine("String Value:  " + strNum);
                ilength = strNum.Length;

                idigits = ilength - idec;

            }

            return idigits;
        }

    }
}

