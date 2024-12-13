
namespace AdventOfCode.Days;

[Day(2024, 13)]
public class Day13 : BaseDay
{
    public override string PartOne(string input)
    {
        var machines = input.Paragraphs().Select(p => ParseMachine(p)).ToList();

        var result = 0L;

        foreach (var machine in machines)
        {
            result += SolveMachine(machine);
        }

        return result.ToString();
    }

    private long SolveMachine((long aX, long aY, long bX, long bY, long prizeX, long prizeY) machine)
    {
        var a = 0L;
        var b = 0L;

        var x = 0L;
        var y = 0L;

        var mx = machine.prizeX / machine.bX;
        var my = machine.prizeY / machine.bY;
        mx++;
        my++;

        if (mx > my)
        {
            x = my * machine.bX;
            y = my * machine.bY;
            b = my;
        }
        else
        {
            x = mx * machine.bX;
            y = mx * machine.bY;
            b = mx;
        }

        while ((x != machine.prizeX || y != machine.prizeY) && b > 0)
        {
            b--;
            x -= machine.bX;
            y -= machine.bY;

            while (x < machine.prizeX && y < machine.prizeY)
            {
                a++;
                x += machine.aX;
                y += machine.aY;
            }
        }

        if (x == machine.prizeX && y == machine.prizeY)
        {
            return b + (a * 3);
        }

        return 0;
    }

    private (long aX, long aY, long bX, long bY, long prizeX, long prizeY) ParseMachine(string input)
    {
        var a = input.Lines().First();
        var b = input.Lines().ToList()[1];
        var prize = input.Lines().Last();

        var ax = long.Parse(a.Words().ToList()[2][2..]);
        var ay = long.Parse(a.Words().ToList()[3][2..]);
        var bx = long.Parse(b.Words().ToList()[2][2..]);
        var by = long.Parse(b.Words().ToList()[3][2..]);
        var prizeX = long.Parse(prize.Words().ToList()[1][2..]); // + 10000000000000;
        var prizeY = long.Parse(prize.Words().ToList()[2][2..]); // + 10000000000000;

        return (ax, ay, bx, by, prizeX, prizeY);
    }

    public override string PartTwo(string input)
    {
        var machines = input.Paragraphs().Select(p => ParseMachine(p)).ToList();

        var result = 0L;

        foreach (var machine in machines)
        {
            result += SolveMachine2(machine);
        }

        return result.ToString();
    }

    private long SolveMachine2((long aX, long aY, long bX, long bY, long prizeX, long prizeY) machine)
    {
        var x = 0L;
        var y = 0L;
        var a = 0L;

        var b = ((machine.prizeY * machine.aX) - (machine.aY * machine.prizeX)) / ((machine.aX * machine.bY) - (machine.aY * machine.bX));

        while ((x != machine.prizeX || y != machine.prizeY) && b >= 0)
        {
            b--;
            x -= machine.bX;
            y -= machine.bY;

            while (x < machine.prizeX && y < machine.prizeY)
            {
                a++;
                x += machine.aX;
                y += machine.aY;
            }
        }

        if (x == machine.prizeX && y == machine.prizeY)
        {
            return b + (a * 3);
        }

        return 0;
    }
}
