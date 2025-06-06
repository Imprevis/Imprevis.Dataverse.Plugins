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
        /// Log level for detailed messages that are typically used for tracing the execution of the application.
        /// </summary>
        Trace = 0,

        /// <summary>
        /// Log level for debugging messages that provide insight into the application's state and behavior during development.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Log level for informational messages that highlight the progress of the application at a high level.
        /// </summary>
        Information = 2,

        /// <summary>
        /// Log level for messages that indicate a potential issue but not an error.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Log level for messages that indicate an error has occurred.
        /// </summary>
        Error = 4,

        /// <summary>
        /// Log level for messages that indicate a critical error has occurred, which may cause the application to stop functioning.
        /// </summary>
        Critical = 5,

        /// <summary>
        /// Log level for messages that should not be logged at all. This is useful for disabling logging without removing code.
        /// </summary>
        None = 6,

    }

    /// <summary>
    /// Interface containing methods for logging messages.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Gets or sets the log level for the logging service.
        /// </summary>
        LogLevel LogLevel { get; set; }

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
