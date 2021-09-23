echo on
REM Build Device VIPERT

Copy ..\Devices\Common\DeviceDB\Common.ddb+..\Devices\Switch\DeviceDB\Switch.ddb+..\Devices\Dwg\DeviceDB\Dwg.ddb+..\Devices\Arb_Gen\DeviceDB\Arb_Gen.ddb+..\Devices\Cntr\DeviceDB\Cntr.ddb+..\Devices\Dcps\DeviceDB\Dcps.ddb+..\Devices\Dso\DeviceDB\Dso.ddb+..\Devices\Dmm\DeviceDB\Dmm.ddb+..\Devices\FncGen\DeviceDB\FncGen.ddb+..\Devices\RfMS_rfMeasan\DeviceDB\RfMS_rfMeasan.ddb+..\Devices\RfMS_Pwr\DeviceDB\RfMS_RfPwr.ddb+..\Devices\RFGen_PM1140B\DeviceDB\RFGen_PM1140B.ddb+..\Devices\Bus_1553\DeviceDB\Bus_1553.ddb+..\Devices\Bus_PciSerial\DeviceDB\Bus_PciSerial.ddb+..\Devices\Bus_Ethernet_Gigabit\DeviceDB\Bus_Ethernet_Gigabit.ddb+..\Devices\Bus_Can\DeviceDB\Bus_Can.ddb+..\Devices\SyncRes\DeviceDB\Srs.ddb+..\Devices\RfMS_rfCntr\DeviceDB\RfMS_rfCntr.ddb+..\Devices\Veo2\DeviceDB\veo2.ddb cmp_DeviceDB_VIPERT.ddb

%TYXROOT%\usr\tyx\prg\PLI -v  < cmp_DeviceDB_VIPERT.ddb %TYXROOT%\usr\tyx\tab\Device.pro ..\Lexfiles\LexDB.LEX -w100 DeviceDB_VIPERT > build.log

copy cmp_DeviceDB_VIPERT.ddb ..\..\..\..\..\Target\ROOTDrive\usr\tyx\sub\IEEE716.89\GPATSCIC\Station\cmp_DeviceDB_VIPERT.ddb

copy DeviceDB_VIPERT.DEV ..\..\..\..\..\Target\ROOTDrive\usr\tyx\sub\IEEE716.89\GPATSCIC\Station\DeviceDB_VIPERT.DEV 

REM Build Device TETS

Copy ..\Devices\Common\DeviceDB\Common.ddb+..\Devices\Switch\DeviceDB\Switch.ddb+..\Devices\Dwg\DeviceDB\Dwg.ddb+..\Devices\Arb_Gen\DeviceDB\Arb_Gen.ddb+..\Devices\Cntr\DeviceDB\Cntr.ddb+..\Devices\Dcps\DeviceDB\Dcps.ddb+..\Devices\Dso_TETS\DeviceDB\Dso_TETS.ddb+..\Devices\Dmm\DeviceDB\Dmm.ddb+..\Devices\FncGen\DeviceDB\FncGen.ddb+..\Devices\RF_Measan_TETS\DeviceDB\RFMeas_TETS.ddb+..\Devices\RF_Pwr_TETS\DeviceDB\RFPwr_E1416A_TETS.ddb+..\Devices\RF_Gen_TETS\DeviceDB\RFGen_GT50008A.ddb+..\Devices\Bus_1553\DeviceDB\Bus_1553.ddb+..\Devices\Bus_PciSerial\DeviceDB\Bus_PciSerial.ddb+..\Devices\Bus_Ethernet_Gigabit\DeviceDB\Bus_Ethernet_Gigabit.ddb+..\Devices\Bus_Can\DeviceDB\Bus_Can.ddb+..\Devices\SyncRes\DeviceDB\Srs.ddb+..\Devices\RF_TETS\RfMS_rfCntr\DeviceDB\RF_TETS\RMMeas_TETS\Rf_Meas.ddb cmp_DeviceDB_TETS.ddb

%TYXROOT%\usr\tyx\prg\PLI -v < cmp_DeviceDB_TETS.ddb %TYXROOT%\usr\tyx\tab\Device.pro ..\Lexfiles\LexDB.LEX -w100 DeviceDB_TETS >> Build.log

copy cmp_DeviceDB_TETS.ddb ..\..\..\..\..\Target\ROOTDrive\usr\tyx\sub\IEEE716.89\GPATSCIC\Station\cmp_DeviceDB_TETS.ddb

copy DeviceDB_TETS.DEV ..\..\..\..\..\Target\ROOTDrive\usr\tyx\sub\IEEE716.89\GPATSCIC\Station\DeviceDB_TETS.DEV 

REM Build Switch VIPERT

Copy ..\Devices\Switch\SwitchDB\SwitchDB.sdb SwitchDB_VIPERT.sdb

%TYXROOT%\usr\tyx\prg\PLI -v  < SwitchDB_VIPERT.sdb %TYXROOT%\usr\tyx\tab\Switch.pro ..\Lexfiles\LexDB.LEX -w100 SwitchDB_VIPERT > build.log

copy SwitchDB_VIPERT.sdb ..\..\..\..\..\Target\ROOTDrive\usr\tyx\sub\IEEE716.89\GPATSCIC\Switch\SwitchDB_VIPERT.sdb

copy SwitchDB_VIPERT.SWX ..\..\..\..\..\Target\ROOTDrive\usr\tyx\sub\IEEE716.89\GPATSCIC\Switch\SwitchDB_VIPERT.SWX 

REM Build Switch TETS

Copy ..\Devices\Switch\SwitchDB\SwitchDB.sdb SwitchDB_TETS.sdb

%TYXROOT%\usr\tyx\prg\PLI -v  < SwitchDB_TETS.sdb %TYXROOT%\usr\tyx\tab\Switch.pro ..\Lexfiles\LexDB.LEX -w100 SwitchDB_TETS > build.log

copy SwitchDB_TETS.sdb ..\..\..\..\..\Target\ROOTDrive\usr\tyx\sub\IEEE716.89\GPATSCIC\Switch\SwitchDB_TETS.sdb

copy SwitchDB_TETS.SWX ..\..\..\..\..\Target\ROOTDrive\usr\tyx\sub\IEEE716.89\GPATSCIC\Switch\SwitchDB_TETS.SWX 

pause
