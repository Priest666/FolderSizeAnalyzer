namespace FolderSizeAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter folder path to analyze: ");
                string folderPath = Console.ReadLine() ?? "";

                if (!Directory.Exists(folderPath))
                {
                    Console.WriteLine("Invalid path. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("\nAnalyzing folder sizes...\n");

                try
                {
                    var folderSizes = FolderAnalyzer.Analyze(folderPath);
                    var sorted = folderSizes.OrderByDescending(kv => kv.Value);

                    foreach (var entry in sorted)
                    {
                        Console.WriteLine($"{FolderAnalyzer.FormatSize(entry.Value),12} - {Path.GetFileName(entry.Key)}");
                    }

                    long totalSize = folderSizes.Values.Sum();
                    Console.WriteLine($"\nTotal Size: {FolderAnalyzer.FormatSize(totalSize)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while analyzing: {ex.Message}");
                }

                Console.Write("\nDo you want to analyze another folder? (y/n): ");
                string answer = Console.ReadLine()?.Trim().ToLower() ?? "";
                if (answer != "y" && answer != "yes")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
            }
        }
    }
}
