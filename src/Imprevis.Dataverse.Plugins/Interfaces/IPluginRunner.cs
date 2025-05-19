namespace Imprevis.Dataverse.Plugins
{
    /// <summary>
    /// Interface for a plugin runner.
    /// </summary>
    public interface IPluginRunner
    {
        /// <summary>
        /// This method is called by the plugin to execute the plugin logic.
        /// </summary>
        void Execute();
    }
}
