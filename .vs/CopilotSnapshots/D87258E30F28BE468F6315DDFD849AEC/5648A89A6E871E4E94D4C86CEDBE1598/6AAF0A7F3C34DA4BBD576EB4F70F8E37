using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure
{
	public class JsonRepository : IContactRepository
	{
		private readonly JsonFileStore<Contact> _fileStore;
		public JsonRepository(JsonFileStore<Contact> fileStore)
		{
			_fileStore = fileStore;
		}
		public async Task AddAsync(Contact contact)
		{
			await _fileStore.UpdateAsync(contacts => contacts.Add(contact));
		}

		public Task DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Contact>> GetAllAsync()
		{
			var contacts = await _fileStore.ReadAsync();
			return contacts;
		}

		public Task<Contact> GetByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Contact contact)
		{
			throw new NotImplementedException();
		}
	}
}
