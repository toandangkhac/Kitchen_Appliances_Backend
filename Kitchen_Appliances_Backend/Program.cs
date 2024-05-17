using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DependencyInjection.Extensions;
using Kitchen_Appliances_Backend.Helper;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Repositores;
using Kitchen_Appliances_Backend.Services;
using Kitchen_Appliances_Backend.Services.ServiceImpl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddConfigureApplication();
builder.Services.AddJwtAuthentication(builder.Configuration);

//AutoMapper
//Get all classes that implement IAutoMapperProfile and register them
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Add service for AutoMapper with the assembly of MappingProfiles
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext to the container for dependency injection
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
