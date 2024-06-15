using Animalsy.BE.Services.PetAPI;
using Animalsy.BE.Services.PetAPI.Data;
using Animalsy.BE.Services.PetAPI.Repository;
using Animalsy.BE.Services.PetAPI.Utilities;
using Animalsy.BE.Services.PetAPI.Validators.Factory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(MappingConfig.RegisterMaps().CreateMapper());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPetRepository, PetRepository>();
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

app.MapControllers();

app.ApplyPendingMigrations();

app.Run();
