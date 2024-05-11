using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custos.DTO
{
    public class PortInformationDto
    {
        public int Id {  get; set; }

        public string SystemId { get; set; }
        public string LocalEndpoint { get; set; }
        public string LocalPort {  get; set; }
        public string RemoteEndpint { get; set; }
        public string RemotePort { get; set; }
        public string State {  get; set; }
        public int ProcessId { get; set; }
        public string Service {  get; set; }

        public DateTime TimeStamp { get; set; }

    }
}
