// simple_socket.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#define MYPORT 21
#define MYADDRESS "192.168.0.2"
#define ATLASADDRESS "192.168.0.1"

//"66.102.7.104"

char myaddr[50], uraddr[50];
unsigned short theport;
SOCKET closer;

SOCKET accept_by_name(const char * ip_or_fqdn, unsigned short port);
void rebind(SOCKET s, bool TCP, const char * ip_or_fqdn, unsigned short port);
int send_udp(const char * ip_or_fqdn, unsigned short port, const char * send_word);
int recieve_udp(const char * ip_or_fqdn, unsigned short port, char * recieve_word, int max_len);
void instructions(void);

int _tmain(int argc, _TCHAR* argv[])
{
	WSADATA wsaData;
	SOCKET isock;
	int sent, returned;
	char recieved[5000]="";

	if(argc <2)
	{
		strcpy(myaddr, MYADDRESS);
		strcpy(uraddr, ATLASADDRESS);
		theport=MYPORT;
		instructions();
	}
	else
	{
		strcpy(myaddr, argv[1]);
		strcpy(uraddr, argv[2]);
		theport=(unsigned short)atoi(argv[3]);
	}

    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) 
	{ 
            fprintf(stderr, "WSAStartup failed.\n"); 
            exit(1); 
    } 

	//udp transfer test - recieve data
	printf("Start the UDP transfer on the ATLAS program.\n");
	while(recieve_udp(myaddr, theport, recieved, 5000)==-1);
	printf("Received UDP string: \"%s\"\n", recieved);

	Sleep(6000);
	printf("Sending data for the UDP receive on the ATLAS program.\n");
	send_udp(uraddr, theport, recieved);	
	recieved[0]='\0';

	//tcp transfer test - recieve data
	printf("Start the TCP transfer on the ATLAS program.\n");
	isock=accept_by_name(myaddr, theport);
		
	returned=recv(isock, recieved, 5000, 0);
	while(returned==-1)
	{
		returned=recv(isock, recieved, 5000, 0);
	}
	printf("Received TCP string: \"%s\"\n", recieved);
	closesocket(isock);
	closesocket(closer);
	//send data back
    printf("Sending data the TCP recieve on the ATLAS program.\n");
	//getch();
	isock=accept_by_name(myaddr, theport);
	int temp=(int)strlen(recieved);
	sent=send(isock, recieved, temp, 0);
	while(sent==-1)
	{
			sent=send(isock, recieved, temp, 0);
	}

	closesocket(isock); //close socket

	WSACleanup();
	printf("End of program.  Press any Key to continue\n");
	getch();
	return 0;
}

SOCKET accept_by_name(const char * ip_or_fqdn, unsigned short port)
{
	sockaddr_in address, tempaddr;
	hostent *get_ip;
	SOCKET s, return_s;
	int status;
	int size;

	address.sin_family = AF_INET;         // host byte order 
    address.sin_port = htons(port);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(myaddr); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(ip_or_fqdn);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	closer=s=socket(AF_INET, SOCK_STREAM, 0);

	status=bind(s, (sockaddr *)&address, sizeof(sockaddr));
	status=listen(s,1);
	
	return_s=accept(s, NULL, NULL);
	while(return_s==-1)
	{
		return_s=accept(s, NULL, NULL); 
	}
	return return_s;
}

int send_udp(const char * ip_or_fqdn, unsigned short port, const char * send_word)
{
	sockaddr_in address;
	hostent *get_ip;
	SOCKET s;
	int retval;

	address.sin_family = AF_INET;         // host byte order 
    address.sin_port = htons(port);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(ip_or_fqdn); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(ip_or_fqdn);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	s=socket(AF_INET, SOCK_DGRAM, 0);

	bind(s, (sockaddr *)&address, sizeof(sockaddr));

	address.sin_family = AF_INET;         // host byte order 
    address.sin_port = htons(port);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(uraddr); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(ip_or_fqdn);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 
	retval=sendto(s, send_word, (int)strlen(send_word), 0, (sockaddr *)&address, sizeof(sockaddr));
	closesocket(s);
	return retval;
}

int recieve_udp(const char * ip_or_fqdn, unsigned short port, char * recieve_word, int max_len)
{
	sockaddr_in address;
	hostent *get_ip;
	SOCKET s;
	int temp, retval;

	address.sin_family = AF_INET;         // host byte order 
    address.sin_port = htons(port);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(ip_or_fqdn); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(ip_or_fqdn);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	s=socket(AF_INET, SOCK_DGRAM, 0);

	bind(s, (sockaddr *)&address, sizeof(sockaddr));

	temp=sizeof(sockaddr);
	address.sin_family = AF_INET;         // host byte order 
    address.sin_port = htons(port);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(uraddr); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(ip_or_fqdn);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	retval= recvfrom(s, recieve_word, max_len, 0, (sockaddr *)&address, &temp);
	closesocket(s);
	return retval;
}

void rebind(SOCKET s, bool TCP, const char * ip_or_fqdn, unsigned short port)
{
	sockaddr_in address;
	hostent *get_ip;
	int status;

	closesocket(s); //close and open socket
	s=socket(AF_INET, SOCK_STREAM, 0);

	address.sin_family = AF_INET;         // host byte order 
    address.sin_port = htons(port);     // short, network byte order 
	address.sin_addr.S_un.S_addr=inet_addr(myaddr); 
	//address.sin_addr.S_un.S_addr=inet_addr("127.0.0.1"); 
	if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
	{
		get_ip = gethostbyname(ip_or_fqdn);
		address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
	}
	memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

	status=bind(s, (sockaddr *)&address, sizeof(sockaddr));

	if(TCP)
	{
		address.sin_family = AF_INET;         // host byte order 
		address.sin_port = htons(port);     // short, network byte order 
		address.sin_addr.S_un.S_addr=inet_addr(ip_or_fqdn); 
		if(address.sin_addr.S_un.S_addr==INADDR_NONE) //a domain name may have been specified
		{
			get_ip = gethostbyname(ip_or_fqdn);
			address.sin_addr.S_un.S_addr = *((u_long*)get_ip->h_addr_list[0]);
		}
		memset(&(address.sin_zero), '\0', 8); // zero the rest of the struct 

		status=connect(s, (sockaddr *)&address, sizeof(sockaddr)); 
	}

	return;
}

void instructions(void)
{
	printf("Defaulting to: IP Address of this machine: %s, IP of Atlas Machine: %s, Port:%d\n", MYADDRESS, ATLASADDRESS, MYPORT);
	printf("If addresses need to be changed, they can be set by calling this program with the following parameters:\n");
	printf("BenchTester [MyIPAddress] [AtlasIPAddress] [Port]\n\n");
	return;
}