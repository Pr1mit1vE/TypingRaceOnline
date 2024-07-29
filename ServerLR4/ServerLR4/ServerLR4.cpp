#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <iostream>
#include <string>
#include <thread>
#include <mutex>
#include <WinSock2.h>
#include <WS2tcpip.h>
#include <locale>
#include <codecvt>
#include <iphlpapi.h>
#pragma comment(lib, "ws2_32.lib")
#pragma comment(lib, "iphlpapi.lib")
#pragma warning(disable : 4996)

#define MAX_BUFFER_SIZE 1024
#define SERVER_PORT 123
#define MAX_CLIENTS 3
#define PRINTNUSERS if (nclients)\
  printf("%d user on-line\n",nclients);\
  else printf("No User on line\n");

char* gateway;
SOCKET Connections[MAX_CLIENTS + 1];
int isTextWritten[std::size(Connections)];
std::mutex mtx;
std::condition_variable cv;
int nclients = 0;
bool isGameStarted = false;

bool serverIsRebooting = false;

void HandleUDPRequests(SOCKET udpSocket)
{
    sockaddr_in clientAddr;
    int clientAddrSize = sizeof(clientAddr);
    char buff[MAX_BUFFER_SIZE];

    while (true)
    {
        if (recvfrom(udpSocket, buff, MAX_BUFFER_SIZE, 0, reinterpret_cast<sockaddr*>(&clientAddr), &clientAddrSize) == SOCKET_ERROR)
        {
            std::cerr << "Failed to receive UDP packet." << std::endl;
            break;
        }
        std::cout << "Received UDP packet from: " << inet_ntoa(clientAddr.sin_addr) << ":" << ntohs(clientAddr.sin_port) << std::endl;
        std::string response = std::to_string(nclients) + " ";
        if(nclients == MAX_CLIENTS)
           response = "Server is full (UDP) " + std::to_string(nclients) + " ";
        if(isGameStarted)
            response = "Server is playing (UDP) " + std::to_string(nclients) + " ";

        if (sendto(udpSocket, response.c_str(), response.length(), 0, reinterpret_cast<sockaddr*>(&clientAddr), clientAddrSize) == SOCKET_ERROR)
        {
            std::cerr << "Failed to send UDP response." << std::endl;
            break;
        }

        std::cout << "Sent UDP response: "<< response << std::endl;
    }
    closesocket(udpSocket);
}

void HandleTCPConnections(int index)
{
    SOCKET tcp_sock = Connections[index];
    char buff[MAX_BUFFER_SIZE];
    bool isTextSended = false;
    while (recv(tcp_sock, &buff[0], sizeof(buff), 0) > 0)
    {
        if (nclients < MAX_CLIENTS && !isGameStarted && !serverIsRebooting)
        {
            std::string StartMessage = "Welcome to the game.You are player: " + std::to_string(index) + " \n" + "Waiting for: " + std::to_string(MAX_CLIENTS - nclients) + " ";
            send(tcp_sock, StartMessage.c_str(), StartMessage.size(), 0);
            int sended = send(tcp_sock, StartMessage.c_str(), StartMessage.size(), 0);
            Sleep(1000);         
        }
        else
        {
            isGameStarted = true;
            if (!isTextSended)
            {
                std::string message = u8"Однако постепенно он успокоился, обмахнулся платком и, произнеся довольно бодро: \"Ну-с, итак...\" - повел речь, прерванную питьем абрикосовой.";
                int sended = send(tcp_sock, message.c_str(), message.size(), 0);
                isTextSended = true;
            }
            else
            {
                for (int i = 1; i <= std::size(Connections); i++)
                {
                    if (i == index || Connections[i] == 0)
                        continue;
                    send(Connections[i], buff, sizeof(buff), NULL);
                }
            }
            if (strstr(buff, "Finished") != nullptr)
                isTextWritten[index] = true;

            if (!serverIsRebooting)
            {
                std::unique_lock<std::mutex> lock(mtx);
                bool allTextWritten = true;            
                for (int i = 1; i <= std::size(Connections); i++)
                {
                    if (!isTextWritten[i] && Connections[i] != 0)
                    {
                        allTextWritten = false;
                        break;
                    }
                }
                if (allTextWritten)
                {
                    serverIsRebooting = true;
                    std::cout << "Server is rebooting..." << std::endl;
                    isGameStarted = false;
                    isTextSended = false;
                }    
            }
            else 
                break;   
            std::unique_lock<std::mutex> lock(mtx);
            cv.notify_all();
        }
    }

    if (serverIsRebooting)
        Sleep(7000);

    nclients--;
    if (nclients == 0)
        isGameStarted = false;

    printf("-disconnect\n"); 
    PRINTNUSERS
    Connections[index] = 0;
    closesocket(Connections[index]);
    closesocket(tcp_sock);
}
int getShluz()
{
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        printf("WSAStartup failed: %d\n", GetLastError());
        return 1;
    }

    // Получаем информацию об адаптерах
    IP_ADAPTER_INFO adapterInfo[16];
    DWORD adapterInfoSize = sizeof(adapterInfo);
    DWORD result = GetAdaptersInfo(adapterInfo, &adapterInfoSize);
    if (result == ERROR_BUFFER_OVERFLOW) {
        printf("GetAdaptersInfo buffer overflow\n");
        return 1;
    }
    else if (result != ERROR_SUCCESS) {
        printf("GetAdaptersInfo failed: %d\n", GetLastError());
        return 1;
    }

    // Ищем адаптер с адресом шлюза
    IP_ADAPTER_INFO* pAdapterInfo = adapterInfo;
    while (pAdapterInfo != NULL) {
        IP_ADDR_STRING* pGateway = &pAdapterInfo->GatewayList;
        if (strcmp(pGateway->IpAddress.String, "0.0.0.0")) {
            while (pGateway != NULL) {
                if (pGateway->IpAddress.String[0] != '\0') {
                    printf("Gateway: %s (adapter: %s)\n", pGateway->IpAddress.String, pAdapterInfo->AdapterName);
                    gateway = pGateway->IpAddress.String;
                    break;
                }
                pGateway = pGateway->Next;
            }

        }
        pAdapterInfo = pAdapterInfo->Next;
    }

    int  dotsCount = 0;
    for (int i = 0; i < 16; i++)
    {
        if (dotsCount == 3)
        {
            gateway[i] = '2';
            gateway[i + 1] = '5';
            gateway[i + 2] = '5';
            break;
        }
        if (gateway[i] == '.')
            dotsCount = dotsCount + 1;
    }
    printf("Gateway: %s \n", gateway);
    // Освобождаем библиотеку Winsock
    WSACleanup();
    return 0;
}

