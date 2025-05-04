using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data.TableModels.Customers;

namespace scheapp.app.DataServices
{
    public class CustomerDataService : ICustomerDataService
    {
        private readonly ILogger _logger;
        private readonly IApiHelper _apiHelper;
        public CustomerDataService(ILogger<CustomerDataService> logger, IApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }
        public async Task<List<Customer>> GetCustomers(int businessId) => await _apiHelper.CallGetApi<List<Customer>>($"/Customer/GetCustomers?businessId={businessId}");
        public async Task SaveCustomers(Customer customer) => await _apiHelper.CallPostApi<Customer>("/Customer/SaveCustomers", customer);
    }
}
