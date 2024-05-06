using AcmeCorp.Controllers;
using AcmeCorp.Database;
using AcmeCorp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeCorp.Test
{
    public class CustomerTest
    {
        [Fact]
        public async Task GetCustomers_ReturnsAllCustomers()
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
                var service = new CustomerService(context);
                var customers = new List<Customer>
                            {
                                new Customer { CustomerId = 1, Name = "John Doe" },
                                new Customer { CustomerId = 2, Name = "Jane Smith" },
                                new Customer { CustomerId = 3, Name = "Bob Johnson" }
                            };
                context.Customers.AddRange(customers);

                await context.SaveChangesAsync();

                // Act
                var result = await service.GetCustomers();

                // Assert
                Assert.Equal(3, result.Count());
                Assert.Contains(result, c => c.Name == "John Doe");
                Assert.Contains(result, c => c.Name == "Jane Smith");
                Assert.Contains(result, c => c.Name == "Bob Johnson");
            }
        }

        [Fact]
        public async Task GetCustomerById_ReturnsCorrectCustomer()
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
                var service = new CustomerService(context);
                var customers = new List<Customer>
                            {
                                new Customer { CustomerId = 1, Name = "John Doe" },
                                new Customer { CustomerId = 2, Name = "Jane Smith" },
                                new Customer { CustomerId = 3, Name = "Bob Johnson" }
                            };
                context.Customers.AddRange(customers);

                await context.SaveChangesAsync();

                // Act
                var result = await service.GetCustomer(2);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Jane Smith", result.Name);
            }
        }

        [Fact]
        public async Task GetCustomers_ReturnsAllCustomers_ThroughController()
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
                var service = new CustomerService(context);
                var customers = new List<Customer>
                            {
                                new Customer { CustomerId = 1, Name = "John Doe" },
                                new Customer { CustomerId = 2, Name = "Jane Smith" },
                                new Customer { CustomerId = 3, Name = "Bob Johnson" }
                            };
                context.Customers.AddRange(customers);

                await context.SaveChangesAsync();

                var controller = new CustomerController(service);

                // Act
                var result = ((OkObjectResult)await controller.Get()).Value as List<Customer>;

                // Assert
                Assert.NotNull(result);
                Assert.Equal(3, result.Count());
                Assert.Contains(result, c => c.Name == "John Doe");
                Assert.Contains(result, c => c.Name == "Jane Smith");
                Assert.Contains(result, c => c.Name == "Bob Johnson");
            }
        }
    }
}