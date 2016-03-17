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


