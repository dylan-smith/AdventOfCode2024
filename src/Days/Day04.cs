
namespace AdventOfCode.Days;

[Day(2024, 4)]
public class Day04 : BaseDay
{
    public override string PartOne(string input)
    {
        var board = input.CreateCharGrid();
        var result = 0;

        for (var x = 0; x < board.Width(); x++)
        {
            for (var y = 0; y < board.Height() - 3; y++)
            {
                if (board[x, y] == 'X' && board[x, y + 1] == 'M' && board[x, y + 2] == 'A' && board[x, y + 3] == 'S')
                {
                    result++;
                }
            }
        }

        for (var x = 0; x < board.Width(); x++)
        {
            for (var y = board.Height() - 1; y > 2; y--)
            {
                if (board[x, y] == 'X' && board[x, y - 1] == 'M' && board[x, y - 2] == 'A' && board[x, y - 3] == 'S')
                {
                    result++;
                }
            }
        }

        for (var y = 0; y < board.Height(); y++)
        {
            for (var x = 0; x < board.Width() - 3; x++)
            {
                if (board[x, y] == 'X' && board[x + 1, y] == 'M' && board[x + 2, y] == 'A' && board[x + 3, y] == 'S')
                {
                    result++;
                }
            }
        }

        for (var y = 0; y < board.Height(); y++)
        {
            for (var x = board.Width() - 1; x > 2; x--)
            {
                if (board[x, y] == 'X' && board[x - 1, y] == 'M' && board[x - 2, y] == 'A' && board[x - 3, y] == 'S')
                {
                    result++;
                }
            }
        }

        for (var x = 0; x < board.Width() - 3; x++)
        {
            for (var y = 0; y < board.Height() - 3; y++)
            {
                if (board[x, y] == 'X' && board[x + 1, y + 1] == 'M' && board[x + 2, y + 2] == 'A' && board[x + 3, y + 3] == 'S')
                {
                    result++;
                }
            }
        }

        for (var x = board.Width() - 1; x > 2; x--)
        {
            for (var y = board.Height() - 1; y > 2; y--)
            {
                if (board[x, y] == 'X' && board[x - 1, y - 1] == 'M' && board[x - 2, y - 2] == 'A' && board[x - 3, y - 3] == 'S')
                {
                    result++;
                }
            }
        }

        for (var x = board.Width() - 1; x > 2; x--)
        {
            for (var y = 0; y < board.Height() - 3; y++)
            {
                if (board[x, y] == 'X' && board[x - 1, y + 1] == 'M' && board[x - 2, y + 2] == 'A' && board[x - 3, y + 3] == 'S')
                {
                    result++;
                }
            }
        }

        for (var x = 0; x < board.Width() - 3; x++)
        {
            for (var y = board.Height() - 1; y > 2; y--)
            {
                if (board[x, y] == 'X' && board[x + 1, y - 1] == 'M' && board[x + 2, y - 2] == 'A' && board[x + 3, y - 3] == 'S')
                {
                    result++;
                }
            }
        }

        return result.ToString();
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
