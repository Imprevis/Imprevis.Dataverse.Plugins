namespace Imprevis.Dataverse.Plugins
{
    using Microsoft.Xrm.Sdk;
    using System;

    public static class LoggingServiceExtensions
    {
        public static void LogDebug(this ILoggingService loggingService, string message)
        {
            loggingService.Log(LogLevel.Debug, message);
        }

        public static void LogDebug(this ILoggingService loggingService, string format, params object[] args)
        {
            loggingService.Log(LogLevel.Debug, format, args);
        }

        public static void LogDebug(this ILoggingService loggingService, Entity entity)
        {
            loggingService.Log(LogLevel.Debug, entity);
        }

        public static void LogDebug(this ILoggingService loggingService, EntityReference entity)
        {
            loggingService.Log(LogLevel.Debug, entity);
        }

        public static void LogDebug(this ILoggingService loggingService, Exception ex)
        {
            loggingService.Log(LogLevel.Debug, ex);
        }

        public static void LogDebug(this ILoggingService loggingService, AttributeCollection attributes)
        {
            loggingService.Log(LogLevel.Debug, attributes);
        }

        public static void LogDebug(this ILoggingService loggingService, ParameterCollection parameters)
        {
            loggingService.Log(LogLevel.Debug, parameters);
        }

        public static void LogDebug(this ILoggingService loggingService, DataCollection<string> values)
        {
            loggingService.Log(LogLevel.Debug, values);
        }

        public static void LogTrace(this ILoggingService loggingService, string message)
        {
            loggingService.Log(LogLevel.Trace, message);
        }

        public static void LogTrace(this ILoggingService loggingService, string format, params object[] args)
        {
            loggingService.Log(LogLevel.Trace, format, args);
        }

        public static void LogTrace(this ILoggingService loggingService, Entity entity)
        {
            loggingService.Log(LogLevel.Trace, entity);
        }

        public static void LogTrace(this ILoggingService loggingService, EntityReference entity)
        {
            loggingService.Log(LogLevel.Trace, entity);
        }

        public static void LogTrace(this ILoggingService loggingService, Exception ex)
        {
            loggingService.Log(LogLevel.Trace, ex);
        }

        public static void LogTrace(this ILoggingService loggingService, AttributeCollection attributes)
        {
            loggingService.Log(LogLevel.Trace, attributes);
        }

        public static void LogTrace(this ILoggingService loggingService, ParameterCollection parameters)
        {
            loggingService.Log(LogLevel.Trace, parameters);
        }

        public static void LogTrace(this ILoggingService loggingService, DataCollection<string> values)
        {
            loggingService.Log(LogLevel.Trace, values);
        }
    }
}
