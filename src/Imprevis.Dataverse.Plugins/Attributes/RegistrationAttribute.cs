namespace Imprevis.Dataverse.Plugins
{
    using Microsoft.Xrm.Sdk;
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegistrationAttribute : Attribute
    {
        public int Mode { get; set; }
        public int Stage { get; set; }
        public string Message { get; set; }
        public string EntityName { get; set; }

        public RegistrationAttribute(int mode, int stage, string message, string entityName = null)
        {
            Mode = mode;
            Stage = stage;
            Message = message;
            EntityName = entityName;
        }

        public bool IsValid(IPluginExecutionContext executionContext)
        {
            var mode = executionContext.Mode;
            var stage = executionContext.Stage;
            var message = executionContext.MessageName;
            var entityName = executionContext.PrimaryEntityName;

            return Stage == stage && Message == message && EntityName == entityName && Mode == mode;
        }
    }
}
