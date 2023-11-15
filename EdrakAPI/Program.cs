using AutoMapper;
using EdrakBusiness;
using EdrakBusiness.IService;
using EdrakBusiness.Mapper;
using EdrakBusiness.Service;
using EdrakBusiness.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.IUnitOfWork;
using Persistence.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

AppSettings AppSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
builder.Services.AddSingleton(AppSettings);
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(AppSettings.DBConnection));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Edrak API",
        Version = "v1",
        Description = "Edrak API",

    });
});
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(OrderMapper), typeof(ProductMapper), typeof(CustomerMapper));
builder.Services.AddTransient<IOrderService, OrderService>();

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
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zomato API V1");
});
app.Run();
