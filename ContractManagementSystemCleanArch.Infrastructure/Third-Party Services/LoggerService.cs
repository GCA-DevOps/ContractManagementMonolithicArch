using Microsoft.Extensions.Logging;


namespace CMS.Infrastructure.Services
{
    public class LoggerService : ILogger
    {
        private readonly ILogger<ILogger> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return _logger.BeginScope(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger?.IsEnabled(logLevel)?? false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _logger.LogTrace(eventId, message);
                    break;
                case LogLevel.Debug:
                    _logger.LogDebug(eventId, message);
                    break;
                case LogLevel.Information:
                    _logger.LogInformation(eventId, message);
                    break;
                case LogLevel.Warning:
                    _logger.LogWarning(eventId, message);
                    break;
                case LogLevel.Error:
                    _logger.LogError(eventId, message, exception);
                    break;
                case LogLevel.Critical:
                    _logger.LogCritical(eventId, message, exception);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

    }
}
