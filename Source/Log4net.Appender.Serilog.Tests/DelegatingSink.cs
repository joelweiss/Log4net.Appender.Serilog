//Taken from Serilog.Tests.Support.DelegatingSink.cs
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;

namespace Log4net.Appender.Serilog.Tests
{
    public class DelegatingSink : ILogEventSink
    {
        readonly Action<LogEvent> _write;

        public DelegatingSink(Action<LogEvent> write)
        {
            if (write == null)
            {
                throw new ArgumentNullException(nameof(write));
            }
            _write = write;
        }

        public void Emit(LogEvent logEvent)
        {
            _write(logEvent);
        }

        public static LogEvent GetLogEvent(Action<ILogger> writeAction)
        {
            LogEvent result = null;
            var logger = new LoggerConfiguration()
                .WriteTo.Sink(new DelegatingSink(le => result = le))
                .CreateLogger();

            writeAction(logger);
            return result;
        }
    }
}
