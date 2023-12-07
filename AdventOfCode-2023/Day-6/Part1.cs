using SharedUtils;

namespace Day_4;

public class Part1: ISolution
{
    public int Solve(string[] lines)
    {
        var times = lines[0].SplitAndTrim(':')[1].SplitAndTrim(' ').Select(int.Parse).ToArray();
        var distance = lines[1].SplitAndTrim(':')[1].SplitAndTrim(' ').Select(int.Parse).ToArray();
        var answer = 1;

        for (var raceIndex = 0; raceIndex < times.Length; raceIndex++)
        {
            var raceTime = times[raceIndex];
            var raceDistance = distance[raceIndex];

            var possibleHoldTimes = new List<int>();
            for (var holdTime = raceTime - 1; holdTime >= 0; holdTime--)
            {
                var remainingTime = raceTime - holdTime;
                var speed = holdTime;
                var distanceTraveled = speed * remainingTime;
                if (distanceTraveled > raceDistance)
                {
                    possibleHoldTimes.Add(holdTime);
                } 
            }

            answer *= possibleHoldTimes.Count;
        }

        return answer;
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