using SharedUtils;

namespace Day_1;

class Solver
{
    public int[] GetNumbersFromString(string inputString)
    {
        // eight8eightwo2two
        // 82
        inputString = inputString.Replace("one", "one1one");
        inputString = inputString.Replace("two", "two2two");
        inputString = inputString.Replace("three", "three3three");
        inputString = inputString.Replace("four", "four4four");
        inputString = inputString.Replace("five", "five5five");
        inputString = inputString.Replace("six", "six6six");
        inputString = inputString.Replace("seven", "seven7seven");
        inputString = inputString.Replace("eight", "eight8eight");
        inputString = inputString.Replace("nine", "nine9nine");
        var numberChars = inputString.Where(char.IsDigit).ToArray();

        var numbers = new List<int>();

        foreach (var numberChar in numberChars)
        {
            numbers.Add(numberChar - '0');
        }

        return numbers.ToArray();
    }

    public int ParseInputLine(string input)
    {
        var numbers = GetNumbersFromString(input);
        return numbers.First() * 10 + numbers.Last();
    }

    public void TestInput()
    {
        Console.WriteLine(ParseInputLine("1abc2"));
        Console.WriteLine(ParseInputLine("pqr3stu8vwx"));
        Console.WriteLine(ParseInputLine("a1b2c3d4e5f"));
        Console.WriteLine(ParseInputLine("treb7uchet"));
        
        Console.WriteLine();
        
        Console.WriteLine(ParseInputLine("two1nine"));
        Console.WriteLine(ParseInputLine("eightwothree"));
        Console.WriteLine(ParseInputLine("abcone2threexyz"));
        Console.WriteLine(ParseInputLine("xtwone3four"));
        Console.WriteLine(ParseInputLine("4nineeightseven2"));
        Console.WriteLine(ParseInputLine("zoneight234"));
        Console.WriteLine(ParseInputLine("7pqrstsixteen"));
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