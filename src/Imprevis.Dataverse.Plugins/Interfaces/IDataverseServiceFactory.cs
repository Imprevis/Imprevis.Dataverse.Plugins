﻿namespace Imprevis.Dataverse.Plugins
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
        /// Gets the <see cref="IDataverseService" /> corresponding to the initiating user of the plugin.
        /// </summary>
        IDataverseService GetUserService();
        
        /// <summary>
        /// Gets the <see cref="IDataverseService" /> corresponding to the requested user ID.
        /// </summary>
        IDataverseService GetUserService(Guid userId);
    }
}
