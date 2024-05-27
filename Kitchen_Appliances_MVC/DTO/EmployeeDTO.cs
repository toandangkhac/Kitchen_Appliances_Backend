namespace Kitchen_Appliances_MVC.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string Fullname { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Image { get; set; }

        public string Email { get; set; } = null!;
    }
}
