/// <summary>
/// 
/// </summary>
namespace Log4net.Appender.Serilog
{
    public static class Configuration
    {
        public static void Configure()
        {
            var serilogAppender = new Log4net.Appender.Serilog.SerilogAppender();
            serilogAppender.ActivateOptions();
            var loggerRepository = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
            loggerRepository.Root.AddAppender(serilogAppender);
            loggerRepository.Configured = true;
        }
    }
}
