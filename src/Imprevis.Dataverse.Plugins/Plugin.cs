namespace Imprevis.Dataverse.Plugins
{
    using System;
    using Imprevis.Dataverse.Plugins.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Extensions;
    using Microsoft.Xrm.Sdk.PluginTelemetry;

    /// <summary>
    /// Base class for plugins.
    /// </summary>
    /// <typeparam name="TRunner">Type of the Plugin Runner.</typeparam>
    public abstract class Plugin<TRunner> : IPlugin where TRunner : IPluginRunner
    {
        private readonly string unsecure;
        private readonly string secure;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Plugin() { }

        /// <summary>
        /// Constructor with unsecure configuration.
        /// </summary>
        public Plugin(string unsecure) : this()
        {
            this.unsecure = unsecure;
        }

        /// <summary>
        /// Constructor with unsecure and secure configuration.
        /// </summary>
        public Plugin(string unsecure, string secure) : this(unsecure)
        {
            this.secure = secure;
        }

        /// <inheritdoc/>
        public void Execute(IServiceProvider serviceProvider)
        {
            var services = new ServiceCollection();

            services.AddScoped(p => serviceProvider.Get<IOrganizationServiceFactory>());
            services.AddScoped(p => serviceProvider.Get<IPluginExecutionContext>());
            services.AddScoped(p => serviceProvider.Get<ILogger>());
            services.AddScoped(p => serviceProvider.Get<ITracingService>());
            services.AddScoped(p => serviceProvider.Get<IServiceEndpointNotificationService>());

            services.AddScoped<ICacheService, MemoryCacheService>();
            services.AddScoped<IDataverseServiceFactory, DataverseServiceFactory>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ILoggingService, LoggingService>();
            services.AddScoped<IConfigurationService>(p => new ConfigurationService(unsecure, secure));
            services.AddScoped<IPluginRegistrationService, PluginRegistrationService>();

            services.AddTransient(typeof(TRunner));

            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            var logger = provider.Get<ILoggingService>();

            try
            {
                logger.LogDebug("Starting execution.");

                var pluginType = GetType();
                
                var registrationService = provider.Get<IPluginRegistrationService>();
                if (!registrationService.IsValid(pluginType))
                {
                    throw new InvalidPluginExecutionException($"Plugin '{pluginType.FullName}' is not registered correctly.");
                }

                var runner = provider.GetService<TRunner>();
                runner.Execute();
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex);

                if (ex is InvalidPluginExecutionException)
                {
                    throw;
                }

                throw new InvalidPluginExecutionException("Something went wrong.", ex);
            }
            finally
            {
                logger.LogDebug("Finishing execution.");
            }
        }

        /// <summary>
        /// Configure additional services in the dependency injection container.
        /// </summary>
        public virtual void ConfigureServices(IServiceCollection services) { }
    }
}