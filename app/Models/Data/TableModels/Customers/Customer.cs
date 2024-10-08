using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.TableModels.Customers
{
    public class Customer
    {

        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int BusinessId { get; set; }
    }
}
