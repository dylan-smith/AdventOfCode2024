
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
        var result = 0;

        for (var x = 0; x < board.Width() - 2; x++)
        {
            for (var y = 0; y < board.Height() - 2; y++)
            {
                if (board[x + 1, y + 1] == 'A')
                {
                    if (board[x, y] == 'M' && board[x + 2, y] == 'S' && board[x, y + 2] == 'M' && board[x + 2, y + 2] == 'S')
                    {
                        result++;
                    }

                    if (board[x, y] == 'S' && board[x + 2, y] == 'S' && board[x, y + 2] == 'M' && board[x + 2, y + 2] == 'M')
                    {
                        result++;
                    }

                    if (board[x, y] == 'M' && board[x + 2, y] == 'M' && board[x, y + 2] == 'S' && board[x + 2, y + 2] == 'S')
                    {
                        result++;
                    }

                    if (board[x, y] == 'S' && board[x + 2, y] == 'M' && board[x, y + 2] == 'S' && board[x + 2, y + 2] == 'M')
                    {
                        result++;
                    }
                }
            }
        }

        return result.ToString();
    }
}
