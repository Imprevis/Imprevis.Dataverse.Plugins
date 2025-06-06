namespace Imprevis.Dataverse.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.PluginTelemetry;

    internal class LoggingService : ILoggingService
    {
        private readonly ITracingService tracingService;
        private readonly ILogger logger;

        public LoggingService(ITracingService tracingService, ILogger logger = null)
        {
            this.tracingService = tracingService;
            this.logger = logger;
        }

        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        public void Log(LogLevel level, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (level < LogLevel)
            {
                return;
            }

            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

            tracingService.Trace($"[{timestamp}] {message}");
            logger?.LogTrace(message);
        }

        public void Log(LogLevel level, string format, params object[] args)
        {
            Log(level, string.Format(format, args));
        }

        public void Log(LogLevel level, EntityReference entity)
        {
            if (entity == null)
            {
                Log(level, "EntityReference is null.");
                return;
            }

            Log(level, "Entity: {0} : {1}", entity.LogicalName, entity.Id);
        }

        public void Log(LogLevel level, Entity entity)
        {
            if (entity == null)
            {
                Log(level, "Entity is null.");
                return;
            }

            Log(level, "Entity = {0} : {1}", entity.LogicalName, entity.Id);
            Log(level, entity.Attributes);
        }

        public void Log(LogLevel level, Exception ex)
        {
            if (ex == null)
            {
                Log(level, "Exception is null.");
                return;
            }

            Log(level, "Exception = {0}", ex.Message);

            if (ex.InnerException != null)
            {
                Log(level, ex.InnerException);
            }
        }

        public void Log(LogLevel level, AttributeCollection attributes)
        {
            Log(level, "Attributes");

            foreach (var attribute in attributes.OrderBy(x => x.Key))
            {
                LogValue(level, attribute.Key, attribute.Value);
            }
        }

        public void Log(LogLevel level, ParameterCollection parameters)
        {
            Log(level, "Parameters");

            foreach (var parameter in parameters.OrderBy(x => x.Key))
            {
                LogValue(level, parameter.Key, parameter.Value);
            }
        }

        public void Log(LogLevel level, DataCollection<string> values)
        {
            var attributes = (AttributeCollection)values.Zip(values, (k, v) => new KeyValuePair<string, object>(k, v));
            Log(level, attributes);
        }

        private void LogValue(LogLevel level, string key, object value)
        {
            switch (value)
            {
                case AliasedValue av:
                    LogValue(level, key, av.Value);
                    return;
                case Entity e:
                    LogKeyValue(level, key, $"{e.LogicalName} : {e.Id:D}");
                    return;
                case EntityReference er:
                    LogKeyValue(level, key, $"{er.LogicalName} : {er.Id:D}");
                    return;
                case Money m:
                    LogKeyValue(level, key, m.Value);
                    return;
                case OptionSetValue osv:
                    LogKeyValue(level, key, osv.Value);
                    return;
                default:
                    LogKeyValue(level, key, value);
                    return;
            }
        }

        private void LogKeyValue(LogLevel level, string key, object value)
        {
            Log(level, " - {0} = {1}", key, value);
        }
    }
}
