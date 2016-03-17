[![Build status](https://ci.appveyor.com/api/projects/status/d3rppaail9pcj1go?svg=true)](https://ci.appveyor.com/project/joelweiss/log4net-appender-serilog)

#Log4net.Appender.Serilog

Send you log4net messages to serilog

# Installation
```
PM> Install-Package Log4net.Appender.Serilog -pre
```

# Usage

You must first call 
```csharp
Log4net.Appender.Serilog.Configuration.Configure(ILogger logger = null);
```
If logger is left null, it will use the `Log.Logger`


