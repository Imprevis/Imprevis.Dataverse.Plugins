namespace Imprevis.Dataverse.Plugins
{
    using System;

    public interface IPluginRegistrationService
    {
        bool IsValid(Type type);
    }
}
