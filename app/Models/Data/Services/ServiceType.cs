using System.ComponentModel.DataAnnotations;
namespace scheapp.app.Models.Data.Services
{
    public class ServiceType
    {

        public int Id { get; set; }
        public string Type { get; set; } = "";
        public int BusinessId { get; set; }
    }
}
