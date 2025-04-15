using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.TableModels.Professionals
{
    public class ProfessionalSchedule
    {
        public int Id { get; set; }
        public int ProfessionalId { get; set; }
        public DateOnly StartDate { get; set; } = new();
        public DateOnly EndDate { get; set; } = new();

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string DaysOfWeek { get; set; }

        public int BusinessId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
