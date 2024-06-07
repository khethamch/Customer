using Customer.Models;

namespace Customer.Services
{
    public interface ICustomer
    {
        Task CreateCustomer(CustomerModel customer);
        Task UpdateCustomer(CustomerModel customer);
        Task UpdateCustomerName(int id, string name);
        Task DeleteCustomer(int id);
        Task<CustomerModel> GetCustomer(int id);
        Task<IEnumerable<CustomerModel>> GetCustomers();
    }
}
