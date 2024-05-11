using custosWorker.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using custos.DTO;
using System.Globalization;
using System.Reflection;
using System.IO.Compression;





namespace custosWorker.Forms
{
    public partial class WorkerForm : Form
    {
        private NotifyIcon trayIcon;
        private readonly IBus _busControl;
        private string systemid = System.Environment.MachineName;
        string basepath = AppDomain.CurrentDomain.BaseDirectory;
        string settingspath = "";
        private System.Windows.Forms.Timer timer;


        public WorkerForm()
        {

            InitializeComponent();
            InitializeTrayIcon();
            InitializeTimer();
            this.Resize += SetMinimizeState;
            trayIcon.DoubleClick += ToggleMinimizeState;

            settingspath = Path.Combine(basepath, "appsettings.Production.json");

            string jsondata = File.ReadAllText(settingspath);
            dynamic output = JsonConvert.DeserializeObject<object>(jsondata);

            _busControl = RabbitConnector.CreateBus(output.RabittMqHost.ToString());

            Scheduler();
            Task.Run(async () =>
            {
                await _busControl.ReceiveFromExchangeAsync<string>("CustOsMessageExchange", async x =>
                {
                    Console.WriteLine(x);
                    label3.Text = x;


                });

                await _busControl.RabbitReceiveAsync<string>("CustOsMessage", async x =>
                {
                    // Implement the action to be taken upon receiving a message
                    Console.WriteLine("Received message: " + x);

                });
            }, CancellationToken.None);



        }




        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000; // Check every 5 seconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (IsInternetConnected())
            {

                label1.Text = "CustOs Connected To Server/Internet";

            }
            else
            {
                label1.Text = "Not Connected To Server/Internet";
            }
        }


        private void Scheduler()
        {
            // MessageBox.Show("enter in scheduler method");

            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        //MessageBox.Show("enter in try block of scheduler");
                        string systemid=System.Environment.MachineName.ToString();
                      
                        await SendHeartBeat();
                        

                    }
                    catch (Exception ex)
                    {
                        Logwrite.LogWrite("Exception in Scheduler Method - " + ex.Message);



                    }


