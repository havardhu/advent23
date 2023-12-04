namespace AdventOfCodeLogic;

public class Day1
{   
    public async Task<int> SolvePart1(ITestDataProvider dataProvider)
    {
        var lines = await dataProvider.GetLinesAsync();
        int sum = 0;

        foreach (var line in lines){
            var numbers = line.ExtractNumbers();
           if (numbers.Length > 0)
        {
            int first = numbers[0];
            int last = numbers[^1];
            sum += int.Parse(numbers.Length > 1 ? $"{first}{last}" : $"{first}{first}");
        }
        }
        return sum;
    }

    public async Task<int> SolvePart2(ITestDataProvider dataProvider)
    {
        var lines = await dataProvider.GetLinesAsync();
        int sum = 0;

        foreach (var line in lines){
            var numbers = line.ExtractStringLiteralsAndDigits2();
            if (numbers.Length > 0)
            {
               int first = numbers[0];
               int last = numbers[^1];
               sum += int.Parse(numbers.Length > 1 ? $"{first}{last}" : $"{first}{first}");
            }
        }
        return sum;
    }

}
