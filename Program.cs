namespace concurrent_file_check;

class Program
{
    static async Task Main(string[] args)
    {
        List<Task<Result>> wordCounts = [];

        foreach (string argument in args)
        {
            wordCounts.Add(CountWords(argument));
        }

        var results = await Task.WhenAll(wordCounts);

        var total = 0;
        foreach (var result in results)
        {
            Console.WriteLine(result.ToString());
            if (result.Error is null)
            {
                total += result.WordCount;
            }
        }

        Console.WriteLine($"Total: {total} words");

    }

    private static async Task<Result> CountWords(string path)
    {
        try
        {
            var text = await File.ReadAllTextAsync(path);
            var wordCount = text.Split((char[]?)null, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Length;
            return new Result(path, wordCount, null);
        }
        catch (Exception e)
        {
            return new Result(path, -1, $"ERROR - {e.Message}");
        }
    }

    private record Result(string Path, int WordCount, string? Error)
    {
        public override string ToString()
        {
            if (Error is null)
            {
                return $"{Path}: {WordCount} words";
            }
            else
            {
                return $"{Path}: {Error}";
            }
        }
    }
}
