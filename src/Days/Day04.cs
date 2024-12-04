
using System.Windows.Documents;

namespace AdventOfCode.Days;

[Day(2024, 4)]
public class Day04 : BaseDay
{
    public override string PartOne(string input)
    {
        var board = input.CreateCharGrid();
        var result = 0;

        foreach (var group in board.GetAllGroups(4))
        {
            if (group is "XMAS" or "SAMX")
            {
                result++;
            }
        }

        return result.ToString();
    }

    public override string PartTwo(string input)
    {
        var board = input.CreateCharGrid();

        char[,] match1 =
        {
            { 'M', '*', 'S' },
            { '*', 'A', '*' },
            { 'M', '*', 'S' }
        };

        char[,] match2 =
        {
            { 'S', '*', 'S' },
            { '*', 'A', '*' },
            { 'M', '*', 'M' }
        };

        char[,] match3 =
        {
            { 'M', '*', 'M' },
            { '*', 'A', '*' },
            { 'S', '*', 'S' }
        };

        char[,] match4 =
        {
            { 'S', '*', 'M' },
            { '*', 'A', '*' },
            { 'S', '*', 'M' }
        };

        var matches = board.CountMatchingBlocks(match1);
        matches += board.CountMatchingBlocks(match2);
        matches += board.CountMatchingBlocks(match3);
        matches += board.CountMatchingBlocks(match4);

        return matches.ToString();
    }
}
