namespace Imprevis.Dataverse.Plugins
{
    using Microsoft.Xrm.Sdk;
    using System;

    internal class DataverseServiceFactory : IDataverseServiceFactory
    {
        private readonly IOrganizationServiceFactory serviceFactory;
        private readonly ICacheService cache;
        private readonly ILoggingService logger;

        public DataverseServiceFactory(IOrganizationServiceFactory serviceFactory, ICacheService cache, ILoggingService logger)
        {
            this.serviceFactory = serviceFactory;
            this.cache = cache;
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

            return new DataverseService(orgService, cache, logger);
        }
    }
}
