namespace Imprevis.Dataverse.Plugins
{
    using System;

    /// <summary>
    /// Interface containing methods for working with DateTime's.
    /// </summary>
    public interface IDateTimeService
    {
        /// <summary>
        /// Get the current time in UTC.
        /// </summary>
        DateTime GetUtc();

        /// <summary>
        /// Get the current time in the current user's local time zone.
        /// </summary>
        DateTime GetLocal();

        /// <summary>
        /// Get the current time in the specified user's local time zone.
        /// </summary>
        DateTime GetLocal(Guid userId);
    }
}
