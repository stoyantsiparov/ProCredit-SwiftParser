# ProCredit Swift MT103 Parser - .NET Web API

This project is a .NET 10 Web API developed as a technical assessment for the .NET Developer Intern position at ProCredit Bank. It is designed to receive `.txt` files containing SWIFT MT103 messages, parse their contents using a custom-built parser, and securely store the structured data into a SQLite database.

## Features

### SWIFT Message Parsing
* 📄 **Custom Parser:** Built entirely from scratch using Regular Expressions (Regex) to extract specific blocks and tags (e.g., `:20:`, `:32A:`, `:50K:`, `:59:`) without relying on any third-party SWIFT libraries.
* 💱 **Data Formatting:** Automatically converts parsed string values into appropriate data types (e.g., formatting transaction amounts into `decimal` for accurate processing).

### Database Management
* 🗄️ **SQLite Integration:** Lightweight, file-based database approach (`bank_data.db`).
* 🛡️ **Raw SQL (No ORM):** Communicates with the database using raw ADO.NET (`SqliteCommand`) and parameterized SQL queries, strictly avoiding Entity Framework as per the requirements while ensuring protection against SQL Injection.
* 🚀 **Auto-Initialization:** The database and required tables are automatically generated upon application startup if they do not already exist.

### Architecture & Infrastructure
* 🏗️ **Clean Code Architecture:** Follows SOLID principles with a clear separation of concerns (Controllers, Services, Repositories).
* 📝 **Advanced Logging:** Integrates **NLog** to automatically generate daily text log files (stored in the `logs` folder), capturing process execution steps and potential errors.
* 📖 **Interactive Documentation:** Fully configured **Swagger UI** that opens automatically upon launch, allowing easy testing of the file upload endpoint.
* 🚫 **No Auth:** Authentication and Authorization middleware have been explicitly removed as requested.

## Technologies

#### Backend
* 🌐 **.NET 10 & ASP.NET Core** for the Web API framework.
* 🧩 **System.Text.RegularExpressions** for complex string parsing.

#### Database
* 💾 **SQLite** & `Microsoft.Data.Sqlite` for secure and efficient data storage without an ORM.

#### Logging & Documentation
* 📊 **NLog** (`NLog.Web.AspNetCore`) for file-based logging.
* 🟢 **Swashbuckle / Swagger** for API testing and documentation.

## Installation & Usage

* Clone the repository:
```bash
git clone [https://github.com/stoyantsiparov/ProCredit-SwiftParser.git](https://github.com/stoyantsiparov/ProCredit-SwiftParser.git)
