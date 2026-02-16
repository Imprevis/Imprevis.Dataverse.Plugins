namespace Imprevis.Dataverse.Plugins.Services;

using Imprevis.Dataverse.Plugins.Requests;
using Microsoft.Xrm.Sdk;
using System;

internal class DateTimeService(IDataverseServiceFactory serviceFactory, IPluginExecutionContext context) : IDateTimeService
{
    public DateTimeOffset GetUtcNow()
    {
        return DateTimeOffset.UtcNow;
    }

    public DateTimeOffset GetLocalNow(Guid userId = default)
    {
        var utcNow = GetUtcNow();
        var timeZone = GetLocalTimeZone(userId);

        return TimeZoneInfo.ConvertTime(utcNow, timeZone);
    }

    public TimeZoneInfo GetLocalTimeZone(Guid userId = default)
    {
        if (userId == default)
        {
            userId = context.UserId;
        }

        var service = serviceFactory.GetUserService(userId);

        return service.Execute(new GetUserTimeZoneInfo(userId));
    }
    public DateTimeOffset ConvertToUtc(DateTimeOffset localDateTime)
    {
        return localDateTime.ToUniversalTime();
    }

    public DateTimeOffset ConvertToLocal(DateTimeOffset utcDateTime, Guid userId = default)
    {
        var timeZone = GetLocalTimeZone(userId);
        return TimeZoneInfo.ConvertTime(utcDateTime, timeZone);
    }

    public DateTimeOffset GetUtcStartOfDay(DateTimeOffset? date = null)
    {
        var targetDate = ConvertToUtc(date ?? GetUtcNow());
        return new DateTimeOffset(targetDate.Year, targetDate.Month, targetDate.Day, 0, 0, 0, TimeSpan.Zero);
    }

    public DateTimeOffset GetLocalStartOfDay(DateTimeOffset? date = null, Guid userId = default)
    {
        var targetDate = ConvertToLocal(date ?? GetUtcNow(), userId);
        var startOfDay = new DateTimeOffset(targetDate.Year, targetDate.Month, targetDate.Day, 0, 0, 0, targetDate.Offset);

        return startOfDay;
    }

    public DateTimeOffset GetUtcStartOfWeek(DateTimeOffset? date = null)
    {
        var targetDate = ConvertToUtc(date ?? GetUtcNow());
        var daysToSubtract = (int)targetDate.DayOfWeek;
        var startOfWeek = targetDate.AddDays(-daysToSubtract);

        return GetUtcStartOfDay(startOfWeek);
    }
    
    public DateTimeOffset GetLocalStartOfWeek(DateTimeOffset? date = null, Guid userId = default)
    {
        var targetDate = ConvertToLocal(date ?? GetUtcNow(), userId);
        var daysToSubtract = (int)targetDate.DayOfWeek;
        var startOfWeek = targetDate.AddDays(-daysToSubtract);

        return GetLocalStartOfDay(startOfWeek, userId);
    }

    public DateTimeOffset GetUtcStartOfMonth(DateTimeOffset? date = null)
    {
        var targetDate = ConvertToUtc(date ?? GetUtcNow());
        return new DateTimeOffset(targetDate.Year, targetDate.Month, 1, 0, 0, 0, TimeSpan.Zero);
    }

    public DateTimeOffset GetLocalStartOfMonth(DateTimeOffset? date = null, Guid userId = default)
    {
        var targetDate = ConvertToLocal(date ?? GetUtcNow(), userId);
        var startOfMonth = new DateTimeOffset(targetDate.Year, targetDate.Month, 1, 0, 0, 0, targetDate.Offset);
        return startOfMonth;
    }

    public DateTimeOffset GetUtcStartOfYear(DateTimeOffset? date = null)
    {
        var targetDate = ConvertToUtc(date ?? GetUtcNow());
        return new DateTimeOffset(targetDate.Year, 1, 1, 0, 0, 0, TimeSpan.Zero);
    }

    public DateTimeOffset GetLocalStartOfYear(DateTimeOffset? date = null, Guid userId = default)
    {
        var targetDate = ConvertToLocal(date ?? GetUtcNow(), userId);
        var startOfYear = new DateTimeOffset(targetDate.Year, 1, 1, 0, 0, 0, targetDate.Offset);
        return startOfYear;
    }

    public bool IsWeekend(DateTimeOffset date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }

    public bool IsWeekday(DateTimeOffset date)
    {
        return !IsWeekend(date);
    }

    public int GetAge(DateTimeOffset birthDate, DateTimeOffset? asOfDate = null)
    {
        var referenceDate = asOfDate ?? GetUtcNow();
        var age = referenceDate.Year - birthDate.Year;

        // Subtract one year if birthday hasn't occurred yet this year
        if (birthDate.Date > referenceDate.AddYears(-age).Date)
        {
            age--;
        }

        return age;
    }

    public DateTimeOffset AddBusinessDays(DateTimeOffset startDate, int businessDays)
    {
        var sign = businessDays < 0 ? -1 : 1;
        var daysToAdd = Math.Abs(businessDays);
        var currentDate = startDate;

        while (daysToAdd > 0)
        {
            currentDate = currentDate.AddDays(sign);

            if (IsWeekday(currentDate))
            {
                daysToAdd--;
            }
        }

        return currentDate;
    }

    public int GetBusinessDaysBetween(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (startDate > endDate)
        {
            return -GetBusinessDaysBetween(endDate, startDate);
        }

        var businessDays = 0;
        var currentDate = startDate.Date.AddDays(1); // Start from day after startDate
        var endDateOnly = endDate.Date;

        while (currentDate <= endDateOnly)
        {
            if (IsWeekday(currentDate))
            {
                businessDays++;
            }

            currentDate = currentDate.AddDays(1);
        }

        return businessDays;
    }
}
