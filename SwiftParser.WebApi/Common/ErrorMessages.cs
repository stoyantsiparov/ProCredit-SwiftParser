namespace SwiftParser.WebApi.Common;

/// <summary>
/// Contains centralized error messages used across the application to ensure consistency.
/// </summary>
public static class ErrorMessages
{
    public const string EmptyFile = "Please attach a valid file.";
    public const string MissingBlock4 = "Invalid SWIFT message: Block {4} is missing.";
    public const string InternalServerError = "An internal server error occurred. Please check the logs for more information.";
    public const string ParsingError = "Error occurred while parsing the SWIFT message.";
    public const string ProcessingError = "An unexpected error occurred during processing.";
}