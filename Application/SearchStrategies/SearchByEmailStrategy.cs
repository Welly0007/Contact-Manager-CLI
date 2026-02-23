using Domain.Entities;

namespace Application.SearchStrategies
{
	public class SearchByEmailStrategy : IContactSearchStrategy
	{
		public IEnumerable<Contact> Search(IEnumerable<Contact> contacts, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				return contacts;
			}

			var term = searchTerm.ToLowerInvariant();
			return contacts.Where(c => c.Email.ToLowerInvariant().Contains(term)).ToList();
		}

		public string GetStrategyName() => "Email";
	}
}
