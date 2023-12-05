namespace AoC2023;

public class Day4
{
    public static void Solver()
    {
        var inputLines = File.ReadAllLines("inputs/day4");
        var res = CalcCardsInstances_Part2(inputLines);
        Console.WriteLine(res);
    }

    public static int CalcWinningPoints_Part1(string[] inputLines)
    {
        var sum = 0;
        foreach (var line in inputLines)
        {
            var card = ParseCard(line);
            var countOfWinningActual = card.Actual.Count(num => card.Winning.Contains(num));
            var points = (1 << countOfWinningActual) / 2;
            sum += points;
        }

        return sum;

    }
    
    public static int CalcCardsInstances_Part2(string[] inputLines)
    {
        var totalCardsInstances = 0;
        var numOfCopiesById = new Dictionary<int, int>();
        foreach (var line in inputLines)
        {
            var card = ParseCard(line);
            var currentCardIdTotalCounts = numOfCopiesById.GetValueOrDefault(card.Id, 0) + 1;
            totalCardsInstances += currentCardIdTotalCounts;

            numOfCopiesById.Remove(card.Id);
            var countOfWinningActual = card.Actual.Count(num => card.Winning.Contains(num));
            for (int i = 1; i <= countOfWinningActual; i++)
            {
                if (numOfCopiesById.ContainsKey(card.Id + i))
                {
                    numOfCopiesById[card.Id + i]+= currentCardIdTotalCounts;
                }
                else
                {
                    numOfCopiesById[card.Id + i] = currentCardIdTotalCounts;
                }
            }
        }

        return totalCardsInstances;

    }


    private static Card ParseCard(string line)
    {
        var cardPrefix = "Card ";
        var colonSplit = line.Split(":", SplitOptions);
        var cardId = int.Parse(colonSplit[0].Substring(cardPrefix.Length));
        var dashSplit = colonSplit[1].Split('|', SplitOptions);
        var winningNums = dashSplit[0].Split(' ', SplitOptions).Select(int.Parse).ToHashSet();
        var actualNums = dashSplit[1].Split(' ', SplitOptions).Select(int.Parse).ToArray();
        return new Card(cardId, winningNums, actualNums);
    }
    private record Card(int Id, HashSet<int> Winning, int[] Actual);
    private const StringSplitOptions SplitOptions =
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

}