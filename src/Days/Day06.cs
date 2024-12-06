
using System.Diagnostics;
using System.Drawing;

namespace AdventOfCode.Days;

[Day(2024, 6)]
public class Day06 : BaseDay
{
    public override string PartOne(string input)
    {
        var map = input.CreateCharGrid();

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

        return seen.Count.ToString();
    }

    public override string PartTwo(string input)
    {
        var map = input.CreateCharGrid();
        var startPos = map.GetPoints('^').First();
        var result = new List<Point>();

        for (var x = 0; x < map.Width(); x++)
        {
            for (var y = 0; y < map.Height(); y++)
            {
                if (x != startPos.X || y != startPos.Y)
                {
                    if (map[x, y] != '#')
                    {
                        map[x, y] = '#';

                        if (EndsInLoop(map, startPos, Direction.Up))
                        {
                            result.Add(new Point(x, y));
                        }

                        map[x, y] = '.';
                    }
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
