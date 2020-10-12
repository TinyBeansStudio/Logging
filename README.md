# TinyBeans.Logging
The intended use of TinyBeans.Logging is to help improve logging without cluttering application logic or using magical components.  The primary component is the ILoggingAspect<T> interface.  Though technically not an aspect since it does require manually calling, it still takes care of logging concerns when calling methods.  Using the ILoggingAspect<T> interface is straight forward.
  
First, add the ILoggingAspect<T> to your IServiceCollection in one of three ways.  Using defaults, supplying the options, or supplying the configuration.
```cs
public class Startup {
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
        services
            .AddLoggingAspect();

        services
            .AddLoggingAspect(options => {
                // Removed for brevity
            });

        services
            .AddLoggingAspect(_configuration.GetSection("LoggingAspect"));
    }
}
```

```json
{
  "LoggingAspect": {
    "ExecutionLogLevel": "Debug",
    "MethodExecutedTemplate": "Executed method {MethodName} on class {ClassName} in assembly {AssemblyName}.",
    "MethodExecutingTemplate": "Executing method {MethodName} on class {ClassName} in assembly {AssemblyName}.",
    "ScopeTemplate": "{ClassName}.{MethodName} ({AssemblyName})",
    "StateItemsLogLevel": "Trace"
  }
}
```

  
Second, replace your current ILogger<T> dependency with ILoggingAspect<T>.
```cs
[Route("/sample")]
public class SampleController : ControllerBase {
    private readonly ILoggingAspect<SampleController> _loggingAspect;

    public SampleController(ILoggingAspect<SampleController> loggingAspect) {
        _loggingAspect = loggingAspect;
    }

    // Removed for brevity
}
```

Third, wrap your method calls using the ILoggingAspect<T>.  The aspect will enforce the correct parameters at compile time.
```cs
//var product = await _sampleManager.SampleMethod(criteria);
var product = await _loggingAspect.InvokeAsync(_sampleManager.SampleMethod, criteria);
```

And with that, pending your log levels allow for it, your application will now log when a method is executing and has executed, along with adding a scope indicating the method you are in.  But wait, there's more!

The ILoggingAspect<T> also uses an ILoggableParser which, if your method parameters and results are decorated with the LoggableAttribute, will add a Dictionary<string, object> scope of all the properties when logging method executing and executed.  If the LoggableAttribute is supplied on the class, you can omit properties with the SensitiveAttribute.
```cs
[Loggable]
public class SampleCriteria {
    public Guid Id { get; set; }
    public string EmailAddress { get; set; }

    [Sensitive(ReplacementValue = "USERNAME EXCLUDED")]
    public string Username { get; set; }

    [Sensitive]
    public string Password { get; set; }
}
```

If the SensitiveAttribute is supplied without a replacement value, the property will be omitted.  If a replacement value is specified, it will replace the value when added to the Dictionary<string, object> scope.
