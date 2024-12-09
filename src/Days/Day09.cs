
namespace AdventOfCode.Days;

[Day(2024, 9)]
public class Day09 : BaseDay
{
    public override string PartOne(string input)
    {
        var disk = new List<int>();
        var freespace = false;
        var id = 0;

        foreach (var c in input.Trim())
        {
            var value = int.Parse($"{c}");

            if (!freespace)
            {
                for (var i = 0; i < value; i++)
                {
                    disk.Add(id);
                }

                id++;
            }
            else
            {
                for (var i = 0; i < value; i++)
                {
                    disk.Add(-1);
                }
            }

            freespace = !freespace;
        }

        var lastFileBlock = 0;
        var lastFileBlockPosition = 0;

        for (var i = disk.Count - 1; i >= 0; i--)
        {
            if (disk[i] != -1)
            {
                lastFileBlock = disk[i];
                lastFileBlockPosition = i;
                break;
            }
        }

        var firstFreeSpace = 0;

        for (var i = 0; i < disk.Count; i++)
        {
            if (disk[i] == -1)
            {
                firstFreeSpace = i;
                break;
            }
        }

        while (firstFreeSpace < lastFileBlockPosition)
        {
            disk[firstFreeSpace] = lastFileBlock;
            disk[lastFileBlockPosition] = -1;

            for (var i = disk.Count - 1; i >= 0; i--)
            {
                if (disk[i] != -1)
                {
                    lastFileBlock = disk[i];
                    lastFileBlockPosition = i;
                    break;
                }
            }

            for (var i = 0; i < disk.Count; i++)
            {
                if (disk[i] == -1)
                {
                    firstFreeSpace = i;
                    break;
                }
            }
        }

        var checksum = CalcChecksum(disk);

        return checksum.ToString();
    }

    private object CalcChecksum(List<int> disk)
    {
        var result = 0L;
        var pos = 0;

        foreach (var item in disk)
        {
            if (item != -1)
            {
                result += item * pos++;
            }
        }

        return result;
    }

    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}
