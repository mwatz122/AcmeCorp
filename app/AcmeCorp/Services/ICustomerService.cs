using AcmeCorp.Database;

namespace AcmeCorp.Services
{
    public interface ICustomerService
    {
        Task CreateCustomer(Customer customer);
        Task<Customer> GetCustomer(long customerId);
        Task<List<Customer>> GetCustomers();
    }
}