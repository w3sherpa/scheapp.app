using System.ComponentModel.DataAnnotations;
namespace scheapp.app.Models.Data.Services
{
    public class Service
    {

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int BusinessId { get; set; }
    }
}
