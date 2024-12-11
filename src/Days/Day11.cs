namespace AdventOfCode.Days;

[Day(2024, 11)]
public class Day11 : BaseDay
{
    public override string PartOne(string input)
    {
        var values = input.Longs();
        var result = 0L;

        foreach (var stone in values)
        {
            result += ProcessStone(stone, 25);
        }

        return result.ToString();
    }

    public override string PartTwo(string input)
    {
        var values = input.Longs();
        var result = 0L;

        foreach (var stone in values)
        {
            result += ProcessStone(stone, 75);
        }

        return result.ToString();
    }

    private readonly Dictionary<(long, int), long> cache = new();

    private long ProcessStone(long stone, int blinks)
    {
        if (blinks == 0)
        {
            return 1;
        }

        if (cache.TryGetValue((stone, blinks), out var cachedResult))
        {
            return cachedResult;
        }

        long result;

        if (stone == 0)
        {
            result = ProcessStone(1, blinks - 1);
        }
        else if (GetDigitCount(stone) % 2 == 0)
        {
            var (left, right) = SplitNumber(stone);
            result = ProcessStone(left, blinks - 1) + ProcessStone(right, blinks - 1);
        }
        else
        {
            result = ProcessStone(stone * 2024, blinks - 1);
        }

        cache[(stone, blinks)] = result;
        return result;
    }

    private static int GetDigitCount(long number)
    {
        return (int)Math.Floor(Math.Log10(number) + 1);
    }

    private static (long left, long right) SplitNumber(long number)
    {
        var digitCount = GetDigitCount(number);
        var halfDigitCount = digitCount / 2;
        var divisor = (long)Math.Pow(10, halfDigitCount);

        var left = number / divisor;
        var right = number % divisor;

        return (left, right);
    }
}
