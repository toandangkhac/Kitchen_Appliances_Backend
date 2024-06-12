using Kitchen_Appliances_MVC.ViewModels.Image;
using Kitchen_Appliances_MVC.ViewModels.Product;

namespace Kitchen_Appliances_MVC.ViewModelData.Image
{
    public class AddImageViewModel
    {
        public List<ProductDTO> Products { get; set; }

        public CreateImage request { get; set; }
    }
}
