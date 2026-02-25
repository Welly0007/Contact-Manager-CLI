# Contact Manager CLI (C# / .NET 8)

A small, practical command-line application for managing contacts (name, phone, email).  
It’s easy to run, fast in practice, and structured like a real application so it can grow without becoming messy using layered architecture.

---

## Requirements
- .NET SDK 8.0+
- Any OS that supports .NET (Windows / macOS / Linux)

---

## How to run

1. Clone the repository  
2. From the repo root, run:

```bash
dotnet build
dotnet run --project .\Contact-Manager-CLI\Contact-Manager-CLI.csproj
````

The interactive menu will appear and guide you through adding, listing, searching, filtering, updating, and deleting contacts.

---

## Notes on design and behavior

Contacts are loaded into memory once when the application starts. All operations (searching, filtering, updating) work against this in-memory state, which reduces disk access and keeps performance predictable. Changes are applied in memory first, then explicitly persisted to disk, instead of constantly reading and writing the file.

Searching is flexible and practical and uses strategy pattern for using different strategy. You can search by specific fields (name, phone, email), perform broader searches across multiple fields, and filter by creation date. This keeps lookups fast while still supporting common real-world workflows like finding recent contacts.

Although this is a CLI app, concurrency is treated seriously. File-based storage can easily break when multiple instances run at the same time or when reads happen during writes. To avoid that, persistence uses an explicit file-locking mechanism:

* Writes are exclusive (only one writer at a time)
* Reads are shared (multiple readers can read concurrently)
* When a writer holds the file, readers wait and retry instead of failing

This makes the JSON storage much safer and more reliable than a naive “read/write directly” approach.

The code is split into clear layers (domain, application, infrastructure, and CLI). Persistence is accessed through repositories, which makes the storage mechanism easy to replace later (for example, switching from JSON to a database) without touching the rest of the application. This also keeps business logic independent from the UI and storage details.

Overall, the app focuses on fast retrieval, safe concurrent access, and a structure that’s easy to extend or modify without rewriting everything.

---

This is probably more architecture than strictly needed for a small CLI tool, but that was intentional. It was built to practice clean layering, repository patterns, strategy-based searching and filtering, and safer concurrent file access in a realistic but manageable project.

```
