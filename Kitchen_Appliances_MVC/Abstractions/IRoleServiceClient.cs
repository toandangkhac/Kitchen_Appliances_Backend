using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Role;
using System.Data;

namespace Kitchen_Appliances_MVC.Abstractions
{
	public interface IRoleServiceClient
	{
		Task<APIResponse<List<RoleDTO>>> GetRoles();
	}
}
