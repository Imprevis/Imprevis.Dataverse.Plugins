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
        /// Get the current time in the current user's local time zone.
        /// </summary>
        DateTime GetLocalNow();

        /// <summary>
        /// Get the current time in the specified user's local time zone.
        /// </summary>
        DateTime GetLocalNow(Guid userId);

        /// <summary>
        /// Get the time zone for the current user.
        /// </summary>
        TimeZoneInfo GetLocalTimeZone();

        /// <summary>
        /// Get the time zone for the specified user.
        /// </summary>
        TimeZoneInfo GetLocalTimeZone(Guid userId);

        /// <summary>
        /// Converts the provided <see cref="DateTime"/> to UTC.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        DateTime ConvertToUtc(DateTime dateTime);

        /// <summary>
        /// Converts the provided <see cref="DateTime"/> in the specified <paramref name="sourceTimeZone"/> to UTC.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="sourceTimeZone"></param>
        /// <returns></returns>
        DateTime ConvertToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone);
    }
}
