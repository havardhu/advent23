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
}