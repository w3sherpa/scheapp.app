using scheapp.app.Models.Data.Services;

namespace scheapp.app.DataServices.Interfaces
{
    public interface IServiceDataService
    {
        Task<List<ServiceDuration>> GetServiceDurations();
        Task<List<Service>> GetServices();
        Task SaveServiceDurations(ServiceDuration serviceDuration);
        Task SaveServices(Service serviceType);
    }
}