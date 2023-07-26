using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopApp.ProductsAPI.Repository;
using ShopApp.ShoppingCartAPI.Repository;
using ShopApp.ShoppingCartAPI.ShoppingCartData;

var builder = WebApplication.CreateBuilder(args);

//Get The Connection String Value
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<ShoppingCartDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(ShopApp.ProductsAPI.Repository.MappingProfile).Assembly);
builder.Services.AddScoped<ICartRepository, CartRepository>();
//builder.Services.AddScoped<ICouponRepository, CouponRepository>();


builder.Services.AddControllers();
//builder.Services.AddHttpClient<ICouponRepository, CouponRepository>(hc => hc.BaseAddress = new Uri(
//     builder.Configuration["APIUrls:CouponsAPI"]));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.Authority = "https://localhost:7003/";
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "shopapp");
    });
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Shopapp.ShoppingCartAPI" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type= ReferenceType.SecurityScheme,
                Id= "Bearer"
            },
            Scheme="outh2",
            Name="Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
