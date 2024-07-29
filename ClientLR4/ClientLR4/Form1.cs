using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientLR4
{
    public partial class Form1 : Form
    {
        string[] wordsInText;
        bool isGameStarted;
        double timeMin;
        double timeSec;
        SearchServers searchForm;
        int playerNumber;
        TcpClient client;
        ProgressBar progressBar;

        public Form1()
        {           
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isGameStarted = false;
            playerNumber = 0;
            timeMin = 0;
            timeSec = 0;
            searchForm = new SearchServers();
            searchForm.Owner = this;
            richTextBox1.Text = "Подключитесь к серверу, чтобы получить текст";
        }

        private void Typing(SynchronizationContext context)
        {       
            for (var i = 0; i<wordsInText.Length; i++)
            {
                if(i != wordsInText.Length-1)
                    wordsInText[i] += " ";
                MarkOrUnmarkTypingWord(wordsInText[i], true, context);
                while (textBox1.Text != wordsInText[i])
                {
                    if (wordsInText[i].StartsWith(textBox1.Text))
                        textBox1.ForeColor = Color.Green;
                    else
                        textBox1.ForeColor = Color.Red;
                }
                ClearTextBox();
                CalculateProgress(context, i+1, progressBar);
                MarkOrUnmarkTypingWord(wordsInText[i], false, context);
                
            }
            StopTimer();
            DisableTextBox();
            CalculateResult(context);

        }

        private void CalculateProgress(SynchronizationContext context, int number, ProgressBar progressBar)
        {
            lock (progressBar)
            {
                context.Post(value =>
                {
                    progressBar.Value = number;
                }, number);
            }
        }

        private void CalculateResult(SynchronizationContext context)
        {
            double seconds = timeSec / 60;
            double minutes = timeMin + seconds;
            //double sec = timeMin * 60 + seconds;
            context.Send(value => {             
                 resultWordsPerMin.Text = Math.Round((wordsInText.Length / minutes)).ToString();
                var countChar = richTextBox1.Text.ToCharArray();
                resultCharPerSec.Text = Math.Round((countChar.Length / minutes)).ToString();
                if(playerNumber == 1)
                    resultLbl1.Text = $"CPM: {resultCharPerSec.Text} Time: {LBMIn.Text}:{LBSec.Text}";
                else if(playerNumber == 2)
                    resultLbl2.Text = $"CPM: {resultCharPerSec.Text} Time: {LBMIn.Text}:{LBSec.Text}";
                else
                    resultLbl3.Text = $"CPM: {resultCharPerSec.Text} Time: {LBMIn.Text}:{LBSec.Text}";
            }, null);
        }

        private void MarkOrUnmarkTypingWord(string word, bool isMarked, SynchronizationContext context)
        {
            if(isMarked)
                context.Send(value => {
                    var indexOfWord = richTextBox1.Text.LastIndexOf(value.ToString());
                    richTextBox1.Select(indexOfWord, value.ToString().Length);
                    richTextBox1.SelectionBackColor = Color.Blue;
                    richTextBox1.SelectionColor = Color.White;
                }, word);
            else
                context.Send(value => {
                    var indexOfWord = richTextBox1.Text.LastIndexOf(value.ToString());
                    richTextBox1.Select(indexOfWord, value.ToString().Length);
                    richTextBox1.SelectionBackColor = Color.White;
                    richTextBox1.SelectionColor = Color.Black;                   
                }, word);
        }

        private void ClearTextBox() =>Invoke(new Action(() => textBox1.Text = ""));
        private void DisableTextBox() => Invoke(new Action(() => textBox1.Enabled = false));
        private void StopTimer() => Invoke(new Action(() => timer1.Enabled = false));

        private async Task TypingAsync(SynchronizationContext context) => await Task.Run(() => Typing(context));
        
        private async Task CommunicatingWithServerAsync(SynchronizationContext context)
        {
            client = new TcpClient();
            try 
            {
                await client.ConnectAsync(searchForm.IpServer, int.Parse(searchForm.PortServer));
            }
            catch(Exception ex)
            {
                UpdateForm(context);
            }
            // Получаем поток для чтения и записи данных
            NetworkStream stream = client.GetStream();
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections()
                .Where(x => x.LocalEndPoint.Equals(client.Client.LocalEndPoint) && x.RemoteEndPoint.Equals(client.Client.RemoteEndPoint)).ToArray();
            if (tcpConnections != null && tcpConnections.Length > 0)
            {
                var prevValueBar1 = 0;
                var prevValueBar2 = 0;
                var prevValueBar3 = 0;
                Task receivingTask = Task.Run(async () =>
                {
                    
                    byte[] receiveBuffer = new byte[1024];
                    try
                    {
                        while (true)
                        {
                            int bytesRead = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);
                            var response = Encoding.UTF8.GetString(receiveBuffer, 0, bytesRead);
                            if (response.Contains("Welcome to the game."))
                            {
                                var str = $"You are conncted to server:{searchForm.IpServer}\n"
                                    + $"{response}";
                                ChangeTextInRichTextBox(str, context);
                                if (response.Contains("You are player:"))
                                {
                                    var number = response.Split(new string[] { "You are player:" }, StringSplitOptions.None);
                                    var stringNumber = number[1].Split(' ');
                                    playerNumber = int.Parse(stringNumber[1]);
                                }
                            }
                            if (response.Contains("Однако"))
                            {
                                response = response.Trim();
                                ChangeTextInRichTextBox(response, context);
                            }
                            if (response.Contains("progressBar") && !response.Contains("CPM"))
                            {
                                var progress = response.Split(' ');
                                bool isNumber = int.TryParse(progress[0], out var a);
                                var number = 0;
                                if (isNumber)
                                    number = a;
                                else
                                {
                                    isNumber = int.TryParse(progress[1], out var b);                                   
                                    if (isNumber)
                                        number = b;
                                }
                                if (number != 0)
                                {
                                    progress = response.Split(new string[] { "progressBar" }, StringSplitOptions.None);
                                    var progressBar = progress[1].Split(' ');
                                    if (int.Parse(progressBar[1]) == 1)
                                    {
                                        CalculateProgress(context, number, progressBar1);
                                    }
                                    if (int.Parse(progressBar[1]) == 2)
                                    {
                                        CalculateProgress(context, number, progressBar2);
                                    }
                                    if (int.Parse(progressBar[1]) == 0)
                                    {
                                        CalculateProgress(context, number, progressBar3);
                                    }
                                }
                            }
                            if (response.Contains("CPM"))
                            {
                                var resultPlayers = Regex.Split(response, @"zxc[0-9] ");
                                string[] result;
                                if(resultPlayers[1].Contains("progressBar"))
                                    result = Regex.Split(resultPlayers[1], "progressBar "); 
                                else
                                    result = Regex.Split(resultPlayers[0], "progressBar ");
                                if (!int.TryParse(result[0], out var v))
                                    result[0] = string.Join("", result[0].Where(char.IsDigit));
                                var playerResult = result[1].Split(' ');
                                var lblResult = $"{playerResult[1]} {playerResult[2]} {playerResult[3]} {playerResult[4]}";
                                var playerNumber = int.Parse(playerResult[0]);          
                                if (playerNumber == 1)
                                {
                                    context.Post(value =>
                                    {
                                        progressBar1.Value = int.Parse(result[0]);
                                        if (resultLbl1.Text == "")
                                            resultLbl1.Text = lblResult;
                                    }, null);
                                }
                                if (playerNumber == 2)
                                {
                                    context.Post(value =>
                                    {
                                        progressBar2.Value = int.Parse(result[0]);
                                        if (resultLbl2.Text == "")
                                            resultLbl2.Text = lblResult;        
                                    }, null);
                                }
                                if (playerNumber == 0)
                                {
                                    context.Post(value =>
                                    {
                                        progressBar3.Value = int.Parse(result[0]);
                                        if (resultLbl3.Text == "")
                                            resultLbl3.Text = lblResult;
                                    }, null);
                                }
                            }
                            if (progressBar1.Value == progressBar1.Maximum)
                                if (prevValueBar1 != progressBar1.Maximum)
                                {
                                    context.Post(value =>
                                    {
                                        if (resultLbl1.Text != "")
                                        {
                                            CalculatePlace(context);
                                            prevValueBar1 = progressBar1.Maximum;
                                        }
                                    }, null);
                                }
                             if (progressBar2.Value == progressBar2.Maximum)
                                if (prevValueBar2 != progressBar2.Maximum)
                                {
                                    context.Post(value =>
                                    {
                                        if (resultLbl2.Text != "")
                                        {
                                            CalculatePlace(context);
                                            prevValueBar2 = progressBar2.Maximum;
                                        }
                                    }, null);
                                }
                             if (progressBar3.Value == progressBar3.Maximum)
                                if (prevValueBar3 != progressBar3.Maximum)
                                {
                                    context.Post(value =>
                                    {
                                        if (resultLbl3.Text != "")
                                        {
                                            CalculatePlace(context);
                                            prevValueBar3 = progressBar3.Maximum;
                                        }
                                    }, null);
                                }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        UpdateForm(context);
                    }
                });
                
                Task sendingTask = Task.Run(async () =>
                {
                    int prevValue = 0;
                    bool isFirstResult = true;
                    byte[] receiveBuffer = new byte[1024];
                    try
                    {
                        while (true)
                        {                          
                            string progressValue = "    ";
                            if (progressBar != null)
                            {
                                if (progressBar.Value != 0)
                                {
                                    if (prevValue != progressBar.Value)
                                    {
                                        progressValue = progressBar.Value.ToString() + $" progressBar {playerNumber} ";
                                        prevValue = progressBar.Value;
                                    }

                                }
                                if (progressBar.Value == progressBar.Maximum)
                                {
                                    if (resultCharPerSec.Text != "")
                                    {
                                        progressValue = $"{progressBar.Value} progressBar " +
                                        $"{playerNumber} CPM: {resultCharPerSec.Text} " +
                                        $"Time: {LBMIn.Text}:{LBSec.Text}zxc{playerNumber} Finished ";
                                        isFirstResult = false;
                                    }                                 
                                }
                            }
                            byte[] sendBuffer = Encoding.UTF8.GetBytes(progressValue);
                            await stream.WriteAsync(sendBuffer, 0, sendBuffer.Length);
                            if (progressValue == "    ")
                                await Task.Delay(1000);

                        }
                    }
                    catch (Exception ex)
                    {
                        UpdateForm(context);
                    }
                });    
            }               
        }

        private void CalculatePlace(SynchronizationContext context)
        {
            var resultP1 = 0;
            var resultP2 = 0;
            var resultP3 = 0;

            context.Post(value =>
            {
                if (resultLbl1.Text != "")
                {
                    var result1 = resultLbl1.Text.Split(' ');
                    resultP1 = int.Parse(result1[1]);
                }
                if (resultLbl2.Text != "")
                {
                    var result2 = resultLbl2.Text.Split(' ');
                    resultP2 = int.Parse(result2[1]);
                }
                if (resultLbl3.Text != "")
                {
                    var result3 = resultLbl3.Text.Split(' ');
                    resultP3 = int.Parse(result3[1]);
                }
                if (resultP1 >= resultP2 && resultP1 >= resultP3)
                {
                    if (!resultLbl1.Text.Contains("место"))
                        resultLbl1.Text += " 1 место";
                    if (resultLbl2.Text.Contains("место"))
                        resultLbl2.Text = Regex.Replace(resultLbl2.Text, @"[0-9] место", "");
                    if (resultLbl3.Text.Contains("место"))
                        resultLbl3.Text = Regex.Replace(resultLbl3.Text, @"[0-9] место", "");
                }
                if (resultP2 >= resultP1 && resultP2 >= resultP3)
                {                             
                    if (!resultLbl2.Text.Contains("место"))
                        resultLbl2.Text += " 1 место";
                    if (resultLbl1.Text.Contains("место"))
                        resultLbl1.Text = Regex.Replace(resultLbl1.Text, @"[0-9] место", "");
                    if (resultLbl3.Text.Contains("место"))
                        resultLbl3.Text = Regex.Replace(resultLbl3.Text, @"[0-9] место", "");
                }
                if (resultP3 > resultP1 && resultP3 > resultP2)
                {

                    if (!resultLbl3.Text.Contains("место"))
                        resultLbl3.Text += " 1 место";
                    if (resultLbl2.Text.Contains("место"))
                        resultLbl2.Text = Regex.Replace(resultLbl2.Text, @"[0-9] место", "");
                    if (resultLbl1.Text.Contains("место"))
                        resultLbl1.Text = Regex.Replace(resultLbl1.Text, @"[0-9] место", "");
                }
            }, null);

            
        }

        private void ChangeTextInRichTextBox(string text, SynchronizationContext context)
        {
            context.Send(value => {
                if (text.Contains("Welcome to the game."))
                {
                    richTextBox1.Text = "";
                    richTextBox1.Text = $"{text}\n";
                    if (Player1.Text == "")
                    {
                        if (playerNumber == 1)
                        {
                            progressBar = progressBar1;
                            Player1.Text = "Вы";
                            Player2.Text = "Игрок 2";
                            Player3.Text = "Игрок 3";
                        }
                        if (playerNumber == 2)
                        {
                            progressBar = progressBar2;
                            Player1.Text = "Игрок 1";
                            Player2.Text = "Вы";
                            Player3.Text = "Игрок 3";
                        }
                    }
                }
                else if (text.Contains("Однако"))
                {
                    if (playerNumber == 0)
                    {
                        progressBar = progressBar3;
                        Player1.Text = "Игрок 1";
                        Player2.Text = "Игрок 2";
                        Player3.Text = "Вы";                    
                    }
                    richTextBox1.Text = text;
                    textBox1.Enabled = true;
                    wordsInText = richTextBox1.Text.Split(' ');
                    progressBar1.Maximum = progressBar2.Maximum 
                    = progressBar3.Maximum = wordsInText.Length;
                                    
                }
                else if (text.Contains("No active"))
                {
                    richTextBox1.Text = $"{text}\n";
                    serchServBtn.Enabled = true;
                }
                else
                    richTextBox1.Text += $"{text}\n";    
                
            }, null);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isGameStarted)
            {        
                SynchronizationContext context = SynchronizationContext.Current;
                isGameStarted = true;
                timer1.Enabled = true;
                TypingAsync(context);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timeSec++;
                if (timeSec >= 60)
                {
                    timeMin++;
                    timeSec = 0;
                }
            }
            DrawTime();
        }
        public void DrawTime()
        {
            LBMIn.Text = String.Format("{0:00}", timeMin);
            LBSec.Text = String.Format("{0:00}", timeSec);
        }

        private void serchServBtn_Click(object sender, EventArgs e)
        {           
            searchForm.ShowDialog();
            if (searchForm.ServerSelected)
            {
                serchServBtn.Enabled = false;
                SynchronizationContext context = SynchronizationContext.Current;
                CommunicatingWithServerAsync(context);
                progressBar1.Value = progressBar2.Value = progressBar3.Value = 0;
            }
        }

        private void UpdateForm(SynchronizationContext context)
        {
            if(client != null)
                client.Close();
            var message = "No active tcp Connection";
            ChangeTextInRichTextBox(message, context);
            isGameStarted = false;
            playerNumber = 0;
            timeMin = 0;
            timeSec = 0;
            context.Post(value =>
            {
                Player1.Text = Player2.Text = Player3.Text = "";
                progressBar1.Value = progressBar2.Value = progressBar3.Value = 0;
                resultLbl1.Text = resultLbl2.Text = resultLbl3.Text = "";
                resultCharPerSec.Text = resultWordsPerMin.Text = "";
                LBMIn.Text = LBSec.Text = "00";
                serchServBtn.Enabled = true;
                textBox1.Enabled = false;
            }, null);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (client != null)
                client.Close();          
        }
    }
}
