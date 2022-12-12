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
        public static int CalculateCount(this IEnumerable<char> extracted, IEnumerable<char> all)
        {
            return extracted.Count() == all.Count() ? extracted.Count() : extracted.Count() + 1;
        }
    }
}
