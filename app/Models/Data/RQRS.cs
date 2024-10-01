namespace scheapp.app.Models.Data
{
    public class RequestByBusinessId
    {
        public int BusinessId { get; set; }
    }
    public class RequestByProfessionalId
    {
        public int ProfessionalId { get; set; }
    }
    public class RequestByServiceId
    {
        public int ProfessionalId { get; set; }
    }
    public class RequestByDate
    {
        public string DateOnly { get; set; } = "";
    }
}