namespace Imprevis.Dataverse.Plugins.Services
{
    using Microsoft.Xrm.Sdk;
    using System;
    using System.Linq;

    internal class RegistrationService : IRegistrationService
    {
        private readonly IPluginExecutionContext executionContext;

        public RegistrationService(IPluginExecutionContext executionContext)
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
            foreach (var attribute in attributes.Cast<RegistrationAttribute>())
            {
                var isValid = attribute.Mode == executionContext.Mode &&
                              attribute.Stage == executionContext.Stage &&
                              attribute.Message == executionContext.MessageName &&
                              attribute.EntityName == executionContext.PrimaryEntityName;

                if (isValid)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
