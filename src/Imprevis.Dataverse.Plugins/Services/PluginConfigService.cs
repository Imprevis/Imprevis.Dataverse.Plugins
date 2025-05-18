namespace Imprevis.Dataverse.Plugins.Services
{
    using Microsoft.Xrm.Sdk;
    using System.Text.Json;

    public class PluginConfigService : IPluginConfigService
    {
        private readonly string unsecure;
        private readonly string secure;

        public PluginConfigService(string unsecure, string secure)
        {
            this.unsecure = unsecure;
            this.secure = secure;
        }

        public TUnsecure GetUnsecure<TUnsecure>()
        {
            if (string.IsNullOrEmpty(unsecure))
            {
                throw new InvalidPluginExecutionException("Unsecure plugin configuration is missing.");
            }

            return JsonSerializer.Deserialize<TUnsecure>(unsecure);
        }

        public TSecure GetSecure<TSecure>()
        {
            if (string.IsNullOrEmpty(secure))
            {
                throw new InvalidPluginExecutionException("Secure plugin configuration is missing.");
            }

            return JsonSerializer.Deserialize<TSecure>(secure);
        }
    }
}
