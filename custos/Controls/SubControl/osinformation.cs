using custos.Common;
using custos.DTO;
using custos.Methods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace custos.Controls.SubControl
{
    public partial class osinformation : UserControl
    {
        OsInfromation os = new OsInfromation();
        //OSDto existingdata=new OSDto();
        private List<OSDto> jsondata = new List<OSDto>();
        OSDto os_data = new OSDto();
        List<OSDto> jsondataread = new List<OSDto>();
        public osinformation()
        {
            InitializeComponent();
        }




        private async void CompareData()
        {
            try
            {
                //foreach (var newData in os_data)
                //{

                var particulardata = jsondataread.Find(x => x.SystemId == os_data.SystemId);
                if (particulardata == null)
                {
                    // If the data doesn't exist in the database, insert it
                    //jsondataread.Add(os_data);
                    await sendOSInfo(os_data);
                }
                else if (!AreEqual(os_data, particulardata))
                {
                    // If the data exists but has changed, update it
                    jsondataread.Clear();
                    jsondataread.Add(os_data);
                    await sendOSInfo(os_data);
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }







        private bool AreEqual(OSDto newData, OSDto existingData)
        {
            // Compare properties of newData and existingData to check if they are equal
            return newData.NoOfDaysLastSystemBoot == existingData.NoOfDaysLastSystemBoot &&
                   newData.Manufacturer == existingData.Manufacturer &&
                   newData.LastBootUpTime == existingData.LastBootUpTime &&
                   newData.LastLogged == existingData.LastLogged &&
                   newData.MembershipType == existingData.MembershipType &&
                   newData.Build_Number == existingData.Build_Number &&
                     newData.Manufacturer == existingData.Manufacturer &&
                       newData.SerialNumber == existingData.SerialNumber &&
                         newData.OS_Name == existingData.OS_Name &&
                           newData.CurrentUser == existingData.CurrentUser &&
                             newData.OS_Version == existingData.OS_Version &&
                             newData.OS_Architecture == existingData.OS_Architecture;


        }












        public async void osinfo()
        {
            SqLiteConn sqlite = new SqLiteConn();
            jsondataread = sqlite.ReadOSTable("operatingSystem");

            var data = os.OsInformation();
            // var osdata = new List<string>();
            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string systemid = System.Environment.MachineName.ToString();


            foreach (var item in data.Get())
            {
                os_data.OS_Name = item["Caption"].ToString();
                os_data.OS_Architecture = item["OSArchitecture"].ToString();
                os_data.OS_Version = item["Version"].ToString();
                os_data.Build_Number = item["BuildNumber"].ToString();
                os_data.Manufacturer = item["Manufacturer"].ToString();
                os_data.SystemId = systemid;
                os_data.TimeStamp = time;

                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("Os Name: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText(item["Caption"].ToString() + "\n\n");


                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("OSArchitecture: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText(item["OSArchitecture"].ToString() + "\n\n");


                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("Os Version: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText(item["Version"].ToString() + "\n\n");



                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("Build Number: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText(item["BuildNumber"].ToString() + "\n\n");


                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("Manufacturer: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText(item["Manufacturer"].ToString() + "\n\n");




                var lastBootTimeString = item["LastBootUpTime"].ToString();


                DateTime lastBootUpTime = ManagementDateTimeConverter.ToDateTime(lastBootTimeString);
                os_data.LastBootUpTime = lastBootUpTime.ToString();

                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("LastBootUpTime: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText(lastBootUpTime.ToString() + "\n\n");




                if (item["LastBootUpTime"] != null)
                {
                    var lastBootTimeString1 = item["LastBootUpTime"].ToString();
                    DateTime lastBootUpTime1 = ManagementDateTimeConverter.ToDateTime(lastBootTimeString1);


                    TimeSpan lastBootDuration = DateTime.UtcNow - lastBootUpTime1.ToUniversalTime();


                    int noOfDaysLastBoot = (int)Math.Round(lastBootDuration.TotalDays);



                    os_data.NoOfDaysLastSystemBoot = noOfDaysLastBoot.ToString();

                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    richTextBox1.AppendText("No of Days Last System Boot: ");
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                    richTextBox1.AppendText(noOfDaysLastBoot.ToString() + " Days" + "\n\n");
                }


                os_data.SerialNumber = item["SerialNumber"].ToString();
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("SerialNumber: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText(item["SerialNumber"].ToString() + "\n\n");



            }

            var data1 = os.OsInformation1();
            var osdata1 = new List<string>();
            foreach (var items in data1.Get())
            {

                string membershipType = items["DomainRole"].ToString();
                //osdata1.Add(items["DomainRole"]?.ToString());
                if (!string.IsNullOrEmpty(membershipType))
                {

                    if (membershipType == "0" || membershipType == "1")
                    {

                        osdata1.Add("Wrokgroup");
                        os_data.MembershipType = "Workgroup";



                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                        richTextBox1.AppendText("MemberShip Type: ");
                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                        richTextBox1.AppendText("Workgroup" + "\n\n");


                    }
                    else if (membershipType == "2" || membershipType == "3" || membershipType == "4" || membershipType == "5")
                    {

                        osdata1.Add("Domain");
                        os_data.MembershipType = "Domain";

                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                        richTextBox1.AppendText("MemberShip Type: ");
                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                        richTextBox1.AppendText("Domain" + "\n\n");

                    }
                    else
                    {

                        osdata1.Add("Unknown");
                        os_data.MembershipType = "Unknown";

                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                        richTextBox1.AppendText("MemberShip Type: ");
                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                        richTextBox1.AppendText("Unknown" + "\n\n");

                    }
                }


            }





            bool isAdmin = IsUserAdministrator();

            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();


            WindowsPrincipal currentPrincipal = new WindowsPrincipal(currentIdentity);

            if (currentIdentity.Name != null)
            {
                //Osobj.LastLogged = currentIdentity.Name;
                os_data.LastLogged = currentIdentity.Name.ToString();

                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("LastLogged: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText(currentIdentity.Name + "\n\n");
            }

            if (currentPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                //Osobj.Role = "Admin";

                os_data.LastLoggedUserRole = "Admin";
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("LastLogged User Role: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText("Admin" + "\n\n");


            }
            else
            {
                //Osobj.Role = "Non Admin";

                os_data.LastLoggedUserRole = "Non Admin";

                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("LastLogged User Role: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText("Non Admin" + "\n\n");
            }
            if (isAdmin)
            {
                //Osobj.AdminStatus = "Current User Is Admin";

                os_data.CurrentUser = "Admin";

                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("Current User: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText("Admin" + "\n\n");
            }
            else
            {
                os_data.CurrentUser = "Non Admin";

                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("Current User: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText("Non Admin" + "\n\n");
            }
            jsondata.Add(os_data);

            List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(jsondata);
            if (jsondataread.Count() == 0)
            {
                sqlite.InsertDataIntoTable("operatingSystem", dict);
                //jsondataread = sqlite.ReadOSTable("operatingSystem");
                await sendOSInfo(os_data);
            }
            else
            {
                CompareData();
            }
            //await sendOSInfo(os_data);



        }






        static DateTime GetLastBootTime()
        {
            DateTime lastBootTime = DateTime.MinValue;

            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT LastBootUpTime FROM Win32_OperatingSystem"))
                {
                    ManagementObjectCollection results = searcher.Get();

                    foreach (ManagementObject result in results)
                    {
                        string lastBootTimeString = result["LastBootUpTime"]?.ToString();
                        lastBootTime = ManagementDateTimeConverter.ToDateTime(lastBootTimeString);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving last boot time: {ex.Message}");
            }

            return lastBootTime;
        }

        public static string GetWindowsActivationApplicationId()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM SoftwareLicensingProduct WHERE LicenseStatus = 1");
                ManagementObjectCollection objCollection = searcher.Get();

                foreach (ManagementObject obj in objCollection)
                {
                    // Assuming the first activated product is the one we want
                    return obj["ApplicationID"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Application ID: {ex.Message}");
            }

            return null;
        }

        public static bool IsWindowsActivated(string applicationId)
        {
            if (string.IsNullOrEmpty(applicationId))
            {
                Console.WriteLine("Application ID is null or empty.");
                return false;
            }

            ManagementScope scope = new ManagementScope(@"\\" + Environment.MachineName + @"\root\cimv2");
            scope.Connect();

            SelectQuery searchQuery = new SelectQuery($"SELECT * FROM SoftwareLicensingProduct WHERE ApplicationID = '{applicationId}' AND LicenseStatus = 1");
            ManagementObjectSearcher searcherObj = new ManagementObjectSearcher(scope, searchQuery);

            using (ManagementObjectCollection obj = searcherObj.Get())
            {
                return obj.Count > 0;
            }
        }




        static bool IsUserAdministrator()
        {
            try
            {
                // Get the identity of the current user
                System.Security.Principal.WindowsIdentity currentIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();

                // Get the principal representing the current user
                System.Security.Principal.WindowsPrincipal currentPrincipal = new System.Security.Principal.WindowsPrincipal(currentIdentity);

                // Check if the user is a member of the Administrator group
                return currentPrincipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
            catch (Exception)
            {
                // If an exception occurs, return false (not an administrator)
                return false;
            }
        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {



        }

        private async void os_load(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            osinfo();
        }
        public async Task sendOSInfo(OSDto data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(APIUrls.OSInformation_url, content);
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
                Console.WriteLine(ex.ToString());

            }

        }
    }

}
