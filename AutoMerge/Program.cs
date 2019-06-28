using System;
using AutoMerge.IO;

namespace AutoMerge
{
    public class Program
    {
        static void Main(string[] args)
        {
            var source = FileService.ReadFile(args[0]);
            var changeA = FileService.ReadFile(args[1]);
            var changeB = FileService.ReadFile(args[2]);

            foreach (string s in source)
            {
                Console.WriteLine(s);
            }
        }
    }
}