using Microsoft.EntityFrameworkCore;
using ProductStockApiProject;
using ProductStockApiProject.Data;
using ProductStockApiProject.Services;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDB")));

builder.Services.AddScoped<ProductService>(); // Add this line to register ProductService

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
