namespace Infrastructure.Persistence
{
	public class FileLock : IFileLock
	{
		private readonly string _filePath;

		public FileLock(string filePath)
		{
			_filePath = filePath;
		}

		public async Task ExecuteAsync(Func<FileStream, Task> action)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

			const int maxAttempts = 200;
			var delay = TimeSpan.FromSeconds(1);

			for (var attempt = 1; attempt <= maxAttempts; attempt++)
			{
				try
				{
					await using var stream = new FileStream(
						_filePath,
						FileMode.OpenOrCreate,
						FileAccess.ReadWrite,
						FileShare.None);


					await action(stream);
					return;
				}
				catch (IOException) when (attempt < maxAttempts)
				{
					await Task.Delay(delay);
				}
			}

			throw new IOException("Timed out waiting to acquire the contacts file lock.");
		}
	}
}
