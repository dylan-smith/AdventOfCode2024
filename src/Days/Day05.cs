namespace AdventOfCode.Days;

[Day(2024, 5)]
public class Day05 : BaseDay
{
    public override string PartOne(string input)
    {
        var rules = input.Paragraphs().First().ParseLines(ParseRule).ToList();
        var updates = input.Paragraphs().Last().ParseLines(line => line.Integers().ToList()).ToList();

        var result = 0L;

        foreach (var update in updates)
        {
            if (UpdateIsValid(update, rules))
            {
                result += GetMiddlePage(update);
            }
        }

        return result.ToString();
    }

    private long GetMiddlePage(List<int> update) => update[update.Count / 2];

    private bool UpdateIsValid(List<int> update, List<(int Before, int After)> rules)
    {
        foreach (var (Before, After) in rules)
        {
            if (update.Contains(Before) && update.Contains(After))
            {
                if (update.IndexOf(Before) > update.IndexOf(After))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private (int Before, int After) ParseRule(string line)
    {
        var before = int.Parse(line.Split('|').First());
        var after = int.Parse(line.Split("|").Last());

        return (before, after);
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}