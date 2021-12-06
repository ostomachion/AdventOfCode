namespace AdventOfCode.Puzzles.Y2021.Days.Day06;

public class Day06 : Day
{
    public override Output Part1()
    {
        DefaultDictionary<int, long> fish = new();
        foreach (var f in Input.Split(',').Select(Int32.Parse))
        {
            fish[f]++;
        }

        for (var i = 0; i < 80; i++)
        {
            DefaultDictionary<int, long> newFish = new();
            foreach (var f in fish.Keys)
            {
                if (f == 0)
                {
                    newFish[6] += fish[f];
                    newFish[8] += fish[f];
                }
                else
                {
                    newFish[f - 1] += fish[f];
                }
            }
            fish = newFish;
        }

        return fish.Values.Sum();
    }

    public override Output Part2()
    {
        DefaultDictionary<int, long> fish = new();
        foreach (var f in Input.Split(',').Select(Int32.Parse))
        {
            fish[f]++;
        }

        for (var i = 0; i < 256; i++)
        {
            DefaultDictionary<int, long> newFish = new();
            foreach (var f in fish.Keys)
            {
                if (f == 0)
                {
                    newFish[6] += fish[f];
                    newFish[8] += fish[f];
                }
                else
                {
                    newFish[f - 1] += fish[f];
                }
            }
            fish = newFish;
        }

        return fish.Values.Sum();
    }
}
