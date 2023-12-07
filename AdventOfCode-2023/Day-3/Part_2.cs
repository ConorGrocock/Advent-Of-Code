using SharedUtils;

namespace Day_3;



internal class Part2
{
    private struct Position
    {
        public int X;
        public int Y;
    }

    private class PartNumber
    {
        public readonly List<Position> Position = new();
        public string Value = "";
    }

    private int ParseInputLine(IReadOnlyList<string> input)
    {
        var partNumbers = new List<PartNumber>();

        for (var i = 0; i < input.Count; i++)
        {
            var currentNumber = new PartNumber();
            for (var j = 0; j < input[i].Length; j++)
            {
                var currentNumberValue = input[i][j];

                if (currentNumber.Value?.Length > 0 && !char.IsNumber(currentNumberValue))
                {
                    partNumbers.Add(currentNumber);
                    currentNumber = new PartNumber();

                    continue;
                }

                if (!char.IsDigit(currentNumberValue))
                {
                    continue;
                }

                currentNumber.Value += currentNumberValue;
                currentNumber.Position.Add( new Position { X= i, Y = j });
            }

            partNumbers.Add(currentNumber);
        }


        var sum = 0;
        for (var gearX = 0; gearX < input.Count; gearX++)
        {
            for (var gearY = 0; gearY < input[gearX].Length; gearY++)
            {
                var currentSpace = input[gearX][gearY];
                if (currentSpace != '*') continue;

                var nearbyNumbers = partNumbers.Where(part =>
                    part.Position.Any(
                        position =>
                            Math.Abs(position.X - gearX) <= 1 &&
                            Math.Abs(position.Y - gearY) <= 1))
                    .ToArray();
                
                if(nearbyNumbers.Length != 2) continue;

                sum += nearbyNumbers.Aggregate(1, (acc, part) => acc * int.Parse(part.Value));
            }
        }

        return sum;
    }

    public void TestInput()
    {
        var sum = 0;
        sum += ParseInputLine(Input.TestInput());
        Console.WriteLine(sum);
    }

    public void SolvePuzzle()
    {
        var lines = Input.ReadAsLines();
        var sum = ParseInputLine(lines);

        Console.WriteLine(sum);
    }
}