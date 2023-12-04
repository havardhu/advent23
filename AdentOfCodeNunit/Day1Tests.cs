using AdventOfCodeLogic;

namespace AdentOfCodeNunit;
 [TestFixture]
public class Day1Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ExtractStringLiteralsWorks(){
        var stringToTest = "123456789";
        var expected = new[]{1,2,3,4,5,6,7,8,9};
        Assert.That( stringToTest.ExtractStringLiteralsAndDigits().SequenceEqual(expected), Is.True);

         stringToTest = "onetwothreefourfivesixseveneightnine";
         Assert.That( stringToTest.ExtractStringLiteralsAndDigits().SequenceEqual(expected), Is.True);

         expected = new[]{1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9};
         stringToTest = "1one2two3three4four5five6six7seven8eight9nine";
         Assert.That( stringToTest.ExtractStringLiteralsAndDigits().SequenceEqual(expected), Is.True);

         stringToTest = "4nineeightseven2";
         expected = new[]{4,9,8,7,2};
         Assert.That( stringToTest.ExtractStringLiteralsAndDigits().SequenceEqual(expected), Is.True);

         stringToTest = "3333threethreethree";
         expected = new[]{3,3,3,3,3,3,3};
         var result = stringToTest.ExtractStringLiteralsAndDigits();
         Assert.That( result.SequenceEqual(expected), Is.True);

         stringToTest = "eightwothree";
         expected = new[]{8,2,3};
         result = stringToTest.ExtractStringLiteralsAndDigits2();
        Assert.That( result.SequenceEqual(expected), Is.True);
    }

    [Test]
    public async Task TestPart1()
    {
        ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet");

        var logic = new AdventOfCodeLogic.Day1();
        var result = await logic.SolvePart1(dataProvider);
        Assert.That(result, Is.EqualTo(142));
    }

    [Test]
    public async Task RunPart1()
    {
        ITestDataProvider dataProvider = new FileTestDataProvider(@"c:\temp\advent23", 1);
        var logic = new AdventOfCodeLogic.Day1();
        var result = await logic.SolvePart1(dataProvider);

        Assert.That(result, Is.Not.Zero);
    }
    

    [Test]
    public async Task TestPart2()
    {
         ITestDataProvider dataProvider = new AdventOfCodeLogic.StringTestDataProvider(@"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen");

        var logic = new AdventOfCodeLogic.Day1();
        var result = await logic.SolvePart2(dataProvider);
        Assert.That(result, Is.EqualTo(281));
    }

    [Test]
    public async Task RunPart2()
    {
        ITestDataProvider dataProvider1 = new SessionDataProvider(1);
      //  ITestDataProvider dataProvider2 = new FileTestDataProvider(@"c:\temp\advent23", 1);
        var logic = new AdventOfCodeLogic.Day1();
        var result1 = await logic.SolvePart2(dataProvider1);
       // var result2 = await logic.SolvePart2(dataProvider2);
       // Assert.That(result1, Is.EqualTo(result2));
    }
}