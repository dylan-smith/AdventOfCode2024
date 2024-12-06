
using System.Diagnostics;
using System.Drawing;

namespace AdventOfCode.Days;

[Day(2024, 6)]
public class Day06 : BaseDay
{
    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();

        return FindGuardRoute(map).Count.ToString();
    }

    private HashSet<Point> FindGuardRoute(char[,] map)
    {
        var seen = new HashSet<Point>();
        var pos = map.GetPoints('^').First();
        var direction = Direction.Up;

        while (map.IsValidPoint(pos))
        {
            seen.Add(pos);
            var newPos = pos.Move(direction);

            while (map.IsValidPoint(newPos) && map[newPos.X, newPos.Y] == '#')
            {
                direction = direction.TurnRight();
                newPos = pos.Move(direction);
            }

            pos = newPos;
        }

        return seen;
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        var startPos = map.GetPoints('^').First();
        var result = new List<Point>();

        var route = FindGuardRoute(map);

        foreach (var point in route)
        {
            if (point.X != startPos.X || point.Y != startPos.Y)
            {
                if (map[point.X, point.Y] != '#')
                {
                    map[point.X, point.Y] = '#';

                    if (EndsInLoop(map, startPos, Direction.Up))
                    {
                        result.Add(new Point(point.X, point.Y));
                    }

                    map[point.X, point.Y] = '.';
                }
            }
        }

        return result.Count.ToString();
    }

    private bool EndsInLoop(char[,] map, Point startPos, Direction direction)
    {
        var seen = new HashSet<(Point pos, Direction dir)>();
        var pos = startPos;

        while (map.IsValidPoint(pos))
        {
            if (!seen.Add((pos, direction)))
            {
                return true;
            }

            var newPos = pos.Move(direction);

            while (map.IsValidPoint(newPos) && map[newPos.X, newPos.Y] == '#')
            {
                direction = direction.TurnRight();
                newPos = pos.Move(direction);
            }

            pos = newPos;
        }

        return false;
    }
}
