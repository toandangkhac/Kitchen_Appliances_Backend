namespace Kitchen_Appliances_Backend.DTO.Employee
{
    public class UpdateEmployeeRequest
    {
        public string? Fullname { get; set; } = null;

        public string? PhoneNumber { get; set; } = null;

        public string? Address { get; set; } = null;

        public IFormFile? Image { get; set; }

    }
}
