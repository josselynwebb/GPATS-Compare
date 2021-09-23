#ifndef _CEMEX_H_
#define _CEMEX_H_

#ifdef Display
#undef Display
#endif

#ifdef GetStr
#undef GetStr
#endif

#ifdef __cplusplus
extern "C" {
#endif

void Display(LPCSTR pszMsg);
void GetStr(LPCSTR pszPrompt, LPSTR pBuffer, UINT nBufLen);
UINT Input(LPCSTR pszPrompt, LPSTR pBuffer, UINT nBufLen);
void Error(LPCSTR pszMsg);
void Warning(LPCSTR pszMsg);
void Info(LPCSTR pszMsg);

#ifdef __cplusplus
}
#endif

#endif /* _CEMEX_H_ */
