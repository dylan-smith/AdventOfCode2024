using System.Drawing;

namespace AdventOfCode.Days;

[Day(2024, 12)]
public class Day12 : BaseDay
{
    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();
        var regions = GetRegions(map);

        var result = 0L;

        foreach (var region in regions)
        {
            var area = 0;
            var perimeter = 0;

            foreach (var p in region.points)
            {
                area++;

                var neighbors = p.GetNeighbors(includeDiagonals: false);

                foreach (var n in neighbors)
                {
                    if (!map.IsValidPoint(n) || map[n.X, n.Y] != map[p.X, p.Y])
                    {
                        perimeter++;
                    }
                }
            }

            result += area * perimeter;
        }

        return result.ToString();
    }

    private List<(char name, List<Point> points)> GetRegions(char[,] map)
    {
        var regions = new List<(char name, List<Point> points)>();

        for (var x = 0; x < map.Width(); x++)
        {
            for (var y = 0; y < map.Height(); y++)
            {
                if (!PointInRegions(x, y, regions))
                {
                    regions.Add(ExpandRegion(map, x, y));
                }
            }
        }

        return regions;
    }

    private bool PointInRegions(int x, int y, List<(char name, List<Point> points)> regions)
    {
        foreach (var (_, points) in regions)
        {
            if (points.Contains(new Point(x, y)))
            {
                return true;
            }
        }

        return false;
    }

    private (char name, List<Point> points) ExpandRegion(char[,] map, int x, int y)
    {
        var name = map[x, y];
        var points = new List<Point>();
        var newPoints = new List<Point>() { new Point(x, y) };

        while (newPoints.Any())
        {
            points.AddRange(newPoints);

            var prev = new List<Point>();

            prev.AddRange(newPoints);
            newPoints.Clear();

            foreach (var point in prev)
            {
                foreach (var neighbor in point.GetNeighbors(includeDiagonals: false))
                {
                    if (map.IsValidPoint(neighbor) && map[neighbor.X, neighbor.Y] == name && !points.Contains(neighbor) && !newPoints.Contains(neighbor))
                    {
                        newPoints.Add(neighbor);
                    }
                }
            }
        }

        return (name, points);
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
