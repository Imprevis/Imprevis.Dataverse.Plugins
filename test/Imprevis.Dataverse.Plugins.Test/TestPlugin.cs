namespace Imprevis.Dataverse.Plugins.Test
{
    using Imprevis.Dataverse.Plugins;
    using Imprevis.Dataverse.Plugins.Test.Requests;
    using Microsoft.Xrm.Sdk;

    public class TestPlugin : Plugin<TestPluginRunner> { }

    public class TestPluginRunner : IPluginRunner
    {
        public TestPluginRunner(IPluginExecutionContext context, IDataverseServiceFactory factory, IDateTimeService dateTime, ILoggingService logger)
        {
            Context = context;
            Factory = factory;
            DateTime = dateTime;
            Logger = logger;
        }

        public IDataverseServiceFactory Factory { get; }
        public IPluginExecutionContext Context { get; }
        public ILoggingService Logger { get; }
        public IDateTimeService DateTime { get; }

        public void Execute()
        {
            var userService = Factory.GetUserService();
            var userName = userService.Execute(new GetUserName());
            Logger.LogInformation("User Name: " + userName);
            
            throw new InvalidPluginExecutionException($"User Name = {userName} | DateTime = {DateTime.GetLocalNow()}");
        }
    }
}
