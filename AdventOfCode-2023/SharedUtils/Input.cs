namespace SharedUtils;

public class Input
{
    public static string[] ReadAsLines()
    {
        var lines = new List<string>();
        using (var sr = new StreamReader("inputFile.txt"))
        {
            while (sr.ReadLine() is { } line)
            {
                lines.Add(line);
            }
        }

        return lines.ToArray();
    }
    
    
    public static string[] TestInput(string inputFileName = "testFile")
    {
        var lines = new List<string>();
        using (var sr = new StreamReader($"{inputFileName}.txt"))
        {
            while (sr.ReadLine() is { } line)
            {
                lines.Add(line);
            }
        }

        return lines.ToArray();
    }
}