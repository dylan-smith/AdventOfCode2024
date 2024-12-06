
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
        return string.Empty;
    }
}
