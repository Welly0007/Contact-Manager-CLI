using System.Text.Json;
using Application.Contracts;
using Application.Services;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Contact_Manager_CLI
{
	public static class ServicesRegistration
	{
		public static IServiceCollection AddContactManagerServices(this IServiceCollection services)
		{
			//Json Read Options
			services.AddSingleton(new JsonSerializerOptions
			{
				WriteIndented = true,
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			});
			//FilePath, could be loaded from config
			services.AddSingleton(sp => Path.Combine(AppContext.BaseDirectory, "Contacts.json"));
			services.AddSingleton<IFileLock>(sp =>
			{
				var filePath = sp.GetRequiredService<string>();
				return new FileLock(filePath);
			});
			services.AddSingleton(sp =>
			{
				var fileLock = sp.GetRequiredService<IFileLock>();
				var filePath = sp.GetRequiredService<string>();
				var options = sp.GetRequiredService<JsonSerializerOptions>();
				return new JsonFileStore<Contact>(fileLock, filePath, options);
			});
			services.AddSingleton(typeof(JsonFileStore<>));
			services.AddSingleton<IContactRepository, JsonRepository>();
			services.AddSingleton<IContactService, ContactService>();
			return services;
		}
	}
}
