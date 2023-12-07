namespace SharedUtils;

public static class StringExtensions
{
    public static string[] SplitAndTrim(this string input, char seperator)
    {
        return input.Split(seperator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }

    public static Dictionary<char, int> FrequencyMap(this string input)
    {
        var dictionary = new Dictionary<char, int>();

        foreach (var c in input.AsSpan())
        {
            if (dictionary.TryGetValue(c, out var value))
            {
                dictionary[c] = value + 1;
            }
            else
            {
                dictionary.Add(c, 1);
            }
        }

        return dictionary;
    }
}