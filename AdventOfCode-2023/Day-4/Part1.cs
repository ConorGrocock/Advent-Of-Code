using SharedUtils;

namespace Day_4;

public class Part1: ISolution
{
    private int Solve(string[] cards)
    {
        var score = 0;
        foreach (var card in cards)
        {
            var cardSegments = card.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var cardId = cardSegments[0];

            var numberSegments = cardSegments[1].Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var winningNumbersString = numberSegments[0];
            var elfNumbersString = numberSegments[1];

            var winningNumbers = winningNumbersString
                .SplitAndTrim(' ')
                .Select(int.Parse)
                .ToArray();
            
            var elfNumbers = elfNumbersString
                .SplitAndTrim(' ')
                .Select(int.Parse)
                .ToArray();

            var commonNumbers = winningNumbers.Intersect(elfNumbers).ToArray();

            score += (int) Math.Pow(2, commonNumbers.Length - 1);
        }

        return score;
    }
    
    public void TestInput(int expectedOutput)
    {
        var score = Solve(Input.TestInput());
        
        Console.Write($"{score} - {expectedOutput} = {(score == expectedOutput ? "Correct" : "Incorrect" )}");
    }

    public void RealInput()
    {
        var score = Solve(Input.ReadAsLines());
        Console.Write($"{score}");
    }
}