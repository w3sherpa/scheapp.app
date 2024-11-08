using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.API;
using scheapp.app.Models.Data.TableModels.Contacts;

namespace scheapp.app.DataServices
{
    public class ContactsDataService : IContactsDataService
    {
        private readonly ILogger _logger;
        private readonly IApiHelper _apiHelper;
        public ContactsDataService(ILogger<ContactsDataService> logger, IApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }

        public async Task<List<ContactType>> GetContactTypes() => await _apiHelper.CallGetApi<List<ContactType>>("/Contacts/GetContactTypes");
        public async Task<List<CustomerContact>> GetCustomerContacts() => await _apiHelper.CallGetApi<List<CustomerContact>>("/Contacts/GetCustomerContacts");
        public async Task<List<ProfessionalContact>> GetProfessionalContacts() => await _apiHelper.CallGetApi<List<ProfessionalContact>>("/Contacts/GetProfessionalContacts");
        public async Task<HttpResponseMessage> SaveContactTypes(ContactType contactType) => await _apiHelper.CallPostApi<ContactType>("/Contacts/SaveContactTypes", contactType);
        public async Task<HttpResponseMessage> SaveCustomerContact(CustomerContact contact) => await _apiHelper.CallPostApi<CustomerContact>("/Contacts/SaveCustomerContact", contact);
        public async Task<HttpResponseMessage> SaveProfessionalContact(ProfessionalContact contact) => await _apiHelper.CallPostApi<ProfessionalContact>("/Contacts/SaveCustomerContact", contact);
        public async Task<HttpResponseMessage> CustomerConfirmScheduleAppointment(CustomerConfirmationRQ confirmation) => await _apiHelper.CallPostApi<CustomerConfirmationRQ>("/Contacts/CustomerConfirmScheduleAppointment", confirmation);
    }
}
