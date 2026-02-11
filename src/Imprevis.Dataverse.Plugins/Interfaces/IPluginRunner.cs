namespace Imprevis.Dataverse.Plugins;

using Microsoft.Xrm.Sdk;

/// <summary>
/// Interface for a plugin runner.
/// </summary>
public interface IPluginRunner
{
    /// <summary>
    /// This method is called by the plugin to execute the plugin logic.
    /// </summary>
    void Execute();
}


/// <summary>
/// Interface for a plugin runner with request and response types.
/// </summary>
/// <typeparam name="TRequest">Type of the request object.</typeparam>
public interface IPluginRunner<in TRequest> : IPluginRunner where TRequest : OrganizationRequest
{
    /// <summary>
    /// This method is called by the plugin to execute the plugin logic.
    /// </summary>
    /// <param name="request">The request object.</param>
    void Execute(TRequest request);
}

/// <summary>
/// Interface for a plugin runner with request and response types.
/// </summary>
/// <typeparam name="TRequest">Type of the request object.</typeparam>
/// <typeparam name="TResponse">Type of the response object.</typeparam>
public interface IPluginRunner<in TRequest, out TResponse> : IPluginRunner where TRequest : OrganizationRequest where TResponse : OrganizationResponse
{
    /// <summary>
    /// This method is called by the plugin to execute the plugin logic.
    /// </summary>
    /// <param name="request">The request object.</param>
    /// <returns>The response object.</returns>
    TResponse Execute(TRequest request);
}