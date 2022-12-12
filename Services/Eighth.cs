using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class Eighth : IDay
    {

        private readonly ILogger<Eighth> _logger;
        private readonly static string s_FILE = "Resources/8.txt";

        public Eighth(ILogger<Eighth> logger)
        {
            _logger = logger;
        }

        public async Task<char[][]> ProcessAsync()
        {
            var text = await s_FILE.ReadLinesAsync();
            var lines = text.Select(s => s.ToCharArray());
            return lines.ToArray();
        }

        public (IEnumerable<char> up, IEnumerable<char> down, IEnumerable<char> right, IEnumerable<char> left) GetAllDirections(char[][] map, int row, int column)
        {
            var curr = map[row][column];
            var up = Enumerable.Range(0, row).Select(r => map[r][column]).Reverse();
            var down = Enumerable.Range(row + 1, map.Length - 1 - row).Select(r => map[r][column]);
            var left = Enumerable.Range(0, column).Select(c => map[row][c]).Reverse();
            var right = Enumerable.Range(column + 1, map.First().Length - column - 1).Select(c => map[row][c]);
            return (up, down, left, right);
        }

        public async Task<string?> FirstAsync()
        {
            var data = await ProcessAsync();
            var visible = 2 * (data.Length - 2) + 2 * (data.GetLength(0));
            for (int row = 1; row < data.Length - 1; row++)
            {
                for (int column = 1; column < data.GetLength(0) - 1; column++)
                {
                    var curr = data[row][column];
                    var (up, down, right, left) = GetAllDirections(data, row, column);
                    if (up.All(t => t - curr < 0) || down.All(t => t - curr < 0) || right.All(t => t - curr < 0) || left.All(t => t - curr < 0))
                    {
                        visible += 1;
                    }
                }
            }
            return $"{visible}";
        }
        public async Task<string?> SecondAsync()
        {
            var data = await ProcessAsync();
            var maxValue = int.MinValue;
            for (int row = 1; row < data.Length - 1; row++)
            {
                for (int column = 1; column < data.GetLength(0) - 1; column++)
                {
                    var curr = data[row][column];
                    var (up, down, left, right) = GetAllDirections(data, row, column);
                    var uc = up.TakeWhile(t => t - curr < 0).CalculateCount(up);
                    var dc = down.TakeWhile(t => t - curr < 0).CalculateCount(down);
                    var rc = right.TakeWhile(t => t - curr < 0).CalculateCount(right);
                    var lc = left.TakeWhile(t => t - curr < 0).CalculateCount(left);
                    var scenicScore = uc * dc * lc * rc;
                    maxValue = scenicScore > maxValue ? scenicScore : maxValue;
                }
            }
            return $"{maxValue}";
        }
    }
}