using System.ComponentModel;
using Microsoft.VisualBasic;

namespace AdventOfCodeLogic;

public class Day2
{   
    const int StartIndex = 5;

    public async Task<int> SolvePart1(ITestDataProvider dataProvider, int red, int green, int blue){
        var lines = await dataProvider.GetLinesAsync();
        var sum = 0;

        foreach (var line in lines){
             bool isValid = true;

            var indexOfColon = line.IndexOf(':');
            var gameId = int.Parse(line.Substring(StartIndex, indexOfColon-StartIndex));
            var draws = line.Substring(indexOfColon+1).Split(";");
            foreach (var draw in draws)
            {
                var cubes = draw.Split(',');
                if (!isValid) continue;

                foreach (var cube in cubes)
                {
                    var splitResult = cube.SplitNumberAndString();
                    if (splitResult.number > -1)
                    {
                        if (splitResult.stringValue.Equals("red", StringComparison.InvariantCultureIgnoreCase)){
                            if (splitResult.number > red){
                                Console.WriteLine($"Game {gameId} is invalid because it contains at least {splitResult.number} red cubes");
                                isValid = false;
                                break;
                            }
                        }

                        if (splitResult.stringValue.Equals("green", StringComparison.InvariantCultureIgnoreCase)){
                             if (splitResult.number > green){
                                Console.WriteLine($"Game {gameId} is invalid because it contains at least {splitResult.number} green cubes");
                                isValid = false;
                                break;
                            }
                        }

                        if (splitResult.stringValue.Equals("blue", StringComparison.InvariantCultureIgnoreCase)){
                           
                             if (splitResult.number > blue){
                                 Console.WriteLine($"Game {gameId} is invalid because it contains at least {splitResult.number} blue cubes");
                                isValid = false;
                                break;
                            }
                        }
                    }
                }

               
            }
             if (isValid){
                    sum += gameId;
                    Console.WriteLine($"Game {gameId} is valid, sum is now {sum}");
                }

        }

        return sum;
    }

    public async Task<int> SolvePart2(ITestDataProvider dataProvider){
        var lines = await dataProvider.GetLinesAsync();
        var sum = 0;

        foreach (var line in lines){

            var indexOfColon = line.IndexOf(':');
            var gameId = int.Parse(line.Substring(StartIndex, indexOfColon-StartIndex));
            var draws = line.Substring(indexOfColon+1).Split(";");
            int minRed = 0;
            int minGreen = 0;
            int minBlue = 0;

            foreach (var draw in draws)
            {
                var cubes = draw.Split(',');
                foreach (var cube in cubes){
                    var splitResult = cube.SplitNumberAndString();
                    if (splitResult.number > -1)
                    {
                        if (splitResult.stringValue.Equals("red", StringComparison.InvariantCultureIgnoreCase)){
                           if (splitResult.number > minRed) minRed = splitResult.number;
                        }

                        if (splitResult.stringValue.Equals("green", StringComparison.InvariantCultureIgnoreCase)){
                             if (splitResult.number > minGreen) minGreen = splitResult.number;
                        }

                        if (splitResult.stringValue.Equals("blue", StringComparison.InvariantCultureIgnoreCase)){
                            if (splitResult.number > minBlue) minBlue = splitResult.number;
                        }
                    }
                }
            }

            sum += (minRed*minBlue*minGreen);
        }
        return sum;

    }
}