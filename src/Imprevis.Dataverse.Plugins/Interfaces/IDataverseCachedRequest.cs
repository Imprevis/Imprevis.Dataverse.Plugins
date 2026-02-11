namespace Imprevis.Dataverse.Plugins;

using System;

/// <summary>
/// Interface for a request to be executed against the Dataverse service that returns a response.
/// </summary>
/// <typeparam name="TResponse">Type of response the request returns.</typeparam>
public interface IDataverseCachedRequest<TResponse> : IDataverseRequest<TResponse>
{
    /// <summary>
    /// Cache key name.
    /// </summary>
    string CacheKey { get; }

    /// <summary>
    /// Duration to cache the response.
    /// </summary>
    TimeSpan CacheDuration { get; }
}
