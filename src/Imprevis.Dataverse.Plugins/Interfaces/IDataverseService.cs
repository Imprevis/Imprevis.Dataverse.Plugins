namespace Imprevis.Dataverse.Plugins
{
    using Microsoft.Xrm.Sdk;

    public interface IDataverseService : IOrganizationService
    {
        /// <summary>
        /// Execute a request against the Dataverse service.
        /// </summary>
        /// <param name="request">Request to execute.</param>
        void Execute(IDataverseRequest request);

        /// <summary>
        /// Execute a request against the Dataverse service and returns a response.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <param name="request">Request to execute.</param>
        /// <returns>Response object returned by the request.</returns>
        TResponse Execute<TResponse>(IDataverseRequest<TResponse> request);
    }
}
