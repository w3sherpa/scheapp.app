using scheapp.data.Db.TableModels.Businesses;

namespace scheapp.app.DataServices.Interfaces
{
    public interface IBusinessDataService
    {
        Task<List<Business>> GetBusinesses();
        Task SaveBusinesses(Business business);
    }
}