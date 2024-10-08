using scheapp.app.Models.Data.TableModels.Customers;

namespace scheapp.app.DataServices.Interfaces
{
    public interface ICustomerDataService
    {
        Task<List<Customer>> GetCustomers();
        Task SaveCustomers(Customer customer);
    }
}