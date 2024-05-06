using AcmeCorp.Database;
using AcmeCorp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcmeCorp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Create a new Order
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        [HttpPost(Name = "create_order")]
        public async Task<IActionResult> Create(Order Order)
        {
            await _orderService.CreateOrder(Order);

            return Ok();
        }
    }
}