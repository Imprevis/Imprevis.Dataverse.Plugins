namespace Imprevis.Dataverse.Plugins
{
    using Microsoft.Xrm.Sdk;
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegistrationAttribute : Attribute
    {
        private readonly int mode;
        private readonly int stage;
        private readonly string message;
        private readonly string entityName;

        public RegistrationAttribute(int mode, int stage, string message, string entityName = null)
        {
            this.mode = mode;
            this.stage = stage;
            this.message = message;
            this.entityName = entityName;
        }

        public bool IsValid(IPluginExecutionContext executionContext)
        {
            var mode = executionContext.Mode;
            var stage = executionContext.Stage;
            var message = executionContext.MessageName;
            var entityName = executionContext.PrimaryEntityName;

            return this.mode == mode && this.stage == stage && this.message == message && this.entityName == entityName;
        }
    }
}
