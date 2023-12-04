/*
Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green*/

using System.Reflection.PortableExecutable;
using AdventOfCodeLogic;

namespace AdentOfCodeNunit;
 [TestFixture]
public class Day2Tests
{

    [Test]
    public void SplitNumberAndStringWorks(){
        var value = " 20 red";

        var result = value.SplitNumberAndString();

        Assert.That(result.number, Is.EqualTo(20));
        Assert.That(result.stringValue, Is.EqualTo("red"));
    }

    [Test]
    public async Task TestPart1()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green");

        var logic = new AdventOfCodeLogic.Day2();
        var result = await logic.SolvePart1(dataProvider, 12, 13, 14);
        Assert.That(result, Is.EqualTo(8));
    }

    [Test]
    public async Task RunPart1()
    {
        ITestDataProvider dataProvider = new SessionDataProvider(2);
        var logic = new AdventOfCodeLogic.Day2();
        var result = await logic.SolvePart1(dataProvider, 12, 13, 14);
        Assert.That(result, Is.EqualTo(2771));
    }

    [Test]
    public async Task RunWithFredrikDataset()
    {
        ITestDataProvider dataProvider = new FileTestDataProvider(@"C:\Temp\advent23\fredrik", 2); 
        var logic = new AdventOfCodeLogic.Day2();
        var result = await logic.SolvePart1(dataProvider, 12, 13, 14);
        Assert.That(result, Is.EqualTo(2265));
    }

    [Test]
    public async Task TestPart2()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green");

        var logic = new AdventOfCodeLogic.Day2();
        var result = await logic.SolvePart2(dataProvider);
        Assert.That(result, Is.EqualTo(2286));
    }

    [Test]
    public async Task RunPart2()
    {
        ITestDataProvider dataProvider = new SessionDataProvider(2);
        var logic = new AdventOfCodeLogic.Day2();
        var result = await logic.SolvePart2(dataProvider);
         Assert.That(result, Is.EqualTo(70924));
    }
}

