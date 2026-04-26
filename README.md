# ProCredit Swift MT103 Parser - .NET Web API

This project is a high-performance **.NET 10 Web API** developed as a technical assessment for the .NET Developer Intern position at **ProCredit Bank**. The system is engineered to ingest `.txt` files containing SWIFT MT103 financial messages, parse them via a custom regex-based engine, and persist the data into a secure SQLite database using raw SQL.

## 🚀 Key Features

### 🔍 Precision SWIFT Parsing
* **Custom Regex Engine:** Built from the ground up using `System.Text.RegularExpressions` to isolate and extract critical SWIFT tags (e.g., `:20:`, `:23B:`, `:32A:`, `:50K:`, `:59:`, `:70:`) without external dependencies.
* **Smart Data Normalization:** Automatically converts and formats complex SWIFT string values into precise C# data types (e.g., parsing comma-separated amounts into `decimal`).

### 🗄️ Secure Data Management
* **Raw ADO.NET Integration:** Communicates with the **SQLite** backend using strictly parameterized queries and `SqliteCommand`. This approach follows bank requirements to avoid ORMs (like Entity Framework) and ensures maximum protection against SQL Injection.
* **Zero-Config Setup:** Features an automated database initializer that creates the schema and tables on startup if they are missing.

### 🏗️ Enterprise-Grade Architecture
* **Clean Architecture:** Implements a strict separation of concerns through the **Repository Pattern** and **Service Layer**, ensuring the code is maintainable and testable.
* **Resilient Logging:** Integrated **NLog** infrastructure that generates structured daily log files for audit trails and error debugging.
* **Live API Documentation:** Equipped with **Swagger UI** for real-time endpoint testing and visual API exploration.

## 🛠 Technologies & Tools

| Category | Technology |
| :--- | :--- |
| **Framework** | .NET 10 (ASP.NET Core Web API) |
| **Parsing** | Regular Expressions (Regex) |
| **Database** | SQLite via `Microsoft.Data.Sqlite` (Raw SQL) |
| **Logging** | NLog (File-based targets) |
| **Docs** | Swashbuckle / Swagger UI |

## 📦 Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/stoyantsiparov/ProCredit-SwiftParser.git](https://github.com/stoyantsiparov/ProCredit-SwiftParser.git)
2. **Navigate to the directory:**
   ```bash
   cd ProCredit-SwiftParser
3. **Build and Run:**
   ```bash
   dotnet run
