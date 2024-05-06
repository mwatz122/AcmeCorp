using AcmeCorp.Database;

namespace AcmeCorp.Services
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
    }
}