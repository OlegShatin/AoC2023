namespace AoC2023;

public class Day1
{
    public static void Solver()
    {
        var inputLines = File.ReadAllLines("inputs/day1");

        var sum = SumLinesNumsPart2(inputLines);

        var res = sum;
        Console.WriteLine(res);
    }

    public static int SumLinesNumsPart1(string[] inputLines)
    {
        var sum = 0;
        foreach (var line in inputLines)
        {
            var firstDigit = line.First(char.IsDigit);
            var lastDigit = line.Reverse().First(char.IsDigit);
            var lineNum = int.Parse("" + firstDigit + lastDigit);
            sum += lineNum;
        }

        return sum;
    }

    public static int SumLinesNumsPart2(string[] inputLines)
    {
        var sum = 0;
        foreach (var line in inputLines)
        {
            var firstDigit = ExtractFirstDigit(line);
            var lastDigit = ExtractLastDigit(line);
            var lineNum = int.Parse("" + firstDigit + lastDigit);
            sum += lineNum;
        }

        return sum;


        char ExtractFirstDigit(string line)
        {
            var digitNameProgressBars = _digitTuples.ToDictionary(pair => pair.digitName, _ => 0);
            foreach (var letter in line)
            {
                if (char.IsDigit(letter))
                    return letter;
                foreach (var digitNameProgress in digitNameProgressBars)
                {
                    var digitName = digitNameProgress.Key;
                    if (digitName[digitNameProgress.Value] == letter)
                        digitNameProgressBars[digitName]++;
                    else if (digitName[0] == letter)
                        digitNameProgressBars[digitName] = 1;
                    else
                        digitNameProgressBars[digitName] = 0;

                    if (digitName.Length == digitNameProgressBars[digitName])
                        return _digitTuples.First(pair => pair.digitName == digitName).digit;
                }
            }

            throw new ArgumentException();
        }

        char ExtractLastDigit(string line)
        {
            var digitNamesProgressBar = _reversedDigitTuples.ToDictionary(pair => pair.digitName, _ => 0);
            for (var i = line.Length - 1; i >= 0; i--)
            {
                var letter = line[i];
                if (char.IsDigit(letter))
                    return letter;
                foreach (var digitNameProgress in digitNamesProgressBar)
                {
                    var digitName = digitNameProgress.Key;
                    if (digitName[digitNameProgress.Value] == letter)
                        digitNamesProgressBar[digitName]++;
                    else if (digitName[0] == letter)
                        digitNamesProgressBar[digitName] = 1;
                    else
                        digitNamesProgressBar[digitName] = 0;

                    if (digitName.Length == digitNamesProgressBar[digitName])
                        return _reversedDigitTuples.First(pair => pair.digitName == digitName).digit;
                }
            }

            throw new ArgumentException();
        }
    }

    private static (char digit, string digitName)[] _digitTuples =
    {
        ('1', "one"),
        ('2', "two"),
        ('3', "three"),
        ('4', "four"),
        ('5', "five"),
        ('6', "six"),
        ('7', "seven"),
        ('8', "eight"),
        ('9', "nine")
    };

    private static (char digit, string digitName)[] _reversedDigitTuples =
        _digitTuples
            .Select(pair => pair with { digitName = string.Concat(pair.digitName.Reverse()) })
            .ToArray();
}