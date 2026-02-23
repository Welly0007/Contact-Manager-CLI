using Domain.Entities;

namespace Application.SearchStrategies
{
	public class SearchByPhoneStrategy : IContactSearchStrategy
	{
		public IEnumerable<Contact> Search(IEnumerable<Contact> contacts, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				return contacts;
			}

			var term = searchTerm.Trim();
			return contacts.Where(c => c.Phone.Contains(term)).ToList();
		}

		public string GetStrategyName() => "Phone";
	}
}
