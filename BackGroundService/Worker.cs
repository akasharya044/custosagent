using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;



namespace BackGroundService
{


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

    public class MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;

        public MEMORYSTATUSEX()
        {
            dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
        }
    }


    [return: MarshalAs(UnmanagedType.Bool)]

    //Memory Usages Files 


    public class Worker : BackgroundService
    {

        //memory usages
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);



        private readonly ILogger<Worker> _logger;

        private System.Threading.Timer _timer1, _timer2, _timer3;
        private string apiUrl = "http://65.2.100.52:1050";
        //private string apiUrl = "https://localhost:7237";


        private string userId = System.Environment.MachineName; // for dynamic user
        private List<ProgramData> allprogramData;
        private List<ProgramDatas> allProgramDatas = new List<ProgramDatas>();

        private List<ProgramData> jsondata { get; set; } = new List<ProgramData>();
        private List<ProgramData> jsondataread { get; set; } = new List<ProgramData>();

        private bool ispop = false;


        //for single Threshold Only
        private double memoryThreshold = 99; //Define Custom Range Here 0 to 100 will treat like Percentage like 25 = 25%
        private long networkThreshold = 3314570000;  //give data in byte ( 3 GB)
        private double cpuThreshold = 95;  //equals to 70%

        //For program wise threshold only 
        private double memoryThresholds = 0.8; // give threshold here.
        private double networkThresholds = 0.8; //
        private double CpuThresholds = 0.8;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            allprogramData = new List<ProgramData>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _timer1 = new System.Threading.Timer(StartService, null, 0, 300000); //will run the program every 60 sec
            _timer2 = new System.Threading.Timer(StartServiceProgramData, null, 0, 60000);

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _timer1?.Change(Timeout.Infinite, 0);
            _timer2?.Change(Timeout.Infinite, 0);

            return base.StopAsync(cancellationToken);
        }

        private async void StartServiceProgramData(object state)
        {
            List<ProgramDatas> softwareInf = new List<ProgramDatas>();

            using (PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                cpuCounter.NextValue();

                await Task.Run(() =>
                {
                    Parallel.ForEach(Process.GetProcesses(), p =>
                    {
                        try
                        {
                            string processName = p.ProcessName;
                            string MainWindowTitle = string.IsNullOrEmpty(p.MainWindowTitle) ? "N/A" : p.MainWindowTitle;
                            long memorySize = p.WorkingSet64;
                            string moduleName = p.MainModule?.ModuleName ?? "N/A";
                            DateTime dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                            long processid = p.Id;
                            float cpuUsage = GetRoundedCpuUsageForProcess(cpuCounter);
                            double networkspeed = GetNetworkSpeed();

                            var software = new ProgramDatas
                            {

                                ProgramNames = processName,
                                UserId = System.Environment.MachineName,
                                MemoryUsages = memorySize,

                                TimeStamp = dateTime,
                                PIDs = processid.ToString(),
                                CPUUsgaess = cpuUsage,
                                NetworkSpeeds = networkspeed

                            };

                            softwareInf.Add(software);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing {p.ProcessName}: {ex.Message}");
                        }
                    });
                });


                SendDatasToApi(softwareInf);
                Task.Delay(2000);

            }


        }
        private double GetNetworkSpeed()
        {
            try
            {
                long totalBytesSent = 0;
                long totalBytesReceived = 0;

                foreach (var networkInterface in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (networkInterface.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                    {
                        totalBytesSent += networkInterface.GetIPStatistics().BytesSent;
                        totalBytesReceived += networkInterface.GetIPStatistics().BytesReceived;
                    }
                }

                // Calculate network speed in bytes per second
                long bytesPerSecond = totalBytesSent;
                return (double)bytesPerSecond;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error measuring network speed: {ex.Message}");
                return 0.0; // Return 0.0 in case of an error.
            }
        }

        private int GetRoundedCpuUsageForProcess(PerformanceCounter cpuCounter)
        {
            System.Threading.Thread.Sleep(100);
            int roundedCpuUsage = (int)Math.Round(cpuCounter.NextValue());
            return roundedCpuUsage;
        }






        private async void StartService(object state)
        {

            bool ispop = false;

            try
            {
                Process[] processes = Process.GetProcesses();
                double cpuUsage = CpuUsages();
                double memoryUsage = MemoryUsagePercentage();
                double networkusage = GetNetworkSpeed();
                string message1 = System.String.Empty;
                string message2 = System.String.Empty;
                string message3 = System.String.Empty;
                DateTime timestamp1 = DateTime.Now;
                DateTime timestamp2 = DateTime.Now;
                DateTime timestamp3 = DateTime.Now;


                if (cpuUsage > cpuThreshold)
                {
                    try
                    {
                        Console.WriteLine($"CPU Usage ({cpuUsage}%) is higher than the threshold ({cpuThreshold}%)");
                        message1 = $"CPU Usage {cpuUsage}% is higher than the threshold {cpuThreshold}%";
                        timestamp1 = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        //if (!ispop)
                        //{
                        //    Alert alert = new Alert("CPU Alert", message1);
                        //    alert.ShowDialog();
                        //    ispop = true;
                        //}


                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (memoryUsage > memoryThreshold)
                {
                    Console.WriteLine($"Memory Usage ({memoryUsage}%) is higher than the threshold ({memoryThreshold}%)");
                    message2 = "Memory Usage " + memoryUsage + " % is higher than the threshold " + memoryThreshold + " %";
                    timestamp2 = DateTime.UtcNow.AddHours(5).AddMinutes(30);



                    //if (!ispop)
                    //{

                    //    Alert alert = new Alert("Memory Alert", message2);
                    //    alert.ShowDialog();
                    //    ispop = true;
                    //}




                }

                if (networkusage > networkThreshold)
                {

                    message3 = "Network Usages is higher than the threshold usages";
                    timestamp3 = DateTime.UtcNow.AddHours(5).AddMinutes(30);

                    //if (!ispop)
                    //{
                    //    Alert alert = new Alert("Network Alert", message3);

                    //    alert.ShowDialog();
                    //    ispop = true;
                    //}


                }

                ProgramData prog = new ProgramData();
                prog.UserId = userId;
                prog.CpuUsgaes = cpuUsage;
                prog.NetworkUsgaes = networkusage;
                prog.MemoryUsages = memoryUsage;
                if (message1 != null || timestamp1 != null)
                {
                    prog.CPU_Alert = message1;
                    prog.CPU_TimeStamp = timestamp1;
                }
                if (message2 != null || timestamp2 != null)
                {
                    prog.Memory_Alert = message2;
                    prog.Memory_TimeStamp = timestamp2;
                }
                if (message3 != null || timestamp3 != null)
                {
                    prog.Network_Alert = message3;
                    prog.Network_TimeStamp = timestamp3;
                }

                allprogramData.Add(prog);
                SendDataToApi(prog);









            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Information: {ex.Message}");
                Console.WriteLine("Error Information:" + ex.ToString());
            }




        }


        private double CpuUsages()
        {
            try
            {
                PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                Thread.Sleep(1000); // Sleep for a short duration to get initial CPU usage


                for (int i = 0; i < 5; i++)
                {
                    double cpuUsage = cpuCounter.NextValue();

                    Thread.Sleep(1000);
                }

                // Return the last observed CPU usage
                return Math.Round(cpuCounter.NextValue());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Information: {ex.Message}");
                Console.WriteLine($"Error Information: {ex}");
                return 0;
            }
        }


        private double MemoryUsagePercentage()
        {
            try
            {
                MEMORYSTATUSEX memoryStatus = new MEMORYSTATUSEX();
                GlobalMemoryStatusEx(memoryStatus);


                double memoryUsagePercentage = 100.0 - ((double)memoryStatus.ullAvailPhys / memoryStatus.ullTotalPhys * 100.0);


                return Math.Round(memoryUsagePercentage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Information: {ex.Message}");
                Console.WriteLine($"Error Information: {ex}");
                return 0;
            }
        }


        private double GetNetworkSpeeds(string processName)
        {
            try
            {
                long totalBytesSent = 0;
                long totalBytesReceived = 0;

                foreach (var networkInterface in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (networkInterface.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                    {
                        totalBytesSent += networkInterface.GetIPStatistics().BytesSent;
                        totalBytesReceived += networkInterface.GetIPStatistics().BytesReceived;
                    }
                }

                // Calculate network speed in bytes per second
                long bytesPerSecond = totalBytesSent;
                return (double)bytesPerSecond;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error measuring network speed: {ex.Message}");
                return 0.0; // Return 0.0 in case of an error.
            }
        }




        //API To Send Data

        private void SendDataToApi(ProgramData data)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(apiUrl);

                    string userJson = System.Text.Json.JsonSerializer.Serialize(data);

                    HttpResponseMessage postResponse = httpClient.PostAsync("/api/additional/additionalinfo/BackgroundService", new StringContent(userJson, Encoding.UTF8, "application/json")).Result;

                    if (postResponse.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"User program data added or updated successfully for UserId: {data.UserId}");
                    }
                    else
                    {
                        _logger.LogError($"Failed to post user program data. Status Code: {postResponse.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending data to API: {ex.Message}");
            }
        }


        //look this also
        private void SendDatasToApi(List<ProgramDatas> datas)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(apiUrl);

                    string userJson = System.Text.Json.JsonSerializer.Serialize(datas);

                    HttpResponseMessage postResponse = httpClient.PostAsync("/api/additional/additionalinfo/addProgramdata", new StringContent(userJson, Encoding.UTF8, "application/json")).Result;

                    if (postResponse.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"User program data added or updated successfully for UserId");
                    }
                    else
                    {
                        _logger.LogError($"Failed to post user program data. Status Code: {postResponse.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending data to API: {ex.Message}");
            }
        }

    }





    public class ProgramData
    {
        public string UserId { get; set; }
        public double? MemoryUsages { get; set; }
        public double? CpuUsgaes { get; set; }

        public double? NetworkUsgaes { get; set; }

        public string? CPU_Alert { get; set; }

        public DateTime? CPU_TimeStamp { get; set; }

        public string? Memory_Alert { get; set; }

        public DateTime? Memory_TimeStamp { get; set; }

        public string? Network_Alert { get; set; }

        public DateTime? Network_TimeStamp { get; set; }

    }


    public class ProgramDatas
    {

        public string UserId { get; set; }

        public string ProgramNames { get; set; }

        public string PIDs { get; set; }

        public long MemoryUsages { get; set; }

        public double NetworkSpeeds { get; set; }
        public int Id { get; set; }

        public double CPUUsgaess { get; set; }

        public DateTime? TimeStamp { get; set; }
    }
}
