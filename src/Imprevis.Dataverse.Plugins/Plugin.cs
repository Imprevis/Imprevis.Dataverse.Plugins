namespace Imprevis.Dataverse.Plugins
{
    using System;
    using System.Text.Json;
    using Imprevis.Dataverse.Plugins.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Extensions;
    using Microsoft.Xrm.Sdk.PluginTelemetry;

    public abstract class Plugin<TRunner> : IPlugin where TRunner : IPluginRunner
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var services = new ServiceCollection();

            services.AddSingleton(p => serviceProvider.Get<IOrganizationServiceFactory>());
            services.AddSingleton(p => serviceProvider.Get<IPluginExecutionContext>());
            services.AddSingleton(p => serviceProvider.Get<ILogger>());
            services.AddSingleton(p => serviceProvider.Get<ITracingService>());
            services.AddSingleton(p => serviceProvider.Get<IServiceEndpointNotificationService>());

            services.AddSingleton<IDataverseServiceFactory, DataverseServiceFactory>();
            services.AddSingleton<ILoggingService, LoggingService>();

            services.AddSingleton(typeof(TRunner));

            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            var logger = provider.Get<ILoggingService>();

            try
            {
                var executionContext = provider.Get<IPluginExecutionContext>();

                if (!HasValidRegistration(executionContext))
                {
                    throw new InvalidPluginExecutionException($"Plugin '{this.GetType().FullName}' is not registered correctly.");
                }

                logger.LogDebug("Starting execution.");

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

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDateTimeService, CurrentDateTimeService>();
            services.AddSingleton<IHttpService, HttpService>();
        }

        private bool HasValidRegistration(IPluginExecutionContext executionContext)
        {
            var type = GetType();

            var attributes = type.GetCustomAttributes(typeof(RegistrationAttribute), true);

            // Don't force user to use Registrations. If there are none, consider it valid.
            if (attributes.Length == 0)
            {
                return true;
            }

            // Check if a Registration matches the current execution context.
            foreach (var attribute in attributes)
            {
                var isValid = (attribute as RegistrationAttribute).IsValid(executionContext);
                if (isValid)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public abstract class Plugin<TRunner, TUnsecure> : Plugin<TRunner> where TRunner : IPluginRunner
    {
        public TUnsecure UnsecureConfig { get; private set; }

        public Plugin(string unsecure) : base()
        {
            UnsecureConfig = JsonSerializer.Deserialize<TUnsecure>(unsecure);
        }
    }

    public abstract class Plugin<TRunner, TUnsecure, TSecure> : Plugin<TRunner, TUnsecure> where TRunner : IPluginRunner
    {
        public TSecure SecureConfig { get; private set; }

        public Plugin(string unsecure, string secure) : base(unsecure)
        {
            SecureConfig = JsonSerializer.Deserialize<TSecure>(secure);
        }
    }
}