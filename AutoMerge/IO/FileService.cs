using System.Collections.Generic;

namespace AutoMerge.IO
{
    public static class FileService
    {
        public static IEnumerable<string> ReadFile(string path)
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
    }
}