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
        DateTimeOffset GetUtcNow();

        /// <summary>
        /// Get the current time in the current user's local time zone or the time zone of the <paramref name="userId"/> specified.
        /// </summary>
        DateTimeOffset GetLocalNow(Guid userId = default);

        /// <summary>
        /// Get the time zone for the current user or the <paramref name="userId"/> specified.
        /// </summary>
        TimeZoneInfo GetLocalTimeZone(Guid userId = default);
    }
}
