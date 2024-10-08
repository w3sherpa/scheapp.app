using Microsoft.EntityFrameworkCore;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data.TableModels.Businesses;

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

        public async Task<List<Business>> GetBusinesses() => await _apiHelper.CallGetApi<List<Business>>("/Business/GetBusinesses");
        public async Task SaveBusinesses(Business business) => await _apiHelper.CallPostApi<Business>("/Business/SaveBusinesses", business);
    }

    //public class BusinessDataService : IBusinessDataService
    //{
    //    Task<List<Business>> IBusinessDataService.GetBusinesses()
    //    {
    //        return Task.FromResult(new List<Business>() { new Business { Name="Ashir's new business"} });
    //    }

    //    Task IBusinessDataService.SaveBusinesses(Business business)
    //    {
    //        int i = 0;
    //        return Task.CompletedTask;
    //    }
    //}
}
