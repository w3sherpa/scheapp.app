

namespace scheapp.app.Models.Data
{
    public class Location
    {       
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string? Lat { get; set; }
        public string? Long { get; set; }
    }
}
