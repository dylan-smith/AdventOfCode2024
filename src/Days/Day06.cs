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

    private Dictionary<Point, (Point prev, Direction dir)> FindGuardRoute(char[,] map)
    {
        var seen = new Dictionary<Point, (Point prev, Direction dir)>();
        var pos = map.GetPoints('^').First();
        var direction = Direction.Up;
        Point prev = new(-1, -1);

        while (map.IsValidPoint(pos))
        {
            if (!seen.ContainsKey(pos))
            {
                seen.Add(pos, (prev, direction));
            }
            var newPos = pos.Move(direction);

            while (map.IsValidPoint(newPos) && map[newPos.X, newPos.Y] == '#')
            {
                direction = direction.TurnRight();
                newPos = pos.Move(direction);
            }

            prev = pos;
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
        var paths = PreCalculatePaths(map);

        foreach (var point in route.Keys)
        {
            if (point.X != startPos.X || point.Y != startPos.Y)
            {
                if (map[point.X, point.Y] != '#')
                {
                    map[point.X, point.Y] = '#';

                    var compensating = UpdatePaths(paths, point, map.Width(), map.Height());

                    if (EndsInLoop(paths, route[point].prev, route[point].dir))
                    {
                        result.Add(new Point(point.X, point.Y));
                    }

                    map[point.X, point.Y] = '.';

                    foreach (var c in compensating)
                    {
                        paths[(c.pos, c.dir)] = c.value;
                    }
                }
            }
        }

        return result.Count.ToString();
    }

    private List<(Point pos, Direction dir, Point value)> UpdatePaths(Dictionary<(Point pos, Direction dir), Point> paths, Point obstacle, int width, int height)
    {
        var result = new List<(Point pos, Direction dir, Point value)>();

        for (var x = 0; x < obstacle.X; x++)
        {
            var pos = new Point(x, obstacle.Y);

            if (paths.ContainsKey((pos, Direction.Right)))
            {
                result.Add((pos, Direction.Right, paths[(pos, Direction.Right)]));
                paths[(pos, Direction.Right)] = new Point(obstacle.X - 1, obstacle.Y);
            }
        }

        for (var x = obstacle.X + 1; x < width; x++)
        {
            var pos = new Point(x, obstacle.Y);

            if (paths.ContainsKey((pos, Direction.Left)))
            {
                result.Add((pos, Direction.Right, paths[(pos, Direction.Left)]));
                paths[(pos, Direction.Left)] = new Point(obstacle.X + 1, obstacle.Y);
            }
        }

        for (var y = 0; y < obstacle.Y; y++)
        {
            var pos = new Point(obstacle.X, y);

            if (paths.ContainsKey((pos, Direction.Down)))
            {
                result.Add((pos, Direction.Right, paths[(pos, Direction.Down)]));
                paths[(pos, Direction.Down)] = new Point(obstacle.X, obstacle.Y - 1);
            }
        }

        for (var y = obstacle.Y + 1; y < height; y++)
        {
            var pos = new Point(obstacle.X, y);

            if (paths.ContainsKey((pos, Direction.Up)))
            {
                result.Add((pos, Direction.Right, paths[(pos, Direction.Up)]));
                paths[(pos, Direction.Up)] = new Point(obstacle.X, obstacle.Y + 1);
            }
        }

        return result;
    }

    private Dictionary<(Point pos, Direction dir), Point> PreCalculatePaths(char[,] map)
    {
        var result = new Dictionary<(Point pos, Direction dir), Point>();

        for (var x = 0; x < map.Width(); x++)
        {
            for (var y = 0; y < map.Height(); y++)
            {
                var pos = new Point(x, y);

                var endPos = FindEndOfPath(map, pos, Direction.Up);
                result.Add((pos, Direction.Up), endPos);

                endPos = FindEndOfPath(map, pos, Direction.Down);
                result.Add((pos, Direction.Down), endPos);

                endPos = FindEndOfPath(map, pos, Direction.Right);
                result.Add((pos, Direction.Right), endPos);

                endPos = FindEndOfPath(map, pos, Direction.Left);
                result.Add((pos, Direction.Left), endPos);
            }
        }

        return result;
    }

    private Point FindEndOfPath(char[,] map, Point pos, Direction dir)
    {
        while(true)
        {
            var newPos = pos.Move(dir);

            if (!map.IsValidPoint(newPos))
            {
                return new Point(-1, -1);
            }

            if (map[newPos.X, newPos.Y] == '#')
            {
                return pos;
            }

            pos = newPos;
        }
    }

    private bool EndsInLoop(Dictionary<(Point pos, Direction dir), Point> paths, Point startPos, Direction direction)
    {
        var seen = new HashSet<(Point pos, Direction dir)>();
        var pos = startPos;

        while (pos.X >= 0)
        {
            if (!seen.Add((pos, direction)))
            {
                return true;
            }

            var newPos = paths[(pos, direction)];

            while (newPos == pos)
            {
                direction = direction.TurnRight();
                newPos = paths[(pos, direction)];
            }

            pos = newPos;
        }

        return false;
    }
}
