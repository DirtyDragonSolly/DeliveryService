using DeliveryService.Models;
using DeliveryService.Models.Requests;
using DeliveryService.Models.Responses;
using DeliveryService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost(nameof(Create))]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> Create([FromBody, Required] CreateOrderRequest request)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                DeliveryTime = request.DeliveryTime,
                District = request.District,
                WeightInKg = request.WeightInKg
            };

            await _orderService.CreateAsync(order);

            return Ok(order.Id);
        }

        [HttpPost(nameof(Filter))]
        [ProducesResponseType(typeof(IEnumerable<OrdersResponse>), 200)]
        public async Task<IActionResult> Filter([FromBody, Required] FilterOrdersRequest request)
        {
            return Ok(await _orderService.FilterAsync(request.CityDistrict, request.FirstDeliveryDateTime));
        }
    }
}
