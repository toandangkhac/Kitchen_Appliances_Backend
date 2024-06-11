namespace Kitchen_Appliances_Backend.DTO.ProductPrice
{
    public class ProductPriceDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public DateTime AppliedDate { get; set; }

        public decimal Price { get; set; }

        public int EmployeeId { get; set; }
    }
}
