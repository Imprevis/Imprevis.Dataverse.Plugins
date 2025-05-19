namespace Imprevis.Dataverse.Plugins.Services
{
    using Imprevis.Dataverse.Plugins.Requests;
    using System;

    internal class CurrentDateTimeService : IDateTimeService
    {
        public CurrentDateTimeService(IDataverseServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        public IDataverseServiceFactory ServiceFactory { get; }

        public DateTime GetUtc()
        {
            return DateTime.UtcNow;
        }

        public DateTime GetLocal()
        {
            return GetLocal(Guid.Empty);
        }

        public DateTime GetLocal(Guid userId)
        {
            var service = ServiceFactory.GetUserService(userId);

            var timeZoneInfo = service.Execute(new GetUserTimeZoneInfo());

            return TimeZoneInfo.ConvertTimeFromUtc(GetUtc(), timeZoneInfo);
        }
    }
}
