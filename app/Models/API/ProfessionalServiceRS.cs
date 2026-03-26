namespace scheapp.app.Models.API
{
    public class ProfessionalServiceRS
    {
        public int? BusinessId { get; set; }
        public List<Professional> Professionals { get; set; } = new();
        public List<Service> Services { get; set; } = new();
        public List<ServiceDuration> ServicesDuration { get; set; } = new();
     }
}
