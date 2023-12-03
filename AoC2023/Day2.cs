namespace AoC2023;

public class Day2
{
    public static void Solver()
    {
        var inputLines = File.ReadAllLines("inputs/day2");
        var res = CalcPowerSumPart2(inputLines);
        Console.WriteLine(res);
    }

    private static readonly IDictionary<string, int> CubesCountsByColor = new Dictionary<string, int>()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 },
    };

    public static int SumLinesNumsPart1(string[] inputLines)
    {
        var sum = 0;
        foreach (var game in ExtractGamesFromLines(inputLines))
        {
            var isValidGame = true;
            foreach (var cubeEntry in game.Rounds.SelectMany(r => r.Cubes))
            {
                isValidGame &= cubeEntry.count <= CubesCountsByColor[cubeEntry.color];
            }

            sum += isValidGame ? game.Id : 0;
        }

        return sum;
    }

    public static int CalcPowerSumPart2(string[] inputLines)
    {
        var sum = 0;
        foreach (var game in ExtractGamesFromLines(inputLines))
        {
            var isValidGame = true;
            var countsOfMinimumSet = game.Rounds
                .SelectMany(r => r.Cubes)
                .GroupBy(cubeEntry => cubeEntry.color)
                .Select(cubesOfSameColor => cubesOfSameColor.Max(cubeEntry => cubeEntry.count))
                .ToArray();
            //note: if there is no cubes of particular color, the power is 0
            if (countsOfMinimumSet.Length < CubesCountsByColor.Count)
                continue;
            var power = countsOfMinimumSet.Aggregate((acc, minColorCubesCount) => acc * minColorCubesCount);
            sum += power;
        }

        return sum;
    }

    private const StringSplitOptions SplitOptions =
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    private static IEnumerable<Game> ExtractGamesFromLines(string[] inputLines)
    {
        var gamePrefix = "Game ";
        foreach (var line in inputLines)
        {
            var splitByColon = line.Split(':', SplitOptions);
            var splitRounds = splitByColon[1].Split(';', SplitOptions);
            var rounds = new List<Round>();
            foreach (var roundLine in splitRounds)
            {
                var cubesEntries = roundLine.Split(',', SplitOptions);
                var cubes = cubesEntries
                    .Select(cubesEntry => cubesEntry.Split(' ', SplitOptions))
                    .Select(countAndColor => (count: int.Parse(countAndColor[0]), color: countAndColor[1]))
                    .ToArray();
                rounds.Add(new Round(cubes));
            }

            var gameId = int.Parse(splitByColon[0].Substring(gamePrefix.Length));
            yield return new Game(gameId, rounds.ToArray());
        }
    }

    private record Game(int Id, Round[] Rounds);

    private record Round((int count, string color)[] Cubes);
}