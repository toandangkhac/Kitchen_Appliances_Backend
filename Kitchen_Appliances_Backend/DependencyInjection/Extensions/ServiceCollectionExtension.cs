using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Repositores;
using Kitchen_Appliances_Backend.Services.ServiceImpl;
using Kitchen_Appliances_Backend.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Kitchen_Appliances_Backend.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;

namespace Kitchen_Appliances_Backend.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfigureApplication(this IServiceCollection services)
        {
            services.AddServices();
            services.AddSwaggers();
            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IRoleRepository, RoleRepository>()
                    .AddTransient<IAccountRepository, AccountRepository>()
                    .AddTransient<IBillRepository, BillRepository>()
                    .AddTransient<ICartDetailRepository, CartDetailRepository>()
                    .AddTransient<ICategoryRepository, CategoryRepository>()
                    .AddTransient<ICustomerRepository, CustomerRepository>()
                    .AddTransient<IEmployeeRepository, EmployeeRepository>()
                    .AddTransient<IImageRepository, ImageRepository>()
                    .AddTransient<IOrderRepository, OrderRepository>()
                    .AddTransient<IOrderdetailRepository, OrderdetailRepository>()
                    .AddTransient<IProductpriceRepository, ProductpriceRepository>()
                    .AddTransient<IProductRepository, ProductRepository>();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();

        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = new JwtConfigOptions();
            configuration.GetSection(nameof(JwtConfigOptions)).Bind(jwtConfig);

            byte[] signingKeyBytes = Encoding.UTF8.GetBytes(jwtConfig.SigningKey);
            services
                .AddAuthentication(otps =>
                {
                    otps.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    otps.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    otps.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(option => {
                    option.RequireHttpsMetadata = false;
                    option.SaveToken = true;
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtConfig.Issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                    };
                });
            services.AddAuthorization(
                options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder(
                        JwtBearerDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser().Build();
                });
        }

        private static void AddSwaggers(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kitchen Application Q04",
                    Version = "v1",
                    Description = "Bán dụng cụ bếp cho nhóm Thiên Địa Hội Q04",
                    Contact = new OpenApiContact
                    {
                        Email = "n20dccn008@student.ptithcm.edu.vn",
                        Name = "Trần Hữu Chiến",
                        Url = new Uri("http://chientran.blog")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    //Type = SecuritySchemeType.ApiKey, sai format code_jwt , right must to 'bearer code_jwt'
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}
