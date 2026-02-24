using Application.Contracts;
using Application.FilterStrategies;
using Application.SearchStrategies;
using Domain.Entities;
using Infrastructure.Persistence.Dtos;

namespace Contact_Manager_CLI
{
	internal sealed class CliApp
	{
		private readonly IContactService _contactService;

		public CliApp(IContactService contactService)
		{
			_contactService = contactService;
		}

		public async Task RunAsync()
		{
			await LoadAndDisplayStartupContactsAsync();

			var exit = false;
			while (!exit)
			{
				CliMenu.DisplayMainMenu();
				var choice = Console.ReadLine()?.Trim();

				try
				{
					switch (choice)
					{
						case "1":
							await AddContactAsync();
							break;
						case "2":
							await EditContactAsync();
							break;
						case "3":
							await DeleteContactAsync();
							break;
						case "4":
							await ViewContactAsync();
							break;
						case "5":
							await ListContactsAsync();
							break;
						case "6":
							await SearchContactsAsync();
							break;
						case "7":
							await FilterContactsAsync();
							break;
						case "8":
							await SaveContactsAsync();
							break;
						case "9":
							exit = await ExitApplicationAsync();
							break;
						default:
							Console.WriteLine("\n? Invalid option. Please select 1-9.");
							break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"\n? Error: {ex.Message}");
				}

				if (!exit)
				{
					Console.WriteLine("\nPress any key to continue...");
					Console.ReadKey();
				}
			}
		}

		private async Task LoadAndDisplayStartupContactsAsync()
		{
			Console.WriteLine("Loading contacts...");
			var initialContacts = await _contactService.GetAllContactsAsync();
			var contactCount = initialContacts.Count();

			if (contactCount > 0)
			{
				Console.WriteLine($"\nFound {contactCount} contact(s):");
				Console.WriteLine(new string('-', 80));
				CliMenu.DisplayContacts(initialContacts);
			}
			else
			{
				Console.WriteLine("No contacts found. Starting with an empty contact list.");
			}

			Console.WriteLine();
		}

		private async Task AddContactAsync()
		{
			Console.Clear();
			Console.WriteLine("??? ADD CONTACT ???\n");

			Console.Write("Name: ");
			var name = Console.ReadLine()?.Trim();
			if (string.IsNullOrWhiteSpace(name))
			{
				Console.WriteLine("? Name cannot be empty.");
				return;
			}

			Console.Write("Phone: ");
			var phone = Console.ReadLine()?.Trim();
			if (string.IsNullOrWhiteSpace(phone))
			{
				Console.WriteLine("? Phone cannot be empty.");
				return;
			}

			Console.Write("Email: ");
			var email = Console.ReadLine()?.Trim();
			if (string.IsNullOrWhiteSpace(email))
			{
				Console.WriteLine("? Email cannot be empty.");
				return;
			}

			await _contactService.AddContactAsync(new ContactRecord(name, phone, email));
			Console.WriteLine("\n? Contact added successfully! (Remember to save)");
		}

		private async Task EditContactAsync()
		{
			Console.Clear();
			Console.WriteLine("??? EDIT CONTACT ???\n");

			Console.Write("Enter Contact ID to edit: ");
			var idInput = Console.ReadLine()?.Trim();

			if (!Guid.TryParse(idInput, out var id))
			{
				Console.WriteLine("? Invalid ID format.");
				return;
			}

			var contact = await _contactService.GetContactByIdAsync(id);
			if (contact == null)
			{
				Console.WriteLine("? Contact not found.");
				return;
			}

			Console.WriteLine($"\nCurrent details:");
			Console.WriteLine($"Name: {contact.Name}");
			Console.WriteLine($"Phone: {contact.Phone}");
			Console.WriteLine($"Email: {contact.Email}");
			Console.WriteLine();

			Console.Write($"New Name (press Enter to keep '{contact.Name}'): ");
			var newName = Console.ReadLine()?.Trim();
			if (!string.IsNullOrWhiteSpace(newName))
			{
				contact.Name = newName;
			}

			Console.Write($"New Phone (press Enter to keep '{contact.Phone}'): ");
			var newPhone = Console.ReadLine()?.Trim();
			if (!string.IsNullOrWhiteSpace(newPhone))
			{
				contact.Phone = newPhone;
			}

			Console.Write($"New Email (press Enter to keep '{contact.Email}'): ");
			var newEmail = Console.ReadLine()?.Trim();
			if (!string.IsNullOrWhiteSpace(newEmail))
			{
				contact.Email = newEmail;
			}

			await _contactService.UpdateContactAsync(contact);
			Console.WriteLine("\n? Contact updated successfully! (Remember to save)");
		}

