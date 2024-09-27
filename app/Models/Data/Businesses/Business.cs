using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.Businesses
{
    public class Business
    {

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Website { get; set; } = "";
        public string StreetOne { get; set; } = "";
        public string? StreetTwo { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string Country { get; set; } = "";
        public string? Lat { get; set; }
        public string? Long { get; set; }
        public bool IsActive { get; set; }
    }
}
