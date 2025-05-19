namespace Imprevis.Dataverse.Plugins
{
    /// <summary>
    /// Interface for a service that provides access to plugin configuration settings.
    /// </summary>
    public interface IPluginConfigService
    {
        /// <summary>
        /// Gets the unsecure configuration settings for the plugin.
        /// </summary>
        TUnsecure GetUnsecure<TUnsecure>();

        /// <summary>
        /// Gets the secure configuration settings for the plugin.
        /// </summary>
        TSecure GetSecure<TSecure>();
    }
}
