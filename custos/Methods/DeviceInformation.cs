using custos.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using static custos.Methods.SystemInfoDTO;
using System.Net.NetworkInformation;
using custos.DTO;

namespace custos.Methods
{
    public class DeviceInformation
    {
        DeviceDetailsDto devicedetails=new DeviceDetailsDto();
        CommonMethod commonmethod=new CommonMethod();

       

        public  DeviceDetailsDto DeviceInfo()
        {
            Events events = new Events();
            events.Event = "Device Info Fetched";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);
            PerformanceCounter totalVirtualMemoryCounter = new PerformanceCounter("Memory", "Committed Bytes");
            PerformanceCounter availableVirtualMemoryCounter = new PerformanceCounter("Memory", "Available Bytes");

            ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2");
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_BaseBoard");
            ObjectQuery displayquery = new ObjectQuery("SELECT * FROM Win32_VideoController");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectSearcher dissearcher = new ManagementObjectSearcher(scope, displayquery);

            long totalVirtualMemory = (long)totalVirtualMemoryCounter.NextValue();
            long availableVirtualMemory = (long)availableVirtualMemoryCounter.NextValue();

            // Convert bytes to megabytes for better readability
            string totalVirtualMemoryMB = (totalVirtualMemory / (1024.0 * 1024.0)).ToString();
            string availableVirtualMemoryMB = (availableVirtualMemory / (1024.0 * 1024.0)).ToString();


            string DisplayManufacturer = ""; // Initialize the variable outside the loop
            string DisplayDetails = ""; // Initialize the variable outside the loop
            string displayname = ""; // Initialize the variable outside the loop
            string osversion = Environment.OSVersion.Platform.ToString();
            string devicename = Environment.MachineName.ToString();
            string ipaddress = GetIpAddress();
            string macaddress = GetMacAddress();
            string bios = GetBiosSerialNumber();
            string user = System.Environment.MachineName;
            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);

            foreach (ManagementObject queryObj in searcher.Get())
            {
                DisplayManufacturer = queryObj["Manufacturer"].ToString();


            }

            foreach (ManagementObject querydis in dissearcher.Get())
            {

                DisplayDetails = querydis["Description"].ToString();

                displayname = querydis["Name"].ToString();

            }
            var data = new DeviceDetailsDto
            {

               VirtualMemory = totalVirtualMemoryMB,
                AvailableVirtualMemory = availableVirtualMemoryMB,
                DisplayManufacturer = DisplayManufacturer,
                DisplayDetails = DisplayDetails,
                DisplayName = displayname,
                DeviceVersion = osversion,
                DeviceName = devicename,
                BIOS = bios,
                MACAddress = macaddress,
                IPAddress = ipaddress,
                SystemId = user,
                TimeStamp = time,
            };
            SendDeviceInfo(data);
            return data;
            
        }
        static string GetIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);

            foreach (IPAddress ipAddress in ipEntry.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddress.ToString();
                }
            }

            return "N/A";
        }
        static string GetBiosSerialNumber()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS"))
            {
                ManagementObjectCollection biosCollection = searcher.Get();
                foreach (ManagementObject bios in biosCollection)
                {
                    return bios["SerialNumber"].ToString();
                }
            }
            return "N/A";
        }

        public static string GetMacAddress()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                //if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                //	networkInterface.OperationalStatus == OperationalStatus.Up)
                //{
                //	return networkInterface.GetPhysicalAddress().ToString();
                //}
                string tempMac = networkInterface.GetPhysicalAddress().ToString();
                if (!string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= 8)
                {
                    return tempMac;
                    //return macAddress;
                }

            }
            return "N/A";
        }
        public async Task SendDeviceInfo(DeviceDetailsDto data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(APIUrls.DeviceInformation_url, content);
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
