using AcmeCorp.Database;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AcmeCorpContext _db;

        public CustomerService(AcmeCorpContext db)
        {
            _db = db;
        }

        public async Task CreateCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomer(long customerId)
        {
            return await _db.Customers.Include(x => x.Orders).FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _db.Customers.Include(x => x.Orders).ToListAsync();
        }
    }
}