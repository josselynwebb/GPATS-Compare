
#include "CPLCFileMgr.hpp"


CPLCFileMgr PCLFileMgr;

int __stdcall readPLCFile_if(){
		  
	return PCLFileMgr.readPLCFile();
}

int __stdcall savePLCData_if(){
	
	return PCLFileMgr.savePLCData();
}

int __stdcall setPLCPathData_if(long path, float freqMHz, float CF_dBm){
	
	return PCLFileMgr.setPLCPathData(path, freqMHz, CF_dBm);
}

int __stdcall getPLCData_if(long path, float freqMHz, float *CF_dBm){
	
	return PCLFileMgr.getPLCData(path, freqMHz, *CF_dBm);
}	

int __stdcall exportToExcel_if(char *fileName){

	return PCLFileMgr.exportToExcel(fileName);
	return 0;
}

int __stdcall exportSplinesToExcel_if(char *fileName){

	return PCLFileMgr.exportSplinesToExcel(fileName);
	return 0;
}

int __stdcall getError_if(int *ErrorCode, char *ErrorDesc, int EDLen){

	PCLFileMgr.getError(*ErrorCode, ErrorDesc, EDLen);
	return 0;
}
