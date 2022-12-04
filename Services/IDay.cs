namespace Advent.Code.Services
{
    public interface IDay
    {
        public Task<string?> FirstAsync();
        public Task<string?> SecondAsync();
    }
}