using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custos.DTO
{
    public class DeviceDetailsDto
    {
        public string DeviceVersion { get; set; }
        public string DeviceName { get; set; }
        public string BIOS { get; set; }
        public string MACAddress { get; set; }
        public string IPAddress { get; set; }
        public string VirtualMemory { get; set; }
        public string AvailableVirtualMemory { get; set; }
        public string DisplayManufacturer { get; set; }
        public string DisplayName { get; set; }
        public string DisplayDetails { get; set; }
        public DateTime TimeStamp { get; set; }
        public string SystemId { get; set; }
    }
}
