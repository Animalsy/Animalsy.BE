using Animalsy.BE.Services.VisitAPI.Configuration;
using Animalsy.BE.Services.VisitAPI.Data;
using Animalsy.BE.Services.VisitAPI.Repository;
using Animalsy.BE.Services.VisitAPI.Services;
using Animalsy.BE.Services.VisitAPI.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(MappingConfiguration.RegisterMaps().CreateMapper());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.Configure<ServiceUrlConfiguration>(builder.Configuration.GetSection("ServiceUrlConfiguration")!);

builder.Services.AddHttpClients(); //Todo: correct DI
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
