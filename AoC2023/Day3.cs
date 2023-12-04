namespace AoC2023;

public class Day3
{
    public static void Solver()
    {
        var inputLines = File.ReadAllLines("inputs/day3");
        var res = CalcGearRatiosSum_Part2(inputLines);
        Console.WriteLine(res);
    }

    public static int CalcEnginePartNumbers_Part1(string[] inputLines)
    {
        var totalSum = 0;
        var charVisitedMatrix = ConvertToCharMatrix(inputLines);
        var visitedNumbersCoordinates = new HashSet<(int x, int y)>();
        for (int y = 0; y < charVisitedMatrix.Length; y++)
        {
            for (int x = 0; x < charVisitedMatrix[y].Length; x++)
            {
                if (CharIsPartNumSymbol(charVisitedMatrix[y][x]))
                {
                    var numbersAroundSymbol = ExtractNumbersAround(y, x, charVisitedMatrix).ToArray();
                    totalSum += numbersAroundSymbol
                        .Where(numAndCoordinate => !visitedNumbersCoordinates.Contains(numAndCoordinate.numCoord))
                        .Sum(numAndCoordinate => numAndCoordinate.num);
                    foreach (var (numCoord, _) in numbersAroundSymbol)
                    {
                        visitedNumbersCoordinates.Add(numCoord);
                    }
                }
            }
        }

        return totalSum;
    }
    
    public static int CalcGearRatiosSum_Part2(string[] inputLines)
    {
        var totalSum = 0;
        var charVisitedMatrix = ConvertToCharMatrix(inputLines);
        for (int y = 0; y < charVisitedMatrix.Length; y++)
        {
            for (int x = 0; x < charVisitedMatrix[y].Length; x++)
            {
                if (CharIsGearSymbol(charVisitedMatrix[y][x]))
                {
                    var numbersAroundSymbol = ExtractNumbersAround(y, x, charVisitedMatrix).ToArray();
                    if (numbersAroundSymbol.Length == 2)
                        totalSum += numbersAroundSymbol[0].num * numbersAroundSymbol[1].num;
                }
            }
        }

        return totalSum;
    }

    private static bool CharIsPartNumSymbol(char symbol)
        => !char.IsNumber(symbol) && symbol != '.';

    private static bool CharIsGearSymbol(char symbol) => symbol == '*';

    private static IEnumerable<((int x, int y) numCoord, int num)> ExtractNumbersAround(
        int yOfSymbol,
        int xOfSymbol,
        char[][] charMatrix)
    {
        var possibleCoordinates = new (int y, int x)[]
            {
                (yOfSymbol - 1, xOfSymbol - 1),
                (yOfSymbol - 0, xOfSymbol - 1),
                (yOfSymbol + 1, xOfSymbol - 1),
                (yOfSymbol - 1, xOfSymbol - 0),
                (yOfSymbol + 1, xOfSymbol - 0),
                (yOfSymbol - 1, xOfSymbol + 1),
                (yOfSymbol - 0, xOfSymbol + 1),
                (yOfSymbol + 1, xOfSymbol + 1),
            }
            .Where(coord => CoordinatesExist(coord, charMatrix))
            .ToArray();
        var visitedCoords = new HashSet<(int x, int y)>();
        foreach (var (y, x) in possibleCoordinates)
        {
            var cellSymbol = charMatrix[y][x];
            if (char.IsNumber(cellSymbol) && !visitedCoords.Contains((x, y)))
            {
                var num = CharDigitToInt(cellSymbol);
                //read num to the left
                int numX = x, minX = x;
                var decimalMultiplier = 1;
                visitedCoords.Add((numX, y));
                while (CoordinatesExist((y, numX - 1), charMatrix)
                       && char.IsNumber(charMatrix[y][numX - 1]))
                {
                    numX--;
                    minX = numX;
                    visitedCoords.Add((numX, y));
                    decimalMultiplier *= 10;
                    num += decimalMultiplier * CharDigitToInt(charMatrix[y][numX]);
                }
                //read num to the right
                numX = x;
                while (CoordinatesExist((y, numX + 1), charMatrix)
                       && char.IsNumber(charMatrix[y][numX + 1]))
                {
                    numX++;
                    visitedCoords.Add((numX, y));
                    num *= 10;
                    num += CharDigitToInt(charMatrix[y][numX]);
                }

                yield return ((minX, y), num);
            }
        }
    }

    private static int CharDigitToInt(char digit) => digit - '0';

    private static bool CoordinatesExist((int y, int x) coord, char[][] matrix)
    {
        return coord is { y: >= 0, x: >= 0 }
               && coord.y < matrix.Length
               && coord.x < matrix[coord.y].Length;
    }

    private static char[][] ConvertToCharMatrix(string[] inputLines)
    {
        var res = new char[inputLines.Length][];
        for (var y = 0; y < inputLines.Length; y++)
        {
            var line = inputLines[y];
            res[y] = line.ToArray();
        }

        return res;
    }
}