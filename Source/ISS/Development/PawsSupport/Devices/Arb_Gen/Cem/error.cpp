#define _CRT_SECURE_NO_DEPRECATE 1

#include "cem.h"
#include <stdio.h>

static char ebuf[256];

int TypeErr (const char *pModName)
{
	sprintf_s (ebuf, sizeof(ebuf), "Type Error For Modifier: %s\n", pModName);
	ErrMsg (7, ebuf);
	return 0;
}

int BusErr (const char *pDevName)
{
	sprintf_s (ebuf, sizeof(ebuf), "Bus Error For Device: %s\n", pDevName);
	ErrMsg (7, ebuf);
	return 0;
}

