namespace Kitchen_Appliances_Backend.DTO.Bill
{
    public class CreateBillRequest
    {
        public int OrderId { get; set; }
        public DateTime PaymentTime { get; set; }
        public decimal Total {  get; set; }
    }
}
