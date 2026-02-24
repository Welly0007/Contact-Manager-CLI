using Domain.Entities;

namespace Application.FilterStrategies
{
	public interface IContactFilterStrategy
	{
		IEnumerable<Contact> Filter(IEnumerable<Contact> contacts, string filterTerm);
		string GetStrategyName();
	}
}
