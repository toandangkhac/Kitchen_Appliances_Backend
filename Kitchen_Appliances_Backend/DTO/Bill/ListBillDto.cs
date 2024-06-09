namespace Kitchen_Appliances_Backend.DTO.Bill
{
	public class ListBillDto
	{
		public int OrderId { get; set; }

		public DateTime PaymentTime { get; set; }

		public decimal Total { get; set; }
	}
}
