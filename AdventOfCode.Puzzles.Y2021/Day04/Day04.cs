namespace AdventOfCode.Puzzles.Y2021.Days.Day04;

public class Day04 : Day
{
    private static bool IsWin(int?[][] board) => board.Any(x => x.All(y => y is null)) || board.Transpose().Any(x => x.All(y => y is null));
    private static int GetScore(int?[][] board) => board.Sum(x => x.Sum(y => y ?? 0));

    public override Output Part1()
    {
        var value = Input.Paragraphs();
        var numbers = value[0].Split(",").Select(Int32.Parse);
        var boards = value.Skip(1).Select(x =>
            x.Lines().Select(y =>
                Regex.Replace(y, @"\s+", " ").Trim().Split(' ').Select(z =>
                    (int?)Int32.Parse(z)
                ).ToArray()
            ).ToArray()
        ).ToArray();

        foreach (var n in numbers)
        {
            foreach (var b in boards)
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (b[x][y] == n)
                            b[x][y] = null;
                    }
                }

                if (IsWin(b))
                {
                    return GetScore(b) * n;
                }
            }
        }

        return AnswerNotFound();
    }

    public override Output Part2()
    {
        var value = Input.Paragraphs();
        var numbers = value[0].Split(",").Select(Int32.Parse);
        int?[][]?[] boards = value.Skip(1).Select(x =>
            x.Lines().Select(y =>
                Regex.Replace(y, @"\s+", " ").Trim().Split(' ').Select(z =>
                    (int?)Int32.Parse(z)
                ).ToArray()
            ).ToArray()
        ).ToArray();

        foreach (var n in numbers)
        {
            for (var i = 0; i < boards.Length; i++)
            {
                var b = boards[i];
                if (b is null)
                    continue;

                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (b[x][y] == n)
                            b[x][y] = null;
                    }
                }

                if (IsWin(b))
                {
                    boards[i] = null;
                    if (boards.All(x => x is null))
                        return GetScore(b) * n;
                }
            }
        }

        return AnswerNotFound();
    }
}
