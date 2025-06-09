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

        public DateTime GetLocalNow(Guid userId = default)
        {
            var timeZone = GetLocalTimeZone(userId);

            return TimeZoneInfo.ConvertTimeFromUtc(GetUtcNow(), timeZone);
        }

        public TimeZoneInfo GetLocalTimeZone(Guid userId = default)
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
