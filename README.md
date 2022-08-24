[![Build Status](https://github.com/joelweiss/Log4net.Appender.Serilog/actions/workflows/master.yml/badge.svg)](https://github.com/joelweiss/Log4net.Appender.Serilog/actions/workflows/master.yml)
[![NuGet Badge](https://buildstats.info/nuget/Log4net.Appender.Serilog?includePreReleases=true)](https://www.nuget.org/packages/Log4net.Appender.Serilog/)

# Log4net.Appender.Serilog

Send you log4net messages to serilog

# Installation
```
PM> Install-Package Log4net.Appender.Serilog
```

# Usage

You must first call 
```csharp
Log4net.Appender.Serilog.Configuration.Configure(ILogger logger = null);
```
If logger is left null, it will use the `Log.Logger`