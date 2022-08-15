using log4net.Core;
using log4net.Util;
using Serilog.Events;

namespace Log4net.Appender.Serilog
{
    public class SerilogAppender : log4net.Appender.AppenderSkeleton
    {
        private readonly global::Serilog.ILogger _Logger;

        public SerilogAppender()
        { }

        public SerilogAppender(global::Serilog.ILogger logger = null)
        {
            _Logger = logger;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            string source = loggingEvent.LoggerName;
            LogEventLevel serilogLevel = ConvertLevel(loggingEvent.Level);
            string template = null;
            object[] parameters = null;

            if (loggingEvent.MessageObject is SystemStringFormat systemStringFormat)
            {
                template = systemStringFormat.Format;
                parameters = systemStringFormat.Args;
            }
            else
            {
                template = loggingEvent.MessageObject?.ToString();
            }

            var logger = (_Logger ?? global::Serilog.Log.Logger).ForContext(global::Serilog.Core.Constants.SourceContextPropertyName, source);
#pragma warning disable Serilog004 // Constant MessageTemplate verifier
            logger.Write(serilogLevel, loggingEvent.ExceptionObject, template, parameters);
#pragma warning restore Serilog004 // Constant MessageTemplate verifier
        }

        private static LogEventLevel ConvertLevel(Level log4netLevel)
        {
            if (log4netLevel == Level.Verbose)
            {
                return LogEventLevel.Verbose;
            }
            if (log4netLevel == Level.Debug)
            {
                return LogEventLevel.Debug;
            }
            if (log4netLevel == Level.Info)
            {
                return LogEventLevel.Information;
            }
            if (log4netLevel == Level.Warn)
            {
                return LogEventLevel.Warning;
            }
            if (log4netLevel == Level.Error)
            {
                return LogEventLevel.Error;
            }
            if (log4netLevel == Level.Fatal)
            {
                return LogEventLevel.Fatal;
            }
            global::Serilog.Debugging.SelfLog.WriteLine("Unexpected log4net logging level ({0}) logging as Information", log4netLevel.DisplayName);
            return LogEventLevel.Information;
        }
    }
}