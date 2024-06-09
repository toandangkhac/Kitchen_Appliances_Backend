namespace Kitchen_Appliances_MVC.ViewModels.CartDetail
{
    public class CreateCartDetailRequest
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
