using System.Security.Claims;

namespace Kitchen_Appliances_Backend.Services.ServiceImpl
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public string UserName => _contextAccessor.HttpContext?.User?.FindFirstValue("Email") ?? "System";

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
    }
}
