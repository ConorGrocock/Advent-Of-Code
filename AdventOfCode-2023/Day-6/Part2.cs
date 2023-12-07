using SharedUtils;

namespace Day_4;

public class Part2: ISolution
{
    public int Solve(string[] lines)
    {
        var raceTime = long.Parse(lines[0].Replace(" ", string.Empty).SplitAndTrim(':')[1]);
        var raceDistance = long.Parse(lines[1].Replace(" ", string.Empty).SplitAndTrim(':')[1]);
        
        var possibleHoldTimes = new List<long>();
        for (long holdTime = 0; holdTime < raceTime; holdTime++)
        {
            var remainingTime = raceTime - holdTime;
            var speed = holdTime;
            var distanceTraveled = speed * remainingTime;
            if (distanceTraveled > raceDistance)
            {
                possibleHoldTimes.Add(holdTime);
            } 
        }
        Console.WriteLine();
        

        return possibleHoldTimes.Count;
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