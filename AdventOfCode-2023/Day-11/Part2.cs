using SharedUtils;

namespace Day_4;

public class Part2: ISolution
{
    private long Solve(string[] inputLines)
    {
        var expansionFactor = 1000000 - 1;
        var rowsToAdd = Enumerable.Range(0, inputLines.Length).Where(row => inputLines[row].All(c => c == '.')).ToArray();
        var colsToAdd = Enumerable.Range(0, inputLines[0].Length).Where(col => inputLines.All(l => l[col] == '.')).ToArray();
        
        var galaxies = new List<Point>();
        for (var row = 0; row < inputLines.Length; row++)
        {
            var rowOffset = rowsToAdd.Count(r => r <= row) * expansionFactor;
            for (var col = 0; col < inputLines[0].Length; col++)
            {
                if (inputLines[row][col] != '#') continue;
                
                var colOffset = colsToAdd.Count(c => c <= col) * expansionFactor;
                galaxies.Add(new Point(row + rowOffset, col + colOffset));
            }
        }

        long sum = 0;
        var permutations = galaxies.SelectMany((g1, i) => galaxies.Skip(i + 1).Select(g2 => (g1, g2))).ToArray();
        
        foreach (var (g1, g2) in permutations)
        {
            sum += g1.DistanceToManhattan(g2);
        }
        
        return sum;
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