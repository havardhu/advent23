
namespace AdventOfCodeLogic;

public class Day6
{
    public async Task<int> SolvePart1(ITestDataProvider dataProvider)
    {
        var lines = await dataProvider.GetLinesAsync();
        
        var times = lines[0].GetNumbers().Select(x => int.Parse(x.number)).ToArray();
        var distances = lines[1].GetNumbers().Select(x => int.Parse(x.number)).ToArray();
        int result = 1;
        for (int i = 0; i < times.Length; i++) result *= CalculateTimesBeaten(times[i], distances[i]);

        return result;
    }

    public async Task<int> SolvePart2(ITestDataProvider dataProvider)
    {
        var lines = await dataProvider.GetLinesAsync();

        var maxTimee = long.Parse(string.Join(String.Empty, lines[0].GetNumbers().Select(x => x.number)));
        var maxDistance = long.Parse(string.Join(String.Empty, lines[1].GetNumbers().Select(x => x.number)));
        return CalculateTimesBeaten(maxTimee, maxDistance);
    }

    private static int CalculateTimesBeaten(long maxTime, long maxDistance)
    {
        int timesMaxBeaten = 0;
        for (int speed = 0; speed <= maxTime; speed++)
        {
            if (speed * (maxTime - speed) > maxDistance)
            {
                timesMaxBeaten++;
            }
        }

        return timesMaxBeaten;
    }

   
}