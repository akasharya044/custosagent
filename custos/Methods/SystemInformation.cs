using System.Collections;
using System.Diagnostics;
using System.Management;
using System.Net.NetworkInformation;
using custos.Common;
using custos.DTO;
using Microsoft.Win32;
using Newtonsoft.Json;
//using static custos.Methods.SystemInfoDTO;
//using static custos.Methods.SystemInfoDTO;

namespace custos.Methods
{
    public class SystemInfoMethod
    {
        CommonMethod commonmethod = new CommonMethod();
        List<HardwareInformationDto> hardwareInfo { get; set; } = new List<HardwareInformationDto>();
        private List<HardwareInformationDto> jsondata = new List<HardwareInformationDto>();


        private List<HardwareInformationDto> jsondataread = new List<HardwareInformationDto>();


        SqLiteConn sqlite;


        public async Task<List<SoftwareInformationDto>> SoftwareData()
        {
            Events events = new Events();
            events.Event = "Software Info Fetched";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);
            List<SoftwareInformationDto> softwareInf = new List<SoftwareInformationDto>();

           
            

            using (PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total" , true))
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
                            DateTime dateTime = p.StartTime;
                            long processid = p.Id;
                            float cpuUsage =   GetRoundedCpuUsageForProcess(cpuCounter); ////cpuCounter.NextValue() / Environment.ProcessorCount;
                            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);

                            var software = new SoftwareInformationDto
                            {
                                WindowTitle = MainWindowTitle + Environment.NewLine,
                                ProcessName = processName,
                                SystemId = System.Environment.MachineName,
                                MemorySize = memorySize.ToString(),
                                ModuleName = moduleName,
                                Starttime = dateTime.ToString(),
                                Pid = processid.ToString(),
                                CpuUsage = cpuUsage.ToString(),
                                TimeStamp = time

                            };

                            softwareInf.Add(software);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing {p.ProcessName}: {ex.Message}");
                        }
                    });
                });

                // Call the SendSoftwareInfo method 

            }

            return softwareInf;
        }

        private int GetRoundedCpuUsageForProcess(PerformanceCounter cpuCounter)
        {
            Thread.Sleep(100);
            int roundedCpuUsage = (int)Math.Round(cpuCounter.NextValue() / Environment.ProcessorCount);
            return roundedCpuUsage;
        }






        public async Task<List<HardwareInformationDto>> HardwareData()
        {
            sqlite = new SqLiteConn();
            jsondataread = sqlite.HardwareInformationTable("HardwareInformation");
            ManagementObjectSearcher searcher;
            int i = 0;
            ArrayList arrayListInformationCollactor = new ArrayList();
            try
            {
                Events events = new Events();
                events.Event = "Hardware Info Fetched";
                events.EventDate = DateTime.Now;
                events.SystemId = System.Environment.MachineName;
               await commonmethod.EventLog(events);
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (ManagementObject mo in searcher.Get())
                {
                    i++;
                    PropertyDataCollection searcherProperties = mo.Properties;
                    DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    foreach (PropertyData sp in searcherProperties)
                    {
                        HardwareInformationDto hardwaredto = new();

                        hardwaredto.SystemId = System.Environment.MachineName;
                        hardwaredto.TimeStamp = time;
                        hardwaredto.Name = sp.Name;
                        hardwaredto.IsArray = sp.IsArray;
                        hardwaredto.IsLocal = sp.IsLocal;
                        hardwaredto.Origin = sp.Origin;
                        hardwaredto.Type = sp.Type != null ? sp.Type.ToString() : "";
                        hardwaredto.Value = sp.Value != null ? sp.Value.ToString() : "";
                        hardwaredto.Qualifires = sp.Qualifiers != null ? sp.Qualifiers.ToString() : "";
                        hardwareInfo.Add(hardwaredto);
                        arrayListInformationCollactor.Add(sp);

                    }
                }
                jsondata = hardwareInfo;
                List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(jsondata);
                if (jsondataread.Count() == 0)
                {
                    sqlite.InsertDataIntoTable("HardwareInformation", dict);
                    await SendHardwareInfo(hardwareInfo);
                }
                else
                {
                    //jsondataread = sqlite.ReadTable("windowServices");


                    CompareData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            // await SendHardwareInfo(hardwareInfo);
            return hardwareInfo;
        }


        private async void CompareData()
        {

            List<HardwareInformationDto> data1 = new List<HardwareInformationDto>();
            foreach (var item in jsondataread)
            {
                var item1 = hardwareInfo.FirstOrDefault(x => x.Name == item.Name);
                if (item1 != null)
                {
                    bool check = AreEquivalent(item1, item);
                    if (!check)
                    {
                        data1.Add(item1);
                    }
                }
            }
            if (data1.Count > 0)
            {
                await SendHardwareInfo(data1);
                List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(hardwareInfo);

                //.ToList();
                sqlite.InsertDataIntoTable("HardwareInformation", dict);
            }

        }



        bool AreEquivalent(HardwareInformationDto s, HardwareInformationDto j)
        {
            return s.IsLocal == j.IsLocal &&
                   s.IsArray == j.IsArray &&
                    s.Qualifires == j.Qualifires &&
                    s.Origin == j.Origin &&
                    s.Name == j.Name &&
                    s.SystemId == j.SystemId &&
                    s.Type == j.Type &&
                    s.Value == j.Value;



        }

        public async Task SendHardwareInfo(List<HardwareInformationDto> data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(APIUrls.Hardware_url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Response responseModel = JsonConvert.DeserializeObject<Response>(responseBody);
                        if (responseModel != null)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

    }




}