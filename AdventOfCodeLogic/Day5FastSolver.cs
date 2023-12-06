using System.Diagnostics;

namespace AdventOfCodeLogic
{
    public class RangeMapCollection
    {
        private List<RangeMap> _maps = new List<RangeMap>();
        public RangeMapCollection(string name)
        {
            Name = name;
        }
        public string Name { get; }
        public void AddMap(RangeMap map)
        {
            _maps.Add(map);
        }

        /// <summary>
        /// Used to map for Part 2
        /// </summary>
        public (long destination, long range)[] Map(long source, long range)
        {
            var orderedMaps = _maps.OrderBy(x => x.Source).ToArray();
            List<(long destination, long range)> result = new List<(long destination, long range)>();
            long remainingRange = range;
            foreach (var map in orderedMaps)
            {
                if (map.Source <= source && map.Source + map.Length > source)
                {
                    var remainingMapRange = map.Length - (source - map.Source);
                    var diff = source - map.Source;
                    if (remainingMapRange >= remainingRange)
                    {
                        Console.WriteLine($"Map {Name}: {source}-{source+remainingRange} => {map.Destination + diff}-{map.Destination + diff + remainingRange}");
                        result.Add((map.Destination + diff, remainingRange));
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Map {Name}: {source}-{source + remainingRange} => {map.Destination + diff}-{map.Destination + diff + remainingRange}");
                        result.Add((map.Destination + diff, remainingMapRange));
                        remainingRange -= remainingMapRange;
                        source += remainingMapRange;
                    }
                }
            }

            if (result.Count == 0)
            {
                Console.WriteLine($"No mapping for {Name}: {source}-{source + range}");
                result.Add((source, range));
            }


            return result.ToArray();
        }

        /// <summary>
        /// Used to map for Part 1
        /// </summary>
        public long Map(long source)
        {
            var matches = _maps.Where(x => x.CheckRange(source)).ToArray(); ;

            if (matches.Length == 1)
            {
                var match = matches[0];
                return match.Map(source);
            }
            else if (matches.Length == 0)
            {
                return source;
            }
            else
            {
                throw new InvalidOperationException($"Found multiple matches when mapping {source} in map {Name}");
            }
        }


    }

    public class RangeMap
    {
        public RangeMap(long destination, long source, long length)
        {
            Destination = destination;
            Source = source;
            Length = length;
        }

        public long Map(long value)
        {
            var diff = value - Source;
            return Destination + diff;
        }
        public bool CheckRange(long value)
        {
            return value >= Source && value < Source + Length;
        }
        
        public long Destination { get; }
        public long Source { get; }
        public long Length { get; }

        public long MaxValue => Source + Length;
    }

    public class Day5Fast1337Solver
    {
        public async Task<long> SolvePart2Recursive(ITestDataProvider dataProvider)
        {
            var lines = await dataProvider.GetLinesAsync();

            var seedPairs = lines[0].GetNumbers().Select(x => long.Parse(x.number)).ToArray();

            List<RangeMapCollection> maps = new List<RangeMapCollection>();
            RangeMapCollection currentMap = null;

            for (int i = 2; i < lines.Length; i++)
            {
                var line = lines[i];
                if (!string.IsNullOrEmpty(line))
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

            var sw = Stopwatch.StartNew();

            List<(long source, long range)> inputForNextMap = new();
            for (int i = 0; i < seedPairs.Length - 1; i += 2)
            {
                inputForNextMap.Add((seedPairs[i], seedPairs[i + 1]));
            }
            
            foreach (var map in maps)
            {
                var tmp = new List<(long, long)>();
                foreach (var input in inputForNextMap)
                {
                    tmp.AddRange(map.Map(input.source, input.range));
                }

                inputForNextMap = tmp;
            }

            var minValue = inputForNextMap.Select(x => x.source).Min();
            sw.Stop();
            Console.WriteLine($"Found minimum value {minValue} in {sw.Elapsed.TotalMilliseconds}ms");
            return minValue;
        }
    }
}
