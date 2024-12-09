using System.Drawing;

namespace AdventOfCode.Days;

[Day(2024, 8)]
public class Day08 : BaseDay
{
    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();
        var nodes = new HashSet<Point>();

        var frequencies = GetFrequencies(map);

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

    private Dictionary<char, List<Point>> GetFrequencies(char[,] map)
    {
        var frequencies = new Dictionary<char, List<Point>>();

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

        return frequencies;
    }

    private IEnumerable<Point> GetAntinodes(Point point1, Point point2)
    {
        var x1 = point1.X - (point2.X - point1.X);
        var x2 = point2.X + (point2.X - point1.X);

        var y1 = point1.Y - (point2.Y - point1.Y);
        var y2 = point2.Y + (point2.Y - point1.Y);

        return new[] { new Point(x1, y1), new Point(x2, y2) };
    }

    private IEnumerable<Point> GetAntinodes2(Point point1, Point point2, char[,] map)
    {
        var xDelta = point2.X - point1.X;
        var yDelta = point2.Y - point1.Y;

        var result = new List<Point>();

        var x = point2.X;
        var y = point2.Y;

        while (map.IsValidPoint(new Point(x, y)))
        {
            result.Add(new Point(x, y));

            x += xDelta;
            y += yDelta;
        }

        x = point1.X;
        y = point1.Y;

        while (map.IsValidPoint(new Point(x, y)))
        {
            result.Add(new Point(x, y));

            x -= xDelta;
            y -= yDelta;
        }

        return result;
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        var frequencies = GetFrequencies(map);
        var nodes = new HashSet<Point>();

        foreach (var f in frequencies.Keys)
        {
            var pairs = frequencies[f].GetCombinations(2);

            foreach (var pair in pairs)
            {
                var antinodes = GetAntinodes2(pair.First(), pair.Last(), map);
                nodes.AddRange(antinodes);
            }
        }

        return nodes.Count.ToString();
    }
}