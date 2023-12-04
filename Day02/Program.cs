using System.Diagnostics;
using System.Dynamic;

var content = File.ReadAllLines("input.txt");

static IReadOnlyCollection<Game> ParseGames(string[] content)
{
    return content.Select(line => ParseGame(line)).ToList();
    Game ParseGame(string input)
    {
        var gameSplitted = input.Split(':');
        var gameId = int.Parse(gameSplitted[0].Replace("Game ", ""));

        var sets = gameSplitted[1].Split(';').Select(s => ParseGameSet(s)).ToList();
        return new Game(gameId, sets);

        GameSet ParseGameSet(string setInput)
        {
            var colors = setInput.Split(',');
            int red = 0;
            int blue = 0;
            int green = 0;
            foreach (var color in colors)
            {
                var currentColor = color.TrimStart();
                if (currentColor.Contains("blue"))
                {
                    blue += int.Parse(currentColor.Split(" ")[0]);
                }
                if (currentColor.Contains("red"))
                {
                    red += int.Parse(currentColor.Split(" ")[0]);
                }
                if (currentColor.Contains("green"))
                {
                    green += int.Parse(currentColor.Split(" ")[0]);
                }
            }
            return new GameSet(red, blue, green);
        }
    }
}

static int GetPartOne(string[] content)
{
    var result = ParseGames(content)
        .Select(g => GetGameResult(g))
        .Where(r => r.IsWin)
        .Sum(g => g.GameId);
    return result;

    GameResult GetGameResult(Game game)
    {
        return new GameResult(game.Id, IsWin(game));

        bool IsWin(Game game)
        {
            const int maxRed = 12;
            const int maxGreen = 13;
            const int maxBlue = 14;

            foreach (var set in game.Sets)
            {
                if (set.Red > maxRed || set.Blue > maxBlue || set.Green > maxGreen)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

static int GetPartTwo(string[] content)
{
    var result = ParseGames(content)
       .Select(g => GetGamePower(g))
       .Sum(g => g.Power);
    return result;

    GamePower GetGamePower(Game game)
    {
        var minRed = game.Sets.Max(s => s.Red);
        var minBlue = game.Sets.Max(s => s.Blue);
        var minGreen = game.Sets.Max(s => s.Green);
        return new GamePower(minRed * minBlue * minGreen);
    }
}


Console.WriteLine("Part 1:");
var stopwatch = Stopwatch.StartNew();
Console.WriteLine(GetPartOne(content));
stopwatch.Stop();
Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds}ms");

Console.WriteLine("Part 2:");
stopwatch = Stopwatch.StartNew();
Console.WriteLine(GetPartTwo(content));
stopwatch.Stop();
Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds}ms");

record GameSet(int Red, int Blue, int Green);

record Game(int Id, IReadOnlyCollection<GameSet> Sets);

record GameResult(int GameId, bool IsWin);

record GamePower(int Power);