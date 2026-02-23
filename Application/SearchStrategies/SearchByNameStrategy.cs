using Domain.Entities;

namespace Application.SearchStrategies
{
	public class SearchByNameStrategy : IContactSearchStrategy
	{
		public IEnumerable<Contact> Search(IEnumerable<Contact> contacts, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				return contacts;
			}

			var term = searchTerm.ToLowerInvariant();
			return contacts.Where(c => c.Name.ToLowerInvariant().Contains(term)).ToList();
		}

		public string GetStrategyName() => "Name";
	}
}
