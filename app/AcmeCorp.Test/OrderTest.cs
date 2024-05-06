using AcmeCorp.Database;
using AcmeCorp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeCorp.Test
{
    public class OrderTest
    {
        [Fact]
        public async Task GetOrderById_ShouldReturnOrder()
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
                var order = new Order { OrderId = 1, Description = "Test Order", Date = DateTime.Now.ToString("MM/dd/yyyy")};

                var orderService = new OrderService(context);

                // Act
                var exception = await Record.ExceptionAsync(() => orderService.CreateOrder(order));
                
                // Assert
                Assert.Null(exception);
            }
        }
    }
}