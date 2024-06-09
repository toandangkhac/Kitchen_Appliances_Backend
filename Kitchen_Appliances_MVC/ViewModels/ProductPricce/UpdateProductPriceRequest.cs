namespace Kitchen_Appliances_MVC.ViewModels.ProductPrice
{
    public class UpdateProductPriceRequest
    {
        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int EmployeeId { get; set; }
    }
}
