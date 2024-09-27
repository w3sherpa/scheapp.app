using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data.Services;

namespace scheapp.app.DataServices
{
    public class ServiceDataService : IServiceDataService
    {
        private readonly ILogger _logger;
        private readonly IApiHelper _apiHelper;
        public ServiceDataService(ILogger<ServiceDataService> logger, IApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }
        public async Task<List<ServiceType>> GetServiceTypes() => await _apiHelper.CallGetApi<List<ServiceType>>("/Service/GetServiceTypes");
        public async Task SaveServiceTypes(ServiceType serviceType) => await _apiHelper.CallPostApi<ServiceType>("/Service/SaveServiceTypes", serviceType);
        public async Task<List<ServiceDuration>> GetServiceDurations() => await _apiHelper.CallGetApi<List<ServiceDuration>>("/Service/GetServiceDurations");
        public async Task SaveServiceDurations(ServiceDuration serviceDuration) => await _apiHelper.CallPostApi<ServiceDuration>("/Service/SaveServiceDurations", serviceDuration);
    }
}
