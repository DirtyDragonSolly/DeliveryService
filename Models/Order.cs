namespace DeliveryService.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public decimal WeightInKg { get; set; }

        public string District { get; set; }

        public DateTime DeliveryTime { get; set; }
    }
}
