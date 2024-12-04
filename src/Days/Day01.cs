namespace AdventOfCode.Days;

[Day(2024, 1)]
public class Day01 : BaseDay
{
    public override string PartOne(string input)
    {
        var left = new List<int>();
        var right = new List<int>();

        foreach (var line in input.Lines())
        {
            left.Add(line.Integers().First());
            right.Add(line.Integers().Last());
        }

        left.Sort();
        right.Sort();

        var totalDistance = left.Zip(right).Sum(x => Math.Abs(x.First - x.Second));

        return totalDistance.ToString();
    }

    public override string PartTwo(string input)
    {
        var left = new List<int>();
        var right = new Dictionary<int, int>();

        foreach (var line in input.Lines())
        {
            left.Add(line.Integers().First());
            right.SafeIncrement(line.Integers().Last());
        }

        var similarity = left.Sum(x => x * right.SafeGet(x));

        return similarity.ToString();
    }
}
