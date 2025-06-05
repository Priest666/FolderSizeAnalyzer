using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSizeAnalyzer
{
    internal static class FolderAnalyzer
    {
        public static Dictionary<string, long> Analyze(string folderPath)
        {
            var folderSizes = new Dictionary<string, long>();

            foreach (var dir in Directory.GetDirectories(folderPath))
            {
                long size = GetDirectoySize(dir);
                folderSizes[dir] = size;
            }

            // Include top-level files
            long rootFileSize = Directory.GetFiles(folderPath)
                .Select(f => new FileInfo(f).Length).Sum();

            return folderSizes;
        }

        public static long GetDirectoySize(string path)
        {
            long size = 0;

            try
            {
                // File sizes
                foreach (var file in Directory.GetFiles(path))
                {
                    size += new FileInfo(file).Length;
                }

                // Subdirectory sizes
                foreach (var dir in Directory.GetDirectories(path))
                {
                    size += GetDirectoySize(dir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return size;
        }

        public static string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }
}
