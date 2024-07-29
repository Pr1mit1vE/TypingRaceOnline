using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ClientLR4
{
    public partial class SearchServers : Form
    {
        private const int BroadcastPort = 123;
        private bool serverSelected;
        public bool ServerSelected { get => serverSelected; }
        private string ipServer;
        public string IpServer { get => ipServer; }
        private string portServer;
        public string PortServer { get => portServer; }
        private List<Server> servers;
        public SearchServers()
        {
            InitializeComponent();
        }

        private void SearchServers_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            var column1 = new DataGridViewColumn();
            column1.HeaderText = "IP";
            column1.Name = "IP";
            column1.CellTemplate = new DataGridViewTextBoxCell();

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Порт";
            column2.Name = "Port";
            column2.CellTemplate = new DataGridViewTextBoxCell();

            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Доступен";
            column3.Name = "Enabled";
            column3.CellTemplate = new DataGridViewTextBoxCell();

            var column4 = new DataGridViewColumn();
            column4.HeaderText = "Кол-во игроков";
            column4.Name = "PlayersCount";
            column4.CellTemplate = new DataGridViewTextBoxCell();

            var column5 = new DataGridViewColumn();
            column5.HeaderText = "Статус";
            column5.Name = "Status";
            column5.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns.Add(column4);
            dataGridView1.Columns.Add(column5);
            dataGridView1.AllowUserToAddRows = false;
            connectBtn.Enabled = false;
            ipServ.Text = "";
            portServ.Text = "";
        }

        private void searchServBtn_Click(object sender, EventArgs e)
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            GatewayIPAddressInformation gateWayAddr = null;
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up && adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                    GatewayIPAddressInformationCollection gatewayAddresses = adapterProperties.GatewayAddresses;
                    if (gatewayAddresses != null)
                    {
                        foreach (GatewayIPAddressInformation gatewayAddress in gatewayAddresses)
                        {
                            if(gatewayAddress.Address.ToString() != "::" )
                                gateWayAddr = gatewayAddress;         
                        }
                    }
                }
            }
            var splitedGatewayAddr = gateWayAddr.Address.ToString().Split('.');
            var gateWayAddress = IPAddress.Parse($"{splitedGatewayAddr[0]}.{splitedGatewayAddr[1]}.{splitedGatewayAddr[2]}.255");
            // Создаем UDP клиента и настраиваем для широковещательных сообщений
            UdpClient client = new UdpClient();
            client.EnableBroadcast = true;
            // Формируем сообщение для отправки
            string message = "Hello servers! Are you there?";
            byte[] data = Encoding.ASCII.GetBytes(message);       
            // Отправляем широковещательное сообщение на порт BroadcastPort
            client.Send(data, data.Length, new IPEndPoint(gateWayAddress, BroadcastPort));
            // Получаем ответ от серверов
            // Устанавливаем таймаут ожидания ответа
            client.Client.ReceiveTimeout = 1000; // 1 секунда
            servers = new List<Server>();
            while (true)
            {
                try
                {
                    IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receiveBuffer = client.Receive(ref serverEndpoint);
                    string response = Encoding.ASCII.GetString(receiveBuffer);

                    var countSimillar = 0;
                    foreach (var server in servers)
                    {
                        bool areEqual = server.PortAndIP.Equals(serverEndpoint);
                        if (areEqual)
                            countSimillar++;
                    }
                    
                    // Добавляем сервер в список
                    if (countSimillar == 0)
                    {
                        var server = new Server();
                        var splitedResponse = response.Split(' ');
                        if (response.Contains("full"))
                        {
                            server.Enabled = "Нет";                          
                            var playresCount = splitedResponse[4];
                            server.PlayersCount = playresCount;
                            server.Status = "Заполнен";
                        }
                        else if (response.Contains("playing"))
                        {
                            server.Enabled = "Нет";
                            var playresCount = splitedResponse[4];
                            server.PlayersCount = playresCount;
                            server.Status = "Игра начата";
                        }
                        else
                        {
                            server.Enabled = "Да";
                            var playresCount = splitedResponse[0];
                            server.PlayersCount = playresCount;
                            server.Status = "Ожидание игроков";
                        }
                        server.PortAndIP = serverEndpoint;
                        servers.Add(server);
                    }
                }
                catch (Exception ex)
                {
                    break;
                }
            }
            // Закрываем клиента
            client.Close();
            dataGridView1.Rows.Clear();
            foreach (var server in servers)
                dataGridView1.Rows.Add($"{server.PortAndIP.Address}", $"{server.PortAndIP.Port}", 
                    $"{ server.Enabled}", $"{server.PlayersCount}/3", $"{server.Status}");           
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = -1;
            for (var i = 0; i < dataGridView1.Rows.Count; i++)
                if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "Нет")
                    index= i;
            
            var selectedServer = dataGridView1.SelectedCells;
            if (e.RowIndex == index)
                selectedServer = null;

            if (selectedServer != null)
            {
                connectBtn.Enabled = true;
                ipServ.Text = $"{selectedServer[0].Value}";
                portServ.Text = $"{selectedServer[1].Value}";
            }
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            ipServer = ipServ.Text.ToString();
            portServer = portServ.Text.ToString();
            serverSelected = true;
            MessageBox.Show("Подключение к серверу: " +
                $"{ipServer}:{portServer}"); 
            this.Close();
        }
    }
    public class Server
    {
        public IPEndPoint PortAndIP;
        public string Enabled;
        public string PlayersCount;
        public string Status;
    }
}
