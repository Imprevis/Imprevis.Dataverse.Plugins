namespace Imprevis.Dataverse.Plugins;

using Microsoft.Xrm.Sdk;
using System;

internal class DataverseServiceFactory(IOrganizationServiceFactory serviceFactory, ICacheService cache, ILoggingService logger) : IDataverseServiceFactory
{
    public IDataverseService GetAdminService()
    {
        return Get(null);
    }

    public IDataverseService GetUserService(Guid userId = default)
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
