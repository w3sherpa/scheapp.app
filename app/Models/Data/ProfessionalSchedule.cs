

namespace scheapp.app.Models.Data
{
    public class ProfessionalSchedule
    {       
        public int Id { get; set; }
        public int ProfessionalId { get; set; }
        public DateTime StartDT { get; set; } = new();
        public DateTime EndDT { get; set; } = new();
    }
}
