
using System.Xml.Linq;

namespace AdventOfCode.Days;

[Day(2024, 11)]
public class Day11 : BaseDay
{
    public override string PartOne(string input)
    {
        var stones = new LinkedList<long>(input.Longs());

        for (var i = 0; i < 25; i++)
        {
            var node = stones.First;

            //var txt = "";

            //foreach (var x in stones)
            //{
            //    txt += x.ToString() + " ";
            //}

            //Log(txt);

            while (node != null)
            {
                if (node.Value == 0)
                {
                    node.Value = 1;
                }
                else if (node.Value.ToString().Length % 2 == 0)
                {
                    stones.AddBefore(node, long.Parse(node.Value.ToString()[..(node.Value.ToString().Length / 2)]));
                    stones.AddBefore(node, long.Parse(node.Value.ToString()[(node.Value.ToString().Length / 2)..]));
                    var right = node.Previous;
                    stones.Remove(node);
                    node = right;
                }
                else
                {
                    node.Value *= 2024;
                }

                node = node.Next;
            }
        }

        return stones.Count.ToString();
    }

    public override string PartTwo(string input)
    {
        var values = input.Longs().ToList();
        var result = 0L;

        foreach (var stone in values)
        {
            result += ProcessStone(stone, 75);
        }

        return result.ToString();
    }

    private readonly Dictionary<long, Dictionary<int, long>> cache = new Dictionary<long, Dictionary<int, long>>();

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

            return result; ;
        }
        else if (stone.ToString().Length % 2 == 0)
        {
            var left = long.Parse(stone.ToString()[..(stone.ToString().Length / 2)]);
            var right = long.Parse(stone.ToString()[(stone.ToString().Length / 2)..]);

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
