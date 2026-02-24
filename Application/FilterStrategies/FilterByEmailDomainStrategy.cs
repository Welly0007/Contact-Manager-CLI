using Domain.Entities;

namespace Application.FilterStrategies
{
	public class FilterByEmailDomainStrategy : IContactFilterStrategy
	{
		public IEnumerable<Contact> Filter(IEnumerable<Contact> contacts, string filterTerm)
		{
			if (string.IsNullOrWhiteSpace(filterTerm))
			{
				return contacts;
			}

			var normalizedDomain = filterTerm.ToLowerInvariant().Trim().TrimStart('@');
			return contacts.Where(c =>
				c.Email.ToLowerInvariant().EndsWith($"@{normalizedDomain}") ||
				c.Email.ToLowerInvariant().EndsWith(normalizedDomain)
			).ToList();
		}

		public string GetStrategyName() => "Email Domain";
	}
}
