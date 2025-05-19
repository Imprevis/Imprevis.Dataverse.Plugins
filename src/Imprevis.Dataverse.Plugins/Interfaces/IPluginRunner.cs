namespace Imprevis.Dataverse.Plugins
{
    public interface IPluginRunner
    {
        /// <summary>
        /// This method is called by the plugin to execute the plugin logic.
        /// </summary>
        void Execute();
    }
}
