using scheapp.app.Models.API;
using scheapp.app.Models.Data.TableModels.Contacts;

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
        Task<HttpResponseMessage> CustomerConfirmScheduleAppointment(CustomerConfirmationRQ confirmation);
    }
}