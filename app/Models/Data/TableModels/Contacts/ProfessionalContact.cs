using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.TableModels.Contacts
{
    public class ProfessionalContact
    {

        public int Id { get; set; }
        public int ProfessionalId { get; set; }
        public int TypeId { get; set; }
        public string Contact { get; set; } = "";
    }
}
