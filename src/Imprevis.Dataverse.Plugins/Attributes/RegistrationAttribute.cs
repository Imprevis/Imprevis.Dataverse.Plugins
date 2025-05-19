namespace Imprevis.Dataverse.Plugins
{
    using System;

    /// <summary>
    /// Attribute to define the valid registations of a plugin with Dataverse.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegistrationAttribute : Attribute
    {
        /// <summary>
        /// Constructor for the RegistrationAttribute.
        /// </summary>
        public RegistrationAttribute(int mode, int stage, string message, string entityName = null)
        {
            Mode = mode;
            Stage = stage;
            Message = message;
            EntityName = entityName;
        }

        internal int Mode { get; }
        internal int Stage { get; }
        internal string Message { get; }
        internal string EntityName { get; }
    }
}
