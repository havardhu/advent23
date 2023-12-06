using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCodeLogic;

public class Day4
{   

    const int StartIndex = 5;

    public async Task<int> SolvePart1(ITestDataProvider dataProvider){
        var lines = await dataProvider.GetLinesAsync();
        var sum = 0;

        foreach (var line in lines){
            var indexOfColon = line.IndexOf(':');
            var gameId = int.Parse(line.Substring(StartIndex, indexOfColon-StartIndex));
            var winnersAndOwn = line.Substring(indexOfColon+1).Split("|").Select(x => x.Trim()).ToArray();

            var winners = winnersAndOwn[0];
            var own = winnersAndOwn[1];
            var winningNumbers = winners.GetNumbers().Select(x => x.number).ToArray();
            var ownNumbers = own.GetNumbers().Select(x => x.number).ToArray();
            var myWinningNumbers = winningNumbers.Intersect(ownNumbers).ToArray();
            if (myWinningNumbers.Length>0)
            {
                sum += (int) Math.Pow(2, myWinningNumbers.Length-1);
            }
            
           
        }

        return sum;
    }

    public async Task<int> SolvePart2(ITestDataProvider dataProvider)
    {
        var lines = await dataProvider.GetLinesAsync();
        
        var lineSums = new Dictionary<int, int>();
        Dictionary<int, int> valueOfCards = new Dictionary<int, int>();

        int processLinesRecursive(int lineIndex)
        {
            if (valueOfCards.ContainsKey(lineIndex)) return valueOfCards[lineIndex];

            var line = lines[lineIndex];
            var winnersAndOwn = line.Substring(line.IndexOf(':') + 1).Split("|").Select(x => x.Trim()).ToArray();

            var winningNumbers = winnersAndOwn[0].GetNumbers().Select(x => x.number).ToArray();
            var ownNumbers = winnersAndOwn[1].GetNumbers().Select(x => x.number).ToArray();
            var numberOfWinners = winningNumbers.Intersect(ownNumbers).Count();
            
            int recursiveSum = 1;
            
            for (int i = 1; i <= numberOfWinners; i++)
            {
                recursiveSum += processLinesRecursive(lineIndex + i);
            }
            valueOfCards.Add(lineIndex, recursiveSum);

            return recursiveSum;
        }

        var totalSum = 0;
        // loop through all lines
        for (var i = 0; i < lines.Length; i++)
        {
            var valueOfCard = processLinesRecursive(i);
            totalSum += valueOfCard;
            Console.WriteLine($"Card {i+1}: Card value = {valueOfCard}, Sum = {totalSum}");
        }

        return totalSum;
    }
}