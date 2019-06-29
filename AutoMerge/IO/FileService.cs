using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Reflection;

namespace AutoMerge.IO
{
    public static class FileService
    {
        public static string[] ReadFile(string path)
        {
            string line;
            var source = new List<string>();
            var file = new System.IO.StreamReader(path);  
            while((line = file.ReadLine()) != null)  
            {
                source.Add(line);
            }
            file.Close();
            return source.ToArray();
        }

        public static void WriteFile(string[] result)
        {
            System.IO.File.WriteAllLines(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/result.txt", result);
        }
    }
}