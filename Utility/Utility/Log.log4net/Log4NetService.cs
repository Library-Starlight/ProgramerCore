using log4net;
using System;
using System.IO;
using System.Reflection;

namespace Log4Demo
{
    /// <summary>
    ///     log4net 日记服务
    /// </summary>
    public class Log4NetService
    {
        private ILog log;

        //private static string configPath = Path.Combine(AlarmCenter.Core.General.GetApplicationRootPath(), "data\\alarmcenter", "log.xml");

        private static string configPath = Path.Combine(new FileInfo(Assembly.GetAssembly(typeof(Log4NetService)).Location).Directory.FullName, "log.xml");
        
        /// <summary>
        /// 构造一个log4net 日记服务
        /// </summary>
        /// <param name="logName">log4net 配置名称</param>
        public Log4NetService(string logName)
        {
            log = log4net.LogManager.GetLogger(logName);
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(configPath));
        }
        /// <summary>
        ///     Log debug message
        /// </summary>
        /// <param name="message">The debug message</param>
        /// <param name="args">the message argument values</param>
        public void Debug(string message, params object[] args)
        {
            log.Debug(string.Format(message, args));
        }

        /// <summary>
        ///     Log debug message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="exception">Exception to write in debug message</param>
        /// <param name="args">The argument values of message</param>
        public void Debug(string message, Exception exception, params object[] args)
        {
            log.Debug(string.Format(message, args), exception);
        }

        /// <summary>
        ///     Log FATAL error
        /// </summary>
        /// <param name="message">The message of fatal error</param>
        /// <param name="args">The argument values of message</param>
        public void Fatal(string message, params object[] args)
        {
            log.Fatal(string.Format(message, args));
        }

        /// <summary>
        ///     log FATAL error
        /// </summary>
        /// <param name="message">The message of fatal error</param>
        /// <param name="exception">The exception to write in this fatal message</param>
        /// <param name="args">The argument values of message</param>
        public void Fatal(string message, Exception exception, params object[] args)
        {
            log.Fatal(string.Format(message, args), exception);
        }

        /// <summary>
        ///     Log message information
        /// </summary>
        /// <param name="message">The information message to write</param>
        /// <param name="args">The arguments values</param>
        public void Info(string message, params object[] args)
        {
            log.Info(string.Format(message, args));
        }

        /// <summary>
        ///     Log warning message
        /// </summary>
        /// <param name="message">The warning message to write</param>
        /// <param name="args">The argument values</param>
        public void Warn(string message, params object[] args)
        {
            log.Warn(string.Format(message, args));
        }

        /// <summary>
        ///     Log error message
        /// </summary>
        /// <param name="message">The error message to write</param>
        /// <param name="args">The arguments values</param>
        public void Error(string message, params object[] args)
        {
            log.Error(string.Format(message, args));
        }

        /// <summary>
        ///     Log an exception message
        /// </summary>
        /// <param name="exception">exception</param>
        public void Error(Exception exception)
        {
            log.Error(null, exception);
        }

        /// <summary>
        ///     Log error message
        /// </summary>
        /// <param name="message">The error message to write</param>
        /// <param name="exception">The exception associated with this error</param>
        /// <param name="args">The arguments values</param>
        public void Error(string message, Exception exception, params object[] args)
        {
            log.Error(string.Format(message, args), exception);
        }
    }
}
