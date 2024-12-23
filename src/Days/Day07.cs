﻿namespace AdventOfCode.Days;

[Day(2024, 7)]
public class Day07 : BaseDay
{
    public override string PartOne(string input)
    {
        var equations = input.ParseLines(ParseEquation).ToList();

        var good = equations.Where(x => IsEquationTrue(x.Answer, x.Numbers)).ToList();
        return good.Sum(x => x.Answer).ToString();
    }

    private bool IsEquationTrue(long answer, List<long> numbers, bool partTwo = false)
    {
        if (numbers.Count == 1)
        {
            return answer == numbers[0];
        }

        // addition
        var newNumbers = numbers.Skip(1).ToList();
        var newAnswer = answer - numbers[0];

        if (newAnswer >= 0 && IsEquationTrue(newAnswer, newNumbers, partTwo))
        {
            return true;
        }

        // multiplication
        newAnswer = answer / numbers[0];
        var remainder = answer % numbers[0];

        if (remainder == 0 && IsEquationTrue(newAnswer, newNumbers, partTwo))
        {
            return true;
        }

        // concatenation
        if (partTwo && answer.ToString().EndsWith(numbers[0].ToString()))
        {
            newAnswer = long.Parse(answer.ToString().ShaveRight(numbers[0].ToString().Length));

            if (IsEquationTrue(newAnswer, newNumbers, partTwo))
            {
                return true;
            }
        }

        return false;
    }

    private (long Answer, List<long> Numbers) ParseEquation(string line)
    {
        var parts = line.Split(new string[] { " ", ":" }, StringSplitOptions.RemoveEmptyEntries);

        var answer = long.Parse(parts.First());
        var numbers = parts.Skip(1).Select(long.Parse);

        return (answer, numbers.Reverse().ToList());
    }

    public override string PartTwo(string input)
    {
        var equations = input.ParseLines(ParseEquation).ToList();

        var good = equations.Where(x => IsEquationTrue(x.Answer, x.Numbers, true)).ToList();
        return good.Sum(x => x.Answer).ToString();
    }
}
