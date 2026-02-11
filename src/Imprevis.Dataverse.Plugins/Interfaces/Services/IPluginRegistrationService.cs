namespace Imprevis.Dataverse.Plugins;

using System;

internal interface IPluginRegistrationService
{
    bool IsValid(Type type);
}
