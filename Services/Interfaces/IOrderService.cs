using DeliveryService.Models;
using DeliveryService.Models.Responses;

namespace DeliveryService.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateAsync(Order order);
        Task<IEnumerable<OrdersResponse>> FilterAsync(string cityDistrict, DateTime firstDeliveryDateTime);
    }
}
