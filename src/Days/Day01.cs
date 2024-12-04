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
        var right = new List<int>();

        foreach (var line in input.Lines())
        {
            left.Add(line.Integers().First());
            right.Add(line.Integers().Last());
        }

        var similarity = 0L;

        foreach (var item in left)
        {
            var count = right.Count(x => x == item);

            similarity += item * count;
        }

        return similarity.ToString();
    }
}
