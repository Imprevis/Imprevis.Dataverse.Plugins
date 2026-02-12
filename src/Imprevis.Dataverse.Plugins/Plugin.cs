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
public abstract class Plugin(string? unsecure = null, string? secure = null) : IPlugin
{
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

        ConfigureServices(services);

        var provider = services.BuildServiceProvider();

        try
        {
            var pluginType = GetType();

            var registrationService = provider.Get<IPluginRegistrationService>();
            if (!registrationService.IsValid(pluginType))
            {
                throw new InvalidPluginExecutionException($"Plugin '{pluginType.FullName}' is not registered correctly.");
            }

            OnExecute(provider);
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
    /// <param name="services">The service collection.</param>
    public virtual void ConfigureServices(IServiceCollection services) { }

    /// <summary>
    /// Execute the runner logic.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    public virtual void OnExecute(IServiceProvider provider) { }
}

/// <summary>
/// Base class for plugins.
/// </summary>
/// <typeparam name="TRunner">Type of the Plugin Runner.</typeparam>
public abstract class Plugin<TRunner>(string? unsecure = null, string? secure = null) : Plugin(unsecure, secure) where TRunner : IPluginRunner
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient(typeof(TRunner));
    }

    /// <inheritdoc/>
    public override void OnExecute(IServiceProvider provider)
    {
        var runner = provider.Get<TRunner>();
        runner.Execute();
    }
}

/// <summary>
/// Base class for actions.
/// </summary>
/// <typeparam name="TRunner">Type of the action runner.</typeparam>
/// <typeparam name="TRequest">Type of the request object.</typeparam>
public abstract class Plugin<TRunner, TRequest>(string? unsecure = null, string? secure = null) : Plugin(unsecure, secure) where TRunner : IPluginRunner<TRequest> where TRequest : OrganizationRequest, new()
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient(typeof(TRunner));
    }

    /// <inheritdoc/>
    public override void OnExecute(IServiceProvider provider)
    {
        var executionContext = provider.Get<IPluginExecutionContext>();

        var request = executionContext.GetRequest<TRequest>();
        if (request == null)
        {
            throw new InvalidPluginExecutionException("Invalid request.");
        }

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
public abstract class Plugin<TRunner, TRequest, TResponse>(string? unsecure = null, string? secure = null) : Plugin(unsecure, secure) where TRunner : IPluginRunner<TRequest, TResponse> where TRequest : OrganizationRequest, new() where TResponse : OrganizationResponse
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient(typeof(TRunner));
    }

    /// <inheritdoc/>
    public override void OnExecute(IServiceProvider provider)
    {
        var executionContext = provider.Get<IPluginExecutionContext>();

        var request = executionContext.GetRequest<TRequest>();
        if (request == null)
        {
            throw new InvalidPluginExecutionException("Invalid request.");
        }

        var runner = provider.Get<TRunner>();
        var response = runner.Execute(request);

        executionContext.SetResponse(response);
    }
}