namespace Infrastructure.Persistence
{
	public interface IFileLock
	{
		Task ExecuteAsync(Func<FileStream, Task> action);
	}
}
