using AoC2023;
using FluentAssertions;

namespace Tests;

public class Day3Tests
{

    [TestCaseSource(nameof(TestCases_Part1))]
    public int PartOne(string fullInput)
    {
        var input = fullInput.Split('\n');
        var actual = Day3.CalcEnginePartNumbers_Part1(input);
        return actual;
    }
    
    [TestCaseSource(nameof(TestCases_Part2))]
    public int PartTwo(string fullInput)
    {
        var input = fullInput.Split('\n');
        var actual = Day3.CalcGearRatiosSum_Part2(input);
        return actual;
    }

    static IEnumerable<TestCaseData> TestCases_Part1()
    {
        var example
            = "467..114..\n" +
              "...*......\n" +
              "..35..633.\n" +
              "......#...\n" +
              "617*......\n" +
              ".....+.58.\n" +
              "..592.....\n" +
              "......755.\n" +
              "...$.*....\n" +
              ".664.598..";
        yield return new TestCaseData(example).Returns(4361).SetName("FirstExample");
    }
    
    static IEnumerable<TestCaseData> TestCases_Part2()
    {
        var example
            = "467..114..\n" +
              "...*......\n" +
              "..35..633.\n" +
              "......#...\n" +
              "617*......\n" +
              ".....+.58.\n" +
              "..592.....\n" +
              "......755.\n" +
              "...$.*....\n" +
              ".664.598..";
        yield return new TestCaseData(example).Returns(467835).SetName("FirstExample");
    }
}