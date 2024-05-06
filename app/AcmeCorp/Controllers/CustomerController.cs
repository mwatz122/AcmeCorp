using AcmeCorp.Database;
using AcmeCorp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcmeCorp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost(Name = "create_customer")]
        public async Task<IActionResult> Create(Customer customer)
        {
            await _customerService.CreateCustomer(customer);

            return Ok();
        }

        /// <summary>
        /// Retrieve a customer by id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}", Name = "get_customer")]
        public async Task<IActionResult> Get(long customerId)
        {
            var customer = await _customerService.GetCustomer(customerId);

            return Ok(customer);
        }

        /// <summary>
        /// Retrieve all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "get_customers")]
        public async Task<IActionResult> Get()
        {
            var customers = await _customerService.GetCustomers();

            return Ok(customers);
        }
    }
}