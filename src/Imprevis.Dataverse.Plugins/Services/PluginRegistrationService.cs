namespace Imprevis.Dataverse.Plugins.Services;

using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

internal class PluginRegistrationService(IPluginExecutionContext executionContext) : IPluginRegistrationService
{
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
            var isValid = true;

            isValid &= attribute.Mode == executionContext.Mode;
            isValid &= attribute.Stage == executionContext.Stage;
            isValid &= attribute.Message == executionContext.MessageName;
            isValid &= attribute.EntityName == null || attribute.EntityName == executionContext.PrimaryEntityName;

            if (isValid)
            {
                return true;
            }
        }

        // Didn't match any registrations, hence it failed validation.
        return false;
    }
}
