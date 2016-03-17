namespace Log4net.Appender.Serilog
{
    public static class Configuration
    {
        /// <summary>
        /// Configures log4net to log to Serilog.
        /// </summary>
        /// <param name="logger">The serilog logger (if left null Log.Logger will be used).</param>
        public static void Configure(global::Serilog.ILogger logger = null)
        {
            var serilogAppender = new Log4net.Appender.Serilog.SerilogAppender(logger);
            serilogAppender.ActivateOptions();
            var loggerRepository = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
            loggerRepository.Root.AddAppender(serilogAppender);
            loggerRepository.Configured = true;
        }
    }
}
