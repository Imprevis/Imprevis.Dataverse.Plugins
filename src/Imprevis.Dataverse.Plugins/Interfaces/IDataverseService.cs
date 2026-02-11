namespace Imprevis.Dataverse.Plugins;

using Microsoft.Xrm.Sdk;

/// <summary>
/// Interface for a service that provides access to the Dataverse service.
/// </summary>
public interface IDataverseService : IOrganizationService
{
    /// <summary>
    /// Execute a request against the Dataverse service.
    /// </summary>
    /// <param name="request">Request to execute.</param>
    void Execute(IDataverseRequest request);

    /// <summary>
    /// Execute a request against the Dataverse service and return a response.
    /// </summary>
    /// <typeparam name="TResponse">Type of the response.</typeparam>
    /// <param name="request">Request to execute.</param>
    /// <returns>Response object returned by the request.</returns>
    TResponse Execute<TResponse>(IDataverseRequest<TResponse> request);

    /// <summary>
    /// Execute a cached request against the Dataverse service and return a response, using the cache if the response is still valid.
    /// </summary>
    /// <typeparam name="TResponse">Type of the response.</typeparam>
    /// <param name="request">Request to execute.</param>
    /// <returns>Response object returned by the request.</returns>
    TResponse? ExecuteCached<TResponse>(IDataverseCachedRequest<TResponse> request);
}
