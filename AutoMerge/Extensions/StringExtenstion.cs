using System;

namespace AutoMerge.Extensions
{
    public static class StringExtension
    {
        public static string CleanString(this string line)
        {
            var result = line.Replace("\t", " ");
            while(result.IndexOf("  ", StringComparison.Ordinal) >= 0)
            {
                result = result.Replace("  ", " ");
            }

            return result;
        }
    }
}