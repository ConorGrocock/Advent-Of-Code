using System.Drawing;
using System.Drawing.Drawing2D;
using SharedUtils;
using AoCPoint = SharedUtils.Point;
using Point = System.Drawing.Point;

namespace Day_4;

public class Part2: ISolution
{
    private static AoCPoint[] Sides = {
        new(-1, 0),
        new(1, 0),
        new(0, -1),
        new(0, 1)
    };
    
    internal struct Pipe
    {
        public char Symbol;
        public AoCPoint Position;

        public Pipe(char symbol, AoCPoint position)
        {
            Symbol = symbol;
            Position = position;
        }

        public bool IsConnected(Pipe neighbor)
        {
            return (Position.Y - neighbor.Position.Y, Position.X - neighbor.Position.X, Symbol, neighbor.Symbol) switch
            {
                (_, _, _, '.') => false,
                (1, 0, '|', _) or (-1, 0, '|', _) => false,
                (0, 1, '-', _) or (0, -1, '-', _) => false,
                (1, 0, 'L', _) or (0, -1, 'L', _) => false,
                (-1, 0, 'J', _) or (0, -1, 'J', _) => false,
                (-1, 0, '7', _) or (0, 1, '7', _) => false,
                (1, 0, 'F', _) or (0, 1, 'F', _) => false,
                (0, 1, '|' or 'L' or 'J', '-' or 'L' or 'J') or (0, -1, '|' or '7' or 'F', '-' or '7' or 'F') => false,
                (1, 0, '-' or '7' or 'J', '|' or '7' or 'J') or (-1, 0, '-' or 'L' or 'F', '|' or 'L' or 'F') => false,
                _ => true
            };
        }
    }

    private AoCPoint? GetStartPoint(string[] mapLines)
    {
        for (var i = 0; i < mapLines.Length; i++)
        {
            var currentLine = mapLines[i];
            for (var j = 0; j < currentLine.Length; j++)
            {
                if (currentLine[j] == 'S') return new(i, j);
            }
        }

        return null;
    }

    private void RenderMap(Pipe[,] map, HashSet<Pipe> visited, Stack<Pipe> toVisit)
    {
        Console.Clear();
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                Console.SetCursorPosition(j,i);
                var hasVisited = visited.Any(pipe => pipe.Position.X == i && pipe.Position.Y == j);
                var willVisit = toVisit.Any(pipe => pipe.Position.X == i && pipe.Position.Y == j);

                if (hasVisited)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                } else if (willVisit)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
                Console.Write(map[i,j].Symbol);
            }
            Console.WriteLine();
        }
    }
    //shamelessly copied from https://stackoverflow.com/a/67403631
    private static bool IsPointInsidePolygon(Point[ ] polygon, Point point)
    {
        var path = new GraphicsPath( );
        path.AddPolygon(polygon);

        var region = new Region(path);
        return region.IsVisible(point);
    }
    
    private int Solve(string[] inputLines)
    {
        var startingPoint = GetStartPoint(inputLines);
        if (startingPoint == null) return -1;
        var mapSize = new AoCPoint(inputLines.Length - 1, inputLines[0].Length - 1);
        
        var pipeMap = new Pipe[inputLines.Length,inputLines[0].Length];
        for (var x = 0; x < inputLines.Length; x++)
        {
            var currentLine = inputLines[x];
            for (var y = 0; y < currentLine.Length; y++)
            {
                pipeMap[x, y] = new(currentLine[y], new(x,y));
            }
        }

        var startingPipe = pipeMap[startingPoint.X, startingPoint.Y];

        var queue = new Stack<Pipe>();
        var pipesCovered = new HashSet<Pipe>();
        queue.Push(startingPipe);
        
        while (queue.Count > 0)
        {
            var element = queue.Pop();
            if (pipesCovered.Contains(element))
                continue;
            
            pipesCovered.Add(element);

            var cursorPosition = Console.GetCursorPosition();
            Console.SetCursorPosition(40, 0);
            Console.WriteLine($"Covered {pipesCovered.Count}/{pipeMap.Length} {Math.Round(Convert.ToDouble(pipesCovered.Count) / Convert.ToDouble(pipeMap.Length) * 100, 2)}%");
            Console.SetCursorPosition(40, 1);
            Console.WriteLine($"{queue.Count} pipes to process");
            Console.SetCursorPosition(40, 4);
            Console.WriteLine($"Currently working on {element.Position}");

            var currentPosition = element.Position;

            var neighbours =
                (from side in Sides 
                    select currentPosition.Move(side) into movedPosition 
                    where movedPosition.IsWithinBounds(mapSize) 
                    select pipeMap[movedPosition.X, movedPosition.Y] into neighbour 
                    where !pipesCovered.Contains(neighbour) 
                    where element.IsConnected(neighbour) 
                    select neighbour)
                .ToList();

            Console.SetCursorPosition(cursorPosition.Left, cursorPosition.Top);

            if (neighbours.Count == 0)
                continue;
            foreach (var item1 in neighbours)
            {
                queue.Push(item1);
            }
        }

        var points = pipesCovered.Select(p => new Point(p.Position.X, p.Position.Y)).ToArray();

        int count = 0;
        int mapWidth = pipeMap.GetLength(0);
        for (var i = 0; i < pipeMap.Length; i++)
        {
            var point = new Point(i % mapWidth, i / mapWidth);
            if(points.Contains(point)) continue;
            if (IsPointInsidePolygon(points, point)) count++;
        }

        return count;
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