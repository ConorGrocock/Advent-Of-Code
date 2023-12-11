using SharedUtils;

namespace Day_4;

public class Part1: ISolution
{
    private long[] PredictNextRow(long[] readings)
    {
        var prediction = new long[readings.Length - 1];

        for (var i = 1; i <= readings.Length - 1; i++)
        {
            prediction[i - 1] = readings[i] - readings[i - 1];
        }

        return prediction;
    }
    
    private long Solve(string[] inputLines)
    {
        long sum = 0;
        
        foreach (var inputLine in inputLines)
        {
            var readings = inputLine.SplitAndTrim(' ').Select(long.Parse).ToArray();
            
            var predictions = new List<long[]>();

            var prediction = PredictNextRow(readings);
            while (prediction.Any(i => i != 0))
            {
                predictions.Add(prediction);
                prediction = PredictNextRow(prediction);
            }

            long nextValue = 0;
            predictions.Reverse();
            predictions.ForEach(value => nextValue += value.Last());

            var reading = readings.Last() + nextValue;

            sum += reading;
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