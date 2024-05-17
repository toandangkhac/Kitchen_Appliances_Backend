using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> getAccountByEmail(string email);
    }
}
