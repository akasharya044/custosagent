using custos.Common;
using custos.Controls.SubControl;
using custos.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace custos.Forms
{
    public partial class Registration : Form
    {

        string name, email, contact, devicetype;
        string settingspath = "";

        public Registration()
        {
            InitializeComponent();
            listBox1.Items.Add("Choose Device Type");
            listBox1.Items.Add("Laptop");
            listBox1.Items.Add("Desktop");


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

        private async void form_data()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    UserRegistrationDto userDto = new UserRegistrationDto();
                    userDto.Name = textBox1.Text;
                    userDto.Email = textBox2.Text;
                    userDto.PhoneNo = textBox4.Text;
                    userDto.SystemId = System.Environment.MachineName;
                    userDto.DisplayLabel = System.Environment.MachineName;
                    userDto.RegistrationDateTime = DateTime.Now;
                    userDto.DeviceType = listBox1.Text;
                    userDto.UniqueKey = textBox3.Text;
                    userDto.MacAddress = GetMacAddress().ToString();
                    userDto.IpAddress = GetPublicIPAddress();
                    userDto.Location = GetUserCountryByIp(userDto.IpAddress);
                    userDto.AgentVersion = "2.0.8";
                    userDto.IsRegistered = true;
                    userDto.IsDelete = false;

                    string jsondata = JsonConvert.SerializeObject(userDto);
                    var content = new StringContent(jsondata,System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(APIUrls.UserRegistration_url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Response responseModel = JsonConvert.DeserializeObject<Response>(responseBody);
                        if (responseModel.Message.Contains("Invalid_Key"))
                        {
                            SubmitAlert alert = new SubmitAlert("Alert", "Invalid Key");
                            alert.Show();


                        }
                        else if (responseModel.Message.Contains("Key_Already_Used"))
                        {
                            SubmitAlert alert = new SubmitAlert("Alert", "Key Already Used");
                            alert.Show();

                        }
                        else
                        {
                            SubmitAlert alert = new SubmitAlert("Registration Done", "You are successfully Registered");
                            this.Close();
                            alert.Show();
                        }

                        

                    }
                    else
                    {
                        SubmitAlert alert = new SubmitAlert("Failed Registration ", "Failed To Register ");
                        alert.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                SubmitAlert alert = new SubmitAlert("Failed Registration ", "Failed To  Register");
                alert.ShowDialog();
            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            string namePattern = @"^[a-zA-Z\s]+$";
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string mobilePattern = @"^\d{10}$";

            // Check if the name matches the pattern
            if (textBox1.Text == "" || !Regex.IsMatch(textBox1.Text, namePattern))
            {
                


                SubmitAlert alert = new SubmitAlert("Alert", "Invalid Name");
                alert.Show();

               
            }
         
            else if (textBox2.Text == "" || !Regex.IsMatch(textBox2.Text, emailPattern))
            {
                SubmitAlert alert = new SubmitAlert("Alert", "Invalid E-mail");
                alert.Show();
            }
            else if (textBox4.Text == "" || !Regex.IsMatch(textBox4.Text, mobilePattern))
            {
                SubmitAlert alert = new SubmitAlert("Alert", "Invalid Contact Number");
                alert.Show();
            }
            else if (listBox1.SelectedItem == null || listBox1.SelectedItem.ToString() == "Choose Device Type")
            {
                SubmitAlert alert = new SubmitAlert("Alert", "Select Device Type Mandatory");
                alert.Show();
            }
            else if(textBox3.Text == "")
            {
                SubmitAlert alert = new SubmitAlert("Alert", "Enter Unique Key Please");
                alert.Show();
            }
            else
            { 

                form_data();
            }

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            string processName = "custos";


            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length > 0)
            {
                processes[0].Kill();

            }
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string processName = "custos";


            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length > 0)
            {
                processes[0].Kill();

            }
            this.Close();
        }
    }
}
