using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

namespace RB
{
    public abstract class BaseDebugger
    {
        public static bool useLog = true;

        protected static ILogger _logger = new UnityLog();
        protected static StreamWriter _streamWriter;
        protected static string _filePath;
        protected static Text _debugText;
        protected static Text _debugLogFilePath;

        public static void InitLogFile()
        {
            try
            {
                _debugText = GameObject.Find("DebugText").GetComponent<Text>();
                _debugLogFilePath = GameObject.Find("DebugLogFilePath").GetComponent<Text>();
                _filePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "UnityDebug.txt";
                _debugLogFilePath.text = _filePath;

                WriteToText("attempting writing log file: " + _filePath);

                File.WriteAllText(_filePath, System.DateTime.Now.ToString() + "\n\n");

                WriteToText("log file created: " + _filePath);
            }
            catch (System.Exception e)
            {
                WriteToText("failed to create log file\n" + e);
            }
        }

        public static void WriteToText(object message)
        {
            _debugText.text = message.ToString();

            _streamWriter = File.AppendText(_filePath);
            _streamWriter.WriteLine(message + "\n");
            _streamWriter.Close();
        }

        public static void ShowDebugText(bool show)
        {
            _debugText.enabled = show;
            _debugLogFilePath.enabled = show;
        }
    }
}