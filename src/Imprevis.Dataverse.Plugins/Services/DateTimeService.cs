namespace Imprevis.Dataverse.Plugins.Services
{
    using Imprevis.Dataverse.Plugins.Requests;
    using System;

    internal class DateTimeService : IDateTimeService
    {
        public DateTimeService(IDataverseServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        public IDataverseServiceFactory ServiceFactory { get; }

        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }

        public DateTime GetLocalNow()
        {
            return GetLocalNow(Guid.Empty);
        }

        public DateTime GetLocalNow(Guid userId)
        {
            var timeZone = GetLocalTimeZone(userId);

            return TimeZoneInfo.ConvertTimeFromUtc(GetUtcNow(), timeZone);
        }

        public TimeZoneInfo GetLocalTimeZone()
        {
            return GetLocalTimeZone(Guid.Empty);
        }

        public TimeZoneInfo GetLocalTimeZone(Guid userId)
        {
            var service = ServiceFactory.GetUserService(userId);

            return service.Execute(new GetUserTimeZoneInfo());
        }

        public DateTime ConvertToUtc(DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime);
        }

        public DateTime ConvertToUtc(DateTime dateTime, TimeZoneInfo timeZone)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone);
        }
    }
}
