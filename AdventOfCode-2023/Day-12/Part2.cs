using System.Runtime.InteropServices.JavaScript;
using System.Text;
using SharedUtils;

namespace Day_4;

public class Part2: ISolution
{
    private long Solve(IEnumerable<string> inputLines)
    {
        var sum = inputLines.Select(line =>
        {
            var input = line.Split(" ");
            var pattern = input[0].Repeat(5, "?");
            var known = input[1].Repeat(5, ",").Split(",").Select(int.Parse).ToList();

            var arrangements = Arrangements(pattern, known);

            return arrangements;
        }).Sum();

        return sum;
    }
private long Arrangements(string pattern, IReadOnlyList<int> sections) => Arrangements(pattern, sections, 0, pattern.Length);

    private long Arrangements(string pattern, IReadOnlyList<int> sections, int patternOffset, int patternLength) =>
        sections.Count switch
        {
            0 => NoSections(pattern, patternOffset, patternLength),
            1 => OneSection(pattern, sections[0], patternOffset, patternLength),
            _ => MultipleSections(pattern, sections, patternOffset, patternLength)
        };

    private long MultipleSections(string pattern, IReadOnlyList<int> sections, int patternOffset, int patternLength)
    {
        var leftSections = sections.Take(sections.Count / 2).ToList();
        var pivotSection = sections[sections.Count / 2];
        var rightSections = sections.Skip(sections.Count / 2 + 1).ToList();

        var beforePivotMin = leftSections.Sum() + leftSections.Count - 1;
        var beforePivot = beforePivotMin;
        var slack = patternLength - sections.Sum() - sections.Count + 2;
        long result = 0;
        for (var i = 0; i < slack; i++, beforePivot++)
        {
            var afterPivot = beforePivot + 1 + pivotSection;
            if (pattern[patternOffset + beforePivot] == '#' || (afterPivot < patternLength && pattern[patternOffset + afterPivot] == '#'))
            {
                continue;
            }

            var middle = OneSection(pattern, pivotSection, patternOffset + beforePivot + 1, pivotSection);
            if (middle == 0)
            {
                continue;
            }

            var left = Arrangements(pattern, leftSections, patternOffset, beforePivot);
            if (left == 0)
            {
                continue;
            }

            var right = Arrangements(pattern, rightSections, patternOffset + afterPivot + 1, patternLength - afterPivot - 1);

            result += left * right;
        }

        return result;
    }

    private long OneSection(string pattern, int section, int patternOffset, int patternLength)
    {
        var slack = patternLength - section - 1 + 2;
        long result = 0;
        for (var i = 0; i < slack; i++)
        {
            var x = 0;
            var isPossible = true;
            for (; isPossible && x < i; x++)
            {
                if (pattern[patternOffset + x] == '#')
                {
                    isPossible = false;
                }
            }

            for (; isPossible && x < i + section; x++)
            {
                if (pattern[patternOffset + x] == '.')
                {
                    isPossible = false;
                }
            }

            for (; isPossible && x < patternLength; x++)
            {
                if (pattern[patternOffset + x] == '#')
                {
                    isPossible = false;
                }
            }

            if (isPossible)
            {
                result++;
            }
        }

        return result;
    }

    private static long NoSections(string pattern, int patternOffset, int patternLength)
    {
        for (var x = 0; x < patternLength; x++)
        {
            if (pattern[patternOffset + x] == '#')
            {
                return 0;
            }
        }

        return 1;
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