using Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Logging.Loggers
{
    public class HtmlFileLogger : ILogger
    {
        private readonly string _filepath;

        public HtmlFileLogger(string filepath) => _filepath = EnsureFileExists(filepath);

        private string EnsureFileExists(string path)
        {
            var directoryPath = Path.GetDirectoryName(path);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = Path.GetFileNameWithoutExtension(path);

            path = Path.Combine(directoryPath, $"{fileName}_{DateTime.Now.ToString("HH_mm_ss_fff")}.html");

            using (var fs = new FileStream(path, FileMode.CreateNew))
            using (var sw = new StreamWriter(fs))
            {
                sw.WriteLine("<!DOCTYPE html>");
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("<title>Marathon Manager Time Record Log</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
                sw.Flush();
            }

            return path;
        }

        public void LogError(string message) => Log(message, "red");
        public void LogMessage(string message) => Log(message);
        public void LogSuccess(string message) => Log(message, "green");

        private void Log(string message, string color = "black")
        {
            var html = File.ReadAllLines(_filepath);
            var htmlToWrite = new List<string>();

            var i = 0;
            while (i < html.Length)
            {
                if (html[i] == "</body>")
                {
                    htmlToWrite.Add($"<div style=\"color: {color};\">{message}</div></br>");
                }

                htmlToWrite.Add(html[i]);
                i++;
            }

            using (var fs = new FileStream(_filepath, FileMode.Truncate))
            using (var sw = new StreamWriter(fs))
                foreach (var line in htmlToWrite)
                    sw.WriteLine(line);
        }
    }
}
