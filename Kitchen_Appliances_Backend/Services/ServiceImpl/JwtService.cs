using Azure.Core;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Kitchen_Appliances_Backend.Services.ServiceImpl
{
    public class JwtService : IJwtService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public JwtService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> CreateJWT(string email)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == email);

            var role = _context.Roles.FirstOrDefault(x => x.Id == account.RoleId);

            var claims = new List<Claim>()
            {
                new ("Email", account.Email),
            };

            claims.Add(new Claim(ClaimTypes.Role, role.Name));

            var jwtOptions = new JwtConfigOptions();
            _configuration.GetSection(nameof(JwtConfigOptions)).Bind(jwtOptions);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(jwtOptions.Issuer,
                jwtOptions.Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(jwtOptions.Expired),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal validateExpiredJwt(string token)
        {
            IdentityModelEventSource.ShowPII = true;

            var jwtOptions = new JwtConfigOptions();
            _configuration.GetSection(nameof(JwtConfigOptions)).Bind(jwtOptions);

            TokenValidationParameters validationParameters = new()
            {
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidAudience = jwtOptions.Issuer,
                ValidIssuer = jwtOptions.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
            };

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            if (validatedToken is not JwtSecurityToken jwtSecurityToken 
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }
    }
}
