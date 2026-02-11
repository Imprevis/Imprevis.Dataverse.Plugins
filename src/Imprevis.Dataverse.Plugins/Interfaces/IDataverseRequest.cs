namespace Imprevis.Dataverse.Plugins;

/// <summary>
/// Interface for a request to be executed against the Dataverse service.
/// </summary>
public interface IDataverseRequest
{
    /// <summary>
    /// The method to execute against the Dataverse service.
    /// </summary>
    void Execute(IDataverseService service, ILoggingService logger);
}

/// <summary>
/// Interface for a request to be executed against the Dataverse service that returns a response.
/// </summary>
/// <typeparam name="TResponse">Type of response the request returns.</typeparam>
public interface IDataverseRequest<TResponse>
{
    /// <summary>
    /// The method to execute against the Dataverse service.
    /// </summary>
    TResponse Execute(IDataverseService service, ILoggingService logger);
}
