namespace Imprevis.Dataverse.Plugins;

/// <summary>
/// Interface for a service that provides access to plugin configuration settings.
/// </summary>
internal interface IPluginConfigService
{
    /// <summary>
    /// Gets the unsecure configuration settings for the plugin.
    /// </summary>
    /// <param name="format">Determins how the data is foratted. This affects which deserializer is used.</param>
    TUnsecure GetUnsecure<TUnsecure>(SerializationFormat format = SerializationFormat.Json);

    /// <summary>
    /// Gets the secure configuration settings for the plugin.
    /// </summary>
    /// <param name="format">Determins how the data is foratted. This affects which deserializer is used.</param>
    TSecure GetSecure<TSecure>(SerializationFormat format = SerializationFormat.Json);
}
