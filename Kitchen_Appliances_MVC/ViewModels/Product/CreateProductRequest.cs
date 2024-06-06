namespace Kitchen_Appliances_MVC.ViewModels.Product
{
    public class CreateProductRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
