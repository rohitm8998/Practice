using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using System.Text.RegularExpressions;
using Trackem.ERT.Api.Filters;
using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Core.ServiceContracts;
using Trackem.ERT.Infra.Contracts;
using Trackem.ERT.Infra.Repositories.DataShaping;
using Trackem.ERT.Web.Apis;
using Trackem.ERT.Web.Apis.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ValidateMediaTypeAttribute>();

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddScoped<IClaimLink, ClaimLink>(); // Hateoss
builder.Services.AddScoped<IMaterialLink, MaterialLink>(); // Hateoss
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureRepositoryManager();
builder.Services.AddScoped<IDataShaper<ClaimViewModel>, DataShaper<ClaimViewModel>>();
builder.Services.AddScoped<IDataShaper<MaterialGridDetailResponse>, DataShaper<MaterialGridDetailResponse>>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureVersioning();
//builder.Services.ConfigureResponseCaching();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwagger();
//builder.Services.AddSwaggerGen(setupAction =>
//{
//    //setupAction.CustomSchemaIds(type => Regex.Replace(type.ToString(), @"^Trackem\.Tnt\.WebApi\.Models\.", ""));

//    setupAction.SwaggerDoc("trackem-ert", new OpenApiInfo { Title = "trackem-ert", Version = "v1" });

//    setupAction.DescribeAllParametersInCamelCase();
//    setupAction.EnableAnnotations();
//    setupAction.IgnoreObsoleteProperties();

//    //setupAction.AddSecurityDefinition("Bearer", new OpenApiSecuritySchemes
//    //{
//    //    Name = "Authorization",
//    //    In = ParameterLocation.Header,
//    //    Type = SecuritySchemeType.Http,
//    //    Scheme = "Bearer"
//    //});
//    //setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
//    //            {
//    //                {
//    //                    new OpenApiSecurityScheme
//    //                    {
//    //                        Reference = new OpenApiReference
//    //                        {
//    //                            Id   = "Bearer",
//    //                            Type = ReferenceType.SecurityScheme
//    //                        }
//    //                    },
//    //                    Array.Empty<string>()
//    //                }
//    //            });
//});

#region Swagger will confgure in depth during AZB2C
//builder.Services.AddSwaggerGen(setupAction =>
//{
//    //setupAction.CustomSchemaIds(type => Regex.Replace(type.ToString(), @"^Trackem\.Tnt\.WebApi\.Models\.", ""));
//    setupAction.CustomSchemaIds(type => Regex.Replace(type.ToString(), @"^ER\.ERT\.Core\.DataModels\.", ""));

//    setupAction.SwaggerDoc("ERT", new OpenApiInfo { Title = "Trackem-ERT", Version = "v1" });

//    setupAction.DescribeAllParametersInCamelCase();
//    setupAction.EnableAnnotations();
//    setupAction.IgnoreObsoleteProperties();

//    setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer"
//    });
//    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
//                {
//                    {
//                        new OpenApiSecurityScheme
//                        {
//                            Reference = new OpenApiReference
//                            {
//                                Id   = "Bearer",
//                                Type = ReferenceType.SecurityScheme
//                            }
//                        },
//                        Array.Empty<string>()
//                    }
//                });
//});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        //options.SwaggerEndpoint("/swagger/trackem-ert/swagger.json", "trackem-ert");
        //options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Trackem ERT API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Trackem ERT API v2");
    });
}


if (app.Environment.IsProduction())
    app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");
//app.UseResponseCaching();
//app.UseHttpCacheHeaders();
app.UseAuthorization();

app.MapControllers();

app.Run();
