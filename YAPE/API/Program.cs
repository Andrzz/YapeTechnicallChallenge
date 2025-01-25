using Application.Interfaces;
using Application.Services;
using Infrastructure.Adapters;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ClientServiceAdapter>(sp =>
{
    var soapUrl = "http://localhost:64119/Service.svc";
    return new ClientServiceAdapter(soapUrl);
});
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonServiceClient>(provider =>
{
    var soapUrl = builder.Configuration["SoapSettings:Url"];
    return new ClientServiceAdapter(soapUrl);
});
builder.Services.AddScoped<IClientService, ClientService>();
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
