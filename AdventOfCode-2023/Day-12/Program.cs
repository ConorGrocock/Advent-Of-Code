﻿// See https://aka.ms/new-console-template for more information

using Day_4;


var part1Solver = new Part1();
Console.WriteLine("=== Test Input ===");
part1Solver.TestInput(21);
Console.WriteLine();
Console.WriteLine("=== Real Input ===");
part1Solver.RealInput();
       
        
Console.WriteLine();
Console.WriteLine("=== Part 2 ===");
Console.WriteLine();
        
var part2Solver = new Part2();
Console.WriteLine("=== Test Input ===");
part2Solver.TestInput(1);
Console.WriteLine();
Console.WriteLine("=== Real Input ===");
part2Solver.RealInput();