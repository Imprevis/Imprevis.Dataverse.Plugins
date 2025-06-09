namespace Imprevis.Dataverse.Plugins
{
    using Microsoft.Xrm.Sdk;
    using System;

    /// <summary>
    /// Interface for creating instances of <see cref="IDataverseService" />.
    /// </summary>
    public interface IDataverseServiceFactory
    {
        /// <summary>
        /// Gets the <see cref="IDataverseService" /> corresponding to the admin/system service.
        /// </summary>
        IDataverseService GetAdminService();

        /// <summary>
        /// Gets the <see cref="IDataverseService" /> corresponding to the user the plugin is executing as (<see cref="IExecutionContext.UserId"/>), or a specific user if a <paramref name="userId"/> is specified.
        /// </summary>
        IDataverseService GetUserService(Guid userId = default);
    }
}
