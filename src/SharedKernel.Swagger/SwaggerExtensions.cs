using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Microsoft.AspNetCore.Builder
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IHostEnvironment env, string version = "v1")
        {
            if (!env.IsDevelopment())
                return services;

            return services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(opt =>
                {
                    opt.CustomSchemaIds(type => type.ToString());

                    var info = new OpenApiInfo
                    {
                        Title = AppDomain.CurrentDomain.FriendlyName,
                        Version = version
                    };

                    opt.SwaggerDoc(info.Version, info);

                    var scheme = new OpenApiSecurityScheme
                    {
                        Description = "Enter Access Token **_(without \"Bearer\" word)_**",
                        Type = SecuritySchemeType.Http,
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT",
                        Reference = new OpenApiReference
                        {
                            Id = "* AccessToken",
                            Type = ReferenceType.SecurityScheme
                        }
                    };

                    opt.AddSecurityDefinition(scheme.Reference.Id, scheme);
                    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { scheme, new string[0] }
                    });
                });
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IHostEnvironment env)
        {
            if (!env.IsDevelopment())
                return app;

            return app
                .UseSwagger()
                .UseSwaggerUI(opt =>
                {
                    opt.DocumentTitle = AppDomain.CurrentDomain.FriendlyName;
                    opt.DocExpansion(DocExpansion.None);
                });
        }
    }
}