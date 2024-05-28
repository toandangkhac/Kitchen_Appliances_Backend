namespace Kitchen_Appliances_Backend.Services.ServiceImpl
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Current { get => DateTime.Now; set => throw new NotImplementedException(); }
    }
}
