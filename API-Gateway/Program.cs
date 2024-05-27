using API_Gateway.Configs;
//using Hellang.Middleware.ProblemDetails;
//using Microsoft.OpenApi.Models;
//using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//add services to the containers

//var routes = "Routes";

//builder.Configuration.AddOcelotWithSwaggerSupport(option =>
//{
//    option.Folder = routes;
//});

//builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddOcelot(builder.Configuration);

//builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
//    .AddOcelot(routes, builder.Environment)
//    .AddEnvironmentVariables();

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot1.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
//});

var app = builder.Build();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseProblemDetails();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});


//app.UseSwaggerForOcelotUI(options =>
//{
//    options.PathToSwaggerGenerator = "/swagger/docs";
//    options.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;
//}).UseOcelot().Wait();

app.UseOcelot().Wait();

//app.MapGet("/values", () => "Xin chao");

app.Run();


    //< PackageReference Include = "MMLib.SwaggerForOcelot" Version = "8.0.0" />

    //< PackageReference Include = "Swashbuckle.AspNetCore" Version = "6.5.0" />

    //< PackageReference Include = "Hellang.Middleware.ProblemDetails" Version = "6.5.1" />
