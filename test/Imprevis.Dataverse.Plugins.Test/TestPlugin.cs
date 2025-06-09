namespace Imprevis.Dataverse.Plugins.Test
{
    using Imprevis.Dataverse.Plugins;
    using Imprevis.Dataverse.Plugins.Extensions;
    using Imprevis.Dataverse.Plugins.Test.Requests;
    using Microsoft.Xrm.Sdk;

    public class TestPlugin : Plugin<TestPluginRunner> { }

    public class TestPluginRunner : IPluginRunner
    {
        public TestPluginRunner(IDataverseServiceFactory factory, IPluginExecutionContext context, ILoggingService logger)
        {
            Factory = factory;
            Context = context;
            Logger = logger;
        }

        public IDataverseServiceFactory Factory { get; }
        public IPluginExecutionContext Context { get; }
        public ILoggingService Logger { get; }

        public void Execute()
        {
            var adminService = Factory.GetAdminService();
            var adminName = adminService.Execute(new GetUserName());
            Logger.LogInformation("Admin Name: " + adminName);

            var userService = Factory.GetUserService();
            var userName = userService.Execute(new GetUserName());
            Logger.LogInformation("User Name: " + userName);
            
            var initService = Factory.GetUserService(Context.UserId);
            var initName = initService.Execute(new GetUserName());
            Logger.LogInformation("Init Name: " + initName);


            throw new InvalidPluginExecutionException($"Admin Name = {adminName} | User Name = {userName} | Init Name = {initName}");
        }
    }
}
