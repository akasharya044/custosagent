using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custos.DTO
{
    public class HardwareInformationDto
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsLocal { get; set; }
        public bool IsArray { get; set; }
        public string Origin { get; set; }
        public string Qualifires { get; set; }
        public string SystemId { get; set; }

        public DateTime TimeStamp { get; set; }

    }
}
