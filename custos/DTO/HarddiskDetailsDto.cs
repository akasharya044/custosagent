using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custos.DTO
{
    public class HarddiskDetailsDto
    {
        public string DriveName { get; set; }
        public string DriveType { get; set; }
        public string DriveFormat { get; set; }
        public string SerialNumber { get; set; }
        public string TotalSize { get; set; }
        public string FreeSpace { get; set; }
        public string AvailableFreeSpace { get; set; }
        public string NonSystemDriveName { get; set; }
        public string NonSystemTotalSpace { get; set; }
        public string NonSystemFreeSpace { get; set; }
        public DateTime TimeStamp { get; set; }
        public string SystemId { get; set; }
    }
}
