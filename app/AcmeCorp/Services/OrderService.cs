using AcmeCorp.Database;

namespace AcmeCorp.Services
{
    public class OrderService : IOrderService
    {
        private readonly AcmeCorpContext _db;

        public OrderService(AcmeCorpContext db)
        {
            _db = db;
        }

        public async Task CreateOrder(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }
    }
}