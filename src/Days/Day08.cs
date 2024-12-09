using System.Drawing;

namespace AdventOfCode.Days;

[Day(2024, 8)]
public class Day08 : BaseDay
{
    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();
        var frequencies = new Dictionary<char, List<Point>>();
        var nodes = new HashSet<Point>();

        for (var x = 0; x < map.Width(); x++)
        {
            for (var y = 0; y < map.Height(); y++)
            {
                if (map[x, y] != '.')
                {
                    if (!frequencies.ContainsKey(map[x, y]))
                    {
                        frequencies.Add(map[x, y], new List<Point>());
                    }

                    frequencies[map[x, y]].Add(new Point(x, y));
                }
            }
        }

        foreach (var f in frequencies.Keys)
        {
            var pairs = frequencies[f].GetCombinations(2);

            foreach (var pair in pairs)
            {
                var antinodes = GetAntinodes(pair.First(), pair.Last());

                foreach (var antinode in antinodes)
                {
                    if (map.IsValidPoint(antinode))
                    {
                        nodes.Add(antinode);
                    }
                }
            }
        }

        return nodes.Count.ToString();
    }

    private IEnumerable<Point> GetAntinodes(Point point1, Point point2)
    {
        var x1 = point1.X - (point2.X - point1.X);
        var x2 = point2.X + (point2.X - point1.X);

        var y1 = point1.Y - (point2.Y - point1.Y);
        var y2 = point2.Y + (point2.Y - point1.Y);

        return new[] { new Point(x1, y1), new Point(x2, y2) };
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}