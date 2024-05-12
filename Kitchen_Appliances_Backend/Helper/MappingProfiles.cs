using AutoMapper;
using Kitchen_Appliances_Backend.DTO;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Helper
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Role, RoleDTO>();
        }
    }
}
