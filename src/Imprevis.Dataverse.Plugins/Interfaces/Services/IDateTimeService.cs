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
        DateTime GetUtcNow();

        /// <summary>
        /// Get the current time in the current user's local time zone or the time zone of the <paramref name="userId"/> specified.
        /// </summary>
        DateTime GetLocalNow(Guid userId = default);

        /// <summary>
        /// Get the time zone for the current user or the <paramref name="userId"/> specified.
        /// </summary>
        TimeZoneInfo GetLocalTimeZone(Guid userId = default);

        /// <summary>
        /// Converts the provided <see cref="DateTime"/> to UTC.
        /// </summary>
        DateTime ConvertToUtc(DateTime dateTime);

        /// <summary>
        /// Converts the provided <see cref="DateTime"/> in the specified <paramref name="sourceTimeZone"/> to UTC.
        /// </summary>
        DateTime ConvertToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone);
    }
}
