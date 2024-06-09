namespace Kitchen_Appliances_MVC.ViewModels.Bill
{
	public class BillDto
	{
		public int OrderId { get; set; }

		public DateTime PaymentTime { get; set; }

		public int CustomerId { get; set; }

		public string? CustomerName { get; set; }

		public int EmployeeId { get; set; }

		public string? EmployeeName { get; set; }

		public decimal Total { get; set; }


	}
}
