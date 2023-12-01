using AoC2023;
using FluentAssertions;

namespace Tests;

public class Day1Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(17, "onewere7")]
    [TestCase(11, "one")]
    [TestCase(11, "1")]
    [TestCase(13, "13zero")]
    [TestCase(27, "2were7")]
    [TestCase(25, "2werefive")]
    [TestCase(281, "two1nine\neightwothree\nabcone2threexyz\nxtwone3four\n4nineeightseven2\nzoneight234\n7pqrstsixteen")]
    [TestCase(93 + 17 + 76, "nine9qnvrzfone1threeptlxqjksbg\nonesix9seven9tggdzxhvm5tctqmseven\nsevenfour6pvhnmmm")]
    [TestCase(82, "eightwo")]
    [TestCase(21, "twone")]
    [TestCase(88, "eeeight")]
    [TestCase(59, "ckpgbsfpdffiveprvqmmczhsthreeeight9")]
    [TestCase(24, "twozcd26onefourr")]
    public void PartTwo(int expected, string fullInput)
    {
        var input = fullInput.Split('\n');
        var actual = Day1.SumLinesNumsPart2(input);
        actual.Should().Be(expected);
    }
}