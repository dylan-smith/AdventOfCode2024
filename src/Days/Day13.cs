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

    private long SolveMachine((int aX, int aY, int bX, int bY, int prizeX, int prizeY) machine)
    {
        var a = 0;
        var b = 0;

        var x = 0;
        var y = 0;

        while (x < machine.prizeX && y < machine.prizeY)
        {
            x += machine.bX;
            y += machine.bY;
            b++;
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

    private (int aX, int aY, int bX, int bY, int prizeX, int prizeY) ParseMachine(string input)
    {
        var a = input.Lines().First();
        var b = input.Lines().ToList()[1];
        var prize = input.Lines().Last();

        var ax = int.Parse(a.Words().ToList()[2][2..]);
        var ay = int.Parse(a.Words().ToList()[3][2..]);
        var bx = int.Parse(b.Words().ToList()[2][2..]);
        var by = int.Parse(b.Words().ToList()[3][2..]);
        var prizeX = int.Parse(prize.Words().ToList()[1][2..]);
        var prizeY = int.Parse(prize.Words().ToList()[2][2..]);

        return (ax, ay, bx, by, prizeX, prizeY);
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
