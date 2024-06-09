using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Role;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RoleRepository(DataContext context, IMapper mapper)
        {   
            this._context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<RoleDTO>>> GetRoles()
        {
            return new ApiResponse<List<RoleDTO>>()
            {
                Status = 200,
                Message = "Lấy danh sách Role thành công",
                Data = _mapper.Map<List<RoleDTO>>(_context.Roles.ToList())
            };
        }
    }
}
