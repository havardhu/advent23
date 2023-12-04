using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCodeLogic;

public class Day3
{   

    public async Task<int> SumOfCompleteFile(ITestDataProvider dataProvider){
        var lines = await dataProvider.GetLinesAsync();
        var sum = 0;

        foreach (var line in lines){
             var numberResults = line.GetNumbers();
             foreach (var n in numberResults){
                sum += int.Parse(n.number);
             }
        }
        return sum;

    }
    public async Task<int> SolvePart1(ITestDataProvider dataProvider){
        var lines = await dataProvider.GetLinesAsync();
        var sum = 0;
      
        bool checkLine(string line, (string number, int startIndex) numberResult){
            int actualStart = Math.Max(numberResult.startIndex-1, 0);
            var subStringLength =   numberResult.number.Length;
            
            if (numberResult.startIndex == 0) subStringLength += 1;
            else subStringLength += 2;

            int actualLength = Math.Min(subStringLength, line.Length - actualStart);
            
            var segmentToCheck = line.Substring(actualStart, actualLength);
            var segmentToCheckContainsSymbols = !segmentToCheck.IsNumericOrDot();
            return segmentToCheckContainsSymbols;
        }


        for (int i = 0; i < lines.Length; i++)
        {
            var numberResults = lines[i].GetNumbers();
            foreach (var numberResult in numberResults)
            {
                if (i > 0){
                    if (checkLine(lines[i-1], numberResult))
                    {
                        sum +=  int.Parse(numberResult.number);
                        continue;
                    }
                }

                 if (checkLine(lines[i], numberResult)){
                    sum +=  int.Parse(numberResult.number);
                    continue;
                }

                if (i < lines.Length-1){
                    if (checkLine(lines[i+1], numberResult)){
                        sum +=  int.Parse(numberResult.number);
                        continue;
                    }
                }

            }
        }
        
        return sum;
    }

    public async Task<int> SolvePart2(ITestDataProvider dataProvider)
    {
        var lines = await dataProvider.GetLinesAsync();
        var sum = 0;

        int[] getGears(string line){
            MatchCollection matches = Regex.Matches(line, "\\*");
            List<int> indices = new List<int>();
            foreach (Match match in matches)
            {
                indices.Add(match.Index);
            }

            return indices.ToArray();
        }

        

        for (var i = 0; i < lines.Length; i++){
            var line = lines[i];
            var gears = getGears(line);
           

            foreach (var gear in gears)
            {
                 List<int> numbers = new List<int>();

                // Find numbers to the left
                var leftResult = line.SeekNumberAtIndex(gear-1);
                if (leftResult.foundNumber) numbers.Add(leftResult.number);

                // Find numbers to the right
                var rightResult = line.SeekNumberAtIndex(gear+1);
                if (rightResult.foundNumber) numbers.Add(rightResult.number);
                
                
                if (i > 0){
                   
                    var lineAbove = lines[i-1];
                    int nextIndexToCheck = gear-1;
                    while (nextIndexToCheck <= gear+1){
                        var aboveResult = lineAbove.SeekNumberAtIndex(nextIndexToCheck);
                        if (aboveResult.foundNumber)
                        {
                            numbers.Add(aboveResult.number);
                            nextIndexToCheck = aboveResult.lastIndex+1;
                        }
                        else
                        {
                            nextIndexToCheck++;
                        }
                    }
                }

                if (i < lines.Length-1){
                     var lineBelow = lines[i+1];
                     var nextIndexToCheck = gear-1;
                     while (nextIndexToCheck <= gear+1){
                        var belowResult = lineBelow.SeekNumberAtIndex(nextIndexToCheck);
                        if (belowResult.foundNumber)
                        {
                            numbers.Add(belowResult.number);
                            nextIndexToCheck = belowResult.lastIndex+1;
                        }
                        else
                        {
                            nextIndexToCheck++;
                        }
                    }
                }

                if (numbers.Count == 2)
                {

                    sum += (numbers[0]*numbers[1]);
                }
            }

            
        }


        return sum;
    }
}