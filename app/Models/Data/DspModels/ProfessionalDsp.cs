using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.DspModels
{
    public class ProfessionalServiceDsp
    {
        public int ProfessionalId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = "";
        public decimal Price { get; set; }
        public int ServiceDuration { get; set; }
        public string DurationType { get; set; } = "";
    }

    public class ProfessionalDsp
    {
        public int ProfessionalId { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
    public class ProfessionalBusinessDetailDsp
    {
        public string AspNetUserId { get; set; } = "";
        public string AspNetUserName { get; set; } = "";
        public int? ProfessionalId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfessionalRole { get; set; }
        public bool EmailConfirmed { get; set; }
        public int? BusinessId { get; set; }
        public string? BusinessName { get; set; } = "";
        public string? BusinessWebsite { get; set; } = "";

    }
}
