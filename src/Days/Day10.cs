using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace AdventOfCode.Days;

[Day(2024, 10)]
public class Day10 : BaseDay
{
    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();

        var trailheads = FindTrails(map);

        return trailheads.Values.Sum(x => x.Distinct().Count()).ToString();
    }

    private Dictionary<Point, IEnumerable<Point>> FindTrails(char[,] map)
    {
        var trailheads = new Dictionary<Point, IEnumerable<Point>>();

        foreach (var trailhead in map.GetPoints('0'))
        {
            trailheads.Add(trailhead, new List<Point>() { trailhead });
        }

        for (var i = 1; i <= 9; i++)
        {
            foreach (var th in trailheads.Keys)
            {
                var newTrails = new List<Point>();

                foreach (var trail in trailheads[th])
                {
                    foreach (var neighbor in trail.GetNeighbors(includeDiagonals: false))
                    {
                        if (map.IsValidPoint(neighbor) && map[neighbor.X, neighbor.Y] == i.ToString()[0])
                        {
                            newTrails.Add(neighbor);
                        }
                    }
                }

                trailheads[th] = newTrails;
            }
        }

        return trailheads;
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();

        var trailheads = FindTrails(map);

        return trailheads.Values.Sum(x => x.Count()).ToString();
    }
}