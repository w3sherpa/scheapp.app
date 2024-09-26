using Microsoft.EntityFrameworkCore;
using scheapp.app.Helpers;
using scheapp.app.Models.Data;

namespace scheapp.app.DataServices
{
    public class CommunicationDataService 
    {
        private readonly ILogger _logger;
        private readonly IApiHelper _apiHelper;
        public CommunicationDataService(ILogger<CommunicationDataService> logger, IApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }

        public async Task<List<CustomerCall>> GetCustomerCalls() => await _apiHelper.CallGetApi<List<CustomerCall>>("/Communication/GetCustomerCalls");
        public async Task<List<ProfessionalCall>> GetProfessionalCalls() => await _apiHelper.CallGetApi<List<ProfessionalCall>>("/Communication/GetProfessionalCalls");

    }
}
