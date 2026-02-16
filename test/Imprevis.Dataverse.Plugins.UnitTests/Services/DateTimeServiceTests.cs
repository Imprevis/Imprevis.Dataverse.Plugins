namespace Imprevis.Dataverse.Plugins.UnitTests.Services;

using Imprevis.Dataverse.Plugins.Services;
using System;
using Xunit;

public class DateTimeServiceTests : TestBase
{
    private readonly IDateTimeService dateTime;

    public DateTimeServiceTests()
    {
        dateTime = new DateTimeService(ServiceFactory, Context);
    }

    [Fact]
    public void GetUtcNow_ShouldReturnTimeCloseToUtcNow()
    {
        var expected = DateTimeOffset.UtcNow;
        var actual = dateTime.GetUtcNow();
    
        var precision = TimeSpan.FromSeconds(2);

        Assert.Equal(expected, actual, precision);
        Assert.Equal(TimeSpan.Zero, actual.Offset);
    }

    [Fact]
    public void GetLocalNow_ShouldReturnTimeCloseToNow()
    {
        var expected = DateTimeOffset.Now;
        var actual = dateTime.GetLocalNow();

        var precision = TimeSpan.FromSeconds(2);

        Assert.Equal(expected, actual, precision);
    }

    [Fact]
    public void GetLocalTimeZone_ShouldReturnLocalTimeZone()
    {
        var expected = TimeZoneInfo.Local;
        var actual = dateTime.GetLocalTimeZone();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ConvertToUtc_ShouldConvertToUtc()
    {
        var local = new DateTimeOffset(2024, 6, 15, 14, 30, 0, TimeSpan.FromHours(-5));
        var result = dateTime.ConvertToUtc(local);

        Assert.Equal(2024, result.Year);
        Assert.Equal(6, result.Month);
        Assert.Equal(15, result.Day);
        Assert.Equal(19, result.Hour); // 14:30 -5 = 19:30 UTC
        Assert.Equal(30, result.Minute);
        Assert.Equal(TimeSpan.Zero, result.Offset);
    }

    [Fact]
    public void GetUtcStartOfDay_ShouldReturnMidnightUtc()
    {
        var date = new DateTimeOffset(2024, 6, 15, 14, 30, 45, TimeSpan.Zero);
        var result = dateTime.GetUtcStartOfDay(date);

        Assert.Equal(2024, result.Year);
        Assert.Equal(6, result.Month);
        Assert.Equal(15, result.Day);
        Assert.Equal(0, result.Hour);
        Assert.Equal(0, result.Minute);
        Assert.Equal(0, result.Second);
        Assert.Equal(TimeSpan.Zero, result.Offset);
    }

    [Fact]
    public void GetLocalStartOfDay_ShouldReturnMidnightInLocalTime()
    {
        var date = new DateTimeOffset(2024, 6, 15, 14, 30, 45, TimeZoneInfo.Local.GetUtcOffset(new DateTime(2024, 6, 15)));
        var result = dateTime.GetLocalStartOfDay(date);

        Assert.Equal(2024, result.Year);
        Assert.Equal(6, result.Month);
        Assert.Equal(15, result.Day);
        Assert.Equal(0, result.Hour);
        Assert.Equal(0, result.Minute);
        Assert.Equal(0, result.Second);
    }

    [Fact]
    public void GetUtcStartOfWeek_ShouldReturnSunday()
    {
        // June 15, 2024 is a Saturday
        var date = new DateTimeOffset(2024, 6, 15, 14, 30, 0, TimeSpan.Zero);
        var result = dateTime.GetUtcStartOfWeek(date);

        // Should return June 9, 2024 (Sunday) at midnight
        Assert.Equal(2024, result.Year);
        Assert.Equal(6, result.Month);
        Assert.Equal(9, result.Day);
        Assert.Equal(DayOfWeek.Sunday, result.DayOfWeek);
        Assert.Equal(0, result.Hour);
        Assert.Equal(0, result.Minute);
    }

    [Fact]
    public void GetUtcStartOfMonth_ShouldReturnFirstDayOfMonth()
    {
        var date = new DateTimeOffset(2024, 6, 15, 14, 30, 0, TimeSpan.Zero);
        var result = dateTime.GetUtcStartOfMonth(date);

        Assert.Equal(2024, result.Year);
        Assert.Equal(6, result.Month);
        Assert.Equal(1, result.Day);
        Assert.Equal(0, result.Hour);
        Assert.Equal(0, result.Minute);
        Assert.Equal(TimeSpan.Zero, result.Offset);
    }

    [Fact]
    public void GetUtcStartOfYear_ShouldReturnJanuaryFirst()
    {
        var date = new DateTimeOffset(2024, 6, 15, 14, 30, 0, TimeSpan.Zero);
        var result = dateTime.GetUtcStartOfYear(date);

        Assert.Equal(2024, result.Year);
        Assert.Equal(1, result.Month);
        Assert.Equal(1, result.Day);
        Assert.Equal(0, result.Hour);
        Assert.Equal(0, result.Minute);
        Assert.Equal(TimeSpan.Zero, result.Offset);
    }

    [Fact]
    public void IsWeekend_ShouldReturnTrueForSaturday()
    {
        var saturday = new DateTimeOffset(2024, 6, 15, 10, 0, 0, TimeSpan.Zero); // Saturday
        Assert.True(dateTime.IsWeekend(saturday));
    }

    [Fact]
    public void IsWeekend_ShouldReturnTrueForSunday()
    {
        var sunday = new DateTimeOffset(2024, 6, 16, 10, 0, 0, TimeSpan.Zero); // Sunday
        Assert.True(dateTime.IsWeekend(sunday));
    }

    [Fact]
    public void IsWeekend_ShouldReturnFalseForWeekday()
    {
        var monday = new DateTimeOffset(2024, 6, 17, 10, 0, 0, TimeSpan.Zero); // Monday
        Assert.False(dateTime.IsWeekend(monday));
    }

    [Fact]
    public void IsWeekday_ShouldReturnTrueForMonday()
    {
        var monday = new DateTimeOffset(2024, 6, 17, 10, 0, 0, TimeSpan.Zero); // Monday
        Assert.True(dateTime.IsWeekday(monday));
    }

    [Fact]
    public void IsWeekday_ShouldReturnFalseForSaturday()
    {
        var saturday = new DateTimeOffset(2024, 6, 15, 10, 0, 0, TimeSpan.Zero); // Saturday
        Assert.False(dateTime.IsWeekday(saturday));
    }

    [Fact]
    public void GetAge_ShouldCalculateCorrectAge()
    {
        var birthDate = new DateTimeOffset(1990, 6, 15, 0, 0, 0, TimeSpan.Zero);
        var asOfDate = new DateTimeOffset(2024, 6, 15, 0, 0, 0, TimeSpan.Zero);

        var age = dateTime.GetAge(birthDate, asOfDate);

        Assert.Equal(34, age);
    }

    [Fact]
    public void GetAge_ShouldNotCountYearIfBirthdayNotReached()
    {
        var birthDate = new DateTimeOffset(1990, 6, 15, 0, 0, 0, TimeSpan.Zero);
        var asOfDate = new DateTimeOffset(2024, 6, 14, 0, 0, 0, TimeSpan.Zero); // Day before birthday

        var age = dateTime.GetAge(birthDate, asOfDate);

        Assert.Equal(33, age); // Still 33, not 34 yet
    }

    [Fact]
    public void AddBusinessDays_ShouldAddBusinessDaysSkippingWeekend()
    {
        // Start on Friday June 14, 2024
        var startDate = new DateTimeOffset(2024, 6, 14, 10, 0, 0, TimeSpan.Zero);
        
        // Add 3 business days: Mon 17, Tue 18, Wed 19
        var result = dateTime.AddBusinessDays(startDate, 3);

        Assert.Equal(2024, result.Year);
        Assert.Equal(6, result.Month);
        Assert.Equal(19, result.Day); // Wednesday
        Assert.Equal(DayOfWeek.Wednesday, result.DayOfWeek);
    }

    [Fact]
    public void AddBusinessDays_ShouldSubtractBusinessDays()
    {
        // Start on Wednesday June 19, 2024
        var startDate = new DateTimeOffset(2024, 6, 19, 10, 0, 0, TimeSpan.Zero);
        
        // Subtract 3 business days: Tue 18, Mon 17, Fri 14
        var result = dateTime.AddBusinessDays(startDate, -3);

        Assert.Equal(2024, result.Year);
        Assert.Equal(6, result.Month);
        Assert.Equal(14, result.Day); // Friday
        Assert.Equal(DayOfWeek.Friday, result.DayOfWeek);
    }

    [Fact]
    public void AddBusinessDays_ShouldPreserveTime()
    {
        var startDate = new DateTimeOffset(2024, 6, 14, 14, 30, 45, TimeSpan.Zero);
        var result = dateTime.AddBusinessDays(startDate, 1);

        Assert.Equal(14, result.Hour);
        Assert.Equal(30, result.Minute);
        Assert.Equal(45, result.Second);
    }

    [Fact]
    public void GetBusinessDaysBetween_ShouldCountBusinessDays()
    {
        // Friday June 14 to Wednesday June 19
        // Should count: Mon 17, Tue 18, Wed 19 = 3 days
        var startDate = new DateTimeOffset(2024, 6, 14, 10, 0, 0, TimeSpan.Zero);
        var endDate = new DateTimeOffset(2024, 6, 19, 10, 0, 0, TimeSpan.Zero);

        var result = dateTime.GetBusinessDaysBetween(startDate, endDate);

        Assert.Equal(3, result);
    }

    [Fact]
    public void GetBusinessDaysBetween_ShouldReturnZeroForSameDay()
    {
        var date = new DateTimeOffset(2024, 6, 14, 10, 0, 0, TimeSpan.Zero);

        var result = dateTime.GetBusinessDaysBetween(date, date);

        Assert.Equal(0, result);
    }

    [Fact]
    public void GetBusinessDaysBetween_ShouldReturnNegativeForReversedDates()
    {
        var startDate = new DateTimeOffset(2024, 6, 19, 10, 0, 0, TimeSpan.Zero);
        var endDate = new DateTimeOffset(2024, 6, 14, 10, 0, 0, TimeSpan.Zero);

        var result = dateTime.GetBusinessDaysBetween(startDate, endDate);

        Assert.Equal(-3, result);
    }

    [Fact]
    public void GetBusinessDaysBetween_ShouldSkipWeekends()
    {
        // Friday June 14 to Monday June 17
        // Should count: Mon 17 = 1 day (skips Sat 15, Sun 16)
        var startDate = new DateTimeOffset(2024, 6, 14, 10, 0, 0, TimeSpan.Zero);
        var endDate = new DateTimeOffset(2024, 6, 17, 10, 0, 0, TimeSpan.Zero);

        var result = dateTime.GetBusinessDaysBetween(startDate, endDate);

        Assert.Equal(1, result);
    }

    [Fact]
    public void GetBusinessDaysBetween_WeekendToWeekend_ShouldReturnZero()
    {
        // Saturday to Sunday
        var startDate = new DateTimeOffset(2024, 6, 15, 10, 0, 0, TimeSpan.Zero);
        var endDate = new DateTimeOffset(2024, 6, 16, 10, 0, 0, TimeSpan.Zero);

        var result = dateTime.GetBusinessDaysBetween(startDate, endDate);

        Assert.Equal(0, result);
    }
}