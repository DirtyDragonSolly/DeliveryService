namespace DeliveryService.Models.Requests
{
    public class FilterOrdersRequest
    {
        public string CityDistrict { get; set; }
        public DateTime FirstDeliveryDateTime { get; set; }
    }
}
