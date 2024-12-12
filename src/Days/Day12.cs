using System.Drawing;

namespace AdventOfCode.Days;

[Day(2024, 12)]
public class Day12 : BaseDay
{
    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();
        var regions = GetRegions(map);

        var result = regions.Sum(r => r.points.Count * GetFences(r.points).Count);

        return result.ToString();
    }

    private List<(char name, List<Point> points)> GetRegions(char[,] map)
    {
        var regions = new List<(char name, List<Point> points)>();

        for (var x = 0; x < map.Width(); x++)
        {
            for (var y = 0; y < map.Height(); y++)
            {
                var point = new Point(x, y);

                if (!PointInRegions(point, regions))
                {
                    regions.Add(ExpandRegion(map, point));
                }
            }
        }

        return regions;
    }

    private bool PointInRegions(Point point, List<(char name, List<Point> points)> regions) => regions.Any(r => r.points.Contains(point));

    private (char name, List<Point> points) ExpandRegion(char[,] map, Point startPoint)
    {
        var name = map[startPoint.X, startPoint.Y];
        var points = new List<Point>() { startPoint };
        var newPoints = new List<Point>() { startPoint };

        while (newPoints.Any())
        {
            var prev = new List<Point>();

            prev.AddRange(newPoints);
            newPoints.Clear();

            foreach (var point in prev)
            {
                foreach (var neighbor in point.GetNeighbors(includeDiagonals: false))
                {
                    if (map.IsValidPoint(neighbor) && map[neighbor.X, neighbor.Y] == name && !points.Contains(neighbor))
                    {
                        newPoints.Add(neighbor);
                        points.Add(neighbor);
                    }
                }
            }
        }

        return (name, points);
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        var regions = GetRegions(map);

        var result = 0L;

        foreach (var (_, points) in regions)
        {
            var sides = CountSides(points);
            result += points.Count * sides;
        }

        return result.ToString();
    }

    private int CountSides(List<Point> points)
    {
        var fences = GetFences(points);
        var used = new List<(Point a, Point b, Direction dir)>();
        var sides = 0;
        
        foreach (var fence in fences)
        {
            if (!used.Contains(fence))
            {
                sides++;

                used.Add(fence);

                if (fence.dir is Direction.Right or Direction.Left)
                {
                    var x = fence.a.X;

                    while (fences.Contains((new Point(x + 1, fence.a.Y), new Point(x + 1, fence.b.Y), fence.dir)))
                    {
                        used.Add((new Point(x + 1, fence.a.Y), new Point(x + 1, fence.b.Y), fence.dir));
                        x++;
                    }

                    x = fence.a.X;

                    while (fences.Contains((new Point(x - 1, fence.a.Y), new Point(x - 1, fence.b.Y), fence.dir)))
                    {
                        used.Add((new Point(x - 1, fence.a.Y), new Point(x - 1, fence.b.Y), fence.dir));
                        x--;
                    }
                }
                else
                {
                    var y = fence.a.Y;

                    while (fences.Contains((new Point(fence.a.X, y + 1), new Point(fence.b.X, y + 1), fence.dir)))
                    {
                        used.Add((new Point(fence.a.X, y + 1), new Point(fence.b.X, y + 1), fence.dir));
                        y++;
                    }

                    y = fence.a.Y;

                    while (fences.Contains((new Point(fence.a.X, y - 1), new Point(fence.b.X, y - 1), fence.dir)))
                    {
                        used.Add((new Point(fence.a.X, y - 1), new Point(fence.b.X, y - 1), fence.dir));
                        y--;
                    }
                }


            }
        }

        return sides;
    }

    private List<(Point a, Point b, Direction dir)> GetFences(List<Point> points)
    {
        var fences = new List<(Point a, Point b, Direction dir)>();

        foreach (var p in points)
        {
            var neighbors = p.GetNeighbors(includeDiagonals: false);

            foreach (var n in neighbors)
            {
                if (!points.Contains(n))
                {
                    if (p.X == n.X)
                    {
                        if (p.Y < n.Y)
                        {
                            fences.Add((p, n, Direction.Right));
                        }
                        else
                        {
                            fences.Add((n, p, Direction.Left));
                        }
                    }
                    else
                    {
                        if (p.X < n.X)
                        {
                            fences.Add((p, n, Direction.Up));
                        }
                        else
                        {
                            fences.Add((n, p, Direction.Down));
                        }
                    }
                }
            }
        }

        return fences;
    }
}
