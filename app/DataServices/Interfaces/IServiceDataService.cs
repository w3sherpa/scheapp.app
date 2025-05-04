using scheapp.app.Models.API;
using scheapp.app.Models.Data.TableModels.Services;

namespace scheapp.app.DataServices.Interfaces
{
    public interface IServiceDataService
    {
        Task<List<ServiceDuration>> GetServiceDurations();
        Task<List<Service>> GetServices();
        Task<List<Service>> GetServices(int businessId);
        Task SaveServiceDurations(ServiceDuration serviceDuration);
        Task SaveServices(Service serviceType);
        Task<GenericApiResponse> DeleteService(DeleteServiceByBusinessIdRQ rq);
    }
}