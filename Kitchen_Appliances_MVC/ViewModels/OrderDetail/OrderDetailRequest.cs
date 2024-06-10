namespace Kitchen_Appliances_MVC.ViewModels.OrderDetail
{
	public class OrderDetailRequest
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
