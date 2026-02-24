using Domain.Entities;

namespace Contact_Manager_CLI
{
	internal static class CliMenu
	{
		public static void DisplayMainMenu()
		{
			Console.Clear();
			Console.WriteLine("╔═══════════════════════════════════════════════╗");
			Console.WriteLine("║     MICROSOFT CONTACT MANAGEMENT SYSTEM       ║");
			Console.WriteLine("╚═══════════════════════════════════════════════╝");
			Console.WriteLine();
			Console.WriteLine("  1. Add Contact");
			Console.WriteLine("  2. Edit Contact");
			Console.WriteLine("  3. Delete Contact");
			Console.WriteLine("  4. View Contact");
			Console.WriteLine("  5. List Contacts");
			Console.WriteLine("  6. Search");
			Console.WriteLine("  7. Filter");
			Console.WriteLine("  8. Save");
			Console.WriteLine("  9. Exit");
			Console.WriteLine();
			Console.Write("Select an option (1-9): ");
		}

		public static void DisplayContacts(IEnumerable<Contact> contacts)
		{
			var contactsList = contacts.ToList();

			if (!contactsList.Any())
			{
				Console.WriteLine("No contacts to display.");
				return;
			}

			Console.WriteLine(new string('-', 80));
			foreach (var contact in contactsList)
			{
				Console.WriteLine($"ID:  {contact.Id}");
				Console.WriteLine($"Name:    {contact.Name}");
				Console.WriteLine($"Phone:   {contact.Phone}");
				Console.WriteLine($"Email:   {contact.Email}");
				Console.WriteLine($"Created: {contact.CreatedAt:yyyy-MM-dd HH:mm:ss} UTC");
				Console.WriteLine(new string('-', 80));
			}
			Console.WriteLine($"Total: {contactsList.Count} contact(s)");
		}
	}
}
