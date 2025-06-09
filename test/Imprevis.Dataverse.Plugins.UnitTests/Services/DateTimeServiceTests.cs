namespace Imprevis.Dataverse.Plugins.UnitTests.Services
{
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
            var expected = DateTime.UtcNow;
            var actual = dateTime.GetUtcNow();
        
            var precision = TimeSpan.FromSeconds(2);

            Assert.Equal(expected, actual, precision);
        }

        [Fact]
        public void GetLocalNow_ShouldReturnTimeCloseToNow()
        {
            var expected = DateTime.Now;
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
        public void ConvertToUtc_ShouldConvertAccurately()
        {
            var expected = DateTime.UtcNow;
            var actual = dateTime.ConvertToUtc(dateTime.GetLocalNow());

            var precision = TimeSpan.FromSeconds(2);

            Assert.Equal(expected, actual, precision);
        }
    }
}