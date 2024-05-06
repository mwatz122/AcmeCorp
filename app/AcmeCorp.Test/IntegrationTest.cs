using AcmeCorp.Controllers;
using AcmeCorp.Database;
using AcmeCorp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeCorp.Test
{
    public class IntegrationTest
    {
        [Fact]
        public async Task GetCustomers_ReturnsAllCustomerOrders()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AcmeCorpContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .UseInternalServiceProvider(new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider())
                .Options;

            using (var context = new AcmeCorpContext(options))
            {
                var customerService = new CustomerService(context);
                var customers = new List<Customer>
                                {
                                    new Customer { CustomerId = 1, Name = "John Doe" },
                                    new Customer { CustomerId = 2, Name = "Jane Smith" },
                                    new Customer { CustomerId = 3, Name = "Bob Johnson" }
                                };
                context.Customers.AddRange(customers);

                await context.SaveChangesAsync();

                var orderService = new OrderService(context);
                var order = new Order { OrderId = 1, Description = "Order 1", Date = DateTime.Now.ToString("MM/dd/yyyy"), CustomerId = 1 };
                await orderService.CreateOrder(order);

                var controller = new CustomerController(customerService);

                // Act
                var result = ((OkObjectResult)await controller.Get()).Value as List<Customer>;

                // Assert
                Assert.NotNull(result);
                Assert.Equal(3, result.Count());
                Assert.Contains(result, c => c.Name == "John Doe");
                Assert.Contains(result, c => c.Name == "Jane Smith");
                Assert.Contains(result, c => c.Name == "Bob Johnson");

                var customerWithOrder = result.FirstOrDefault(x => x.CustomerId == 1);
                Assert.NotNull(customerWithOrder);
                Assert.NotNull(customerWithOrder.Orders);
                Assert.Equal("Order 1", customerWithOrder.Orders.FirstOrDefault()?.Description);
            }
        }
    }
}
