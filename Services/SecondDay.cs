using Microsoft.Extensions.Logging;

using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class SecondDay : IDay
    {
        private readonly ILogger<SecondDay> _logger;
        private readonly static string s_FILE = "Resources/2.txt";

        private readonly static Dictionary<string, string> s_WINS = new Dictionary<string, string>
        {
            {"A", "Z"},
            {"B", "X"},
            {"C", "Y"}
        };

        private readonly static Dictionary<string, string> s_DRAWS = new Dictionary<string, string>
        {
            {"A", "X"},
            {"B", "Y"},
            {"C", "Z"}
        };

        private readonly static Dictionary<string, string> s_LOSSES = new Dictionary<string, string>
        {
            {"A", "Y"},
            {"B", "Z"},
            {"C", "X"}
        };

        private readonly static Dictionary<string, int> s_SHAPE_VALUES = new Dictionary<string, int>
        {
            {"Z", 3},
            {"Y", 2},
            {"X", 1}
        };

        private readonly static Dictionary<string, int> s_SHAPE_VALUES_OP = new Dictionary<string, int>
        {
            {"C", 3},
            {"B", 2},
            {"A", 1}
        };

        private readonly static int s_WIN = 6;
        private readonly static int s_DRAW = 3;

        public SecondDay(ILogger<SecondDay> logger)
        {
            _logger = logger;
        }

        private async Task<IEnumerable<(string, string)>?> ProcessAsync()
        {
            var input = await s_FILE.ReadLinesAsync();
            _logger.LogDebug($"File {s_FILE} is valid? {input != null}");
            return input?.Select(s =>
            {
                var pairs = s.Split(" ");
                return (pairs.FirstOrDefault(string.Empty), pairs.Skip(1).FirstOrDefault(string.Empty));
            });
        }

        private int DecideFirst((string, string) pair)
        {
            var shapeValue = s_SHAPE_VALUES[pair.Item2];
            if (s_LOSSES[pair.Item1] == pair.Item2)
            {
                return s_WIN + shapeValue;
            }

            if (s_DRAWS[pair.Item1] == pair.Item2)
            {
                return s_DRAW + shapeValue;
            }

            return shapeValue;
        }

        private int DecideSecond((string, string) pair)
        {
            if (pair.Item2 == "Z")
            {
                var shape = s_LOSSES[pair.Item1];
                return s_WIN + s_SHAPE_VALUES[shape];
            }
            if (pair.Item2 == "Y")
            {
                return s_DRAW + s_SHAPE_VALUES_OP[pair.Item1];
            }
            return s_SHAPE_VALUES[s_WINS[pair.Item1]];
        }

        public async Task<string?> FirstAsync()
        {
            var groups = await ProcessAsync();
            return groups?.Select(s => DecideFirst(s)).Sum().ToString();
        }
        public async Task<string?> SecondAsync()
        {
            var groups = await ProcessAsync();
            return groups?.Select(s => DecideSecond(s)).Sum().ToString();
        }
    }
}