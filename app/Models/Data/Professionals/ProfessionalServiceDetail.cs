using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.Professionals
{
    public class ProfessionalService
    {

        public int Id { get; set; }
        public int ProfessionalId { get; set; }
        public int ServiceTypeId { get; set; }
        public int ServiceDurationId { get; set; }
        public decimal Price { get; set; }
    }
}
