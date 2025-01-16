using FoodScanApp;
using FoodScanApp.Controllers;
using FoodScanApp.Services;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient<FoodDataService>(); // Lägg till HttpClient som tjänst
builder.Services.Configure<FoodApiSettings>(builder.Configuration.GetSection("FoodApi"));
builder.Services.AddHttpClient<IFoodDataService, FoodDataService>();


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
