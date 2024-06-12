namespace Kitchen_Appliances_Backend.DTO.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateTime CreateAt { get; set; }

        public int CustomerId { get; set; }

        public int Status { get; set; }
		public string AddressShipping { get; set; }

		public bool PaymentStatus { get; set; }

        public int? EmployeeId { get; set; }
    }
}
