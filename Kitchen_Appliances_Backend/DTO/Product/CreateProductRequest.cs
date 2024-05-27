namespace Kitchen_Appliances_Backend.DTO.Product
{
    public class CreateProductRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }
    }
}
