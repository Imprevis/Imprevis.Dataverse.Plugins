namespace Imprevis.Dataverse.Plugins
{
    using Microsoft.Xrm.Sdk;
    using System;

    /// <summary>
    /// Extensions for the ILoggingService interface.
    /// </summary>
    public static class LoggingServiceExtensions
    {
        /// <summary>
        /// Logs a message at the Debug level.
        /// </summary>
        public static void LogDebug(this ILoggingService loggingService, string message)
        {
            loggingService.Log(LogLevel.Debug, message);
        }

        /// <summary>
        /// Logs a message at the Debug level with a formatted string.
        /// </summary>
        public static void LogDebug(this ILoggingService loggingService, string format, params object[] args)
        {
            loggingService.Log(LogLevel.Debug, format, args);
        }

        /// <summary>
        /// Logs an Entity at the Debug level.
        /// </summary>
        public static void LogDebug(this ILoggingService loggingService, Entity entity)
        {
            loggingService.Log(LogLevel.Debug, entity);
        }

        /// <summary>
        /// Logs an EntityReference at the Debug level.
        /// </summary>
        public static void LogDebug(this ILoggingService loggingService, EntityReference entity)
        {
            loggingService.Log(LogLevel.Debug, entity);
        }

        /// <summary>
        /// Logs an Exception at the Debug level.
        /// </summary>
        public static void LogDebug(this ILoggingService loggingService, Exception ex)
        {
            loggingService.Log(LogLevel.Debug, ex);
            loggingService.Log(LogLevel.Debug, ex.StackTrace);
        }

        /// <summary>
        /// Logs an AttributeCollection at the Debug level.
        /// </summary>
        public static void LogDebug(this ILoggingService loggingService, AttributeCollection attributes)
        {
            loggingService.Log(LogLevel.Debug, attributes);
        }

        /// <summary>
        /// Logs a ParameterCollection at the Debug level.
        /// </summary>
        public static void LogDebug(this ILoggingService loggingService, ParameterCollection parameters)
        {
            loggingService.Log(LogLevel.Debug, parameters);
        }

        /// <summary>
        /// Logs a DataCollection at the Debug level.
        /// </summary>
        public static void LogDebug(this ILoggingService loggingService, DataCollection<string> values)
        {
            loggingService.Log(LogLevel.Debug, values);
        }

        /// <summary>
        /// Logs a message at the Trace level.
        /// </summary>
        public static void LogTrace(this ILoggingService loggingService, string message)
        {
            loggingService.Log(LogLevel.Trace, message);
        }

        /// <summary>
        /// Logs a message at the Trace level with a formatted string.
        /// </summary>
        public static void LogTrace(this ILoggingService loggingService, string format, params object[] args)
        {
            loggingService.Log(LogLevel.Trace, format, args);
        }

        /// <summary>
        /// Logs an Entity at the Trace level.
        /// </summary>
        public static void LogTrace(this ILoggingService loggingService, Entity entity)
        {
            loggingService.Log(LogLevel.Trace, entity);
        }

        /// <summary>
        /// Logs an EntityReference at the Trace level.
        /// </summary>
        public static void LogTrace(this ILoggingService loggingService, EntityReference entity)
        {
            loggingService.Log(LogLevel.Trace, entity);
        }

        /// <summary>
        /// Logs an Exception at the Trace level.
        /// </summary>
        public static void LogTrace(this ILoggingService loggingService, Exception ex)
        {
            loggingService.Log(LogLevel.Trace, ex);
            loggingService.Log(LogLevel.Trace, ex.StackTrace);
        }

        /// <summary>
        /// Logs an AttributeCollection at the Trace level.
        /// </summary>
        public static void LogTrace(this ILoggingService loggingService, AttributeCollection attributes)
        {
            loggingService.Log(LogLevel.Trace, attributes);
        }

        /// <summary>
        /// Logs a ParameterCollection at the Trace level.
        /// </summary>
        public static void LogTrace(this ILoggingService loggingService, ParameterCollection parameters)
        {
            loggingService.Log(LogLevel.Trace, parameters);
        }

        /// <summary>
        /// Logs a DataCollection at the Trace level.
        /// </summary>
        public static void LogTrace(this ILoggingService loggingService, DataCollection<string> values)
        {
            loggingService.Log(LogLevel.Trace, values);
        }
    }
}
