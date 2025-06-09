namespace Imprevis.Dataverse.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text.Json;
    using System.Xml.Serialization;

    internal class HttpService : IHttpService
    {
        public TResponse Send<TResponse>(string method, string url, Stream body = null, Dictionary<string, string> headers = null, SerializationFormat format = SerializationFormat.Json)
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
                switch (format)
                {
                    case SerializationFormat.Json:
                        return JsonSerializer.Deserialize<TResponse>(stream);
                    case SerializationFormat.Xml:
                        var serializer = new XmlSerializer(typeof(TResponse));
                        return (TResponse)serializer.Deserialize(stream);
                    default:
                        throw new NotSupportedException("Invalid serialization format.");
                }
            }
        }

        public TResponse Send<TRequest, TResponse>(string method, string url, TRequest body, Dictionary<string, string> headers = null, SerializationFormat format = SerializationFormat.Json)
        {
            if (headers == null)
            {
                headers = new Dictionary<string, string>();
            }

            using (var stream = new MemoryStream())
            {
                if (format == SerializationFormat.Json)
                {
                    headers.Add("Content-Type", "application/json");

                    JsonSerializer.Serialize(stream, body);
                }
                else if (format == SerializationFormat.Xml)
                {
                    headers.Add("Content-Type", "application/xml");

                    var serializer = new XmlSerializer(typeof(TResponse));
                    serializer.Serialize(stream, body);
                }

                return Send<TResponse>(method, url, stream, headers);
            }
        }
    }
}
