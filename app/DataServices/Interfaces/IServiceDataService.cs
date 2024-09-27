using scheapp.app.Models.Data.Services;

namespace scheapp.app.DataServices.Interfaces
{
    public interface IServiceDataService
    {
        Task<List<ServiceDuration>> GetServiceDurations();
        Task<List<ServiceType>> GetServiceTypes();
        Task SaveServiceDurations(ServiceDuration serviceDuration);
        Task SaveServiceTypes(ServiceType serviceType);
    }
}