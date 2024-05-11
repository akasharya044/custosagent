using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using custos.Common;
using custos.DTO;
using custos.Forms;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace custos.Controls.SubControl
{
    public partial class OSCoreInformation : UserControl
    {
        CommonMethod commonmethod = new CommonMethod();
        private List<OSCoreDto> jsondata = new List<OSCoreDto>();
        OSCoreDto dto;

        List<OSCoreDto> jsondataread = new List<OSCoreDto>();
        public OSCoreInformation()
        {
            InitializeComponent();
        }

        private async void CompareData()
        {
            try
            {
                //foreach (var newData in os_data)
                //{

                var particulardata = jsondataread.Find(x => x.SystemId == dto.SystemId);
                if (particulardata == null)
                {
                    // If the data doesn't exist in the database, insert it
                    await oscoreInfo(dto);
                }
                else if (!AreEqual(dto, particulardata))
                {
                    // If the data exists but has changed, update it
                    jsondataread.Clear();
                    jsondataread.Add(dto);
                    await oscoreInfo(dto);
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }


        private bool AreEqual(OSCoreDto newData, OSCoreDto existingData)
        {
            // Compare properties of newData and existingData to check if they are equal
            return newData.WindowActivationStatus == existingData.WindowActivationStatus &&
                   newData.WindowProductKey == existingData.WindowProductKey &&
                   newData.SystemId == existingData.SystemId;




        }












        public async void oscoreinfo()
        {
            SqLiteConn sqlite = new SqLiteConn();
            jsondataread = sqlite.ReadOSCoreTable("OSCore");


            Events events = new Events();
            events.Event = "OS core Info Fetched";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);
            dto = new OSCoreDto();
            string applicationId = GetWindowsActivationApplicationId();
            if (applicationId != null)
            {
                bool isActivated = IsWindowsActivated(applicationId);
                //    // Osobj.IsActivated = isActivated;


                // Reset font style to regular
                // listBox1.Items.Add(isActivated.ToString() + "\n\n");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText("Windows Activation Status: ");
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
                richTextBox1.AppendText(isActivated.ToString() + "\n\n");
                dto.WindowActivationStatus = isActivated.ToString();

            }
            DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string systemid = System.Environment.MachineName.ToString();
            dto.SystemId = systemid;
            dto.TimeStamp = time;


            string Key = GetWindowsProductKey();
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Windows Product key: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(Key.ToString() + "\n\n");
            dto.WindowProductKey = Key.ToString();

            //if (!string.IsNullOrEmpty(Key))
            //{
            //    //Osobj.WindowKey = Key;
            // Reset font style to regular
            // listBox1.Items.Add(Key.ToString() + "\n\n");
            //oscoreInfo(dto);
            jsondata.Add(dto);
            List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(jsondata);
            if (jsondataread.Count == 0)
            {
                sqlite.InsertDataIntoTable("OSCore", dict);
                await oscoreInfo(dto);
            }
            else
            {
                CompareData();
            }
     

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
        public static string GetWindowsProductKey()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("wmic", "path softwarelicensingservice get OA3xOriginalProductKey");
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                using (Process process = new Process() { StartInfo = psi })
                {
                    process.Start();
                    string result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    string input = result;

                    // Define the pattern for the product key
                    string pattern = @"\b([A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5})\b";

                    // Use Regex.Match to find the first match in the input string
                    Match match = Regex.Match(input, pattern);

                    // Check if a match is found
                    if (match.Success)
                    {
                        // Extract the matched product key
                        string productKey = match.Groups[1].Value;
                        return productKey;
                        // Print the extracted product key
                        //Console.WriteLine(productKey);
                    }
                    else
                    {
                        Console.WriteLine("No product key found in the input string.");
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Windows Product Key: {ex.Message}");
            }

            return null;
        }
        private async void oscore_load(object sender, EventArgs e)
        {
            //Loader loader = new Loader();
            custos.Forms.Windowcoreform loader1 = new custos.Forms.Windowcoreform();
            loader1.Show();
            await Task.Delay(10);
            oscoreinfo();
            loader1.Close();




        }
        public async Task oscoreInfo(OSCoreDto data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(APIUrls.OSCoreInformation_url, content);
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged_2(object sender, EventArgs e)
        {

        }
    }


}