int main()
{
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
    {
        std::cerr << "Failed to initialize winsock." << std::endl;
        return -1;
    }

    SOCKET udpSocket = socket(AF_INET, SOCK_DGRAM, 0);
    if (udpSocket == INVALID_SOCKET)
    {
        std::cerr << "Failed to create UDP socket." << std::endl;
        WSACleanup();
        return -1;
    }

    SOCKET tcpSocket = socket(AF_INET, SOCK_STREAM, 0);
    if (tcpSocket == INVALID_SOCKET)
    {
        std::cerr << "Failed to create TCP socket." << std::endl;
        closesocket(udpSocket);
        WSACleanup();
        return -1;
    }

    getShluz();
    sockaddr_in serverAddr;
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(SERVER_PORT);
    serverAddr.sin_addr.s_addr = INADDR_ANY("192.168.0.1");

    if (bind(udpSocket, (sockaddr*)&serverAddr,
        sizeof(serverAddr)))
    {
        std::cerr << "Failed to bind UDP socket." << std::endl;
        closesocket(udpSocket);
        closesocket(tcpSocket);
        WSACleanup();
        return 1;
    }

    if (bind(tcpSocket, (sockaddr*)&serverAddr,
        sizeof(serverAddr)))
    {
        std::cerr << "Failed to bind TCP socket." << std::endl;
        closesocket(udpSocket);
        closesocket(tcpSocket);
        WSACleanup();
        return 1;
    }
    if (listen(tcpSocket, 0x100))
    {
        std::cerr << "Failed to listen for TCP connections." << std::endl;
        closesocket(udpSocket);
        closesocket(tcpSocket);
        WSACleanup();
        return 1;
    }
    std::cout << "UDP server started. Waiting for requests..." << std::endl;
    std::cout << "TCP server started. Waiting for connections..." << std::endl;
    std::thread udpThread(HandleUDPRequests, udpSocket);
    udpThread.detach();
    SOCKET client_socket;    // сокет для клиента
    sockaddr_in client_addr;    // адрес клиента
    int client_addr_size = sizeof(client_addr);
    while ((client_socket = accept(tcpSocket, (sockaddr*)&client_addr, &client_addr_size)))
    {
        if (serverIsRebooting && nclients == 0)
        {
            serverIsRebooting = false;
            for (int i = 0; i < std::size(Connections); i++)
                isTextWritten[i] = 0;
        }
        if (nclients < MAX_CLIENTS && !isGameStarted)
        {
            nclients++;
            PRINTNUSERS;
            CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)HandleTCPConnections, (LPVOID)nclients, NULL, NULL);
            Connections[nclients] = client_socket;
        }
    }
    WSACleanup();
    return 0;
}


