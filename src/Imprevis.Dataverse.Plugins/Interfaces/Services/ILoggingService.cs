namespace Imprevis.Dataverse.Plugins
{
    using System;
    using Microsoft.Xrm.Sdk;

    /// <summary>
    /// Enumeration representing the different log levels.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Log level for messages that are useful for debugging. Only works in Debug builds.
        /// </summary>
        Debug = 0,

        /// <summary>
        /// Log level for messages that are useful for troubleshooting.
        /// </summary>
        Trace = 1,
    }

    /// <summary>
    /// Interface containing methods for logging messages.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Logs a message with the specified log level.
        /// </summary>
        void Log(LogLevel level, string message);

        /// <summary>
        /// Logs a message with the specified log level.
        /// </summary>
        void Log(LogLevel level, string format, params object[] args);

        /// <summary>
        /// Logs an Entity with the specified log level.
        /// </summary>
        void Log(LogLevel level, Entity entity);

        /// <summary>
        /// Logs an EntityReference with the specified log level.
        /// </summary>
        void Log(LogLevel level, EntityReference entity);

        /// <summary>
        /// Logs an Exception with the specified log level.
        /// </summary>
        void Log(LogLevel level, Exception ex);

        /// <summary>
        /// Logs an AttributeCollection with the specified log level.
        /// </summary>
        void Log(LogLevel level, AttributeCollection attributes);

        /// <summary>
        /// Logs a ParameterCollection with the specified log level.
        /// </summary>
        void Log(LogLevel level, ParameterCollection parameters);

        /// <summary>
        /// Logs a DataCollection with the specified log level.
        /// </summary>
        void Log(LogLevel level, DataCollection<string> values);
    }
}
