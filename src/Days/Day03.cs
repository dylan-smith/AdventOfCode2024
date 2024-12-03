using System.Drawing;

namespace AdventOfCode.Days;

[Day(2024, 3)]
public class Day03 : BaseDay
{
    public override string PartOne(string input)
    {
        var result = 0L;

        for (var i = 0; i < input.Length - 8; i++)
        {
            var foo = input.Substring(i, 4);
            if (input.Substring(i, 4) == "mul(")
            {
                var comma = input.IndexOf(',', i + 4);
                var endParen = input.IndexOf(')', comma + 1);

                if (comma > 0 && endParen > 0)
                {
                    var foo2 = input.Substring(i + 4, comma - (i + 4));

                    if (int.TryParse(input.Substring(i + 4, comma - (i + 4)), out var arg1))
                    {
                        var foo3 = input.Substring(comma + 1, endParen - comma - 1);

                        if (int.TryParse(input.Substring(comma + 1, endParen - comma - 1), out var arg2))
                        {
                            result += arg1 * arg2;
                        }
                    }
                }
            }
        }

        return result.ToString();
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
