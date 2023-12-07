namespace Day_3;

class Solution
{
    public static void Main()
    {
        var part1Solver = new Part1();
        Console.WriteLine("=== Test Input ===");
        part1Solver.TestInput();
        Console.WriteLine();
        Console.WriteLine("=== Real Input ===");
        part1Solver.SolvePuzzle();
       
        
        Console.WriteLine();
        Console.WriteLine("=== Part 2 ===");
        Console.WriteLine();
        
        var part2Solver = new Part2();
        Console.WriteLine("=== Test Input ===");
        part2Solver.TestInput();
        Console.WriteLine();
        Console.WriteLine("=== Real Input ===");
        part2Solver.SolvePuzzle();
    }
}