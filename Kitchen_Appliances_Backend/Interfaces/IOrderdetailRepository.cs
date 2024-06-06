namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IOrderdetailRepository
    {
        Task<bool> CreateOrderDetails(List<int> cartDetailIds);


    }
}
