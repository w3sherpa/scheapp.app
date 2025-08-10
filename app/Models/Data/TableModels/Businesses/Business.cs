using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.TableModels.Businesses
{
    public class Business
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [EmailAddress]
        public string? Email { get; set; }
       
        [Url]
        public string Website { get; set; } = "";
        [Required]
        public string StreetOne { get; set; } = "";
        public string? StreetTwo { get; set; } = "";
        [Required]
        public string City { get; set; } = "";
        [Required]
        public string State { get; set; } = "";
        [Required]
        public string ZipCode { get; set; } = "";
        [Required]
        public string Country { get; set; } = "";
        public string? Lat { get; set; }
        public string? Long { get; set; }
        public bool IsActive { get; set; }
    }
}
