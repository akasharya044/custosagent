using custos.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static custos.Methods.DeviceInformation;
using custos.DTO;
using custos.Common;
using Newtonsoft.Json;

namespace custos.Controls.SubControl
{
    public partial class Deviceinfo : UserControl
    {
        private List<DeviceDetailsDto> jsondata = new List<DeviceDetailsDto>();

        List<DeviceDetailsDto> jsondataread = new List<DeviceDetailsDto>();
        DeviceDetailsDto dev;
        DeviceInformation device;
        SqLiteConn sqlite;
        public Deviceinfo()
        {
            InitializeComponent();
        }

        private async void device_load(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            devicedata();

        }



        private bool AreEqual(DeviceDetailsDto newData, DeviceDetailsDto existingData)
        {
            // Compare properties of newData and existingData to check if they are equal
            return newData.AvailableVirtualMemory == existingData.AvailableVirtualMemory &&
                   newData.DisplayDetails == existingData.DisplayDetails &&
                   newData.DeviceVersion == existingData.DeviceVersion &&
                   newData.DeviceName == existingData.DeviceName &&
                   newData.BIOS == existingData.BIOS &&
                   newData.IPAddress == existingData.IPAddress &&
                   newData.MACAddress == existingData.MACAddress &&
                   newData.DisplayManufacturer == existingData.DisplayManufacturer &&
                   newData.VirtualMemory == existingData.VirtualMemory &&
                   newData.SystemId == existingData.SystemId &&
                   newData.DisplayName == existingData.DisplayName;



        }
        private async void CompareData()
        {
            try
            {
                //foreach (var newData in os_data)
                //{

                var particulardata = jsondataread.Find(x => x.SystemId == dev.SystemId);
                if (particulardata == null)
                {
                    // If the data doesn't exist in the database, insert it
                    await device.SendDeviceInfo(dev);
                }
                else if (!AreEqual(dev, particulardata))
                {
                    // If the data exists but has changed, update it
                    jsondataread.Clear();
                    jsondataread.Add(dev);
                    await device.SendDeviceInfo(dev);
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        public async void devicedata()
        {
            SqLiteConn sqlite = new SqLiteConn();
            
            jsondataread = sqlite.ReaddeviceTable("deviceInformation");
         
            device = new DeviceInformation();
            dev = device.DeviceInfo();  // Assign the result of DeviceInfo() to dev



            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Device Version: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(Environment.OSVersion.Platform.ToString() + "\n\n");

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Device Name: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(Environment.MachineName.ToString() + "\n\n");


            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("BIOS NO: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(GetBiosSerialNumber().ToString() + "\n\n");

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("MAC Address: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(GetMacAddress().ToString() + "\n\n");

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("IP Address: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(GetIpAddress().ToString() + "\n\n");


            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Total Virtual Memory: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(dev.VirtualMemory + " MB" + "\n\n");

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Total AvailabeVirtualMemory: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(dev.AvailableVirtualMemory + " MB" + "\n\n");


            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Display Manufacturer: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(dev.DisplayManufacturer + "\n\n");

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Display Name: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(dev.DisplayName + "\n\n");

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Display Details: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(dev.DisplayDetails + "\n\n");
            jsondata.Add(dev);
            List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(jsondata);
            if (jsondataread.Count() == 0)
            {
                sqlite.InsertDataIntoTable("deviceInformation", dict);
               
                await device.SendDeviceInfo(dev);
            }
            else
            {
                CompareData();
            }



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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
