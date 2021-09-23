The intent of this software is to allow a TPS user see the internal camera of the VEO-2, as there is currently no way for ATLAS to gain access to this asset.  The CoyoteNAM will perform the following:

1.  Verify the CoyoteNAM was called with the proper of number of parameters (2).  If not, an error generated (error code -100), reported to the user, and the NAM closes.  

2.  Verify the Coyote executable is resident on the VIPER/T system hard drive.  If not, an error is generated (error code -101), reported to the user, and the NAM closes.  

3.  Set the EO sensor stage to the Laser Camera.  Verify that this occurs; if not, error is generated (error code -103), reported to the user, and the NAM closes.  

4.  Power is then applied to the Pleora VEO-2 internal camera.  Verify that this occurs; if not, error is generated (error code -102), reported to the user, and the NAM closes.  
5.  Coyote executable is called.  The user will then manipulate the Coyote software as he wishes.

6.  The NAM will suspend operation until the Coyote software is closed.  The NAM will then report SUCCESS (error code 0) to the ATLAS user.

7.  The NAM will be called with two arguments.  They are required for error code and error strings to be returned from the NAM.  For example, the NAM would be called as such from ATLAS:

DECLARE, VARIABLE, 'EO-ERROR-CODE' IS INTEGER  $
DECLARE, VARIABLE, 'EO-RETURN-STRING' IS STRING (256) OF CHAR  $        

PERFORM, 'COYOTENAM' ('EO-ERROR-CODE', 'EO-RETURN-STRING')  $
