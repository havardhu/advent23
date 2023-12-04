using AdventOfCodeLogic;

namespace AdentOfCodeNunit;
 [TestFixture]
public class Day3Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetNumbersExtensionWorks(){
        var testInput = "....123....435.....56.7....567..";
        var result = testInput.GetNumbers();
        
        Assert.That(int.Parse(result[0].number), Is.EqualTo(123));
        Assert.That(result[0].startIndex, Is.EqualTo(4));
    }


    [Test]
    [TestCase('.', false)]
    [TestCase('1', false)]
    [TestCase('*', true)]
    [TestCase('#', true)]
     [TestCase('-', true)]
    public void IsNotAlphaNumericWorks(char value, bool expectedResult){
        Assert.That(value.IsNotAlphaNumeric('.'), Is.EqualTo(expectedResult));
    }

    [Test]
    public async Task TestPart1()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..");

        var logic = new AdventOfCodeLogic.Day3();
        var result = await logic.SolvePart1(dataProvider);
        Assert.That(result, Is.EqualTo(4361));
    }

    [Test]
    public async Task TestPart1_1()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"
#######      
#.....#
#.111.#
#.....#
#######");

        var logic = new AdventOfCodeLogic.Day3();
        var result = await logic.SolvePart1(dataProvider);
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public async Task TestPart1_2()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"
#.....#
#.111.#
#....##
        ");

        var logic = new AdventOfCodeLogic.Day3();
        var result = await logic.SolvePart1(dataProvider);
        Assert.That(result, Is.EqualTo(111));
    }

    [Test]
    public async Task TestPart1_3()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"97..
...*
100.");

        var logic = new AdventOfCodeLogic.Day3();
        var result = await logic.SolvePart1(dataProvider);
        Assert.That(result, Is.EqualTo(100));
    }

     [Test]
    public async Task TestPart1_4()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"12.......*..
+.........34
.......-12..
..78........
..*....60...
78.........9
.5.....23..$
8...90*12...
............
2.2......12.
.*.........*
1.1..503+.56");

        var logic = new AdventOfCodeLogic.Day3();
        var result = await logic.SolvePart1(dataProvider);
        Assert.That(result, Is.EqualTo(925));
    }


    [Test]
    public async Task RunPart1()
    {
        ITestDataProvider dataProvider = new SessionDataProvider(3);

        var logic = new AdventOfCodeLogic.Day3();
        var sum = await logic.SumOfCompleteFile(dataProvider);
        var result = await logic.SolvePart1(dataProvider);
        Assert.That(result, Is.EqualTo(535351));
    }
    
    [Test]
     [TestCase("...211....",-1, false,0,0,0)]
    [TestCase("...211....", 0, false,0,0,0)]
    [TestCase("..211..", 1, false,0,0,0)]
    [TestCase("..211..", 2, true,211, 2, 4)]
    [TestCase("..211..", 3, true,211, 2, 4)]
    [TestCase("..211..", 4, true,211, 2, 4)]
    [TestCase("..211..", 5, false,0,0,0)]
    [TestCase("..211..", 6, false,0,0,0)]
    [TestCase("..211..", 7, false,0,0,0)]
    [TestCase("467..114..", 2, true,467,0,2)]
    [TestCase("78.........9", 1, true,78,0,1)]
    [TestCase("2.2......12.", 10, true,12,9,10)]
    [TestCase("1.1..503+.56", 10, true,56,10,11)]
    public void SeekNumberAtIndexWorks(string input, int startIndex, bool expectedFind, int expectedNumber, int expectedStartIndex, int expectedEndIndex)
    {
        var result = input.SeekNumberAtIndex(startIndex);
        Assert.That(result.foundNumber, Is.EqualTo(expectedFind));
        if (expectedFind){
            Assert.That(result.number, Is.EqualTo(expectedNumber));
            Assert.That(result.firstIndex, Is.EqualTo(expectedStartIndex));
            Assert.That(result.lastIndex, Is.EqualTo(expectedEndIndex));

        }
       
    }


    [Test]
    public async Task TestPart2()
    {
         ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..");

        var logic = new AdventOfCodeLogic.Day3();

        var result = await logic.SolvePart2(dataProvider);
        Assert.That(result, Is.EqualTo(467835));
    }



     [Test]
    public async Task TestPart2_2()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"
12.......*..
+.........34
.......-12..
..78........
..*....60...
78.........9
.5.....23..$
8...90*12...
............
2.2......12.
.*.........*
1.1..503+.56");

        var logic = new AdventOfCodeLogic.Day3();
        var result = await logic.SolvePart2(dataProvider);
        Assert.That(result, Is.EqualTo(6756));
    }

     [Test]
    public async Task TestPart2_3()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"
..78........
..*....60...
78.........9");

        var logic = new AdventOfCodeLogic.Day3();
        var result = await logic.SolvePart2(dataProvider);
        Assert.That(result, Is.EqualTo(6084));
    }

     [Test]
    public async Task TestPart2_4()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"
2.2......12.
.*.........*
1.1..503+.56");

        var logic = new AdventOfCodeLogic.Day3();
        var result = await logic.SolvePart2(dataProvider);
        Assert.That(result, Is.EqualTo(672));
    }


    

    [Test]
    public async Task RunPart2()
    {
         ITestDataProvider dataProvider = new SessionDataProvider(3);

        var logic = new AdventOfCodeLogic.Day3();

        var result = await logic.SolvePart2(dataProvider);
        Assert.That(result, Is.EqualTo(467835));
    }
}