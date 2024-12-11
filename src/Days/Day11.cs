namespace AdventOfCode.Days;

[Day(2024, 11)]
public class Day11 : BaseDay
{
    public override string PartOne(string input)
    {
        var stones = new LinkedList<long>(input.Longs());

        for (var i = 0; i < 25; i++)
        {
            var node = stones.First;

            //var txt = "";

            //foreach (var x in stones)
            //{
            //    txt += x.ToString() + " ";
            //}

            //Log(txt);

            while (node != null)
            {
                if (node.Value == 0)
                {
                    node.Value = 1;
                }
                else if (node.Value.ToString().Length % 2 == 0)
                {
                    stones.AddBefore(node, long.Parse(node.Value.ToString()[..(node.Value.ToString().Length / 2)]));
                    stones.AddBefore(node, long.Parse(node.Value.ToString()[(node.Value.ToString().Length / 2)..]));
                    var right = node.Previous;
                    stones.Remove(node);
                    node = right;
                }
                else
                {
                    node.Value *= 2024;
                }

                node = node.Next;
            }
        }

        return stones.Count.ToString();
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
