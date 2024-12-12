using System.Drawing;
using System.Reflection.Metadata;

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
            result += region.points.Count * GetFences(region.points).Count;
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
