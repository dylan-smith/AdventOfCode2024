using System.Windows.Input;

namespace AdventOfCode.Days;

[Day(2024, 9)]
public class Day09 : BaseDay
{
    public override string PartOne(string input)
    {
        var disk = BuildDisk(input.Trim());

        var lastFilePosition = GetLastFilePosition(disk, disk.Count);
        var firstFreeSpace = GetFirstFreeSpacePosition(disk);

        while (firstFreeSpace < lastFilePosition)
        {
            disk[firstFreeSpace] = disk[lastFilePosition];
            disk[lastFilePosition] = -1;

            lastFilePosition = GetLastFilePosition(disk, lastFilePosition);
            firstFreeSpace = GetFirstFreeSpacePosition(disk, firstFreeSpace);
        }

        return CalcChecksum(disk).ToString();
    }

    private int GetFirstFreeSpacePosition(List<int> disk, int start = -1)
    {
        for (var i = start + 1; i < disk.Count; i++)
        {
            if (disk[i] == -1)
            {
                return i;
            }
        }

        return -1;
    }

    private int GetLastFilePosition(List<int> disk, int start)
    {
        for (var i = start - 1; i >= 0; i--)
        {
            if (disk[i] != -1)
            {
                return i;
            }
        }

        return -1;
    }

    private List<int> BuildDisk(string input)
    {
        var disk = new List<int>();
        var freespace = false;
        var id = 0;

        foreach (var c in input)
        {
            var value = int.Parse(c.ToString());

            if (!freespace)
            {
                disk.AddMany(id, value);
                id++;
            }
            else
            {
                disk.AddMany(-1, value);
            }

            freespace = !freespace;
        }

        return disk;
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
        var (fileLocations, freespaceLocations) = ProcessInput(input.Trim());

        for (var i = fileLocations.Count - 1; i >= 0; i--)
        {
            var newLocations = freespaceLocations.Where(x => x.position < fileLocations[i].position).Where(x => x.length >= fileLocations[i].length).ToList();

            if (newLocations.Any())
            {
                fileLocations[i] = (fileLocations[i].id, fileLocations[i].length, newLocations.First().position);
                
                for (var j = 0; j < freespaceLocations.Count; j++)
                {
                    if (freespaceLocations[j].position == newLocations.First().position)
                    {
                        freespaceLocations[j] = (freespaceLocations[j].length - fileLocations[i].length, freespaceLocations[j].position + fileLocations[i].length);
                        break;
                    }
                }
            }
        }

        var checksum = 0L;

        foreach (var (id, length, position) in fileLocations)
        {
            for (var i = 0; i < length; i++)
            {
                checksum += (position + i) * id;
            }
        }

        return checksum.ToString();
    }

    private (List<(int id, int length, int position)> fileLocations, List<(int length, int position)> freespaceLocations) ProcessInput(string input)
    {
        var fileLocations = new List<(int id, int length, int position)>();
        var freespaceLocations = new List<(int length, int position)>();

        var freespace = false;
        var id = 0;
        var pos = 0;

        foreach (var c in input.Trim())
        {
            var value = int.Parse($"{c}");

            if (!freespace)
            {
                fileLocations.Add((id++, value, pos));
                pos += value;
            }
            else
            {
                freespaceLocations.Add((value, pos));
                pos += value;
            }

            freespace = !freespace;
        }

        return (fileLocations, freespaceLocations);
    }
}