                    await Task.Delay(30000);
                    
                }
            });
        }

        private async Task SendHeartBeat()
        {
            try
            {
                //MessageBox.Show("enter in sendheartbeat block");

                await _busControl.SendAsync<string>("CustOsdashboarddataHb", systemid);

                Logwrite.LogWrite("Heartbeat Received" + DateTime.Now.ToString());
                WriteTolblhb("Heartbeat- " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Logwrite.LogWrite("Exception in SendHeartBeat Method : " + ex.Message);
            }

        }



        public void WriteTolblhb(string text)
        {
            try
            {
                if (label2.InvokeRequired)
                {
                    Action safeWrite = delegate { WriteTolblhb($"{text}"); };
                    label2.Invoke(safeWrite);
                }
                else
                    label2.Text = text;
            }
            catch (Exception ex)
            {
                Logwrite.LogWrite("Exception in WriteTolblhb Method - " + ex.Message);

            }
        }

        public void WriteToFd(string text)
        {
            try
            {
                if (label3.InvokeRequired)
                {
                    Action safeWrite = delegate { WriteToFd($"{text}"); };
                    label3.Invoke(safeWrite);
                }
                else
                    label3.Text = text;
            }
            catch (Exception ex)
            {
                Logwrite.LogWrite("Exception in WriteToFd Method - " + ex.Message);
            }
        }

        private async Task OnFeedBackReceived(string message)
        {
            try
            {
                if (message.ToLower() != null && message.ToLower() == System.Environment.MachineName.ToLower())
                {

                    WriteToFd("Received :" + DateTime.Now.ToString());
                    label3.Text = message;

                }
            }
            catch (Exception ex)
            {
                Logwrite.LogWrite("Exception in OnFeedBackReceived Method: " + ex.Message);
            }
        }




        private void InitializeTrayIcon()
        {
            trayIcon = new NotifyIcon();
            trayIcon.Text = "CustOs";
            trayIcon.Icon = this.Icon;

        }


        private void ToggleMinimizeState(object sender, EventArgs e)
        {
            try
            {
                bool isMinimized = this.WindowState == FormWindowState.Minimized;
                this.WindowState = (isMinimized) ? FormWindowState.Normal : FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in ToggleMinimizeState Method - " + ex.Message);
            }
        }
        private void SetMinimizeState(object sender, EventArgs e)
        {

            bool isMinimized = this.WindowState == FormWindowState.Minimized;
            this.ShowInTaskbar = !isMinimized;
            trayIcon.Visible = isMinimized;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Worker_Load(object sender, EventArgs e)
        {
            // MessageBox.Show("Enter in FrmWorker_Load");
            this.WindowState = FormWindowState.Minimized;//
            toolTip2.AutoPopDelay = 5000;
            toolTip2.InitialDelay = 1000;
            toolTip2.ReshowDelay = 500;
            toolTip2.IsBalloon = true;
            toolTip2.ToolTipTitle = "CustOs";
            toolTip2.ToolTipIcon = ToolTipIcon.Info;
            toolTip2.UseAnimation = true;
            toolTip2.UseFading = true;
            //Force the ToolTip text to be displayed whether or not the form is active.
            toolTip2.ShowAlways = true;
            //ToolTip
            trayIcon.BalloonTipText = "CustOs [Working]";
            trayIcon.BalloonTipTitle = "CustOs";
            RegisterDevice();
            checkforupdate();
            Autocleanup();
            diskcheckup();




        }


        private void Worker_Closed(object sender, FormClosedEventArgs e)
        {
            base.OnClosed(e);
            trayIcon.Dispose();
            GC.WaitForFullGCComplete();
        }

        private void Worker_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;

            }
            else
            {
                e.Cancel = false;
            }
        }


        private bool IsInternetConnected()
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("www.google.com");
                return reply.Status == IPStatus.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkforupdate()
        {
            WebClient webClient = new WebClient();
            var client = new WebClient();
            if (!webClient.DownloadString("http://65.2.100.52:1008/Version.txt").Equals("2.0.8"))
            {
                if (MessageBox.Show("A new update is available! Do you want to download it?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    try
                    {
                       

                        using (Process compiler = new Process())
                        {
                            compiler.StartInfo.FileName = "cmd.exe";
                            compiler.StartInfo.Arguments = "msiexec /x {32E81D02-C171-4E69-92E1-7978983ED7AF}";
                            compiler.StartInfo.UseShellExecute = false;
                            compiler.StartInfo.RedirectStandardOutput = true;
                            compiler.StartInfo.CreateNoWindow = true;
                            compiler.Start();


                            compiler.Close();
                        }
                        string path = "C:\\Program Files (x86)\\RCPL\\CustOs";
                        DirectoryInfo dir = new DirectoryInfo(path);

                        if (Directory.Exists(path))
                        {

                            try
                            {
                                dir.Delete(true);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error deleting directory: {ex.Message}");
                            }
                        }
                        if (File.Exists(@".\CustOsSetup.msi")) { File.Delete(@".\CustOsSetup.msi"); }
                        client.DownloadFile("http://65.2.100.52:1008/CustOsSetup.zip", @"CustOsSetup.zip");
                        string zipPath = @".\CustOsSetup.zip";
                        string extractPath = @".\";
                        ZipFile.ExtractToDirectory(zipPath, extractPath);
                        Process process = new Process();
                        process.StartInfo.FileName = "msiexec.exe";
                        process.StartInfo.Arguments = string.Format("/i CustOsSetup.msi");
                        this.Close();
                        process.Start();
                    }
                    catch (Exception ex) { }
                    {

                    }
                }

            }
        }

        private async void RegisterDevice()
        {
            try
            {
                string macAddress = GetMacAddress().ToString(); 
                RegisterDeviceDTO cust = new RegisterDeviceDTO();
                cust.entity_type = "Device";
                cust.IsDeleted = false;
                
                string publicip = GetPublicIPAddress();
                cust.IpAddress = publicip;
                TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                long secondsSinceEpoch = (long)t.TotalSeconds;
                cust.MacAddress = macAddress;
                cust.DisplayLabel = System.Environment.MachineName;
                cust.LastUpdateTime = secondsSinceEpoch;

                cust.AgentVersion = "2.0.8";
                cust.SubType = "Desktop";
                
                cust.systemid = System.Environment.MachineName;
                cust.Location = GetUserCountryByIp(publicip);
                string jsondata = File.ReadAllText(settingspath);
                dynamic output = JsonConvert.DeserializeObject<object>(jsondata);
                string registerurl = output.REGISTER_DEVICE.ToString();

                using (HttpClient httpClient = new HttpClient())
                {
                    var jsonData = JsonConvert.SerializeObject(cust);
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(registerurl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Response responseModel = JsonConvert.DeserializeObject<Response>(responseBody);
                        string test = "T";

                    }
                }

            }
            catch (Exception ex)
            {

            }

        }

        static string GetMacAddress()
        {
            string macAddress = "";

            try
            {

                NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                foreach (NetworkInterface nic in networkInterfaces)
                {


                    string tempMac = nic.GetPhysicalAddress().ToString();
                    if (!string.IsNullOrEmpty(tempMac) &&
                        tempMac.Length >= 8)
                    {
                        macAddress = tempMac;
                        return macAddress;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error retrieving MAC address: " + ex.Message);
            }

            return macAddress;
        }

        public static string GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                
            }
            catch (Exception)
            {
                ipInfo.Loc = null;
            }

            return ipInfo.Loc;
        }

        private string GetPublicIPAddress()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string ipAddress = client.DownloadString("https://api.ipify.org");
                    return ipAddress;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching public IP address: " + ex.Message);
                return null;
            }
        }
        
        private async void Autocleanup()
        {
            try
            {
                string TempPath = Path.GetTempPath();
                long folderthreshold = 1452515229;
                if (Directory.Exists(TempPath))
                {
                    DirectoryInfo folder = new DirectoryInfo(TempPath);
                    long totalFolderSize = folderSize(folder);
                    if (totalFolderSize > folderthreshold)
                    {
                        File.Delete(TempPath);
                    }
                }
            }
            catch(Exception ex)
            {

            }

        }

        static long folderSize(DirectoryInfo folder)
        {
            long totalSizeOfDir = 0;

           
            FileInfo[] allFiles = folder.GetFiles();
            foreach (FileInfo file in allFiles)
            {
                totalSizeOfDir += file.Length;
            }
            DirectoryInfo[] subFolders = folder.GetDirectories();
            foreach (DirectoryInfo dir in subFolders)
            {
                totalSizeOfDir += folderSize(dir);
            }
            return totalSizeOfDir;
        }


        private void diskcheckup()
        {
            try
            {
               
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c chkdsk C: /scan /forceofflinefix")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string errors = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                   
                    if (output.Contains("found and fixed")) 
                    {
                        
                        Alert alert = new Alert("Attention", "Corrupt Files Found and Fixed");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to run chkdsk: {ex.Message}");
            }
        }

        



        public class IpInfo
        {
            [JsonProperty("ip")]
            public string Ip { get; set; }

            [JsonProperty("hostname")]
            public string Hostname { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("region")]
            public string Region { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("loc")]
            public string Loc { get; set; }

            [JsonProperty("org")]
            public string Org { get; set; }

            [JsonProperty("postal")]
            public string Postal { get; set; }
        }
    }



}











