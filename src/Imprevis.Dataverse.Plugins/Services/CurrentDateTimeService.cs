namespace Imprevis.Dataverse.Plugins.Services
{
    using Imprevis.Dataverse.Plugins.Requests;
    using System;

    public class CurrentDateTimeService : IDateTimeService
    {
        public CurrentDateTimeService(IDataverseServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        public IDataverseServiceFactory ServiceFactory { get; }

        public DateTime Get()
        {
            return DateTime.UtcNow;
        }

        public DateTime GetUserLocal()
        {
            var service = ServiceFactory.GetUserService();

            var timeZoneInfo = service.Execute(new GetUserTimeZoneInfo());
            
            return TimeZoneInfo.ConvertTimeFromUtc(Get(), timeZoneInfo);
        }
    }
}
