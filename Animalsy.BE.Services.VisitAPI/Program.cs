using Animalsy.BE.Services.VisitAPI.Configuration;
using Animalsy.BE.Services.VisitAPI.Data;
using Animalsy.BE.Services.VisitAPI.Middleware;
using Animalsy.BE.Services.VisitAPI.Repository;
using Animalsy.BE.Services.VisitAPI.Repository.Builder.Factory;
using Animalsy.BE.Services.VisitAPI.Repository.ResponseHandler;
using Animalsy.BE.Services.VisitAPI.Services;
using Animalsy.BE.Services.VisitAPI.Utilities;
using Animalsy.BE.Services.VisitAPI.Validators.Factory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure different appsettings files based on working environment

builder.Configuration.AddEnvironmentVariables();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

if (builder.Environment.IsDevelopment())
{
    var devenv = Environment.GetEnvironmentVariable("CONFIG_ENV");
    builder.Configuration.AddJsonFile($"appsettings.Development.{devenv}.json", optional: true, reloadOnChange: true);
}

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(MappingConfiguration.RegisterMaps().CreateMapper());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IVisitResponseBuilderFactory, VisitResponseBuilderFactory>();
builder.Services.AddTransient<IApiService, ApiService>();
builder.Services.AddTransient<IResponseHandler, ResponseHandler>();
builder.Services.AddScoped<IValidatorFactory, ValidatorFactory>();
builder.Services.AddValidators();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<ServiceUrlConfiguration>(builder.Configuration.GetSection(nameof(ServiceUrlConfiguration))!);
builder.Services.AddHttpClients();
builder.Services.AddHttpContextAccessor();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.ApplyPendingMigrations();

app.Run();
