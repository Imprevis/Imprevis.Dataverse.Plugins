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
        /// Logs a message at the Information level.
        /// </summary>
        public static void LogInformation(this ILoggingService loggingService, string message)
        {
            loggingService.Log(LogLevel.Information, message);
        }

        /// <summary>
        /// Logs a message at the Information level with a formatted string.
        /// </summary>
        public static void LogInformation(this ILoggingService loggingService, string format, params object[] args)
        {
            loggingService.Log(LogLevel.Information, format, args);
        }

        /// <summary>
        /// Logs an Entity at the Information level.
        /// </summary>
        public static void LogInformation(this ILoggingService loggingService, Entity entity)
        {
            loggingService.Log(LogLevel.Information, entity);
        }

        /// <summary>
        /// Logs an EntityReference at the Information level.
        /// </summary>
        public static void LogInformation(this ILoggingService loggingService, EntityReference entity)
        {
            loggingService.Log(LogLevel.Information, entity);
        }

        /// <summary>
        /// Logs an Exception at the Information level.
        /// </summary>
        public static void LogInformation(this ILoggingService loggingService, Exception ex)
        {
            loggingService.Log(LogLevel.Information, ex);
            loggingService.Log(LogLevel.Information, ex.StackTrace);
        }

        /// <summary>
        /// Logs an AttributeCollection at the Information level.
        /// </summary>
        public static void LogInformation(this ILoggingService loggingService, AttributeCollection attributes)
        {
            loggingService.Log(LogLevel.Information, attributes);
        }

        /// <summary>
        /// Logs a ParameterCollection at the Information level.
        /// </summary>
        public static void LogInformation(this ILoggingService loggingService, ParameterCollection parameters)
        {
            loggingService.Log(LogLevel.Information, parameters);
        }

        /// <summary>
        /// Logs a DataCollection at the Information level.
        /// </summary>
        public static void LogInformation(this ILoggingService loggingService, DataCollection<string> values)
        {
            loggingService.Log(LogLevel.Information, values);
        }

        /// <summary>
        /// Logs a message at the Warning level.
        /// </summary>
        public static void LogWarning(this ILoggingService loggingService, string message)
        {
            loggingService.Log(LogLevel.Warning, message);
        }

        /// <summary>
        /// Logs a message at the Warning level with a formatted string.
        /// </summary>
        public static void LogWarning(this ILoggingService loggingService, string format, params object[] args)
        {
            loggingService.Log(LogLevel.Warning, format, args);
        }

        /// <summary>
        /// Logs an Entity at the Warning level.
        /// </summary>
        public static void LogWarning(this ILoggingService loggingService, Entity entity)
        {
            loggingService.Log(LogLevel.Warning, entity);
        }

        /// <summary>
        /// Logs an EntityReference at the Warning level.
        /// </summary>
        public static void LogWarning(this ILoggingService loggingService, EntityReference entity)
        {
            loggingService.Log(LogLevel.Warning, entity);
        }

        /// <summary>
        /// Logs an Exception at the Warning level.
        /// </summary>
        public static void LogWarning(this ILoggingService loggingService, Exception ex)
        {
            loggingService.Log(LogLevel.Warning, ex);
            loggingService.Log(LogLevel.Warning, ex.StackTrace);
        }

        /// <summary>
        /// Logs an AttributeCollection at the Warning level.
        /// </summary>
        public static void LogWarning(this ILoggingService loggingService, AttributeCollection attributes)
        {
            loggingService.Log(LogLevel.Warning, attributes);
        }

        /// <summary>
        /// Logs a ParameterCollection at the Warning level.
        /// </summary>
        public static void LogWarning(this ILoggingService loggingService, ParameterCollection parameters)
        {
            loggingService.Log(LogLevel.Warning, parameters);
        }

        /// <summary>
        /// Logs a DataCollection at the Warning level.
        /// </summary>
        public static void LogWarning(this ILoggingService loggingService, DataCollection<string> values)
        {
            loggingService.Log(LogLevel.Warning, values);
        }

        /// <summary>
        /// Logs a message at the Error level.
        /// </summary>
        public static void LogError(this ILoggingService loggingService, string message)
        {
            loggingService.Log(LogLevel.Error, message);
        }

        /// <summary>
        /// Logs a message at the Error level with a formatted string.
        /// </summary>
        public static void LogError(this ILoggingService loggingService, string format, params object[] args)
        {
            loggingService.Log(LogLevel.Error, format, args);
        }

        /// <summary>
        /// Logs an Entity at the Error level.
        /// </summary>
        public static void LogError(this ILoggingService loggingService, Entity entity)
        {
            loggingService.Log(LogLevel.Error, entity);
        }

        /// <summary>
        /// Logs an EntityReference at the Error level.
        /// </summary>
        public static void LogError(this ILoggingService loggingService, EntityReference entity)
        {
            loggingService.Log(LogLevel.Error, entity);
        }

        /// <summary>
        /// Logs an Exception at the Error level.
        /// </summary>
        public static void LogError(this ILoggingService loggingService, Exception ex)
        {
            loggingService.Log(LogLevel.Error, ex);
            loggingService.Log(LogLevel.Error, ex.StackTrace);
        }

        /// <summary>
        /// Logs an AttributeCollection at the Error level.
        /// </summary>
        public static void LogError(this ILoggingService loggingService, AttributeCollection attributes)
        {
            loggingService.Log(LogLevel.Error, attributes);
        }

        /// <summary>
        /// Logs a ParameterCollection at the Error level.
        /// </summary>
        public static void LogError(this ILoggingService loggingService, ParameterCollection parameters)
        {
            loggingService.Log(LogLevel.Error, parameters);
        }

        /// <summary>
        /// Logs a DataCollection at the Error level.
        /// </summary>
        public static void LogError(this ILoggingService loggingService, DataCollection<string> values)
        {
            loggingService.Log(LogLevel.Error, values);
        }

        /// <summary>
        /// Logs a message at the Critical level.
        /// </summary>
        public static void LogCritical(this ILoggingService loggingService, string message)
        {
            loggingService.Log(LogLevel.Critical, message);
        }

        /// <summary>
        /// Logs a message at the Critical level with a formatted string.
        /// </summary>
        public static void LogCritical(this ILoggingService loggingService, string format, params object[] args)
        {
            loggingService.Log(LogLevel.Critical, format, args);
        }

        /// <summary>
        /// Logs an Entity at the Critical level.
        /// </summary>
        public static void LogCritical(this ILoggingService loggingService, Entity entity)
        {
            loggingService.Log(LogLevel.Critical, entity);
        }

        /// <summary>
        /// Logs an EntityReference at the Critical level.
        /// </summary>
        public static void LogCritical(this ILoggingService loggingService, EntityReference entity)
        {
            loggingService.Log(LogLevel.Critical, entity);
        }

        /// <summary>
        /// Logs an Exception at the Critical level.
        /// </summary>
        public static void LogCritical(this ILoggingService loggingService, Exception ex)
        {
            loggingService.Log(LogLevel.Critical, ex);
            loggingService.Log(LogLevel.Critical, ex.StackTrace);
        }

        /// <summary>
        /// Logs an AttributeCollection at the Critical level.
        /// </summary>
        public static void LogCritical(this ILoggingService loggingService, AttributeCollection attributes)
        {
            loggingService.Log(LogLevel.Critical, attributes);
        }

        /// <summary>
        /// Logs a ParameterCollection at the Critical level.
        /// </summary>
        public static void LogCritical(this ILoggingService loggingService, ParameterCollection parameters)
        {
            loggingService.Log(LogLevel.Critical, parameters);
        }

        /// <summary>
        /// Logs a DataCollection at the Critical level.
        /// </summary>
        public static void LogCritical(this ILoggingService loggingService, DataCollection<string> values)
        {
            loggingService.Log(LogLevel.Critical, values);
        }
    }
}
