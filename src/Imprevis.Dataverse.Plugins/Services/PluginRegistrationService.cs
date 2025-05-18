namespace Imprevis.Dataverse.Plugins.Services
{
    using Microsoft.Xrm.Sdk;
    using System;
    using System.Text.Json;
    using System.Threading;

    public class PluginRegistrationService : IPluginRegistrationService
    {
        private readonly IPluginExecutionContext executionContext;

        public PluginRegistrationService(IPluginExecutionContext executionContext)
        {
            this.executionContext = executionContext;
        }

        public bool IsValid(Type pluginType)
        {
            var attributes = pluginType.GetCustomAttributes(typeof(RegistrationAttribute), true);

            // Don't force user to use Registrations. If there are none, consider it valid.
            if (attributes.Length == 0)
            {
                return true;
            }

            // Check if a Registration matches the current execution context.
            foreach (var attribute in attributes)
            {
                var isValid = (attribute as RegistrationAttribute).IsValid(executionContext);
                if (isValid)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
