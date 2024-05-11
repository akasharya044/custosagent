using custos.Common;
using custos.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace custos.Methods
{
    public class services
    {
        CommonMethod commonmethod=new CommonMethod();

        public List<WindowsServicesDto> NoOfServices()
        {
            List<WindowsServicesDto> servicesList = new List<WindowsServicesDto>();

            try
            {
                Events events = new Events();
                events.Event = "Window Services Info Fetched";
                events.EventDate = DateTime.Now;
                events.SystemId = System.Environment.MachineName;
                commonmethod.EventLog(events);
                ServiceController[] services = ServiceController.GetServices();
                string id = System.Environment.MachineName;
                DateTime time = DateTime.UtcNow.AddHours(5).AddMinutes(30);

                foreach (ServiceController service in services)
                {
                    WindowsServicesDto nosos = new WindowsServicesDto();

                    nosos.ServiceName = service.ServiceName;
                    nosos.ServiceDisplayName = service.DisplayName;
                    nosos.ServiceStatus = service.Status.ToString();
                    nosos.Startup = (service.StartType == ServiceStartMode.Automatic);
                    nosos.SystemId = id;
                    nosos.TimeStamp = time;


                    servicesList.Add(nosos);
                }
                

                return servicesList;
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
        public async Task sendwindowservicesInfo(List<WindowsServicesDto> data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                using (HttpClient httpClient = new HttpClient())
                {
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(APIUrls.WindowService_url, content);
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
