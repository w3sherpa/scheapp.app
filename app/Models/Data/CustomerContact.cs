using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data
{
    public class CustomerContact
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TypeId { get; set; }
        public string Contact { get; set; } = "";
    }
}
