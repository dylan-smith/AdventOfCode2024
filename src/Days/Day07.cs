
namespace AdventOfCode.Days;

[Day(2024, 7)]
public class Day07 : BaseDay
{
    public override string PartOne(string input)
    {
        var equations = input.ParseLines(ParseEquation).ToList();

        var good = equations.Where(x => IsEquationTrue(x.Answer, x.Numbers)).ToList();

        foreach (var g in good)
        {
            Log($"GOOD EQUATION: {g.Answer}");
            IsEquationTrue(g.Answer, g.Numbers);
        }

        return good.Sum(x => x.Answer).ToString();
    }

    private bool IsEquationTrue(long answer, IEnumerable<long> numbers)
    {
        if (numbers.Count() == 1)
        {
            return answer == numbers.First();
        }

        // addition
        var newNumbers = numbers.Skip(1);
        var newAnswer = answer - numbers.First();

        if (IsEquationTrue(newAnswer, newNumbers))
        {
            Log($"{numbers.First()} + ...");
            return true;
        }

        // multiplication
        newAnswer = answer / numbers.First();
        var remainder = answer % numbers.First();

        if (remainder == 0 && IsEquationTrue(newAnswer, newNumbers))
        {
            Log($"{numbers.First()} * ...");
            return true;
        }

        return false;
    }

    private bool IsEquationTrue2(long answer, IEnumerable<long> numbers)
    {
        if (numbers.Count() == 1)
        {
            return answer == numbers.First();
        }

        // addition
        var newNumbers = numbers.Skip(1);
        var newAnswer = answer - numbers.First();

        if (newAnswer >= 0 && IsEquationTrue2(newAnswer, newNumbers))
        {
            Log($"{numbers.First()} + ...");
            return true;
        }

        // multiplication
        newAnswer = answer / numbers.First();
        var remainder = answer % numbers.First();

        if (remainder == 0 && IsEquationTrue2(newAnswer, newNumbers))
        {
            Log($"{numbers.First()} * ...");
            return true;
        }

        // concatenation
        if (answer.ToString().EndsWith(numbers.First().ToString()))
        {
            newAnswer = long.Parse(answer.ToString().ShaveRight(numbers.First().ToString().Length));

            if (IsEquationTrue2(newAnswer, newNumbers))
            {
                Log($"{numbers.First()} || ...");
                return true;
            }
        }

        return false;
    }

    private (long Answer, IEnumerable<long> Numbers) ParseEquation(string line)
    {
        var parts = line.Split(new string[] { " ", ":" }, StringSplitOptions.RemoveEmptyEntries);

        var answer = long.Parse(parts.First());
        var numbers = parts.Skip(1).Select(long.Parse);

        return (answer, numbers.Reverse());
    }

    public override string PartTwo(string input)
    {
        var equations = input.ParseLines(ParseEquation).ToList();

        var good = equations.Where(x => IsEquationTrue2(x.Answer, x.Numbers)).ToList();

        foreach (var g in good)
        {
            Log($"GOOD EQUATION: {g.Answer}");
            IsEquationTrue2(g.Answer, g.Numbers);
        }

        return good.Sum(x => x.Answer).ToString();
    }
}
