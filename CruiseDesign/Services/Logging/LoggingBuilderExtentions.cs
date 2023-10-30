using Microsoft.Extensions.Logging;

namespace CruiseDesign.Services.Logging
{
    public static class LoggingBuilderExtentions
    {
        public static void AddAppCenterLogger(this ILoggingBuilder builder)
        {
            builder.AddProvider(new AppCenterLoggerProvider());
        }
    }
}