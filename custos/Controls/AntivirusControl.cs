using System.Diagnostics;
using System.IO;
using System;
using System.Management;
using System.Windows.Forms;
using System.Text;
using custos.Methods;
using Newtonsoft.Json;
using static custos.Methods.AntivirusDTO;
using custos.DTO;
using custos.Controls.SubControl;
using custos.Common;

namespace custos.Controls
{

    public partial class AntivirusControl : UserControl
    {
        AntivirusMethod antivirusMethod = new AntivirusMethod();
        private List<AntivirusDetailsDto> jsondata = new List<AntivirusDetailsDto>();

        List<AntivirusDetailsDto> jsondataread = new List<AntivirusDetailsDto>();
        AntivirusDetailsDto data;
        public AntivirusControl()
        {
            InitializeComponent();
        }

        private void Antivirus_Load(object sender, EventArgs e)
        {
            GetAntiviruInfo();

        }
        private async void CompareData()
        {
            try
            {
                //foreach (var newData in os_data)
                //{

                var particulardata = jsondataread.Find(x => x.SystemId == data.SystemId);
                if (particulardata == null)
                {
                    // If the data doesn't exist in the database, insert it
                    await AntivirusDetails(data);
                }
                else if (!AreEqual(data, particulardata))
                {
                    // If the data exists but has changed, update it
                    jsondataread.Clear();
                    jsondataread.Add(data);

                    await AntivirusDetails(data);
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }


        private bool AreEqual(AntivirusDetailsDto newData, AntivirusDetailsDto existingData)
        {
            // Compare properties of newData and existingData to check if they are equal
            return newData.AntivirusName == existingData.AntivirusName &&
                   newData.SystemId == existingData.SystemId;



        }



        public async void GetAntiviruInfo()
        {

            try
            {
                SqLiteConn sqlite = new SqLiteConn();
                jsondataread = sqlite.ReadantivirusTable("AntivirusDetails");
           



                var outputData = antivirusMethod.AntivirusInfo();

                var antivirusData = new List<string>();

                string anti = string.Empty;

                foreach (var result in outputData.Get())
                {
                    //antivirusData.Add(result["displayName"].ToString());
                    anti = result["displayName"].ToString();
                    antivirusData.Add(anti);
                }
                data = new AntivirusDetailsDto();
                int baseFontSize = 10;
                int productNumber = 1;
                AVList.ReadOnly = true;
                AVList.Text = "";
                AVList.SelectionFont = new Font(AVList.Font.FontFamily, 13, FontStyle.Bold);
                AVList.AppendText(antivirusData.Count + " Antivirus Products Found\n");
                AVList.AppendText(Environment.NewLine);
                AVList.SelectionIndent = 0;

                AVList.SelectionFont = new Font(AVList.Font, FontStyle.Regular);
                DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);

                foreach (string product in antivirusData)
                {
                    data.SystemId = System.Environment.MachineName;

                    data.AntivirusName = product;
                    data.Id = productNumber;
                    data.TimeStamp = time;
                    Font productFont = new Font(AVList.Font.FontFamily, baseFontSize, FontStyle.Regular);
                    AVList.SelectionFont = productFont;

                    AVList.AppendText($"{productNumber}. {product}{Environment.NewLine}");
                    productNumber++;
                }
                jsondata.Add(data);
                List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(jsondata);
                if (jsondataread.Count() == 0)
                {
                    sqlite.InsertDataIntoTable("AntivirusDetails", dict);

                    await AntivirusDetails(data);
                }
                else
                {
                    CompareData();
                }
                //await AntivirusDetails(data);

            }
            catch (Exception ex)
            {
               
            }
          
        }

        private void AVList_TextChanged(object sender, EventArgs e)
        {

        }
        public async Task AntivirusDetails(AntivirusDetailsDto data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(APIUrls.AntivirusInformation_url, content);
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