using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context)
        {   
            this._context = context;
        }
        public ICollection<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
