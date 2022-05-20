using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Trackem.ERT.Core.ServiceContracts;
using Trackem.ERT.Core.Services;
using Trackem.ERT.Infra.Contracts;
using Trackem.ERT.Infra.EFCore;
using Trackem.ERT.Infra.Repositories;
using Trackem.ERT.Web.Apis.Controllers;

namespace Trackem.ERT.Web.Apis.Extensions;
public static class ServiceExtensions
{

    /// <summary>
    /// Configure CORS
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {

        });


    /// <summary>
    /// SQL Connection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContextPool<RepositoryContext>(optionsAction =>
        {
            SqlAuthenticationProvider.SetProvider(
                SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow,
                new AzureSqlAuthenticationProvider()
            );

            string connectionString = configuration.GetConnectionString("sqlConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                // Fallback to obsolete/deprecated connection string setting
                connectionString = configuration.GetConnectionString("sqlConnection");
            }

            optionsAction.UseSqlServer(
                connectionString,
                sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.CommandTimeout(180);
                    sqlServerOptionsAction.EnableRetryOnFailure(2);
                }
            );

            //if (_env.IsDevelopment())
            //{
            optionsAction.EnableSensitiveDataLogging();
            optionsAction.EnableDetailedErrors();
            optionsAction.LogTo(Console.WriteLine);
            //}
        });


    }

    /// <summary>
    /// ConfigureRepositoryManager
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();

    /// <summary>
    /// ConfigureServiceManager
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();

    /// <summary>
    /// AddCustomMediaTypes
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    public static void AddCustomMediaTypes(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(config =>
        {
            var newtonsoftJsonOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
            if (newtonsoftJsonOutputFormatter != null)
            {
                newtonsoftJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.trackem.hateoas+json");
            }
            var xmlOutputFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();
            if (xmlOutputFormatter != null)
            {
                xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.trackem.hateoas+xml");
            }
        });
    }

    #region Versioning

    // <summary>
    /// ConfigureVersioning
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //opt.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            opt.Conventions.Controller<MaterialController>().HasApiVersion(new ApiVersion(1, 0));
            opt.Conventions.Controller<MaterialV2Controller>().HasDeprecatedApiVersion(new ApiVersion(2, 0));
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Trackem ERT API",
                Version = "v1",
                Description = "ERT API Developed by Trackem",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Alok Saxena",
                    Email = "alok.saxena@trackem.com.au",
                    Url = new Uri("https://trackem.com/alok"),
                },
                License = new OpenApiLicense
                {
                    Name = "Trackem LICX",
                    Url = new Uri("https://example.com/license"),
                }

            });
            s.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "Trackem ERT API",
                Version = "v2"
            });
            var xmlFile = $"{typeof(Trackem.ERT.Core.DataModels.AssemblyReference).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            s.IncludeXmlComments(xmlPath);

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            s.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                { new OpenApiSecurityScheme
                { Reference = new OpenApiReference
                { Type = ReferenceType.SecurityScheme,Id = "Bearer"
                },
                    Name = "Bearer",
                },
                    new List<string>()
                }
            });

        });
    }

    #endregion

    #region cache
    public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();

    #endregion
}