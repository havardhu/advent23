using System.Diagnostics;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCodeLogic;



public class Day5
{


    public async Task<long> SolvePart1(ITestDataProvider dataProvider)
    {
        var lines = await dataProvider.GetLinesAsync();

        var seeds = lines[0].GetNumbers().Select(x => long.Parse(x.number)).ToArray();

        List<RangeMapCollection> maps = new List<RangeMapCollection>();
        RangeMapCollection currentMap = null;

        for (int i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            else
            {
                var numbers = line.GetNumbers();
                if (!numbers.Any())
                {
                    currentMap = new RangeMapCollection(line);
                    maps.Add(currentMap);
                }

                if (numbers.Any())
                {
                    var destination = long.Parse(numbers[0].number);
                    var source = long.Parse(numbers[1].number);
                    var range = long.Parse(numbers[2].number);
                    if (currentMap == null)
                    {
                        throw new InvalidOperationException("Expected map to exist");
                    }

                    currentMap.AddMap(new RangeMap(destination, source, range));
                }
            }
        }

        long smallest = long.MaxValue;

        foreach (var seed in seeds)
        {
            var mappedValue = seed;
            foreach (var map in maps)
            {
                var pre = mappedValue;
                mappedValue = map.Map(mappedValue);
                Console.WriteLine($"{map.Name}: {pre} => {mappedValue}");
            }

            if (mappedValue < smallest)
                smallest = mappedValue;
        }

        return smallest;
    }

    public async Task<long> SolvePart2BruteForce(ITestDataProvider dataProvider)
    {
        var lines = await dataProvider.GetLinesAsync();

        var seedPairs = lines[0].GetNumbers().Select(x => long.Parse(x.number)).ToArray();

        List<RangeMapCollection> maps = new List<RangeMapCollection>();
        RangeMapCollection currentMap = null;

        for (int i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            else
            {
                var numbers = line.GetNumbers();
                if (!numbers.Any())
                {
                    currentMap = new RangeMapCollection(line);
                    maps.Add(currentMap);
                }

                if (numbers.Any())
                {
                    var destination = long.Parse(numbers[0].number);
                    var source = long.Parse(numbers[1].number);
                    var range = long.Parse(numbers[2].number);
                    if (currentMap == null)
                    {
                        throw new InvalidOperationException("Expected map to exist");
                    }

                    currentMap.AddMap(new RangeMap(destination, source, range));
                }
            }
        }



        List<Task<long>> tasks = new List<Task<long>>();


        for (var i = 0; i < seedPairs.Length - 2; i += 2)
        {
            var start = seedPairs[i];
            var range = seedPairs[i + 1];

            tasks.Add(Task.Run(() => RunSeedRange(start, range, maps)));
        }

        var sw = Stopwatch.StartNew();
        var values = await Task.WhenAll(tasks);
        sw.Stop();
        Console.WriteLine($"Completed tasks in {sw.Elapsed.TotalSeconds} seconds");
        var smallest = values.Min();

        return smallest;
    }

    private long RunSeedRange(long start, long range, List<RangeMapCollection> maps)
    {
        var sw = Stopwatch.StartNew();
        long smallest = long.MaxValue;


        for (long j = 0; j < range; j++)
        {
            var mappedValue = j + start;
            foreach (var map in maps)
            {
                mappedValue = map.Map(mappedValue);
            }

            if (mappedValue < smallest)
                smallest = mappedValue;

            if (j % 10000000 == 0)
            {
                var percent = ((j + 1D) / range) * 100;
                Console.WriteLine($"Task completion: {percent}%");
            }
        }

        sw.Stop();
        Console.WriteLine($"Completed {start}-{range} in {sw.Elapsed.TotalSeconds} seconds");
        return smallest;
    }




}