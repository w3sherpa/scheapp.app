using scheapp.app.Models.Data;
using System.Threading.Tasks;

namespace scheapp.app.DataServices.Interfaces
{
    public interface IContactsDataService
    {
        Task<List<ContactType>> GetContactTypes();
        Task<List<CustomerContact>> GetCustomerContacts();
        Task<List<ProfessionalContact>> GetProfessionalContacts();
        Task<HttpResponseMessage> SaveContactTypes(ContactType contactType);
        Task<HttpResponseMessage> SaveCustomerContact(CustomerContact contact);
        Task<HttpResponseMessage> SaveProfessionalContact(ProfessionalContact contact);
    }
}