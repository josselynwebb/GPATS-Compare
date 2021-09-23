// All error codes go here

#ifndef INCLUDED_PLCERRORS_HPP
#define INCLUDED_PLCERRORS_HPP


const enum {
	NO_PLC_ERROR		 = 0,
	FILE_IO				 = 1,
	MEM_ALLOCATION_FAULT = 2,
	PLC_DATA_EXPORT		 = 3,
	PATH_ERROR           = 4
};

#endif