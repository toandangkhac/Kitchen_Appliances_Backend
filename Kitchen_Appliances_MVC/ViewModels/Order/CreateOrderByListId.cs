namespace Kitchen_Appliances_MVC.ViewModels.Order
{
	public class CreateOrderByListId
	{
		public int EmployeeId { get; set; }
		public int CustomerId { get; set; }
		public List<int> Ids { get; set; }
	}
}
