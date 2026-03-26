using scheapp.app.Models.API;

namespace scheapp.app.DataServices.Interfaces
{
    public interface ICommunicationDataService
    {
        Task<List<CustomerCall>> GetCustomerCalls();
        //Task<List<ProfessionalCall>> GetProfessionalCalls();
        Task UpdateTwilioCallStatus(TwilioStatusCallback statusCallback);
    }
}