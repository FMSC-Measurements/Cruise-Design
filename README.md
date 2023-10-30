# Cruise Design

For deployment create a pull request using the default pull request template (located at /.github/pull_request_template.md) and follow checklist


# Core Components
This application uses Microsoft.Extensions.DependencyInjection.IServiceLocator to facilitate dependency injection. 
The ServiceProvider provides access to registered services and automatically supplies services to other registered services
that require them as defined by their constructors. To register new services see the `Program.ConfigureServices` method.
The ServiceProvider instance can be accessed via the static property `Program.ServiceProvider` however, if a class requires 
access to the ServiceProvider it is preferred to request it by adding `IServiceProvider` as a parameter for the class's constructor.
For more information on using the Service  Provider see [Microsoft's Tutorial on dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage)

# Logging
Logging has been configured through the for mentioned Service Provider. 
When requesting a logger you should specify what class the logger will be logging for
by specifying the class as a type parameter. For example creating a logger for a class `ClassA`
you would request a logger as `ILogger<ClassA>`
For more information see [Logging in .net](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging)


# Build

## Code Template Files (.cst)
.cst files are code template files that will generate a .local.cs file on build.
Placeholder text in the format of `$(somePlaceHolder)`  are replace with values 
of the same name defined as Properties in the project file or as environment variables.
