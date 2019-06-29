using System;
using AutoMerge.IO;
using AutoMerge.Merger;

namespace AutoMerge
{
    public class Program
    {
        static void Main(string[] args)
        {
            var source = FileService.ReadFile(args[0]);
            var changeA = FileService.ReadFile(args[1]);
            var changeB = FileService.ReadFile(args[2]);
            var result = MergeService.ThreeWayMerge(source, changeA, changeB);
            FileService.WriteFile(result);
        }
    }
}