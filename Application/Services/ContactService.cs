using Domain.Entities;

namespace Application.Services
{
	public class ContactService
	{
		public ContactService() { }
		public void AddContact(string name, string phone, string email)
		{
			var contact = new Contact
			{
				Id = Guid.NewGuid(),
				Name = name,
				Phone = phone,
				Email = email
			}
			;
		}
	}
}
