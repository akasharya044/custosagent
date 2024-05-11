using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace custos.Common
{
    public static class APIUrls
    {

        //static string url = "https://localhost:7237";
       static string url = "http://65.2.100.52:1050";

        public static string DeviceInformation_url = APIUrls.url + "/api/deviceInformation/adddeviceInformation";
        public static string HarddiskInformation_url = APIUrls.url+"/api/harddiskInformation/addharddiskInformation";
        public static string Software_url = APIUrls.url + "/api/systemInformation/addsoftwareinfo";
        public static string Hardware_url = APIUrls.url + "/api/systemInformation/addhardwareinfo";
        public static string InstalledSoftwareInformation_url = APIUrls.url + "/api/installedSoftware/addinstalledSoftwareInformation";
        public static string OSCoreInformation_url = APIUrls.url + "/api/operatingSystem/addoscoreInformation";
        public static string OSInformation_url = APIUrls.url + "/api/operatingSystem/addOperatingSystemInformation";
        public static string PortInformation_url = APIUrls.url + "/api/portInformation/addportInformation";
        public static string WindowService_url = APIUrls.url + "/api/windowservices/addwindowServiceInformation";
        public static string AntivirusInformation_url = APIUrls.url + "/api/antivirusInformation/addantivirusInformation";
        public static string TicketPost_url = APIUrls.url + "/api/ticketrecords";
        public static string UserRegistration_url = APIUrls.url + "/api/userRegistration";
        public static string GetRegistration_url= APIUrls.url + "/api/userRegistration?systemid=";


        public static QuestionData questiondata { get; set; }
		public static List<AnswerData> answerdata { get; set; }
	}

    public  class CustOsTicket
    {
        public int Id { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? EntityType { get; set; } = string.Empty;
        public string? ServiceDeskGroup { get; set; } = string.Empty;
        public string? RegisteredForActualService { get; set; } = string.Empty;
        public string? RequestedByPerson { get; set; } = string.Empty;
        public string? Priority { get; set; } = string.Empty;
        public int? RegisteredForDevice_c { get; set; } = 0;
        public int? CategoryId { get; set; } = 0;
        public int? SubCategoryId { get; set; } = 0;
        public int? AreaId { get; set; } = 0;
        public int? ContactPerson { get; set; } = 0;
        public bool? TicketGenerated { get; set; } = true; //If not generated reprocess it in handler
        public int? TicketStatus { get; set; } = 1; /*0 pending,1 ticket generated,2 resolved,3 feedback received*/
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
        public string? ResolvedDateTime { get; set; } = string.Empty;
        public string? SystemId { get; set; } //New field added 
        public string? AssignedTo { get; set; } = string.Empty;
        public string? FeedBackRemark { get; set; } = string.Empty;
        public string? ExpertAssignee { get; set; } = string.Empty;
        public string? ExpertAssigneeName { get; set; } = string.Empty;
        public string? RequestedByPersonName { get; set; } = string.Empty;
        public string? RegisteredForLocation { get; set; } = string.Empty;
        public string? Region { get; set; } = string.Empty;
        public string? Severity { get; set; } = string.Empty;
    }

    public class UserRegistrationDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public string? SystemId { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public string? MacAddress { get; set; }
        public string? UniqueKey { get; set; }
        public string? DeviceType { get; set; }
        public string? Location { get; set; }
        public string? AgentVersion { get; set; }
        public string UserName { get; set; }
        public string? DisplayLabel { get; set; }
        public string? IpAddress { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsDelete { get; set; }
    }
}
