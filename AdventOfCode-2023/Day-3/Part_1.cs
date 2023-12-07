using SharedUtils;

namespace Day_3;


internal class Part1
{
    internal class PartNumber
    {
        public string Value = "";
        public bool Valid;

        public bool IsValid()
        {
            return Valid;
        }
    }
    
    private int ParseInputLine(IReadOnlyList<string> input)
    {
        var partNumbers = new List<PartNumber>();
        PartNumber currentNumber = new PartNumber();
        
        for (var i = 0; i < input.Count; i++)
        {
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

                currentNumber.Valid = 
                    currentNumber.Valid || 
                    CheckSurroundings(input, i, j);
            }
            
            partNumbers.Add(currentNumber);
        }

        var sum = 0;
        
        partNumbers.ForEach(part =>
        {
            if (part.IsValid())
            {
                sum += int.Parse(part.Value);
            }
        });

        return sum;
    }

    private bool CheckSurroundings(IReadOnlyList<string> input, int i, int j)
    {
        return 
            IsValidSpace(input, i - 1, j - 1) || IsValidSpace(input, i - 1,j) || IsValidSpace(input, i - 1,j + 1) ||
            IsValidSpace(input, i,j - 1) || IsValidSpace(input, i,j) || IsValidSpace(input, i,j + 1) ||
            IsValidSpace(input, i + 1,j - 1) || IsValidSpace(input, i + 1,j) || IsValidSpace(input, i + 1,j + 1);
    }

    private bool IsValidSpace(IReadOnlyList<string> input, int x, int y)
    {
        if (x < 0 || y < 0) return false;
        if (x > input.Count - 1 || y > input[x].Length - 1) return false;
        if (input[x][y] == '.') return false;
        if (char.IsNumber(input[x][y])) return false;
        
        return true;
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