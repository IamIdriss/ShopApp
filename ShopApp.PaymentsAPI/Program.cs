using ShopApp.PaymentsAPI.RabbitMQSender;
using ShopApp.PaymentsAPI.Repository;
using ShopApp.PaymentsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IPaymentProcessor, PaymentProcessor>();
builder.Services.AddSingleton<IRabbitMQPaymentResultMessageSender, RabbitMqPaymentResultMessageSender>();
builder.Services.AddHostedService<RabbitMQPaymentConsumer>();

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
