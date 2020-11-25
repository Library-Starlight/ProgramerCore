using System;
using System.Collections.Generic;
using System.Text;

namespace Log
{
    /// <summary>
    /// Logs to a specific file
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly FileManager _fileManager = new FileManager();

        #region Public Properties

        /// <summary>
        /// The path to write the log file to 
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// If true, logs the current time with each message
        /// </summary>
        public bool LogTime { get; set; } = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filePath">The path to log to </param>
        public FileLogger(string filePath)
        {
            FilePath = filePath;
        }

        #endregion

        #region Logger Methods

        /// <summary>
        /// Handles the log message being passed in
        /// </summary>
        /// <param name="message">The message being log</param>
        /// <param name="level">The level of the log message</param>
        public void Log(string message, LogLevel level)
        {
            // Get current time
            var currentTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            // Prepend the time to the log if desired
            var timeLogString = LogTime ? $"[{currentTime}] " : string.Empty;

            // Write the message to the log file
            _fileManager.WriteAllTextToFileAsync($"{timeLogString} {level.ToString()} {message}{Environment.NewLine}", FilePath, append: true);
        }

        #endregion
    }
}
