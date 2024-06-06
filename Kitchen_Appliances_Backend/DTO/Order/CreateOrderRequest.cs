using Kitchen_Appliances_Backend.DTO.OrderDetail;

namespace Kitchen_Appliances_Backend.DTO.Order
{
    public class CreateOrderRequest
    {
        public DateTime CreatedAt { get; set; }
        public int CustomerId { get; set; }
        public List<OrderDetailRequest> OrderDetailRequests { get; set; }
    }
}
