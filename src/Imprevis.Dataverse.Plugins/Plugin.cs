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
        private readonly Type pluginType;
        private readonly string unsecure;
        private readonly string secure;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Plugin()
        {
            this.pluginType = GetType();
        }

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

            services.AddSingleton(p => serviceProvider.Get<IOrganizationServiceFactory>());
            services.AddSingleton(p => serviceProvider.Get<IPluginExecutionContext>());
            services.AddSingleton(p => serviceProvider.Get<ILogger>());
            services.AddSingleton(p => serviceProvider.Get<ITracingService>());
            services.AddSingleton(p => serviceProvider.Get<IServiceEndpointNotificationService>());

            services.AddSingleton<ICacheService, MemoryCacheService>();
            services.AddSingleton<IDataverseServiceFactory, DataverseServiceFactory>();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<ILoggingService, LoggingService>();
            services.AddSingleton<IConfigurationService>(p => new ConfigurationService(unsecure, secure));
            services.AddSingleton<IRegistrationService, RegistrationService>();

            services.AddTransient(typeof(TRunner));

            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            try
            {
                var registrationService = provider.Get<IRegistrationService>();
                if (!registrationService.IsValid(pluginType))
                {
                    throw new InvalidPluginExecutionException($"Plugin '{pluginType.FullName}' is not registered correctly.");
                }

                var runner = provider.Get<TRunner>();
                runner.Execute();
            }
            catch (Exception ex)
            {
                var logger = provider.Get<ILoggingService>();
                logger.LogCritical(ex);

                if (ex is InvalidPluginExecutionException)
                {
                    throw;
                }

                throw new InvalidPluginExecutionException("Something went wrong.", ex);
            }
        }

        /// <summary>
        /// Configure additional services in the dependency injection container.
        /// </summary>
        public virtual void ConfigureServices(IServiceCollection services) { }
    }
}