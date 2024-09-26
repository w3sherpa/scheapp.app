

namespace scheapp.app.Models.Data
{
    public class CustomerContact
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TypeId { get; set; }
        public string Contact { get; set; } = "";
    }
}
