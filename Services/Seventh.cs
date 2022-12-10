using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Advent.Code.Extensions;

namespace Advent.Code.Services
{
    public class Seventh : IDay
    {

        private readonly ILogger<Seventh> _logger;
        private readonly static string s_FILE = "Resources/7.txt";

        public Seventh(ILogger<Seventh> logger)
        {
            _logger = logger;
        }

        public async Task<Dictionary<string, long>> ProcessAsync()
        {
            var path = Path.GetFullPath("/");
            var lines = await s_FILE.ReadLinesAsync();
            var fileSystem = new Dictionary<string, List<long>>();
            var dirTree = new Dictionary<string, List<string>>();
            foreach (var line in lines)
            {
                if (line.StartsWith("$ cd"))
                {
                    path = Path.GetFullPath(Path.Join(path, line.Replace("$ cd ", "")));
                }
                else if (!line.StartsWith("$") && !line.StartsWith("dir"))
                {
                    long size;
                    if (!long.TryParse(line?.Split(" ")?.FirstOrDefault(), out size))
                    {
                        _logger.LogWarning($"Parsing {line} for size failed");
                        size = 0;
                    }
                    if (fileSystem.ContainsKey(path))
                    {
                        fileSystem[path].Add(size);

                    }
                    else
                    {
                        fileSystem.Add(path, new List<long> { size });
                    }
                }
                else if (!line.StartsWith("$") && line.StartsWith("dir"))
                {
                    var subDir = Path.GetFullPath(Path.Join(path, line.Replace("dir ", "")));
                    if (dirTree.ContainsKey(path))
                    {
                        dirTree[path].Add(subDir);
                    }
                    else
                    {
                        dirTree[path] = new List<string> { subDir };
                    }
                }
            }
            var expandedTree = dirTree.ToDictionary(k => k.Key, v => v.Value.SelectMany(dir =>
            {
                var queue = new Queue<string>();
                var dirs = new List<string>();
                queue.Enqueue(dir);
                while (queue.Count != 0)
                {
                    var elem = queue.Dequeue();
                    var expanded = dirTree.GetValueOrDefault(elem, new List<string>());
                    dirs.Add(elem);
                    foreach (var element in expanded)
                    {
                        if (!dirTree.ContainsKey(element))
                        {
                            dirs.Add(element);
                        }
                        else
                        {
                            queue.Enqueue(element);
                        }
                    }
                }
                return dirs.Distinct().SelectMany(s => fileSystem.GetValueOrDefault(s, new List<long>()));
            }));
            foreach (var tree in expandedTree)
            {
                if (fileSystem.ContainsKey(tree.Key))
                {

                    fileSystem[tree.Key].AddRange(tree.Value);
                }
                else
                {
                    fileSystem.Add(tree.Key, tree.Value.ToList());
                }
            }
            return fileSystem.ToDictionary(k => k.Key, v => v.Value.Sum());
        }

        public async Task<string?> FirstAsync()
        {
            var fileTree = await ProcessAsync();
            return $"{fileTree.Where(v => v.Value <= 100000).Sum(v => v.Value)}";
        }
        public async Task<string?> SecondAsync()
        {
            var fileTree = await ProcessAsync();
            var spaceLeft = 70000000 - fileTree["/"];
            var spaceNeeded = 30000000 - spaceLeft;
            return $"{fileTree.Where(t => t.Value >= spaceNeeded).Min(s => s.Value)}";
        }
    }
}