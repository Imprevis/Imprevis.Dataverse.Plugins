namespace Imprevis.Dataverse.Plugins
{
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
        /// Gets the <see cref="IDataverseService" /> corresponding to the initiating user of the plugin, or a specific user if a <paramref name="userId"/> is specified.
        /// </summary>
        IDataverseService GetUserService(Guid userId = default);
    }
}
