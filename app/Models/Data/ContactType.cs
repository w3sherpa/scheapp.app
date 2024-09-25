using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data
{
    public class ContactType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = "";
    }
}
