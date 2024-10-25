using DeliveryService.Models;
using DeliveryService.Models.Responses;
using DeliveryService.Options;
using DeliveryService.Services.Interfaces;
using System.Text.Json;

namespace DeliveryService.Services.Implementations
{
    public class OrderService : IOrderService
    {
        public readonly DeliverySettings _deliverySettings;

        private readonly ILogger _logger;

        public OrderService(
            DeliverySettings deliverySettings, 
            ILogger<OrderService> logger)
        {
            _deliverySettings = deliverySettings;
            _logger = logger;
        }

        public async Task CreateAsync(Order order)
        {
            await using (var writer = new StreamWriter(_deliverySettings.InputFile, true))
            {
                await writer.WriteLineAsync(JsonSerializer.Serialize(order));
            }

            _logger.LogInformation("Заявка создана: {0}", order.Id);
        }

        public async Task<IEnumerable<OrdersResponse>> FilterAsync(string cityDistrict, DateTime firstDeliveryDateTime)
        {            
            var orders = await GetOrdersFromFileAsync();

            var result = orders
                .Where(w => w.District == cityDistrict && w.DeliveryTime == firstDeliveryDateTime)
                .Select(s => new OrdersResponse
                {
                    Id = s.Id,
                    DeliveryTime = s.DeliveryTime,
                    District = s.District,
                    WeightInKg = s.WeightInKg
                });

            _logger.LogInformation("Выдан результат фильтрации по входным данным Города доставки: {0}; и времени доставки: {1}", cityDistrict, firstDeliveryDateTime);
            return result;
        }

        private async Task<IEnumerable<Order>> GetOrdersFromFileAsync()
        {
            return (await File.ReadAllLinesAsync(_deliverySettings.InputFile))
                .Select(line => JsonSerializer.Deserialize<Order>(line));
        }
    }
}
