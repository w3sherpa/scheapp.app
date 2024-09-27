using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.Businesses
{
    public class BusinessConfigurationCategory
    {

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsSettingListType { get; set; }
    }
}
