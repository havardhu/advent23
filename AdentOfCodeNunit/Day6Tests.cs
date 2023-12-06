using AdventOfCodeLogic;

namespace AdentOfCodeNunit;
 [TestFixture]
public class Day6Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestPart1()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"Time:      7  15   30
Distance:  9  40  200");

        var logic = new AdventOfCodeLogic.Day6();
        var result = await logic.SolvePart1(dataProvider);
        Assert.That(result, Is.EqualTo(288));
    }

    [Test]
    public async Task RunPart1()
    {
        ITestDataProvider dataProvider = new SessionDataProvider(6);

        var logic = new AdventOfCodeLogic.Day6();
        var result = await logic.SolvePart1(dataProvider);
        Assert.That(result, Is.EqualTo(275724));
    }

    [Test]
    public async Task TestPart2()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"Time:      7  15   30
Distance:  9  40  200");

        var logic = new AdventOfCodeLogic.Day6();
        var result = await logic.SolvePart2(dataProvider);
        Assert.That(result, Is.EqualTo(71503));
    }

    [Test]
    public async Task RunPart2()
    {
        ITestDataProvider dataProvider = new SessionDataProvider(6);

        var logic = new AdventOfCodeLogic.Day6();
        var result = await logic.SolvePart2(dataProvider);
        Assert.That(result, Is.EqualTo(37286485));
    }


}