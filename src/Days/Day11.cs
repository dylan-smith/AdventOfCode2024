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

    private readonly Dictionary<long, Dictionary<int, long>> cache = new();

    private long ProcessStone(long stone, int blinks)
    {
        if (blinks == 0)
        {
            return 1;
        }

        if (cache.ContainsKey(stone))
        {
            if (cache[stone].ContainsKey(blinks))
            {
                return cache[stone][blinks];
            }
        }
        else
        {
            cache.Add(stone, new Dictionary<int, long>());
        }

        long result;

        if (stone == 0)
        {
            result = ProcessStone(1, blinks - 1);

            return result;
        }
        else if (stone.ToString().Length % 2 == 0)
        {
            var txt = stone.ToString();
            var len = txt.Length / 2;

            var left = long.Parse(txt[..len]);
            var right = long.Parse(txt[len..]);

            result = ProcessStone(left, blinks - 1) + ProcessStone(right, blinks - 1);

            return result;
        }
        else
        {
            result = ProcessStone(stone * 2024, blinks - 1);
        }

        cache[stone].SafeSet(blinks, result);
        return result;
    }
}
