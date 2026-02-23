# Microsoft Contact Management System

A modern command-line interface (CLI) contact management application built with C# and .NET 8, designed to efficiently handle contact information for Microsoft's vendor and enterprise partner relationships.

## ?? Features

### Core Functionality
- **Add Contact**: Create new contacts with automatic ID and timestamp generation
- **Edit Contact**: Modify existing contact information
- **Delete Contact**: Remove contacts with confirmation prompt
- **View Contact**: Display detailed information for a specific contact
- **List Contacts**: Show all contacts in the system
- **Search**: Find contacts using Strategy Pattern with multiple search options:
  - Search by Name (contains)
  - Search by Phone (contains)
  - Search by Email (contains)
  - Search by All Fields
- **Filter**: Filter contacts by email domain (e.g., microsoft.com)
- **Save**: Explicitly save changes to persistent storage
- **Exit**: Close the application with optional save prompt

### Architecture Highlights
- **Clean Architecture**: Separation of concerns with Domain, Application, Infrastructure, and Presentation layers
- **SOLID Principles**: Well-structured, maintainable, and extensible code
- **Strategy Pattern**: Flexible search implementation allowing easy addition of new search criteria
- **In-Memory Operations**: Fast performance with data loaded at startup
- **Explicit Persistence**: Save only when requested, preventing unnecessary I/O operations
- **JSON Storage**: Human-readable data format for easy inspection and debugging
- **Thread-Safe File Operations**: Proper file locking for concurrent access scenarios

## ??? Project Structure

```
Contact-Manager-CLI/
??? Domain/        # Core business entities and contracts
?   ??? Entities/
?   ?   ??? Contact.cs        # Contact entity with ID, Name, Phone, Email, CreatedAt
?   ??? Contracts/
?       ??? IContactRepository.cs
??? Application/    # Business logic and services
?   ??? Services/
?   ?   ??? ContactService.cs # Main contact management logic
?   ??? Contracts/
?   ?   ??? IContactService.cs
?   ??? SearchStrategies/     # Strategy Pattern implementation
?       ??? IContactSearchStrategy.cs      # Search strategy interface
?       ??? SearchByNameStrategy.cs        # Name search implementation
?       ??? SearchByPhoneStrategy.cs       # Phone search implementation
?       ??? SearchByEmailStrategy.cs  # Email search implementation
?       ??? SearchByAllFieldsStrategy.cs   # All fields search implementation
??? Infrastructure/    # Data persistence and external concerns
?   ??? JsonRepository.cs     # In-memory repository with JSON persistence
?   ??? Persistence/
?       ??? JsonFileStore.cs  # Generic JSON file operations
?       ??? FileLock.cs     # Thread-safe file locking
?       ??? IFileLock.cs
??? Contact-Manager-CLI/      # Console application entry point
    ??? Program.cs    # Main menu and user interaction
    ??? ServicesRegistration.cs # Dependency injection configuration
```

## ?? Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or higher
- Terminal/Command Prompt
- Git (for cloning the repository)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Welly0007/Contact-Manager-CLI.git
   cd Contact-Manager-CLI
   ```

2. **Build the solution**
   ```bash
   dotnet build
   ```

3. **Run the application**
   ```bash
   dotnet run --project Contact-Manager-CLI
   ```

   Or navigate to the project directory:
   ```bash
   cd Contact-Manager-CLI
   dotnet run
   ```

## ?? Usage Guide

### Application Flow

1. **Startup**: The application loads all contacts from `Contacts.json` (if it exists) and displays the count
2. **Main Menu**: Choose from 9 available operations
3. **In-Memory Operations**: All changes are stored in memory for fast access
4. **Save**: Explicitly save changes when needed
5. **Exit**: Option to save before closing the application

### Menu Options

```
?????????????????????????????????????????????????
?     MICROSOFT CONTACT MANAGEMENT SYSTEM?
?????????????????????????????????????????????????

  1. Add Contact
  2. Edit Contact
  3. Delete Contact
  4. View Contact
  5. List Contacts
  6. Search
  7. Filter
  8. Save
  9. Exit
```

### Example Usage

#### Adding a Contact
```
Select an option (1-9): 1

??? ADD CONTACT ???

Name: John Doe
Phone: +1-555-0123
Email: john.doe@microsoft.com

? Contact added successfully! (Remember to save)
```

#### Searching Contacts (Strategy Pattern)
```
Select an option (1-9): 6

??? SEARCH CONTACTS ???

Select search criteria:
  1. Search by Name (contains)
  2. Search by Phone (contains)
  3. Search by Email (contains)
  4. Search by All Fields

Select option (1-4): 1

Enter search term (Name): John

? Found 2 result(s) using 'Name' search:

--------------------------------------------------------------------------------
ID:      a1b2c3d4-e5f6-7890-abcd-ef1234567890
Name:    John Doe
Phone:   +1-555-0123
Email:   john.doe@microsoft.com
Created: 2025-01-15 14:30:45 UTC
--------------------------------------------------------------------------------
ID:      b2c3d4e5-f6a7-8901-bcde-f12345678901
Name:    John Smith
Phone:   +1-555-0456
Email:   john.smith@microsoft.com
Created: 2025-01-15 15:22:10 UTC
--------------------------------------------------------------------------------
```

#### Filtering by Email Domain
```
Select an option (1-9): 7

??? FILTER CONTACTS ???

Enter email domain to filter (e.g., microsoft.com): microsoft.com

