namespace DeliveryService.Models.Responses
{
    public class OrdersResponse
    {
        public Guid Id { get; set; }

        public decimal WeightInKg { get; set; }

        public string District { get; set; }

        public DateTime DeliveryTime { get; set; }
    }
}
