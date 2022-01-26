using MyRetail.Models;
using MyRetail.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IProductDetailsService, ProductDetailsService>();    
builder.Services.AddScoped<IProductPriceService, ProductPriceService>();
builder.Services.Configure<ProductDetailsConfiguration>(builder.Configuration.GetSection("ProductDetails"));
builder.Services.Configure<ProductPriceStoreConfiguration>(
builder.Configuration.GetSection("ProductPriceStoreDatabase"));

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
