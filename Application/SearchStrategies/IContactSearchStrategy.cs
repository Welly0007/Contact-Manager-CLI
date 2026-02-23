using Domain.Entities;

namespace Application.SearchStrategies
{
	public interface IContactSearchStrategy
	{
		IEnumerable<Contact> Search(IEnumerable<Contact> contacts, string searchTerm);
		string GetStrategyName();
	}
}
