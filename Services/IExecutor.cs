namespace Advent.Code.Services
{
    public interface IExecutor
    {
        public Task ExecuteAsync(Type type);
    }
}