		private async Task DeleteContactAsync()
		{
			Console.Clear();
			Console.WriteLine("??? DELETE CONTACT ???\n");

			Console.Write("Enter Contact ID to delete: ");
			var idInput = Console.ReadLine()?.Trim();

			if (!Guid.TryParse(idInput, out var id))
			{
				Console.WriteLine("? Invalid ID format.");
				return;
			}

			var contact = await _contactService.GetContactByIdAsync(id);
			if (contact == null)
			{
				Console.WriteLine("? Contact not found.");
				return;
			}

			Console.WriteLine($"\nContact details:");
			Console.WriteLine($"Name: {contact.Name}");
			Console.WriteLine($"Phone: {contact.Phone}");
			Console.WriteLine($"Email: {contact.Email}");
			Console.WriteLine();

			Console.Write("Are you sure you want to delete this contact? (yes/no): ");
			var confirmation = Console.ReadLine()?.Trim().ToLowerInvariant();

			if (confirmation == "yes" || confirmation == "y")
			{
				await _contactService.DeleteContactAsync(id);
				Console.WriteLine("\n? Contact deleted successfully! (Remember to save)");
			}
			else
			{
				Console.WriteLine("\n? Deletion cancelled.");
			}
		}

		private async Task ViewContactAsync()
		{
			Console.Clear();
			Console.WriteLine("??? VIEW CONTACT ???\n");

			Console.Write("Enter Contact ID: ");
			var idInput = Console.ReadLine()?.Trim();

			if (!Guid.TryParse(idInput, out var id))
			{
				Console.WriteLine("? Invalid ID format.");
				return;
			}

			var contact = await _contactService.GetContactByIdAsync(id);
			if (contact == null)
			{
				Console.WriteLine("? Contact not found.");
				return;
			}

			Console.WriteLine();
			Console.WriteLine("?????????????????????????????????????????????????????????????????????????????");
			Console.WriteLine($"  ID:           {contact.Id}");
			Console.WriteLine($"  Name:         {contact.Name}");
			Console.WriteLine($"  Phone:        {contact.Phone}");
			Console.WriteLine($"  Email:        {contact.Email}");
			Console.WriteLine($"  Created At:   {contact.CreatedAt:yyyy-MM-dd HH:mm:ss} UTC");
			Console.WriteLine("?????????????????????????????????????????????????????????????????????????????");
		}

		private async Task ListContactsAsync()
		{
			Console.Clear();
			Console.WriteLine("??? LIST ALL CONTACTS ???\n");

			var contacts = await _contactService.GetAllContactsAsync();
			CliMenu.DisplayContacts(contacts);
		}

