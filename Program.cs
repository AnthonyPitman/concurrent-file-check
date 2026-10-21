namespace concurrent_file_check;

class Program
{
    static async Task Main(string[] args)
    {
        List<Task<int>> wordCounts = [];

        foreach (string argument in args)
        {
            wordCounts.Add(CountWords(argument));
        }

        var result = await Task.WhenAll(wordCounts);

        var total = 0;
        for (var i = 0; i < args.Length; i++)
        {
            var currentPath = args[i];
            var wordCount = result[i];
            if (wordCount > -1)
            {
                Console.WriteLine($"{currentPath}: {wordCount} words");
                total += wordCount;
            }
            else if (wordCount == -1)
                Console.WriteLine($"{currentPath}: ERROR - file not found");
            else
                Console.WriteLine($"{currentPath}: ERROR - file could not be read");
        }
        Console.WriteLine($"Total: {total} words");

    }

    private static async Task<int> CountWords(string path)
    {
        if (!Path.Exists(path))
        {
            return -1;
        }

        try
        {
            var text = await File.ReadAllTextAsync(path);
            var wordCount = text.Split([' ', '\n', '\t'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Length;
            return wordCount;
        }
        catch (Exception)
        {
            return -2;
        }
    }
}
