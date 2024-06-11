using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.DTO.CartItem
{
    public class CartDetailDTO
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