? Found 5 contact(s) with domain 'microsoft.com'
```

#### Editing a Contact
```
Select an option (1-9): 2

??? EDIT CONTACT ???

Enter Contact ID to edit: a1b2c3d4-e5f6-7890-abcd-ef1234567890

Current details:
Name: John Doe
Phone: +1-555-0123
Email: john.doe@microsoft.com

New Name (press Enter to keep 'John Doe'): John Smith
New Phone (press Enter to keep '+1-555-0123'): 
New Email (press Enter to keep 'john.doe@microsoft.com'): john.smith@microsoft.com

? Contact updated successfully! (Remember to save)
```

## ?? Data Storage

- **Format**: JSON
- **Location**: `Contact-Manager-CLI/Contacts.json`
- **Structure**:
  ```json
  [
  {
    "id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
      "name": "John Doe",
    "phone": "+1-555-0123",
      "email": "john.doe@microsoft.com",
      "createdAt": "2025-01-15T14:30:45.1234567Z"
    }
  ]
  ```

## ?? Technical Details

### Design Patterns

#### Strategy Pattern (Search Functionality)
The application implements the Strategy Pattern for search operations, providing flexible and extensible search capabilities:

- **IContactSearchStrategy**: Interface defining the contract for all search strategies
- **SearchByNameStrategy**: Searches contacts by name (case-insensitive, contains)
- **SearchByPhoneStrategy**: Searches contacts by phone number (exact match, contains)
- **SearchByEmailStrategy**: Searches contacts by email (case-insensitive, contains)
- **SearchByAllFieldsStrategy**: Searches across all fields (name, phone, email, ID)

**Benefits:**
- **Open/Closed Principle**: Easy to add new search strategies without modifying existing code
- **Single Responsibility**: Each strategy focuses on one specific search criterion
- **Testability**: Each strategy can be tested independently
- **Flexibility**: Users can choose the most appropriate search method for their needs

#### Other Patterns
- **Repository Pattern**: Abstraction over data access
- **Dependency Injection**: Loose coupling between layers
- **Service Layer Pattern**: Business logic separation

### Key Technologies
- **C# 12.0**: Modern language features (required properties, pattern matching)
- **.NET 8**: Latest framework capabilities
- **System.Text.Json**: High-performance JSON serialization
- **Async/Await**: Non-blocking I/O operations

### Performance Optimizations
- In-memory operations for all CRUD actions
- Lazy loading of data
- Efficient search implementations using LINQ
- File locking with retry logic for concurrent access

## ?? Testing the Application

### Manual Testing Steps

1. **Add multiple contacts** with different email domains and similar names
2. **List all contacts** to verify they appear correctly
3. **Test each search strategy**:
   - Search by name for "John"
   - Search by phone for "555"
   - Search by email for "microsoft"
   - Search by all fields for various terms
4. **Filter** by email domain (e.g., microsoft.com, gmail.com)
5. **View** a specific contact using its ID
6. **Edit** a contact and verify changes
7. **Delete** a contact with confirmation
8. **Save** and verify the `Contacts.json` file is updated
9. **Exit without saving** to test data persistence
10. **Restart the application** to verify data loads correctly

### Testing Search Strategies

Create test contacts with varied data:
```
Name: John Doe, Phone: +1-555-0123, Email: john.doe@microsoft.com
Name: Jane Smith, Phone: +1-555-0456, Email: jane.smith@gmail.com
Name: John Wilson, Phone: +44-207-1234, Email: john.wilson@microsoft.com
```

Test each search strategy:
- **By Name**: "John" should find John Doe and John Wilson
- **By Phone**: "555" should find contacts with 555 in phone
- **By Email**: "microsoft" should find Microsoft email addresses
- **All Fields**: "john" should find all Johns (name) and johns in emails

### Testing Concurrent Access

Open two terminal windows and run the application in both to test file locking:

```bash
# Terminal 1
dotnet run --project Contact-Manager-CLI

# Terminal 2
dotnet run --project Contact-Manager-CLI
```

Add contacts in both instances and save to verify proper synchronization.

## ?? Notes

- All timestamps are stored in UTC for consistency across time zones
- Contact IDs are auto-generated GUIDs ensuring uniqueness
- The application prompts for confirmation before destructive operations (delete)
- Unsaved changes are warned about when exiting
- All searches are case-insensitive for better user experience
- Phone search is exact match to handle various phone formats
- Filter supports domain names with or without the @ symbol

## ?? Contributing

This project follows Microsoft coding standards and best practices:
- Clean, readable code with proper naming conventions
- XML documentation for public APIs
- SOLID principles adherence
- Comprehensive error handling
- Design patterns for common scenarios

## ?? License

This project is developed for educational purposes.

## ?? Author

**Welly0007**
- GitHub: [@Welly0007](https://github.com/Welly0007)

## ?? Requirements Met

? Object-Oriented Programming
? SOLID Principles  
? **Strategy Pattern** (Search functionality with 4 different strategies)  
? Clean Architecture (Domain, Application, Infrastructure, Presentation)  
? All 9 menu operations implemented  
? In-memory operations with explicit save  
? JSON storage system  
? C# implementation  
? Contact entity with all required fields  
? Auto-generated ID and timestamps  
? Load and display contacts at startup  
? Comprehensive menu system  
? **Flexible search with user-selectable criteria** (Name, Phone, Email, All Fields)  
? Filter capabilities by email domain

---

**Last Updated**: February 2025  
**Version**: 2.0.0 (Strategy Pattern Implementation)  
**Target Framework**: .NET 8
