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
            if (input.Substring(i, 4) == "mul(")
            {
                var comma = input.IndexOf(',', i + 4);
                var endParen = input.IndexOf(')', comma + 1);

                if (comma > 0 && endParen > 0)
                {
                    if (int.TryParse(input.AsSpan(i + 4, comma - (i + 4)), out var arg1))
                    {
                        if (int.TryParse(input.AsSpan(comma + 1, endParen - comma - 1), out var arg2))
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
        var result = 0L;
        var enabled = true;

        for (var i = 0; i < input.Length - 8; i++)
        {
            if (input.Substring(i, 4) == "do()")
            {
                enabled = true;
            }

            if (enabled)
            {
                if (input.Substring(i, 7) == "don't()")
                {
                    enabled = false;
                }

                if (input.Substring(i, 4) == "mul(")
                {
                    var comma = input.IndexOf(',', i + 4);
                    var endParen = input.IndexOf(')', comma + 1);

                    if (comma > 0 && endParen > 0)
                    {
                        if (int.TryParse(input[(i + 4)..comma], out var arg1))
                        {
                            if (int.TryParse(input[(comma + 1)..endParen], out var arg2))
                            {
                                result += arg1 * arg2;
                            }
                        }
                    }
                }
            }
        }

        return result.ToString();
    }
}
