# TinyBeans.Logging
The purpose TinyBeans.Logging is to help improve logging without cluttering application logic or using magical components.

## Logging Aspect
The primary component is the ILoggingAspect&lt;T&gt; interface.  Though technically not an aspect since it does require manually calling, it still takes care of logging concerns when calling methods.  Using the ILoggingAspect&lt;T&gt; interface is straight forward.
  
First, add the ILoggingAspect&lt;T&gt; to your IServiceCollection in one of three ways.  Using defaults, supplying the options, or supplying the configuration.
```cs
public class Startup {
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
        services
            .AddTinyLogging();

        services
            .AddTinyLogging(options => {
                // Removed for brevity
            });

        services
            .AddTinyLogging(_configuration.GetSection("TinyLogging"));
    }
}
```

```json
{
  "TinyLogging": {
    "ExecutionLogLevel": "Debug",
    "MethodExecutedTemplateValue": "Executed method {MethodName} on class {ClassName} in assembly {AssemblyName}.",
    "MethodExecutingTemplateValue": "Executing method {MethodName} on class {ClassName} in assembly {AssemblyName}.",
    "ScopeTemplateValue": "{ClassName}.{MethodName} ({AssemblyName})",
    "StateItemsLogLevel": "Trace"
  }
}
```

  
Second, replace your current ILogger&lt;T&gt; dependency with ILoggingAspect&lt;T&gt;.  Don't worry, there is an ILogger&lt;T&gt; property on the ILoggingAspect&lt;T&gt; interface, so you will not need another dependency for additional logging.
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

Third, wrap your method calls using the ILoggingAspect&lt;T&gt;.  The aspect will enforce the correct parameters at compile time.
```cs
//var product = await _sampleManager.SampleMethod(criteria);
var product = await _loggingAspect.InvokeAsync(_sampleManager.SampleMethod, criteria);
```

And with that, pending your log levels allow for it, your application will now log when a method is executing and has executed, along with adding a scope indicating the method you are in.

## But wait, there's more!
The ILoggingAspect&lt;T&gt; also uses an ILoggableParser which, if your method parameters and results are decorated with the LoggableAttribute, will add a Dictionary&lt;string, object&gt; scope of all the properties when logging method executing and executed.  If the LoggableAttribute is supplied on the class, you can omit properties with the OmitAttribute.  If you wish to substitute the property value with a different one, use the ReplaceAttribute.  Replacement values do not need to match the property type.
```cs
[Loggable]
public class SampleCriteria {
    public Guid Id { get; set; }
    public string EmailAddress { get; set; }

    [Replace("USERNAME EXCLUDED")]
    public string Username { get; set; }

    [Omit]
    public string Password { get; set; }
}
```

## Notes
This component was designed with a specific purpose in mind, that being to aid in logging between layers in an application written around the iDesign architecture pattern.  It was also designed with the intention that calls into other layers would be done with strongly typed classes and not just strings or value types.  I have not tested the results of passing strings or value types, nor is it a scenario I intend to support.