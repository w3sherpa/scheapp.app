using System.ComponentModel.DataAnnotations;
namespace scheapp.app.Models.Data.Services
{
    public class ServiceDuration
    {

        public int Id { get; set; }
        public int Duration { get; set; }
        public string DurationType { get; set; } = "";
        public int BusinessId { get; set; }
    }
}
