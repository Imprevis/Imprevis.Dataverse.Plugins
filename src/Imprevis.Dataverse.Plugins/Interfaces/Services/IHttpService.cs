namespace Imprevis.Dataverse.Plugins
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Interface containing methods for interacting with an HTTP service.
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Send a request to an HTTP service and return the response.
        /// </summary>
        TResponse Send<TResponse>(string method, string url, Stream body = null, Dictionary<string, string> headers = null, SerializationFormat format = SerializationFormat.Json);

        /// <summary>
        /// Send a request to an HTTP service and return the response.
        /// </summary>
        TResponse Send<TRequest, TResponse>(string method, string url, TRequest body, Dictionary<string, string> headers = null, SerializationFormat format = SerializationFormat.Json);
    }
}
