using Animalsy.BE.Services.CustomerAPI.Configuration;
using Animalsy.BE.Services.CustomerAPI.Data;
using Animalsy.BE.Services.CustomerAPI.Middleware;
using Animalsy.BE.Services.CustomerAPI.Repository;
using Animalsy.BE.Services.CustomerAPI.Repository.Builder.Factory;
using Animalsy.BE.Services.CustomerAPI.Repository.ResponseHandler;
using Animalsy.BE.Services.CustomerAPI.Services;
using Animalsy.BE.Services.CustomerAPI.Utilities;
using Animalsy.BE.Services.CustomerAPI.Validators.Factory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(MappingConfiguration.RegisterMaps().CreateMapper());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerProfileBuilderFactory, CustomerProfileBuilderFactory>();
builder.Services.AddTransient<IApiService, ApiService>();
builder.Services.AddTransient<IResponseHandler, ResponseHandler>();
builder.Services.AddScoped<IValidatorFactory, ValidatorFactory>();
builder.Services.AddValidators();

builder.Services.Configure<ServiceUrlConfiguration>(builder.Configuration.GetSection(nameof(ServiceUrlConfiguration))!);
builder.Services.AddHttpClients();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging(cfg =>
{
    cfg.AddConsole(opt =>
        opt.LogToStandardErrorThreshold = LogLevel.Error);
});

builder.Services.AddSwaggerGenWithAuthentication();
builder.AddAppAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.ApplyPendingMigrations();

app.Run();
