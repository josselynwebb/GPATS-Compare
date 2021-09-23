#ifndef	_TYPEDEF_H_
#define	_TYPEDEF_H_

//
//	typedef may be system dependent, the following is for NT
//

typedef	short			TShort, *PShort;
typedef	unsigned short	TUShort, TWord, *PUShort, *PWord;
typedef	unsigned char	TByte, *PByte;
typedef	TWord			TBool, *PBool;
typedef	char			*PChar;

typedef	int				*PInt;
typedef	unsigned int	TUInt, *PUInt;
typedef	long			TLong, *PLong;
typedef	unsigned long	TULong, TDWord, *PULong, *PDWord;
typedef	double			TDouble, *PDouble;
typedef	void			*PVoid, *PUserData;

typedef	const char		*PCChar;
typedef	const TByte		*PCByte;
typedef	const TShort	*PCShort;
typedef	const TWord		*PCWord, *PCUShort, *PCBool;
typedef	const int		*PCInt;
typedef	const TUInt		*PCUInt;
typedef	const TLong		*PCLong;
typedef	const TULong	*PCULong, *PCDWord;
typedef	const TDouble	*PCDouble;
typedef	const void		*PCVoid;


#endif	// _TYPEDEF_H
