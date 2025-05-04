using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.API;
using scheapp.app.Models.Data.TableModels.Services;

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
        public async Task<List<Service>> GetServices() => await _apiHelper.CallGetApi<List<Service>>("/Service/GetServices");
        public async Task<List<Service>> GetServices(int businessId) => await _apiHelper.CallGetApi<List<Service>>($"/Service/GetServicesByBusinessId?businessId={businessId}");
        public async Task SaveServices(Service serviceType) => await _apiHelper.CallPostApi<Service>("/Service/SaveServices", serviceType);
        public async Task<List<ServiceDuration>> GetServiceDurations() => await _apiHelper.CallGetApi<List<ServiceDuration>>("/Service/GetServiceDurations");
        public async Task SaveServiceDurations(ServiceDuration serviceDuration) => await _apiHelper.CallPostApi<ServiceDuration>("/Service/SaveServiceDurations", serviceDuration);
        public async Task<GenericApiResponse> DeleteService(DeleteServiceByBusinessIdRQ rq) => await _apiHelper.CallPostApi<DeleteServiceByBusinessIdRQ, GenericApiResponse>("/Service/GetServices",rq);


    }
}
