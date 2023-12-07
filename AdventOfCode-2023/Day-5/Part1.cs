using SharedUtils;

namespace Day_5;

public class Part1 : ISolution
{
    private double Solve(string[] lines)
    {
        var seeds = lines[0]
            .Split(':')[1]
            .SplitAndTrim(' ')
            .Select(uint.Parse)
            .ToArray();

        var maps = ParseMap(lines);

        foreach (var mapLines in maps)
        {
            ProcessMap(mapLines, seeds);
        }

        return seeds.Min();
    }

    private static void ProcessMap(string[] mapLines, uint[] seeds)
    {
        for (var i = 0; i < seeds.Length; i++)
        {
            ProcessSeed(mapLines, seeds, i);
        }
    }

    private static void ProcessSeed(string[] mapLines, uint[] seeds, int seedIndex)
    {
        foreach (var mapLine in mapLines.Skip(1))
        {
            var sections = mapLine.SplitAndTrim(' ');
            var destination = uint.Parse(sections[0]);
            var source = uint.Parse(sections[1]);
            var length = uint.Parse(sections[2]);


            var seed = seeds[seedIndex];
            if (seed >= source && seed < source + length)
            {
                var sourceIndex = seed - source;
                seeds[seedIndex] = destination + sourceIndex;
                return;
            }
        }
    }

    private static List<string[]> ParseMap(string[] lines)
    {
        var maps = new List<string[]>();

        var currentMap = new List<string>();
        foreach (var line in lines.Skip(2))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                maps.Add(currentMap.ToArray());
                currentMap = new List<string>();
                continue;
            }

            currentMap.Add(line);
        }

        maps.Add(currentMap.ToArray());
        return maps;
    }

    public void TestInput(int expectedOutput)
    {
        var inputLines = Input.TestInput();
        var answer = Solve(inputLines);

        Console.Write($"{answer} - {expectedOutput} = {(answer == expectedOutput ? "Correct" : "Incorrect")}");
    }

    public void RealInput()
    {
        var inputLines = Input.ReadAsLines();
        var answer = Solve(inputLines);

        Console.Write($"{answer}");
    }
}