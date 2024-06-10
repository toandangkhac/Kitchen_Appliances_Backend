namespace Kitchen_Appliances_MVC.ViewModels.CartDetail
{
	public class UpdateCartDetailRequest
	{
		public int ProductId { get; set; }

		public int CustomerId { get; set; }

		public int Quantity { get; set; }
	}
}
