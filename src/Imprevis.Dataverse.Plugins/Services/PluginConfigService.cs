namespace Imprevis.Dataverse.Plugins.Services
{
    using Microsoft.Xrm.Sdk;
    using System;
    using System.IO;
    using System.Text.Json;
    using System.Xml.Serialization;

    internal class PluginConfigService : IPluginConfigService
    {
        private readonly string unsecure;
        private readonly string secure;

        public PluginConfigService(string unsecure, string secure)
        {
            this.unsecure = unsecure;
            this.secure = secure;
        }

        public TUnsecure GetUnsecure<TUnsecure>(SerializationFormat type = SerializationFormat.Json)
        {
            if (string.IsNullOrEmpty(unsecure))
            {
                throw new InvalidPluginExecutionException("Unsecure plugin configuration is missing.");
            }

            return Deserialize<TUnsecure>(unsecure, type);
        }

        public TSecure GetSecure<TSecure>(SerializationFormat type = SerializationFormat.Json)
        {
            if (string.IsNullOrEmpty(secure))
            {
                throw new InvalidPluginExecutionException("Secure plugin configuration is missing.");
            }

            return Deserialize<TSecure>(secure, type);
        }

        private TObject Deserialize<TObject>(string value, SerializationFormat type)
        {
            switch (type)
            {
                case SerializationFormat.Json:
                    return JsonSerializer.Deserialize<TObject>(value);
                case SerializationFormat.Xml:
                    var serializer = new XmlSerializer(typeof(TObject));
                    using (var reader = new StringReader(value))
                    {
                        return (TObject)serializer.Deserialize(reader);
                    }
                default:
                    throw new NotSupportedException("Invalid serialization format.");
            }
        }
    }
}
