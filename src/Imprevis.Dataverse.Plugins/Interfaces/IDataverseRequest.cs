namespace Imprevis.Dataverse.Plugins
{
    public interface IDataverseRequest
    {
        void Execute(IDataverseService service, ILoggingService logger);
    }

    public interface IDataverseRequest<TResponse>
    {
        TResponse Execute(IDataverseService service, ILoggingService logger);
    }
}
