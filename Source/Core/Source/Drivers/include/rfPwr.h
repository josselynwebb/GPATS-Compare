#define FLAG 10

#define	RFERROR		(-1)
#define	SUCCESS		0

#define	TABLE81D	"TetsHP8481D_"
#define	TABLE81A	"TetsHP8481A_"
#define	PWRHD1		"HP8481D"
#define	PWRHD2		"HP8481A"
#define	ININAME		"\\Tets.ini"

#define	D_WATTS		1
#define	D_DBM		2
#define	D_PERCENT	3
#define	D_DB		4

#define	HD1		1
#define	HD2		2

static int	FirstTime = 1;
static int	PowerMeterResetFlag = 1;

static char	hd1SerialNo[24];
static char	hd2SerialNo[24];

char		RFPwrString[80];

extern int		IsSimOrDeb(char dev_name[20]);
extern void		modsw_cls();
extern void		modsw_opn();
extern void		ResetRFPwr(void);
extern double	FthRFPwr(void);
extern void		InxRFPwr(void);
extern int		SetRFPwr(int, int);
