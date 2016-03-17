using log4net.Core;
using log4net.Util;
using Serilog.Events;
using System;
using System.Linq.Expressions;
using System.Reflection;
using Serilog;

namespace Log4net.Appender.Serilog
{
    public class SerilogAppender : log4net.Appender.AppenderSkeleton
    {
        private static readonly Func<SystemStringFormat, string> _FormatGetter;
        private static readonly Func<SystemStringFormat, object[]> _ArgumentsGetter;
        private readonly global::Serilog.ILogger _Logger;

        static SerilogAppender()
        {
            _FormatGetter = GetFieldAccessor<SystemStringFormat, string>("m_format");
            _ArgumentsGetter = GetFieldAccessor<SystemStringFormat, object[]>("m_args");
        }

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

            SystemStringFormat systemStringFormat = loggingEvent.MessageObject as SystemStringFormat;
            if (systemStringFormat != null)
            {
                template = _FormatGetter(systemStringFormat);
                parameters = _ArgumentsGetter(systemStringFormat);
            }
            else
            {
                template = loggingEvent.MessageObject?.ToString();
            }

            var logger = (_Logger ?? global::Serilog.Log.Logger).ForContext(global::Serilog.Core.Constants.SourceContextPropertyName, source);
            logger.Write(serilogLevel, loggingEvent.ExceptionObject, template, parameters);
        }

        static LogEventLevel ConvertLevel(Level log4netLevel)
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

        //taken from http://rogeralsing.com/2008/02/26/linq-expressions-access-private-fields/
        public static Func<T, TField> GetFieldAccessor<T, TField>(string fieldName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "arg");
            MemberExpression member = Expression.Field(param, fieldName);
            LambdaExpression lambda = Expression.Lambda(typeof(Func<T, TField>), member, param);
            Func<T, TField> compiled = (Func<T, TField>)lambda.Compile();
            return compiled;
        }
    }
}
