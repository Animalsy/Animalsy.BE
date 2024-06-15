using Animalsy.BE.Services.ProductAPI;
using Animalsy.BE.Services.ProductAPI.Data;
using Animalsy.BE.Services.ProductAPI.Repository;
using Animalsy.BE.Services.ProductAPI.Utilities;
using Animalsy.BE.Services.ProductAPI.Validators.Factory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(MappingConfig.RegisterMaps().CreateMapper());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IValidatorFactory, ValidatorFactory>();
builder.Services.AddValidators();
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

app.ApplyPendingMigrations();

app.MapControllers();

app.Run();
