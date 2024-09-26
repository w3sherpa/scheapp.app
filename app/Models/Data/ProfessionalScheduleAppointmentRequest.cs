

namespace scheapp.app.Models.Data
{
    public class ProfessionalScheduleAppointmentRequest
    {       
        public int Id { get; set; }
        public int ProfessionalId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDT { get; set; } = DateTime.MinValue;
        public DateTime EndDT { get; set; } = DateTime.MinValue;
        public bool ProfessionalConfirmed { get; set; }
        public bool CustomerConfirmed { get; set; }

    }
}
