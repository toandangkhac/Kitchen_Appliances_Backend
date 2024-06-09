namespace Kitchen_Appliances_Backend.DTO.Order
{
	public class CreateOrderByListId
	{
		public int EmployeeId { get; set; }
		public int CustomerId { get; set; }
		public List<int> Ids { get; set; }
	}
}
