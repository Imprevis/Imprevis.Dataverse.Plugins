namespace Imprevis.Dataverse.Plugins
{
    using System.Collections.Generic;
    using System.IO;

    public interface IHttpService
    {
        TResponse Send<TResponse>(string method, string url, Stream body = null, Dictionary<string, string> headers = null);
        TResponse Send<TRequest, TResponse>(string method, string url, TRequest body, Dictionary<string, string> headers = null);
    }
}
