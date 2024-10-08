using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.TableModels.Professionals
{
    public class Professional
    {

        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = "";
        public int BusinessId { get; set; }
        public string AspNetUserId { get; set; } = "";
    }
}
