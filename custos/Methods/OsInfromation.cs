using custos.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using static custos.Methods.SystemInfoDTO;

namespace custos.Methods
{
    public class OsInfromation
    {

        CommonMethod method = new CommonMethod();
        ManagementObjectSearcher searcher;
        ManagementObjectSearcher searcher1;

        public ManagementObjectSearcher OsInformation()
        {

            try
            {

                Events events = new Events();
                events.Event = "Os Information Fetched";
                events.EventDate = DateTime.Now;
                events.SystemId = System.Environment.MachineName;
                method.EventLog(events);

                searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
                //searcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");


            }
            catch(Exception ex) {
                


            }
            return searcher;
        }

        public ManagementObjectSearcher OsInformation1()
        {

            try
            {

                Events events = new Events();
                events.Event = "Os Information Fetched";
                events.EventDate = DateTime.Now;
                events.SystemId = System.Environment.MachineName;
                method.EventLog(events);

                //searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
                searcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");


            }
            catch (Exception ex)
            {



            }
            return searcher1;
        }
        public async Task SendOSInfo(OSDTO data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(OSInfoAPI.OS_url, content);
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
    public class OSDTO
    {
        public string OS_Name;
        public string OS_Version;
        public string OS_Architecture;
        public string Build_Number;
        public string Manufacturer;
        public string LastBootUpTime;
        public string NoOfDaysLastSystemBoot;
        public string SerialNumber;
        public string MembershipType;
        public string LastLogged;
        public string LastLoggedUserRole;
        public string CurrentUser;

    }
    public class OSInfoAPI
    {

        public static string OS_url = "";
        //public static string Hardware_url = "";

    }
}
