namespace Kitchen_Appliances_MVC.ViewModels.Product
{
    public class UpdateProductRequest
    {
        public string? Name { get; set; } = null;

        public string? Description { get; set; } = null;

        //public int CategoryId { get; set; }
        public int Quantity { get; set; } = 0;

        public decimal? Price { get; set; } = null;
    }
}
