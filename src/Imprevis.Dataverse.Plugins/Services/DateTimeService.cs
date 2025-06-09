namespace Imprevis.Dataverse.Plugins.Services
{
    using Imprevis.Dataverse.Plugins.Requests;
    using Microsoft.Xrm.Sdk;
    using System;

    internal class DateTimeService : IDateTimeService
    {
        public DateTimeService(IDataverseServiceFactory serviceFactory, IPluginExecutionContext context)
        {
            ServiceFactory = serviceFactory;
            Context = context;
        }

        public IDataverseServiceFactory ServiceFactory { get; }
        public IPluginExecutionContext Context { get; }

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
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (userId == default)
            {
                userId = Context.UserId;
            }

            var service = ServiceFactory.GetUserService(userId);

            return service.Execute(new GetUserTimeZoneInfo(userId));
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
