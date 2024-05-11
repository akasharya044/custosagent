using custos.Common;
using custos.DTO;
using custos.Methods;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static custos.Methods.services;

namespace custos.Controls.SubControl
{
    public partial class Services : UserControl
    {
        services windowservice = new services();
        private List<WindowsServicesDto> jsondata = new List<WindowsServicesDto>();

        private List<WindowsServicesDto> servicesList;
        private List<WindowsServicesDto> jsondataread = new List<WindowsServicesDto>();

        SqLiteConn sqlite;
        public Services()
        {
            InitializeComponent();
        }


        public async void servicedata()
        {
            sqlite = new SqLiteConn();
          
            jsondataread = sqlite.ReadTable("windowServices");
            
            servicesList = windowservice.NoOfServices();


            dataGridView1.Columns.Add("ServiceName", "Service Name");
            dataGridView1.Columns.Add("DisplayName", "Display Name");
            dataGridView1.Columns.Add("Status", "Status");
            dataGridView1.Columns.Add("Startup", "Startup");

            foreach (var ser in servicesList)
            {
                dataGridView1.Rows.Add(ser.ServiceName, ser.ServiceDisplayName, ser.ServiceStatus, ser.Startup);
            }
            jsondata = servicesList;
            List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(jsondata);
            if (jsondataread.Count() == 0)
            {
                sqlite.InsertDataIntoTable("windowServices", dict);
                await windowservice.sendwindowservicesInfo(servicesList);
            }
            else
            {
                //jsondataread = sqlite.ReadTable("windowServices");


                CompareData();
            }








            //jsondata = JsonConvert.DeserializeObject<List<OSDto>>(data.Data.ToString());

            //  var temp = jsondata.Where(r => servicesList.Select(e => e.SystemId).Contains(r.SystemId)).ToList();





        }


        private async void CompareData()
        {

            List<WindowsServicesDto> data = new List<WindowsServicesDto>();
            foreach (var item in jsondataread)
            {
                var item1 = servicesList.FirstOrDefault(x => x.ServiceName.ToLower() == item.ServiceName.ToLower());
                if (item1 != null)
                {
                    bool check = AreEquivalent(item1, item);
                    if (!check)
                    {
                        data.Add(item1);
                    }
                }
            }
            if (data.Count > 0)
            {
                await windowservice.sendwindowservicesInfo(data);
                List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(servicesList);

                //.ToList();
                sqlite.InsertDataIntoTable("windowServices", dict);
            }

        }



        bool AreEquivalent(WindowsServicesDto s, WindowsServicesDto j)
        {
            return s.Startup == j.Startup &&
                   s.ServiceStatus == j.ServiceStatus &&
                    s.ServiceName == j.ServiceName &&
                    s.ServiceDisplayName == j.ServiceDisplayName &&
                    s.SystemId == j.SystemId;


        }






























        //private bool AreEqual(ProgramData newData, ProgramData existingData)
        //{
        //    // Compare properties of newData and existingData to check if they are equal
        //    return newData.MemoryUsages == existingData.MemoryUsages &&
        //           newData.CpuUsgaes == existingData.CpuUsgaes &&
        //           newData.NetworkUsgaes == existingData.NetworkUsgaes &&
        //           newData.CPU_Alert == existingData.CPU_Alert &&
        //           newData.Memory_Alert == existingData.Memory_Alert &&
        //           newData.Network_Alert == existingData.Network_Alert;
        //}




        private async void service_load(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            servicedata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
