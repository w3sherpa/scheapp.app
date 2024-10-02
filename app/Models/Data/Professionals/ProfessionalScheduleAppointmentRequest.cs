using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.Professionals
{
    public class ProfessionalScheduleAppointmentRequest
    {

        public int Id { get; set; }
        public int ProfessionalId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime StartDT { get; set; } = DateTime.MinValue;
        public DateTime EndDT { get; set; } = DateTime.MinValue;
        public bool ProfessionalConfirmed { get; set; }
        public bool CustomerConfirmed { get; set; }
        public int BusinessId { get; set; }
    }
}
