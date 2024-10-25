using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models.Requests
{
    public class CreateOrderRequest
    {
        [Range(1, int.MaxValue)]
        public decimal WeightInKg { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public DateTime DeliveryTime { get; set; }
    }
}
