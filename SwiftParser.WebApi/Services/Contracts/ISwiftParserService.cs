using SwiftParser.WebApi.Models;

namespace SwiftParser.WebApi.Services.Contracts;

/// <summary>
/// Defines the contract for parsing raw SWIFT message content.
/// </summary>
public interface ISwiftParserService
{
    /// <summary>
    /// Parses the raw string content of a SWIFT MT103 file into a structured model.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the message format is invalid.</exception>
    Mt103Message Parse(string rawContent);
}