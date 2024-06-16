using Animalsy.BE.Services.VendorAPI.Configuration;
using Animalsy.BE.Services.VendorAPI.Data;
using Animalsy.BE.Services.VendorAPI.Middleware;
using Animalsy.BE.Services.VendorAPI.Repository;
using Animalsy.BE.Services.VendorAPI.Repository.Builder.Factory;
using Animalsy.BE.Services.VendorAPI.Repository.ResponseHandler;
using Animalsy.BE.Services.VendorAPI.Services;
using Animalsy.BE.Services.VendorAPI.Utilities;
using Animalsy.BE.Services.VendorAPI.Validators.Factory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(MappingConfiguration.RegisterMaps().CreateMapper());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IVendorProfileBuilderFactory, VendorProfileBuilderFactory>();
builder.Services.AddTransient<IApiService, ApiService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IResponseHandler, ResponseHandler>();
builder.Services.AddScoped<IValidatorFactory, ValidatorFactory>();
builder.Services.AddValidators();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
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
