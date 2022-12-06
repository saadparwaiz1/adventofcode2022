namespace Advent.Code.Extensions
{
    public static class Extension
    {
        public static async Task<string> ReadTextAsync(this string path)
        {
            return await File.ReadAllTextAsync(path);
        }

        public static async Task<string[]> ReadLinesAsync(this string path)
        {
            return await File.ReadAllLinesAsync(path);
        }

        public static IEnumerable<string> AddPadding(this string str, int pad)
        {
            var count = str.Count();
            return count == pad ? new List<string> { string.IsNullOrWhiteSpace(str) ? "[0]" : str } : Enumerable.Repeat("[0]", count / pad).ToList();
        }
    }
}