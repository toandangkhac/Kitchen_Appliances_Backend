using System.ComponentModel.DataAnnotations;

namespace Kitchen_Appliances_MVC.ViewModels.Image
{
    public class CreateImage
    {
        [Required]
        public IFormFile? Url { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
