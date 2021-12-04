namespace AdventOfCode.Puzzles.Y2021.Days.Day04;

public class Day04 : Day
{
    public override Output Part1()
    {
        var value = Input.Split("\n\n");
        var ns = value[0].Split(",").Select(Int32.Parse);
        var boards = value.Skip(1).Select(x => x.Lines().Select(x => Regex.Replace(x, @"\s+", " ").Trim()).Split(' ').ToArray()).ToArray();
        //var boards = boardsP.Select(x => new SparsePlaneGrid2D<int>)
        foreach (var n in ns)
        {
            foreach (var b in boards)
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (b[x][y] == n.ToString())
                            b[x][y] = null;
                    }
                }

                for (int y = 0; y < 5; y++)
                {
                    bool win = true;
                    for (int x = 0; x < 5; x++)
                    {
                        if (b[x][y] is not null)
                            win = false;
                    }
                    if (win)
                    {
                        var w = 0;
                        for (int j = 0; j < 5; j++)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                w += Int32.Parse(b[i][j] ?? "0");
                            }
                        }
                        return w * n;
                    }
                }

                for (int x = 0; x < 5; x++)
                {
                    bool win = true;
                    for (int y = 0; y < 5; y++)
                    {
                        if (b[x][y] is not null)
                            win = false;
                    }
                    if (win)
                    {
                        var w = 0;
                        for (int j = 0; j < 5; j++)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                w += Int32.Parse(b[i][j] ?? "0");
                            }
                        }
                        return w * n;
                    }
                }
            }
        }

        return AnswerNotFound();
    }

    public override Output Part2()
    {
        //var value = Input.Parse<Test>(@"(\d+:N)+ % ',' \s+ (((\d+:Items) % ' '+)+:Values % '\n')+:Boards % '\n\n'");
        var value = Input.Split("\n\n");
        var ns = value[0].Split(",").Select(Int32.Parse);
        var boards = value.Skip(1).Select(x => x.Lines().Select(x => Regex.Replace(x, @"\s+", " ").Trim()).Split(' ').ToArray()).ToArray();
        //var boards = boardsP.Select(x => new SparsePlaneGrid2D<int>)
        foreach (var n in ns)
        {
            for (var bb = 0; bb < boards.Length; bb++)
            {

                var b = boards[bb];
                if (b is null)
                    continue;

                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (b[x][y] == n.ToString())
                            b[x][y] = null;
                    }
                }

                for (int y = 0; y < 5; y++)
                {
                    bool win = true;
                    for (int x = 0; x < 5; x++)
                    {
                        if (b[x][y] is not null)
                            win = false;
                    }
                    if (win)
                    {
                        boards[bb] = null;

                        if (boards.Count(x => x is not null) == 0)
                        {
                            var w = 0;
                            for (int j = 0; j < 5; j++)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    w += Int32.Parse(b[i][j] ?? "0");
                                }
                            }
                            return w * n;
                        }
                    }
                }

                for (int x = 0; x < 5; x++)
                {
                    bool win = true;
                    for (int y = 0; y < 5; y++)
                    {
                        if (b[x][y] is not null)
                            win = false;
                    }
                    if (win)
                    {
                        boards[bb] = null;

                        if (boards.Count(x => x is not null) == 0)
                        {
                            var w = 0;
                            for (int j = 0; j < 5; j++)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    w += Int32.Parse(b[i][j] ?? "0");
                                }
                            }
                            return w * n;
                        }
                    }
                }
            }
        }

        return AnswerNotFound();
    }
}
