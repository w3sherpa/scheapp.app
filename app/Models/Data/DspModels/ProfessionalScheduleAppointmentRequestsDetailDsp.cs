using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.DspModels
{
    public class ProfessionalScheduleAppointmentRequestsDetailDsp
    {
        public int ScheduleAppointmentId { get; set; }
        public DateTime StartDT { get; set; } = new();
        public DateTime EndDT { get; set; } = new();
        public bool ProfessionalConfirmed { get; set; }
        public bool CustomerConfirmed { get; set; }
        public DateTime RequestDate { get; set; } = new();
        public string ProFirst { get; set; } = "";
        public string ProMiddle { get; set; } = "";
        public string ProLast { get; set; } = "";
        public string ServiceName { get; set; } = "";
        public string CustFrist { get; set; } = "";
        public string CustMiddle { get; set; } = "";
        public string CustLast { get; set; } = "";
    }
}
