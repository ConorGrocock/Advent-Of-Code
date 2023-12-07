using SharedUtils;

namespace Day_4;

public class Part2: ISolution
{
    private static int Solve(string[] cards)
    {
        var cardScores = new Dictionary<int,int>();
        
        foreach (var card in cards)
        {
            var cardSegments = card.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var cardCode = cardSegments[0];
            var cardId = int.Parse(cardCode.Where(char.IsNumber).ToArray());

            var numberSegments = cardSegments[1].Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var winningNumbersString = numberSegments[0];
            var elfNumbersString = numberSegments[1];

            var winningNumbers = winningNumbersString
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            
            var elfNumbers = elfNumbersString
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var commonNumbers = winningNumbers.Intersect(elfNumbers).ToArray();
            cardScores.Add(cardId, commonNumbers.Length);
        }

        var score = 0;
        foreach (var (key, _) in cardScores)
        {
            score += ProcessCard(cardScores, key);
        }

        return score;
    }

    private static int ProcessCard(Dictionary<int,int> cardScores, int id)
    {
        var primaryScore = cardScores[id];
        
        var cardsPlayed = 1;
        for (var i = 0; i < primaryScore; i++)
        {
            cardsPlayed += ProcessCard(cardScores, id + i + 1);
        }
        
        return cardsPlayed;
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