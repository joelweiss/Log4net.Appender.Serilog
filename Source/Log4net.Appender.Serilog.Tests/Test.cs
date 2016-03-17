using System;
using System.Collections.Generic;
using Xunit;
using Serilog.Events;
using Serilog;

namespace Log4net.Appender.Serilog.Tests
{
    public class Test
    {
        [Fact]
        public void TestForwardsToSerilog()
        {
            Log4net.Appender.Serilog.Configuration.Configure();
            var log = log4net.LogManager.GetLogger("TypeName");

            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Verbose()
                 .WriteTo.Sink(sink)
                 .CreateLogger();

            log.Debug("Debug");
            log.DebugFormat("Debug {0}", "Param0");
            log.Info("Info");
            log.DebugFormat("Info {0}", "Param0");
            log.Warn("Warn");
            log.DebugFormat("Warn {0}", "Param0");
            log.Error("Error");
            log.Error("Error", new Exception());
            log.DebugFormat("Error {0}", "Param0");
            log.Fatal("Fatal");
            log.DebugFormat("Fatal {0}", "Param0");


            Assert.Equal(11, events.Count);
            Assert.Equal(events[1].RenderMessage(), "Debug \"Param0\"");
            Assert.NotNull(events[7].Exception);
        }
    }
}
