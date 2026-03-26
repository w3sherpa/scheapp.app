using scheapp.app.Models.API;

namespace scheapp.app.DataServices.Interfaces
{
    public interface IBusinessDataService
    {
        Task<List<Business>> GetBusinesses();
        Task SaveBusinesses(Business business);
    }
}