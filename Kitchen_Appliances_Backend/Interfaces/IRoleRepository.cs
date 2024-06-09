using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Role;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IRoleRepository
    {
        Task<ApiResponse<List<RoleDTO>>> GetRoles();
    }
}
