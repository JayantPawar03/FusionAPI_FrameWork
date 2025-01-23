using FusionAPI_Framework.Interfaces;
using FusionAPI_Framework.Models;
using FusionAPI_Framework.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ILabourService, LabourService>();
builder.Services.AddTransient<IDepartmentService,DepartmentService>();
builder.Services.AddTransient<IProduct, ProductService>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
//builder.Services.AddDbContext<LabourContext>(o=>o.UseSqlServer(builder.Configuration.GetConnectionString("apicon")));

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
