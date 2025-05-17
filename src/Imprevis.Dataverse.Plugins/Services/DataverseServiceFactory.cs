namespace Imprevis.Dataverse.Plugins
{
    using Microsoft.Xrm.Sdk;
    using System;

    public class DataverseServiceFactory : IDataverseServiceFactory
    {
        private readonly IOrganizationServiceFactory serviceFactory;
        private readonly ILoggingService logger;

        public DataverseServiceFactory(IOrganizationServiceFactory serviceFactory, ILoggingService logger)
        {
            this.serviceFactory = serviceFactory;
            this.logger = logger;
        }

        public IDataverseService GetAdminService()
        {
            return Get(null);
        }

        public IDataverseService GetUserService()
        {
            return Get(Guid.Empty);
        }

        public IDataverseService GetUserService(Guid userId)
        {
            return Get(userId);
        }

        private IDataverseService Get(Guid? userId)
        {
            // Guid.Empty == Current User, null == SYSTEM User, Other == Other User
            var orgService = serviceFactory.CreateOrganizationService(userId);

            return new DataverseService(orgService, logger);
        }
    }
}
