using System.ComponentModel.DataAnnotations;

namespace Kitchen_Appliances_Backend.DTO.Image
{
    public class CreateImageRequest
    {
        [Required]
        public IFormFile? Url { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
