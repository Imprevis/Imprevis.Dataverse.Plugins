namespace Imprevis.Dataverse.Plugins
{
    using System;
    using Microsoft.Xrm.Sdk;

    public enum LogLevel
    {
        Debug = 0,
        Trace = 1,
    }

    public interface ILoggingService
    {
        void Log(LogLevel level, string message);
        void Log(LogLevel level, string format, params object[] args);
        void Log(LogLevel level, Entity entity);
        void Log(LogLevel level, EntityReference entity);
        void Log(LogLevel level, Exception ex);
        void Log(LogLevel level, AttributeCollection attributes);
        void Log(LogLevel level, ParameterCollection parameters);
        void Log(LogLevel level, DataCollection<string> values);
    }
}
