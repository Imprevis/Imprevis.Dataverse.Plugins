namespace Imprevis.Dataverse.Plugins
{
    public interface IPluginConfigService
    {
        TUnsecure GetUnsecure<TUnsecure>();
        TSecure GetSecure<TSecure>();
    }
}
