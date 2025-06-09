namespace Imprevis.Dataverse.Plugins
{
    using System;

    internal interface IRegistrationService
    {
        bool IsValid(Type type);
    }
}
