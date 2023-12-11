using System.Collections.Concurrent;
using SharedUtils;

namespace Day_4;

public class Part2: ISolution
{
    private class Step
    {
        public string Left;
        public string Right;

        public override string ToString()
        {
            return $"{Left}, {Right}";
        }
    }

    private static Dictionary<string, Step> ParseSteps(IEnumerable<string> inputLines)
    {
        var directions = new Dictionary<string, Step>();
        foreach (var inputLine in inputLines)
        {
            var stepComponents = inputLine.SplitAndTrim('=');
            var stepName = stepComponents[0];
            var stepDirections = stepComponents[1]
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .SplitAndTrim(',');

            directions.TryAdd(stepName, new Step
            {
                Left = stepDirections[0], Right = stepDirections[1]
            });
        }

        return directions;
    }
    
    private static long Solve(string[] inputLines)
    {
        var turns = inputLines.First().SplitAndTrim();
        var directions = ParseSteps(inputLines.Skip(2).ToArray());

        var startPoints = directions.Keys.Where(key => key.Last() == 'A');
        var startPointSteps = new List<long>();

        Parallel.ForEach(startPoints, startPoint =>
        {
            var turnsTaken = CalculateTurnsTakenFromStartPoint(startPoint, turns, directions);
            startPointSteps.Add(turnsTaken);
        });

        return LCM(startPointSteps.ToArray());
    }
    
    static long GCF(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    } 
    
    static long LCM(long[] items)
    {
        long answer = 0;

        foreach (var c in items)
        {
            if (answer == 0) answer = c;
            else answer = LCM(answer, c);
        }

        return answer;
    }

    static long LCM(long a, long b)
    {
        return a / GCF(a, b) * b;
    }

    private static int CalculateTurnsTakenFromStartPoint(string currentStep, IReadOnlyList<char> turns, IReadOnlyDictionary<string, Step> directions)
    {
        var turnsTaken = 0;
        while (!currentStep.EndsWith("Z"))
        {
            var currentTurn = turns[turnsTaken % turns.Count];

            currentStep = currentTurn switch
            {
                'L' => directions[currentStep].Left,
                'R' => directions[currentStep].Right,
                _ => currentStep
            };

            turnsTaken++;
        }

        return turnsTaken;
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