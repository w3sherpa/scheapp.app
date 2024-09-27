using Microsoft.EntityFrameworkCore;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data.Businesses;

namespace scheapp.app.DataServices
{
    public class BusinessDataService : IBusinessDataService
    {
        private readonly ILogger _logger;
        private readonly IApiHelper _apiHelper;
        public BusinessDataService(ILogger<BusinessDataService> logger, IApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }

        public async Task<List<Business>> GetBusinesses() => await _apiHelper.CallGetApi<List<Business>>("/Business/GetBusinesss");
        public async Task SaveBusinesses(Business business) => await _apiHelper.CallPostApi<Business>("/Business/SaveBusinesss", business);
    }
}
