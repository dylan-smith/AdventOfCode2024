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
        var rules = input.Paragraphs().First().ParseLines(ParseRule).ToList();
        var updates = input.Paragraphs().Last().ParseLines(line => line.Integers().ToList()).ToList();

        var result = 0L;

        foreach (var update in updates)
        {
            if (!UpdateIsValid(update, rules))
            {
                var newUpdate = ReorderUpdate(update, rules);

                result += GetMiddlePage(newUpdate);
            }
        }

        return result.ToString();
    }

    private List<int> ReorderUpdate(List<int> update, List<(int Before, int After)> rules)
    {
        var validRules = GetValidRules(rules, update).ToList();
        var result = new List<int>();

        while (validRules.Count > 0)
        {
            foreach (var (Before, After) in validRules)
            {
                if (!validRules.Any(x => x.After == Before))
                {
                    result.Add(Before);
                    break;
                }
            }

            validRules.RemoveAll(x => result.Contains(x.Before));
        }

        return result;
    }

    private IEnumerable<(int Before, int After)> GetValidRules(List<(int Before, int After)> rules, List<int> update)
    {
        foreach (var rule in rules)
        {
            if (update.Contains(rule.Before) && update.Contains(rule.After))
            {
                yield return rule;
            }
        }
    }
}