		private async Task SearchContactsAsync()
		{
			Console.Clear();
			Console.WriteLine("??? SEARCH CONTACTS ???\n");
			Console.WriteLine("Select search criteria:");
			Console.WriteLine("  1. Search by Name (contains)");
			Console.WriteLine("  2. Search by Phone (contains)");
			Console.WriteLine("  3. Search by Email (contains)");
			Console.WriteLine("  4. Search by All Fields");
			Console.WriteLine();
			Console.Write("Select option (1-4): ");

			var strategyChoice = Console.ReadLine()?.Trim();
			IContactSearchStrategy? strategy = strategyChoice switch
			{
				"1" => new SearchByNameStrategy(),
				"2" => new SearchByPhoneStrategy(),
				"3" => new SearchByEmailStrategy(),
				"4" => new SearchByAllFieldsStrategy(),
				_ => null
			};

			if (strategy == null)
			{
				Console.WriteLine("? Invalid search option.");
				return;
			}

			Console.WriteLine();
			Console.Write($"Enter search term ({strategy.GetStrategyName()}): ");
			var searchTerm = Console.ReadLine()?.Trim();

			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				Console.WriteLine("? Search term cannot be empty.");
				return;
			}

			var results = await _contactService.SearchContactsAsync(searchTerm, strategy);
			var contactsList = results.ToList();

			Console.WriteLine($"\n? Found {contactsList.Count} result(s) using '{strategy.GetStrategyName()}' search:\n");
			CliMenu.DisplayContacts(contactsList);
		}

		private async Task FilterContactsAsync()
		{
			Console.Clear();
			Console.WriteLine("??? FILTER CONTACTS ???\n");
			Console.WriteLine("Select filter criteria:");
			Console.WriteLine("  1. Filter by Email Domain (e.g., microsoft.com)");
			Console.WriteLine("  2. Created Before a Date (UTC)");
			Console.WriteLine("  3. Created After a Date (UTC)");
			Console.WriteLine();
			Console.Write("Select option (1-3): ");

			var strategyChoice = Console.ReadLine()?.Trim();
			IContactFilterStrategy? strategy = strategyChoice switch
			{
				"1" => new FilterByEmailDomainStrategy(),
				"2" => new FilterByCreatedBeforeDateStrategy(),
				"3" => new FilterByCreatedAfterDateStrategy(),
				_ => null
			};

			if (strategy == null)
			{
				Console.WriteLine("? Invalid filter option.");
				return;
			}

			Console.WriteLine();
			Console.Write($"Enter filter term ({strategy.GetStrategyName()}) [e.g., 2025-01-31]: ");
			var filterTerm = Console.ReadLine()?.Trim();

			if (string.IsNullOrWhiteSpace(filterTerm))
			{
				Console.WriteLine("? Filter term cannot be empty.");
				return;
			}

			var results = await _contactService.FilterContactsAsync(filterTerm, strategy);
			var contactsList = results.ToList();

			Console.WriteLine($"\n? Found {contactsList.Count} contact(s) using '{strategy.GetStrategyName()}' filter:\n");
			CliMenu.DisplayContacts(contactsList);
		}

		private async Task SaveContactsAsync()
		{
			Console.Clear();
			Console.WriteLine("??? SAVE CONTACTS ???\n");
			Console.Write("Saving contacts to file...");

			await _contactService.SaveAsync();

			Console.WriteLine(" Done!");
			Console.WriteLine("\n? All contacts saved successfully!");
		}

		private async Task<bool> ExitApplicationAsync()
		{
			Console.Clear();
			Console.WriteLine("??? EXIT ???\n");
			Console.Write("Do you want to save changes before exiting? (yes/no): ");
			var saveChoice = Console.ReadLine()?.Trim().ToLowerInvariant();

			if (saveChoice == "yes" || saveChoice == "y")
			{
				Console.Write("\nSaving contacts...");
				await _contactService.SaveAsync();
				Console.WriteLine(" Done!");
				Console.WriteLine("? Contacts saved successfully!");
			}
			else
			{
				Console.WriteLine("\n?? Exiting without saving. All changes will be lost.");
			}

			Console.WriteLine("\nThank you for using Microsoft Contact Management System!");
			Console.WriteLine("Goodbye!");
			await Task.Delay(1500);
			return true;
		}
	}
}
