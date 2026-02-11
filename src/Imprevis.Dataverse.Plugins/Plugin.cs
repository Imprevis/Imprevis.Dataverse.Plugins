namespace Imprevis.Dataverse.Plugins;

using System;
using Imprevis.Dataverse.Plugins.Extensions;
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
        services.AddSingleton<IPluginConfigService>(p => new PluginConfigService(unsecure, secure));
        services.AddSingleton<IPluginRegistrationService, PluginRegistrationService>();

        services.AddTransient(typeof(TRunner));

        ConfigureServices(services);

        var provider = services.BuildServiceProvider();

        try
        {
            var registrationService = provider.Get<IPluginRegistrationService>();
            if (!registrationService.IsValid(pluginType))
            {
                throw new InvalidPluginExecutionException($"Plugin '{pluginType.FullName}' is not registered correctly.");
            }

            ExecuteInternal(provider);
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
    /// Execute the runner logic.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    protected virtual void ExecuteInternal(IServiceProvider provider)
    {
        var runner = provider.Get<TRunner>();
        runner.Execute();
    }

    /// <summary>
    /// Configure additional services in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public virtual void ConfigureServices(IServiceCollection services) { }
}

/// <summary>
/// Base class for actions.
/// </summary>
/// <typeparam name="TRunner">Type of the action runner.</typeparam>
/// <typeparam name="TRequest">Type of the request object.</typeparam>
public abstract class Plugin<TRunner, TRequest> : Plugin<TRunner> where TRunner : IPluginRunner<TRequest> where TRequest : OrganizationRequest
{
    /// <inheritdoc/>
    public Plugin() : base() { }

    /// <inheritdoc/>
    public Plugin(string unsecure) : base(unsecure) { }

    /// <inheritdoc/>
    public Plugin(string unsecure, string secure) : base(unsecure, secure) { }

    /// <inheritdoc/>
    protected override void ExecuteInternal(IServiceProvider provider)
    {
        var executionContext = provider.Get<IPluginExecutionContext>();

        var request = executionContext.GetRequest<TRequest>();

        var runner = provider.Get<TRunner>();
        runner.Execute(request);
    }
}

/// <summary>
/// Base class for custom actions with response.
/// </summary>
/// <typeparam name="TRunner">Type of the action runner.</typeparam>
/// <typeparam name="TRequest">Type of the request object.</typeparam>
/// <typeparam name="TResponse">Type of the response object.</typeparam>
public abstract class Plugin<TRunner, TRequest, TResponse> : Plugin<TRunner> where TRunner : IPluginRunner<TRequest, TResponse> where TRequest : OrganizationRequest where TResponse : OrganizationResponse
{
    /// <inheritdoc/>
    public Plugin() : base() { }

    /// <inheritdoc/>
    public Plugin(string unsecure) : base(unsecure) { }

    /// <inheritdoc/>
    public Plugin(string unsecure, string secure) : base(unsecure, secure) { }

    /// <inheritdoc/>
    protected override void ExecuteInternal(IServiceProvider provider)
    {
        var executionContext = provider.Get<IPluginExecutionContext>();

        var request = executionContext.GetRequest<TRequest>();

        var runner = provider.Get<TRunner>();
        var response = runner.Execute(request);

        executionContext.SetResponse(response);
    }
}