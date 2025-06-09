namespace Imprevis.Dataverse.Plugins.UnitTests
{
    using Imprevis.Dataverse.Plugins.Requests;
    using Moq;
    using System;

    public class TestBase
    {
        public TestBase()
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

            var adminService = new Mock<IDataverseService>();
            adminService.Setup(x => x.Execute(It.IsAny<GetUserTimeZoneInfo>())).Returns(TimeZoneInfo.Utc);

            var userService = new Mock<IDataverseService>();
            userService.Setup(x => x.Execute(It.IsAny<GetUserTimeZoneInfo>())).Returns(TimeZoneInfo.Local);

            var factory = new Mock<IDataverseServiceFactory>();
            factory.Setup(x => x.GetAdminService()).Returns(adminService.Object);
            factory.Setup(x => x.GetUserService(Guid.Empty)).Returns(userService.Object);

            ServiceFactory = factory.Object;
        }

        public IDataverseServiceFactory ServiceFactory { get; private set; }
    }
}