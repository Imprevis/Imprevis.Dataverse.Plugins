namespace Imprevis.Dataverse.Plugins
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text.Json;

    public class HttpService : IHttpService
    {
        public TResponse Send<TResponse>(string method, string url, Stream body = null, Dictionary<string, string> headers = null)
        {
            var request = WebRequest.CreateHttp(url);
            request.Method = method;
            request.Timeout = 60000;
            request.KeepAlive = false;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (body != null)
            {
                using (var stream = request.GetRequestStream())
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(body);
                }
            }

            var response = request.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                return JsonSerializer.Deserialize<TResponse>(stream);
            }
        }

        public TResponse Send<TRequest, TResponse>(string method, string url, TRequest body, Dictionary<string, string> headers = null)
        {
            using (var stream = new MemoryStream())
            {
                JsonSerializer.Serialize(stream, body);

                return Send<TResponse>(method, url, stream, headers);
            }
        }
    }
}
