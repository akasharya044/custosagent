using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custos.DTO
{
    public class AntivirusDetailsDto
    {
        public int Id {  get; set; }
        public string SystemId {  get; set; }
        public string AntivirusName {  get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
