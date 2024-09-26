

namespace scheapp.app.Models.Data
{
    public class ProfessionalCall
    {       
        public int Id { get; set; }
        public int ProfessionalContactId { get; set; }
        public DateTime CalledAt { get; set; } = DateTime.MinValue;
        public string Message { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
