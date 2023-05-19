using NLog;
using Services.Abstract;

namespace Services.Concrete
{
    public class LoggerManager : ILoggerService
    {
        //staticle logger ifadesi bir kere üretildi mi herkes bunu kullanabilsin
        private static ILogger _logger=LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
           _logger.Debug(message);
        }

        public void LogError(string message)
        {
          _logger.Error(message);
        }

        public void LogInfo(string message)
        {
           _logger.Info(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }
    }
}
