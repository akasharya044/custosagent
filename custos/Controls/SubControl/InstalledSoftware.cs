using custos.Common;
using custos.DTO;
using custos.Methods;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace custos.Controls.SubControl
{
    public partial class InstalledSoftware : UserControl
    {
        List<InstalledSoftwareDto> softwareList = new List<InstalledSoftwareDto>();
        CommonMethod commonmethod = new CommonMethod();
        private List<InstalledSoftwareDto> jsondata = new List<InstalledSoftwareDto>();


        private List<InstalledSoftwareDto> jsondataread = new List<InstalledSoftwareDto>();

        SqLiteConn sqlite;

        public InstalledSoftware()
        {
            InitializeComponent();
        }
        public async void Installed()
        {
            sqlite = new SqLiteConn();
            jsondataread = sqlite.ReadInstalledSoftwareTable("InstalledSoftware");
            Events events = new Events();
            events.Event = "Installed software Info Fetched";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            string registry_keys = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            dataGridView1.Columns.Add("Display", "Display Name");
            dataGridView1.Columns.Add("DisplayVersion", "Version");
            dataGridView1.Columns.Add("Publisher", "Publisher");
            dataGridView1.Columns.Add("EstimatedSize", "Size");
            dataGridView1.Columns.Add("InstallDate", "Installed Date");
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_keys))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        if (subkey != null)
                        {
                            object name = subkey.GetValue("DisplayName");
                            object Version = subkey.GetValue("DisplayVersion");
                            object publisher = subkey.GetValue("Publisher");
                            object size = subkey.GetValue("EstimatedSize");
                            object installDateString = subkey.GetValue("InstallDate");


                            if (name != null)
                            {
                                string displayName = name.ToString();
                                string displayVersion = Version?.ToString() ?? "N/A";
                                string softwarePublisher = publisher?.ToString() ?? "N/A";
                                string softwaresize = size?.ToString() ?? "N/A";
                                string installedOn = "N/A";
                                DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);

                                if (DateTime.TryParseExact(installDateString?.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime installDate))
                                {

                                    installedOn = installDate.ToString("yyyy-MM-dd");
                                }


                                dataGridView1.Rows.Add(displayName, displayVersion, softwarePublisher, softwaresize, installedOn);


                                // Create SystemSoftware object and add it to softwareInfo list
                                // SystemSoftware system = new SystemSoftware();
                                InstalledSoftwareDto system = new InstalledSoftwareDto();
                                system.DisplayName = displayName;
                                system.Version = displayVersion;
                                system.Publisher = softwarePublisher;
                                system.Size = softwaresize;
                                system.InstalledOn = installedOn;
                                system.SystemId = System.Environment.MachineName;
                                system.TimeStamp = time;

                                //dataGridView1.DataSource= system;
                                softwareList.Add(system);
                            }
                        }
                    }

                }


                //SoftwareInfo(softwareInfo);



            }
            //Events events = new Events();
            //events.Event = "Installed Software Info Fetched";
            //events.EventDate = DateTime.Now;
            //events.SystemId = System.Environment.MachineName;
            //commonmethod.EventLog(events);

            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        if (subkey != null)
                        {
                            object name = subkey.GetValue("DisplayName");
                            object Version = subkey.GetValue("DisplayVersion");
                            object publisher = subkey.GetValue("Publisher");
                            object size = subkey.GetValue("EstimatedSize");
                            object installDateString = subkey.GetValue("InstallDate");


                            if (name != null)
                            {
                                string displayName = name.ToString();
                                string displayVersion = Version?.ToString() ?? "N/A";
                                string softwarePublisher = publisher?.ToString() ?? "N/A";
                                string softwaresize = size?.ToString() ?? "N/A";
                                string installedOn = "N/A";
                                DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);

                                if (DateTime.TryParseExact(installDateString?.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime installDate))
                                {
                                    installedOn = installDate.ToString("yyyy-MM-dd");
                                }

                                dataGridView1.Rows.Add(displayName, displayVersion, softwarePublisher, softwaresize, installedOn);
                                var data = new InstalledSoftwareDto()
                                {


                                    DisplayName = displayName,
                                    Version = displayVersion,
                                    Publisher = softwarePublisher,
                                    InstalledOn = installedOn,
                                    Size = softwaresize,
                                    SystemId = System.Environment.MachineName,
                                    TimeStamp = time
                                };
                                softwareList.Add(data);


                                // Create SystemSoftware object and add it to softwareInfo list
                                //SystemSoftware system = new SystemSoftware();
                                // system.Name = displayName;
                                // system.Version = displayVersion;
                                //system.Publisher = softwarePublisher;
                                //system.Size = softwaresize;
                                //system.InstalledOn = installedOn;
                                //system.SystemId = System.Environment.MachineName;

                                //dataGridView1.DataSource= system;
                                //softwareInfo.Add(system);
                            }
                        }
                    }

                }
                // SoftwareInfo(softwareInfo);


            }
            jsondata = softwareList;
            List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(jsondata);
            if (jsondataread.Count() == 0)
            {
                sqlite.InsertDataIntoTable("InstalledSoftware", dict);
                await SoftwareInfo(softwareList);
            }
            else
            {
                //jsondataread = sqlite.ReadTable("windowServices");


                CompareData();
            }


            //SoftwareInfo(softwareList);

        }
        private async void CompareData()
        {

            List<InstalledSoftwareDto> data1 = new List<InstalledSoftwareDto>();
            foreach (var item in jsondataread)
            {
                var item1 = softwareList.FirstOrDefault(x => x.DisplayName == item.DisplayName);
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
                await SoftwareInfo(data1);
                List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(softwareList);

                //.ToList();
                sqlite.InsertDataIntoTable("InstalledSoftware", dict);
            }

        }



        bool AreEquivalent(InstalledSoftwareDto s, InstalledSoftwareDto j)
        {
            return s.DisplayName == j.DisplayName &&
                   s.Publisher == j.Publisher &&
                    s.Version == j.Version &&
                    s.SystemId == j.SystemId &&
                    s.Size == j.Size &&
                    s.InstalledOn == j.InstalledOn;



        }
        private void system_load(object sender, EventArgs e)
        {

            Installed();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }






        public async Task SoftwareInfo(List<InstalledSoftwareDto> data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            using (HttpClient httpClient = new HttpClient())
            {
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(APIUrls.InstalledSoftwareInformation_url, content);
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
    }


}


