namespace Imprevis.Dataverse.Plugins;

using System;

/// <summary>
/// Interface containing methods for working with DateTime values.
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
    
    /// <summary>
    /// Convert a DateTime to UTC.
    /// </summary>
    /// <param name="localDateTime">The local DateTime to convert.</param>
    DateTimeOffset ConvertToUtc(DateTimeOffset localDateTime);

    /// <summary>
    /// Convert a UTC DateTime to the user's local time zone.
    /// </summary>
    /// <param name="utcDateTime">The UTC DateTime to convert.</param>
    /// <param name="userId">The user ID whose timezone to use. Defaults to current user.</param>
    DateTimeOffset ConvertToLocal(DateTimeOffset utcDateTime, Guid userId = default);

    /// <summary>
    /// Get the start of day (midnight) in UTC for the current user's timezone.
    /// </summary>
    /// <param name="date">The date to get start of day for. If null, uses current date.</param>
    DateTimeOffset GetUtcStartOfDay(DateTimeOffset? date = null);

    /// <summary>
    /// Get the start of day (midnight) in the user's local time zone.
    /// </summary>
    /// <param name="date">The date to get start of day for. If null, uses current date.</param>
    /// <param name="userId">The user ID whose timezone to use. Defaults to current user.</param>
    DateTimeOffset GetLocalStartOfDay(DateTimeOffset? date = null, Guid userId = default);
    
    /// <summary>
    /// Get the start of the current week (Sunday) in UTC.
    /// </summary>
    /// <param name="date">The date within the week. If null, uses current date.</param>
    DateTimeOffset GetUtcStartOfWeek(DateTimeOffset? date = null);

    /// <summary>
    /// Get the start of the current week (Sunday) in the user's local time zone.
    /// </summary>
    /// <param name="date">The date within the week. If null, uses current date.</param>
    /// <param name="userId">The user ID whose timezone to use. Defaults to current user.</param>
    DateTimeOffset GetLocalStartOfWeek(DateTimeOffset? date = null, Guid userId = default);

    /// <summary>
    /// Get the start of the current month in UTC.
    /// </summary>
    /// <param name="date">The date within the month. If null, uses current date.</param>
    DateTimeOffset GetUtcStartOfMonth(DateTimeOffset? date = null);

    /// <summary>
    /// Get the start of the current month in the user's local time zone.
    /// </summary>
    /// <param name="date">The date within the month. If null, uses current date.</param>
    /// <param name="userId">The user ID whose timezone to use. Defaults to current user.</param>
    DateTimeOffset GetLocalStartOfMonth(DateTimeOffset? date = null, Guid userId = default);

    /// <summary>
    /// Get the start of the current year in UTC.
    /// </summary>
    /// <param name="date">The date within the year. If null, uses current date.</param>
    DateTimeOffset GetUtcStartOfYear(DateTimeOffset? date = null);

    /// <summary>
    /// Get the start of the current year in the user's local time zone.
    /// </summary>
    /// <param name="date">The date within the year. If null, uses current date.</param>
    /// <param name="userId">The user ID whose timezone to use. Defaults to current user.</param>
    DateTimeOffset GetLocalStartOfYear(DateTimeOffset? date = null, Guid userId = default);

    /// <summary>
    /// Check if a date is a weekend (Saturday or Sunday).
    /// </summary>
    bool IsWeekend(DateTimeOffset date);

    /// <summary>
    /// Check if a date is a weekday (Monday through Friday).
    /// </summary>
    bool IsWeekday(DateTimeOffset date);

    /// <summary>
    /// Get age in years from a birth date.
    /// </summary>
    /// <param name="birthDate">The birth date.</param>
    /// <param name="asOfDate">The date to calculate age as of. If null, uses current date.</param>
    int GetAge(DateTimeOffset birthDate, DateTimeOffset? asOfDate = null);

    /// <summary>
    /// Add business days to a date (skips weekends).
    /// </summary>
    /// <param name="startDate">The starting date.</param>
    /// <param name="businessDays">Number of business days to add (can be negative).</param>
    DateTimeOffset AddBusinessDays(DateTimeOffset startDate, int businessDays);

    /// <summary>
    /// Calculate the number of business days between two dates (excludes weekends).
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    int GetBusinessDaysBetween(DateTimeOffset startDate, DateTimeOffset endDate);
}
