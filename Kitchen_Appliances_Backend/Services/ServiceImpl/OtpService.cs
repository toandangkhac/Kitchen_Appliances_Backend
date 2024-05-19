namespace Kitchen_Appliances_Backend.Services.ServiceImpl
{
    public class OtpService : IOtpService
    {
        private const string allowedChars = "0123456789";

        public string GenerateOTP(int digitNumber = 6)
        {
            Random random = new Random();
            char[] chars = new char[digitNumber];
            for (int i = 0; i < digitNumber; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
    }
}
