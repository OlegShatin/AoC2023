using AoC2023;
using FluentAssertions;

namespace Tests;

public class Day2Tests
{

    [TestCaseSource(nameof(TestCases_Part1))]
    public int PartOne(string fullInput)
    {
        var input = fullInput.Split('\n');
        var actual = Day2.SumLinesNumsPart1(input);
        return actual;
    }
    
    [TestCaseSource(nameof(TestCases_Part2))]
    public int PartTwo(string fullInput)
    {
        var input = fullInput.Split('\n');
        var actual = Day2.CalcPowerSumPart2(input);
        return actual;
    }

    static IEnumerable<TestCaseData> TestCases_Part1()
    {
        //limitation: 12 red cubes, 13 green cubes, and 14 blue cubes
        var example
            = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\n" +
              "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\n" +
              "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\n" +
              "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\n" +
              "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
        yield return new TestCaseData(example).Returns(8).SetName("FirstExample");
        yield return new TestCaseData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green").Returns(1).SetName("Simple passing");
        yield return new TestCaseData("Game 1: 3 blue, 200 red").Returns(0).SetName("Simple not passing");
        yield return new TestCaseData("Game 1: 3 blue, 13 red").Returns(0).SetName("Color exceeds only it's threshold");
        yield return new TestCaseData("Game 1: 14 blue, 12 red, 13 green").Returns(1).SetName("Passing, colors do not exceed their threshold");
    }
    
    static IEnumerable<TestCaseData> TestCases_Part2()
    {
        var example
            = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\n" +
              "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\n" +
              "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\n" +
              "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\n" +
              "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
        yield return new TestCaseData(example).Returns(2286).SetName("FirstExample");
        yield return new TestCaseData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green").Returns(6 * 4 * 2).SetName("Simple passing");
        yield return new TestCaseData("Game 1: 3 blue, 200 red").Returns(0).SetName("Missed color results 0");
        yield return new TestCaseData("Game 1: 1 blue, 1 red, 1 green; 1 blue, 1 red, 1 green; 1 blue, 1 red, 1 green").Returns(1).SetName("Power does not sum between rounds");
        yield return new TestCaseData("Game 1: 1 blue, 1 red, 1 green; 1 blue, 1 red, 1 green;\nGame 1: 1 blue, 1 red, 1 green").Returns(2).SetName("Power sum between games");
    }
}