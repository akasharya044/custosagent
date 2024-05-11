
using System.Diagnostics;
using System.Management;
using custos.Common;
using Newtonsoft.Json;
using static custos.Methods.AntivirusDTO;

namespace custos.Methods;

public class AntivirusMethod
{
    CommonMethod commonmethod = new CommonMethod();
    ManagementObjectSearcher searcher;
    public ManagementObjectSearcher AntivirusInfo()
    {
        try
        {
            Events events = new Events();
            events.Event = "Antivirus Info Fetched";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            searcher = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
        }
        catch (Exception ex)
        {
            //MessageBox.Show("Some problem occurred. Contact Your Administrator.");
        }
        return searcher;
    }
}
public class AntivirusDTO
{
    public class AntivirusData
    {
        [JsonProperty("AntivirusProduct")]
        public string[] AntivirusProduct { get; set; }

        [JsonProperty("AntivirusStatus")]
        public int[] AntivirusStatus { get; set; }
    }
}
