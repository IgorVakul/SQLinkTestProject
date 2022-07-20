#region using

using AutoMapper;
using DAL.Repositories.Classes;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services.AutoMapperProfile;
using Services.Interfaces;
using Services.Services;
using WebAPI.Middlewares;
using WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;

#endregion using

#region builder



var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseStorage, CourseStorage>();

//add automapper
builder.Services.AddScoped(serviceProvider =>
{
    var domainProfile = ActivatorUtilities.GetServiceOrCreateInstance<AutoMapperProfile>(serviceProvider);

    var config = new MapperConfiguration(config => config
        .AddProfile(domainProfile)
    );

    return config.CreateMapper();
});

IServiceCollection serviceCollection = builder.Services.AddDbContext<SqLinkTestDBContext>(opt =>
{
    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"), x => x.UseNetTopologySuite());

    if (environment.IsDevelopment())
    {
        opt.EnableSensitiveDataLogging(true);
    }
});

builder.Services.Configure<IPWhiteListOptions>(builder.Configuration.GetSection("IPWhitelistOptions"));

builder.Services.AddScoped(container =>
{
    var loggerFactory = container.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger<ClientIpCheckActionFilter>();    
    return new ClientIpCheckActionFilter(configuration["IPWhiteList"], logger);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "SQLink API",
        Description = "SQLink API"
    });

    // This filter removed the version parameter from the list of parameters for each operation
    c.OperationFilter<RemoveVersionFromParameter>();

    // This filter uses the group name (v1) in place of the version segment in the generated route
    c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
});

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddMemoryCache();

#endregion builder

#region app

var app =  builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseApiVersioning();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("Content-Disposition"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCustomExceptionsMiddleware();
//app.UseIPWhiteListMiddleware();
//app.UseCheckApiKeyMiddleware();

app.Run();

#endregion app
