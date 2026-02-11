namespace Imprevis.Dataverse.Plugins.Test;

using Imprevis.Dataverse.Plugins;
using Imprevis.Dataverse.Plugins.Test.Requests;
using Microsoft.Xrm.Sdk;

public class TestPlugin : Plugin<TestPluginRunner> { }

public class TestPluginRunner(IDataverseServiceFactory factory, IDateTimeService dateTime, ILoggingService logger) : IPluginRunner
{
    public void Execute()
    {
        var userService = factory.GetUserService();
        var userName = userService.Execute(new GetUserName());
        logger.LogInformation("User Name: " + userName);
        
        throw new InvalidPluginExecutionException($"User Name = {userName} | DateTime = {dateTime.GetLocalNow()}");
    }
}
