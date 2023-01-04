# Serilog.Enrichers.ShortTypeName

Enriches logs with just the class name. This requires the log to already contain the SourceContext property.

Logs with SourceContext "MyAssembly.MyNamespace.MyType", will be enriched with ShortTypeName "MyType".

## Usage

Best used with [Serilog.Extensions.Logging](https://github.com/serilog/serilog-extensions-logging) which will automatically enrich the logs with SourceContext that's required for this enricher.

Set the output template to something like `"[{Timestamp:HH:mm:ss} {Level:u3}] ({ShortTypeName}) {Message:lj}{NewLine}{Exception}"`

## Example

appsettings.json
```json
{
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console", "Serilog.Enrichers.ShortTypeName" ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "System": "Warning",
        "Microsoft": "Warning",
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] ({ShortTypeName}) {Message:lj}{NewLine}{Exception}"
        }
      },
    ],
    "Enrich": [ "WithShortTypeName" ]
  }
}
```

Startup code
```csharp
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var services = new ServiceCollection();
services.AddLogging(loggingBuilder =>
    loggingBuilder.AddSerilog(dispose: true));
```

Usage
```csharp
public class MyClass
{
    private readonly ILogger<MyClass> logger;

    public MyClass(ILogger<MyClass> logger)
    {
        this.logger = logger;
    }

    public void DoSomething()
    {
        this.logger.LogInformation("hello world");
    }
}
```

Outputs:
`[21:14:35 INF] (MyClass) hello world`
