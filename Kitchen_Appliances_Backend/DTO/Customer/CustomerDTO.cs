namespace Kitchen_Appliances_Backend.DTO.Customer
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        public string Fullname { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string Email { get; set; } = null!;
    }
}
