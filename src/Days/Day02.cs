

namespace AdventOfCode.Days;

[Day(2024, 2)]
public class Day02 : BaseDay
{
    public override string PartOne(string input)
    {
        var reports = input.ParseLines(line => line.Integers().ToList()).ToList();

        var safeCount = reports.Count(report => IsSafe(report));

        return safeCount.ToString();
    }

    private bool IsSafe(List<int> report)
    {
        var increases = 0;
        var decreases = 0;

        for (int i = 1; i < report.Count(); i++)
        {
            if (report[i] > report[i - 1])
            {
                increases++;
            }

            if (report[i] < report[i - 1])
            {
                decreases++;
            }

            if (increases > 0 && decreases > 0)
            {
                return false;
            }

            if (report[i] == report[i - 1])
            {
                return false;
            }

            if (Math.Abs(report[i] - report[i - 1]) > 3)
            {
                return false;
            }
        }

        return true;
    }

    public override string PartTwo(string input)
    {
        var reports = input.ParseLines(line => line.Integers().ToList()).ToList();

        var safeCount = 0;

        foreach (var report in reports)
        {
            if (IsSafe(report))
            {
                safeCount++;
            } else
            {
                for (var i = 0; i < report.Count; i++)
                {
                    var newReport = new List<int>(report);
                    newReport.RemoveAt(i);

                    if (IsSafe(newReport))
                    {
                        safeCount++;
                        break;
                    }
                }
            }
        }

        return safeCount.ToString();
    }
}
