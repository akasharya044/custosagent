using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custos.DTO
{
    public class InstalledSoftwareDto
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
        public string Size { get; set; }
        public string InstalledOn { get; set; }
        public string SystemId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
