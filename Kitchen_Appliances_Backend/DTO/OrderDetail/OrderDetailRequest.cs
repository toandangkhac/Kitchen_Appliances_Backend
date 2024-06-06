namespace Kitchen_Appliances_Backend.DTO.OrderDetail
{
    public class OrderDetailRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
