﻿using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Log
{
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Writes the text to the specified file
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="path">The path of the file to write to</param>
        /// <param name="append">If true, write the text to the end of the file, otherwise overrides any existing file</param>
        /// <returns></returns>
        public async Task WriteAllTextToFileAsync(string text, string path, bool append = false)
        {
            // Normalize  path
            path = NormalizePath(path);

            // Resolve to absolute path
            path = ResolvePath(path);

            // Lock the task 
            await AsyncAwaiter.AwaitAsync(nameof(FileManager) + path, async () =>
            {
                // Run the synchronous file access as a new task
                await Task.Run(() =>
                {
                    // Write the log message to file
                    using (var fileStream = (TextWriter)new StreamWriter(File.Open(path, append ? FileMode.Append : FileMode.Create, FileAccess.Write)))
                    {
                        fileStream.Write(text);
                    }
                });
            });
        }

        /// <summary>
        /// Normalizing a path based on the curretn operating system
        /// For Windows, replace forward slash with backslash. For Mac, replace backslash with forward slash.
        /// </summary>
        /// <param name="path">The path to normalize</param>
        /// <returns></returns>
        public string NormalizePath(string path)
        {
            // If on Windows...
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                // Replace any / with \
                return path?.Replace('/', '\\').Trim();
            // If on Linux or Mac
            else
                // Replace any \ with /
                return path?.Replace('\\', '/').Trim();
        }

        /// <summary>
        /// Resolves any relative elements of the path to absolute
        /// </summary>
        /// <param name="path">The path to resolve</param>
        /// <returns></returns>
        public string ResolvePath(string path)
        {
            // Resolve the path to absolute
            return Path.GetFullPath(path);
        }
    }
}