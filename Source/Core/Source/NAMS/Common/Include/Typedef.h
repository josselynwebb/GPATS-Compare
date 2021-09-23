#ifndef	_TYPEDEF_H_
#define	_TYPEDEF_H_

#include <stddef.h>
//
//  This file is used for distribution in "<usr>\tyx\include" only.
//	

//
//	typedef may be system dependent, the following is for NT
//
typedef	short			TShort, Short, *PShort;
typedef	unsigned short	TUShort, TWord, UShort, Word, *PUShort, *PWord;
typedef	unsigned char	TByte, Byte, *PByte;
typedef	TWord			TBool, *PBool;
typedef	char			*PChar;

typedef	int				*PInt;
typedef	unsigned int	TUInt, UInt, *PUInt;
typedef	long			TLong, *PLong;
typedef	unsigned long	TULong, TDWord, ULong, DWord, *PULong, *PDWord;
typedef	double			TDouble, *PDouble;
typedef	void			*PVoid, *PUserData;

typedef	const char		*PCChar;
typedef	const Byte		*PCByte;
typedef	const Short		*PCShort;
typedef	const Word		*PCWord, *PCUShort, *PCBool;
typedef	const int		*PCInt;
typedef	const UInt		*PCUInt;
typedef	const long		*PCLong;
typedef	const ULong		*PCULong, *PCDWord;
typedef	const double	*PCDouble;
typedef	const void		*PCVoid;

// strings
#ifdef  _UNICODE
// UNICODE
typedef wchar_t tchar_t;
#else
// ANSI
typedef char tchar_t;
#endif

typedef char* PSTR;
typedef const char* PCSTR;
typedef wchar_t* PWSTR;
typedef const wchar_t* PCWSTR;
typedef tchar_t* PTSTR;
typedef const tchar_t* PCTSTR;

#ifndef interface
#define interface struct
#endif

#endif	// _TYPEDEF_H
