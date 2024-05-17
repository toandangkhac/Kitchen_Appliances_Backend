namespace Kitchen_Appliances_Backend.DTO.Account
{
    public class AccountDTO
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool Status { get; set; }

        public int RoleId { get; set; }
    }
}
