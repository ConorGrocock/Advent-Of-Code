using System.Collections.Concurrent;
using SharedUtils;

namespace Day_4;

public class Part1: ISolution
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

    private Dictionary<string, Step> ParseSteps(string[] inputLines)
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
    
    private int Solve(string[] inputLines)
    {
        var turnsTaken = 0;
        var turns = inputLines.First().SplitAndTrim();
        var directions = ParseSteps(inputLines.Skip(2).ToArray());

        var currentStep = "AAA";
        while (currentStep != "ZZZ")
        {
            var currentTurn = turns[turnsTaken % turns.Length];

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