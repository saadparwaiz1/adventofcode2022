namespace Advent.Code.Extensions
{
    public static class IOExtensions
    {
        public static async Task<string> ReadTextAsync(this string path)
        {
            return await File.ReadAllTextAsync(path);
        }

        public static async Task<string[]> ReadLinesAsync(this string path)
        {
            return await File.ReadAllLinesAsync(path);
        }
    }
}