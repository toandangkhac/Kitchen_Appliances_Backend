namespace Kitchen_Appliances_Backend.Services
{
    public interface IOtpService
    {
        public string GenerateOTP(int digitNumber = 6);
    }
}
