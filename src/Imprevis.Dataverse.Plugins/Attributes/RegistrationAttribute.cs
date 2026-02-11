namespace Imprevis.Dataverse.Plugins;

using System;

/// <summary>
/// Attribute to define the valid registations of a plugin with Dataverse.
/// </summary>
/// <remarks>
/// Constructor for the RegistrationAttribute.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegistrationAttribute(int mode, int stage, string message, string entityName = null) : Attribute
{
    internal int Mode { get; } = mode;
    internal int Stage { get; } = stage;
    internal string Message { get; } = message;
    internal string EntityName { get; } = entityName;
}
