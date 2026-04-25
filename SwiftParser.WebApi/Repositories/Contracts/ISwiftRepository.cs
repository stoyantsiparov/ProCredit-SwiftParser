using SwiftParser.WebApi.Models;

namespace SwiftParser.WebApi.Repositories.Contracts;

/// <summary>
/// Defines the contract for database operations related to SWIFT messages.
/// </summary>
public interface ISwiftRepository
{
    /// <summary>
    /// Initializes the SQLite database and creates necessary tables if they do not exist.
    /// </summary>
    Task InitializeDatabaseAsync();

    /// <summary>
    /// Persists a parsed MT103 message to the underlying database.
    /// </summary>
    Task SaveMessageAsync(Mt103Message message);
}