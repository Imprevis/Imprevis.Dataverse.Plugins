namespace Imprevis.Dataverse.Plugins
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegistrationAttribute : Attribute
    {
        public RegistrationAttribute(int mode, int stage, string message, string entityName = null)
        {
            Mode = mode;
            Stage = stage;
            Message = message;
            EntityName = entityName;
        }

        public int Mode { get; }
        public int Stage { get; }
        public string Message { get; }
        public string EntityName { get; }
    }
}
