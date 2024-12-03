using System.Drawing;

namespace AdventOfCode.Days;

[Day(2024, 3)]
public class Day03 : BaseDay
{
    public override string PartOne(string input)
    {
        return ProcessMemory(input, withDonts: false).ToString();
    }

    public override string PartTwo(string input)
    {
        return ProcessMemory(input).ToString();
    }

    private long ProcessMemory(string memory, bool withDonts = true)
    {
        var result = 0L;
        var enabled = true;

        for (var i = 0; i < memory.Length - 8; i++)
        {
            if (withDonts && memory.Substring(i, 4) == "do()")
            {
                enabled = true;
            }

            if (enabled)
            {
                if (withDonts && memory.Substring(i, 7) == "don't()")
                {
                    enabled = false;
                }

                if (memory.Substring(i, 4) == "mul(")
                {
                    var comma = memory.IndexOf(',', i + 4);
                    var endParen = memory.IndexOf(')', comma + 1);

                    if (comma > 0 && endParen > 0)
                    {
                        if (int.TryParse(memory[(i + 4)..comma], out var arg1))
                        {
                            if (int.TryParse(memory[(comma + 1)..endParen], out var arg2))
                            {
                                result += arg1 * arg2;
                            }
                        }
                    }
                }
            }
        }

        return result;
    }
}
