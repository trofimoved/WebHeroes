using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebHeroes.Code
{
    public static class Logger
    {
        private static void WriteToLog(string str)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + $"Logs\\BatleLog_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}.txt";
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(str);
                sw.WriteLine("*------*");
            }
        }
        public static void Write(string str)
        {
            WriteToLog(str);
        }
    }
}