using System.Text.Json.Serialization;

namespace Kitchen_Appliances_Backend.DTO.CartItem
{
    public class UpdateCartDetailRequest
    {
		//[JsonIgnore]
		//public string UserId { get; set; }

		//[JsonIgnore]
		//public long CartDetailId { get; set; }
		public int ProductId { get; set; }

		public int CustomerId { get; set; }

		public int Quantity { get; set; }
    }
}
