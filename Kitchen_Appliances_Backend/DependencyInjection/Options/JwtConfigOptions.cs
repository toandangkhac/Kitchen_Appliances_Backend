namespace Kitchen_Appliances_Backend.DependencyInjection.Options
{
    public class JwtConfigOptions
    {
        public string Issuer { get; set; }
        public string SigningKey { get; set; }
        public int Expired { get; set; }
    }
}
