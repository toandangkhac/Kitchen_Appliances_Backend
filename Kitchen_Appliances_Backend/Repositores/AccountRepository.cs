using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        
        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Account> getAccountByEmail(string email)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == email);
            return account;
        }
    }
}
