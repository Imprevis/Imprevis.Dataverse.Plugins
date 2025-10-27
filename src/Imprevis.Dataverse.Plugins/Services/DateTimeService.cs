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
    }
}
