namespace Imprevis.Dataverse.Plugins.UnitTests;

using Imprevis.Dataverse.Plugins.Requests;
using Microsoft.Xrm.Sdk;
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

        var userId = Guid.NewGuid();

        var factory = new Mock<IDataverseServiceFactory>();
        factory.Setup(x => x.GetAdminService()).Returns(adminService.Object);
        factory.Setup(x => x.GetUserService(userId)).Returns(userService.Object);
        factory.Setup(x => x.GetUserService(Guid.Empty)).Returns(userService.Object);

        ServiceFactory = factory.Object;

        var context = new Mock<IPluginExecutionContext>();
        context.Setup(x => x.UserId).Returns(userId);

        Context = context.Object;
    }

    public IDataverseServiceFactory ServiceFactory { get; private set; }

    public IPluginExecutionContext Context { get; }
}