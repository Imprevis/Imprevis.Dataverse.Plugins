namespace Imprevis.Dataverse.Plugins
{
    using Microsoft.Xrm.Sdk;

    public interface IDataverseService : IOrganizationService
    {
        void Execute(IDataverseRequest request);
        TResponse Execute<TResponse>(IDataverseRequest<TResponse> request);
    }

}
