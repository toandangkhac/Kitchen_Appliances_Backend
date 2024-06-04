namespace Kitchen_Appliances_Backend.DTO.ProductPrice
{
    public class UpdateProductPriceRequest
    {
        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int EmployeeId { get; set; }
    }
}
