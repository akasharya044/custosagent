using custos.Common;
using custos.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace custosWorker.Methods
{
    public class Port
    {

        List<PortInformationDto> data = new List<PortInformationDto>();
        private static string ExecuteNetstatCommand()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c netstat -ano";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }

        private static int ParseProcessId(string netstatOutput, int port)
        {
            string[] lines = netstatOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                if (line.Contains($":{port}"))
                {
                    string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string processIdString = parts.Last();
                    if (int.TryParse(processIdString, out int processId))
                    {
                        return processId;
                    }
                }
            }

            return -1; // Process ID not found
        }

        private static int GetProcessId(int port)
        {
            string netstatOutput = ExecuteNetstatCommand();
            return ParseProcessId(netstatOutput, port);
        }

        private string ExtractPortNumber(string localAddress)
        {
            int colonIndex = localAddress.LastIndexOf(':');
            return colonIndex != -1 ? localAddress.Substring(colonIndex + 1) : localAddress;
        }

        private string RemovePortNumber(string localAddress)
        {
            int colIndex1 = localAddress.IndexOf(":");
            return colIndex1 != -1 ? localAddress.Substring(0, colIndex1) : localAddress;
        }

        public async Task sendPort(List<PortInformationDto> data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(APIUrls.PortInformation_url, content);
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


        public async void portdata()
        {

            try
            {
                //    sqlite = new SqLiteConn();
                //    jsondataread = sqlite.ReadPortTable("PortInformation");
                //    Events events = new Events();
                //    events.Event = "Port Info Fetched";
                //    events.EventDate = DateTime.Now;
                //    events.SystemId = System.Environment.MachineName;
                //    commonmethod.EventLog(events);
                //    dataGridView1.Columns.Add("LocalEndpoint", "Local Endpoint");
                //    dataGridView1.Columns.Add("port", "Local Port");
                //    dataGridView1.Columns.Add("RemoteEndpoint", "Remote Endpoint");
                //    dataGridView1.Columns.Add("port1", "Remote Port");
                //    dataGridView1.Columns.Add("State", "State");
                //    dataGridView1.Columns.Add("ProcessID", "Process ID");
                //    //dataGridView1.Columns.Add("TimeStamp", "TimeStamp");

                //    Controls.Add(dataGridView1);

                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();


                foreach (TcpConnectionInformation connection in connections)
                {

                    string localEndpoint = RemovePortNumber(connection.LocalEndPoint.ToString());
                    string port = ExtractPortNumber(connection.LocalEndPoint.ToString());
                    string remoteEndpoint = RemovePortNumber(connection.RemoteEndPoint.ToString());
                    string port1 = ExtractPortNumber(connection.RemoteEndPoint.ToString());
                    string state = connection.State.ToString();
                    int processId = GetProcessId(connection.LocalEndPoint.Port);
                    string servicename = "N/A";
                    string userid = System.Environment.MachineName;
                    DateTime Time = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    PortInformationDto portinfo = new PortInformationDto()
                    {
                        ProcessId = processId,
                        Service = servicename,
                        State = state,
                        RemotePort = port1,
                        RemoteEndpint = remoteEndpoint,
                        LocalPort = port,
                        LocalEndpoint = localEndpoint,
                        SystemId = userid,
                        TimeStamp = Time,
                    };
                    //dataGridView1.Rows.Add(localEndpoint, port, remoteEndpoint, port1, state, processId);

                    data.Add(portinfo);





                }
            }
            catch (Exception ex)
            {

            }
            await sendPort(data);
            //sendPort(data);
            //jsondata = data;
            //List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(jsondata);
            //if (jsondataread.Count() == 0)
            //{
            //    sqlite.InsertDataIntoTable("PortInformation", dict);
            //    
            //}
            //else
            //{
            //    //jsondataread = sqlite.ReadTable("windowServices");


            //    CompareData();
            //}

        }
    }
}