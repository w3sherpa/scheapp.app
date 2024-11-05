using scheapp.app.Models.Data.TableModels.Professionals;
using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.View
{
    public class CreateProfessionalSchedule
    {
        public int? ProfessionalId { get; set; }
        public string? FirstName { get; set; } = "";
        public string? LastName { get; set; } = "";
        public DateTime StartDT { get; set; } = new();
        public DateTime EndDT { get; set; } = new();
        public int? BusinessId { get; set; }
    }
    
    
    public class ProfessionalScheduleVM
    {
        public int? BusinessId { get; set; }
        public int? ProfessionalId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public List<ProfessionalSchedule> Schedules { get; set; } = new();
    }
}
