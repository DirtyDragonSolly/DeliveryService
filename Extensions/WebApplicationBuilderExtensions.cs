using DeliveryService.Options;
using DeliveryService.Services.Implementations;
using DeliveryService.Services.Interfaces;
using Microsoft.OpenApi.Models;
using Serilog;

namespace DeliveryService.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder ConfigureDIContainer(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Logging.ClearProviders();
            applicationBuilder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
            
            applicationBuilder.Services.AddControllers();

            applicationBuilder.AddSettings();
            applicationBuilder.AddSwaggerConfiguration();
            applicationBuilder.AddLogic();

            return applicationBuilder;
        }

        private static WebApplicationBuilder AddSettings(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.AddSettings<DeliverySettings>(nameof(DeliverySettings));

            return applicationBuilder;
        }

        private static WebApplicationBuilder AddSettings<TService>(this WebApplicationBuilder applicationBuilder, string sectionName = null, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TService : class
        {
            applicationBuilder.AddSettings<TService, TService>(sectionName, serviceLifetime);

            return applicationBuilder;
        }

        private static WebApplicationBuilder AddSettings<TService, TServiceImplementation>(this WebApplicationBuilder applicationBuilder, string sectionName = null, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TService : class
            where TServiceImplementation : TService
        {
            applicationBuilder.Services.Add(new ServiceDescriptor(
                typeof(TService),
                _ => applicationBuilder.Configuration.GetSection(sectionName ?? typeof(TServiceImplementation).Name).Get<TServiceImplementation>(),
                serviceLifetime));

            return applicationBuilder;
        }

        private static WebApplicationBuilder AddLogic(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddScoped<IOrderService, OrderService>();

            return applicationBuilder;
        }

        private static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Delivery.Service.Api"
                });
            });

            return applicationBuilder;
        }
    }
}
