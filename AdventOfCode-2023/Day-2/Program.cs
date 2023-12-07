using SharedUtils;

namespace Day_2;

class Solver
{
    private Dictionary<string, int> maxCubes = new()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };
    
    public Dictionary<string, int> GetCubesFromString(string inputString)
    {
        var maxColors = new Dictionary<string, int>()
        {
            { "red", 0 },
            { "green", 0 },
            { "blue", 0 }
        };
        
        var hands = inputString.Split(';');
        foreach (var hand in hands)
        {
            var colors = new Dictionary<string, int>()
            {
                { "red", 0 },
                { "green", 0 },
                { "blue", 0 }
            };
            
            var handColors = hand.Trim().Split(',');
            foreach (var handColor in handColors)
            {
                var colorCount = handColor.Trim().Split(' ');
                Int32.TryParse(colorCount[0], out var count);
                colors[colorCount[1].Trim()] += count;

            }
            
            foreach (var (color, count) in colors)
            {
                if (maxColors[color] < colors[color])
                {
                    maxColors[color] = count;
                }
            }
        }

        return maxColors;
    }

    public int ParseInputLine(string input)
    {
        var gameInput = input.Split(':');
        var gameId = gameInput[0];
        var cubes = gameInput[1];
        var numbers = GetCubesFromString(cubes);

        var impossible = numbers.Any(colorCount =>
            maxCubes[colorCount.Key] < colorCount.Value);
        
        var power = numbers.Aggregate(1, (acc, colorCount) => acc * colorCount.Value);
        
        Console.WriteLine($"{gameId}: {impossible} - {power}");
      
        return power;
    }

    public void TestInput()
    {
        var sum = 0;
        sum += ParseInputLine("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green");
        sum += ParseInputLine("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue");
        sum += ParseInputLine("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red");
        sum += ParseInputLine("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red");
        sum += ParseInputLine("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green");
        
        Console.WriteLine(sum);
    }

    public void SolvePuzzle()
    {
        var lines = Input.ReadAsLines();
        var sum = lines.Sum(ParseInputLine);

        Console.WriteLine(sum);
    }

    public static void Main()
    {
        var solver = new Solver();
        solver.SolvePuzzle();
    }
}