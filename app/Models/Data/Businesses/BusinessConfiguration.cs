using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.Businesses
{
    public class BusinessConfiguration
    {

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
        public int BusinessConfigurationCategoryId { get; set; }
        public int BusinessId { get; set; }
    }
}
