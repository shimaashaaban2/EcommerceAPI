using ECommerceApp.Data;
using ECommerceApp.Services;
using ECommerceApp.Services.AddressServices;
using ECommerceApp.Services.CancellationServices;
using ECommerceApp.Services.CategoryServices;
using ECommerceApp.Services.CustomerServices;
using ECommerceApp.Services.FeedbackServices;
using ECommerceApp.Services.OrderServices;
using ECommerceApp.Services.PaymentServices;
using ECommerceApp.Services.ProductServices;
using ECommerceApp.Services.RefundServices;
using ECommerceApp.Services.ShoppingCartServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        // This will use the property names as defined in the C# mo
        options.JsonSerializerOptions.PropertyNamingPolicy= null; // Preserve property names as defined in the model
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICancellationService, CancellationService>();
// Registering the RefundService
builder.Services.AddScoped<RefundService>();
// Register Refund Processing Background Service
builder.Services.AddHostedService<RefundProcessingBackgroundService>();

// Register Payment Background Service
builder.Services.AddHostedService<PendingPaymentService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<EmailService>();

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
