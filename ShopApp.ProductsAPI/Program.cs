using Microsoft.EntityFrameworkCore;
using ShopApp.ProductsAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Get the Connection String Value
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Registering the DbContext
builder.Services.AddDbContext<ProductsDbContext>(options =>options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
