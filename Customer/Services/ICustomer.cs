using Customer.Models;

namespace Customer.Services
{
    public interface ICustomer
    {
        List<CustomerModel> GetCustomers();
        List<CustomerModel> GetCustomersWithNewOne();
    }
}
