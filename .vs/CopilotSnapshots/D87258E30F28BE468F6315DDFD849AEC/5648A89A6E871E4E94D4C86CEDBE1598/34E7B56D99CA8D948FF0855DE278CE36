using Domain.Entities;

namespace Application.Contracts
{
	public interface IContactService
	{
		Task<IEnumerable<Contact>> GetAllContactsAsync();
		Task<Contact?> GetContactByIdAsync(Guid id);
		Task AddContactAsync(string name, string phone, string email);
		Task UpdateContactAsync(Contact contact);
		Task DeleteContactAsync(Guid id);
	}
}
