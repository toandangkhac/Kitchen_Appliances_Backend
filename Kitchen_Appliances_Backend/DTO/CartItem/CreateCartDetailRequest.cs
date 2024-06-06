namespace Kitchen_Appliances_Backend.DTO.CartItem
{
    public class CreateCartDetailRequest
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
