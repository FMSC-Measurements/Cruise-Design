using Microsoft.Extensions.Logging;

namespace CruiseDesign.Services.Logging
{
    public class AppCenterLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new AppCenterLogger(categoryName);

        public void Dispose()
        { /* do nothing */ }
    }
}