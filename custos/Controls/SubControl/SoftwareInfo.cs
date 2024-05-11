
using System.Text.Json.Serialization;
using System.Windows.Forms;
using custos.Forms;
using custos.Methods;
using Microsoft.Win32;
using custos.DTO;
using Newtonsoft.Json;
using custos.Common;
//using static custos.Methods.SystemInfoDTO;

namespace custos.Controls;


public partial class SoftwareInfo : UserControl
{
    SystemInfoMethod systemInfoMethod = new SystemInfoMethod();
    private List<SoftwareInformationDto> jsondata = new List<SoftwareInformationDto>();


    private List<SoftwareInformationDto> jsondataread = new List<SoftwareInformationDto>();
    List<SoftwareInformationDto> groupedData;

    SqLiteConn sqlite;
    public SoftwareInfo()
    {
        InitializeComponent();

    }
    private async void SoftwareInfo_Load(object sender, EventArgs e)
    {

        Loader loader = new Loader();
        loader.Show();
       
        await Task.Delay(1200);
        GetSoftwareInfo();
        loader.Close();

    }

    
    public async void GetSoftwareInfo()
    {
        try
        {
            sqlite = new SqLiteConn();
            jsondataread = sqlite.ReadSoftwareInformationTable("SoftwareInformation");


            var data = await systemInfoMethod.SoftwareData();
            groupedData = data.GroupBy(item => item.ModuleName).Select(group => new SoftwareInformationDto
            {
                ProcessName = group.Key,
                WindowTitle = group.Select(item => item.WindowTitle).FirstOrDefault(),
                SystemId = group.Select(item => item.SystemId).FirstOrDefault(),

                MemorySize = group.Sum(item => (item.MemorySize).FirstOrDefault()).ToString(),
                //Size = group.Select(item => item.Size).FirstOrDefault(),
                Pid = string.Join(", ", group.Select(item => item.Pid)),
                Starttime = group.Select(item => item.Starttime).FirstOrDefault(),
                ModuleName = group.Select(item => item.ModuleName).FirstOrDefault(),
                CpuUsage = group.Select(item => item.CpuUsage).FirstOrDefault(),
                TimeStamp = group.Select(item => item.TimeStamp).FirstOrDefault(),

            }).ToList();


            dataGridView1.Columns.Add("ProcessName", "Process Name");
            //dataGridView1.Columns.Add("WindowTitle", "Window Title");
            //dataGridView1.Columns.Add("SystemId", "Host Name");
            dataGridView1.Columns.Add("MemorySize", "Memory Size");
            dataGridView1.Columns.Add("CPU", "CPU");

            dataGridView1.Columns.Add("StartTime", "Start Time");
            dataGridView1.Columns.Add("Pid", "PID");

            dataGridView1.Columns.Add("WindowTitle", "Window Title");
       

            // Add data to the grid
            foreach (var item in groupedData)
            {
                dataGridView1.Rows.Add(item.ProcessName, item.MemorySize.ToString() + " MB", item.CpuUsage + " %", item.Starttime, item.Pid, item.WindowTitle);
            }
            //await SendSoftwareInfo(groupedData);

            // Optional: Auto-size columns based on content
            //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
            jsondata = groupedData;
            List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(jsondata);
            if (jsondataread.Count() == 0)
            {
                sqlite.InsertDataIntoTable("SoftwareInformation", dict);
                await SendSoftwareInfo(groupedData);
            }
            else
            {
                //jsondataread = sqlite.ReadTable("windowServices");


                CompareData();
            }



        }
        catch (Exception ex)
        {

        }

    }
    private async void CompareData()
    {

        List<SoftwareInformationDto> data1 = new List<SoftwareInformationDto>();
        foreach (var item in jsondataread)
        {
            var item1 = groupedData.FirstOrDefault(x => x.ProcessName == item.ProcessName);
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
            await SendSoftwareInfo(data1);
            List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(groupedData);

            //.ToList();
            sqlite.InsertDataIntoTable("SoftwareInformation", dict);
        }

    }



    bool AreEquivalent(SoftwareInformationDto s, SoftwareInformationDto j)
    {
        return s.Pid == j.Pid &&
               s.ProcessName == j.ProcessName &&
                s.ModuleName == j.ModuleName &&
                s.CpuUsage == j.CpuUsage &&
                s.MemorySize == j.MemorySize &&
                s.Starttime == j.Starttime &&
                s.SystemId == j.SystemId &&
                s.WindowTitle == j.WindowTitle;



    }
    public async Task SendSoftwareInfo(List<SoftwareInformationDto> data)
    {
        try
        {
            var jsonData = JsonConvert.SerializeObject(data);
            using (HttpClient httpClient = new HttpClient())
            {
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(APIUrls.Software_url, content);
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

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}
