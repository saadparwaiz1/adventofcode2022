using System.Text.RegularExpressions;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class Fifth : IDay
    {

        private readonly ILogger<Fifth> _logger;
        private readonly static string s_FILE = "Resources/5.txt";
        private readonly static Regex regex = new Regex("(.)\\1{2}");

        public Fifth(ILogger<Fifth> logger)
        {
            _logger = logger;
        }

        //TODO: Maybe a Custom Parser?
        // this is a bit ugly
        private async Task<Dictionary<string, List<string>>> GetBoard()
        {
            var lines = await s_FILE.ReadLinesAsync();
            return lines
                            .TakeWhile(line => line != "")
                            .Reverse()
                            .Skip(1)
                            .Select(l => l.Replace("] [", "]_["))
                            .Select(l => l.Replace("] ", "]_"))
                            .Select(l => l.Replace(" [", "_["))
                            .Select(l => l.Replace("_   _", "_[0]_"))
                            .Select(l => l.Replace("_   ", "_[0]"))
                            .Select(l => l.Replace("    _", "_[0]_"))
                            .Select(l => l.Replace("    _", "_[0]_"))
                            .Select(l => l.Replace("    _", "_[0]_"))
                            .Select(l => l.Replace("    _", "_[0]_"))
                            .Select(l => l.Split("_"))
                            .Select(l => l.Select((val, idx) => (idx, val)))
                            .Select(l => l.ToDictionary(item => $"{item.idx + 1}", item => new List<string> { item.val }))
                            .Aggregate((acc, next) => acc
                                                        .Concat(next)
                                                        .ToLookup(kvp => kvp.Key, kvp => kvp.Value)
                                                        .ToDictionary(group => group.Key, group => group.SelectMany(x => x).Where(x => x != "[0]" && !string.IsNullOrWhiteSpace(x)).ToList())
                            );
        }

        private async Task<IEnumerable<(string from, string to, int times)>> GetMoves()
        {
            var lines = await s_FILE.ReadLinesAsync();
            return lines
                                .SkipWhile(t => !t.Contains("move"))
                                .Select(s => s.Split(" "))
                                .Select(s => (from: s[3], to: s[5], times: int.Parse(s[1])));
        }

        public async Task<string?> FirstAsync()
        {
            var boardTask = GetBoard();
            var moveTask = GetMoves();
            await Task.WhenAll(boardTask, moveTask);
            var board = boardTask.Result;
            var moves = moveTask.Result;
            foreach (var move in moves)
            {
                for (int i = 0; i < move.times; i++)
                {
                    var from = board[move.from];
                    var item = from.Last();
                    board[move.to].Add(item);
                    from.RemoveAt(from.LastIndexOf(item));
                }
            }
            return string.Join("", board.Select(pair => pair.Value.LastOrDefault())).Replace("[", "").Replace("]", "");
        }
        public async Task<string?> SecondAsync()
        {
            var boardTask = GetBoard();
            var moveTask = GetMoves();
            await Task.WhenAll(boardTask, moveTask);
            var board = boardTask.Result;
            var moves = moveTask.Result;
            foreach (var move in moves)
            {
                board[move.to].AddRange(board[move.from].TakeLast(move.times));
                for (int i = 0; i < move.times; i++)
                {
                    var from = board[move.from];
                    var item = from.Last();
                    from.RemoveAt(from.LastIndexOf(item));
                }
            }
            return string.Join("", board.Select(pair => pair.Value.LastOrDefault())).Replace("[", "").Replace("]", "");
        }
    }
}