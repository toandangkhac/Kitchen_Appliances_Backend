using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.ApiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7178") });

builder.Services.AddScoped<IEmployeeClient, EmployeeClientService>();
builder.Services.AddScoped<IAccountClient, AccountClientService>();
builder.Services.AddScoped<ICustomerServiceClient, CustomerClientService>();
builder.Services.AddScoped<IProductServiceClient, ProductServiceClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
