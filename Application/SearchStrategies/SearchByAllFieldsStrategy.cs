using Domain.Entities;

namespace Application.SearchStrategies
{
	public class SearchByAllFieldsStrategy : IContactSearchStrategy
	{
		public IEnumerable<Contact> Search(IEnumerable<Contact> contacts, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				return contacts;
			}

			var term = searchTerm.ToLowerInvariant();
			return contacts.Where(c =>
				c.Name.ToLowerInvariant().Contains(term) ||
				c.Phone.Contains(searchTerm) ||
				c.Email.ToLowerInvariant().Contains(term) ||
				c.Id.ToString().Contains(searchTerm)
			).ToList();
		}

		public string GetStrategyName() => "All Fields";
	}
}
