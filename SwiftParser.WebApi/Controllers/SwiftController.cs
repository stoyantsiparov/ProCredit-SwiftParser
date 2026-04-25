using Microsoft.AspNetCore.Mvc;
using SwiftParser.WebApi.Common;
using SwiftParser.WebApi.Models;
using SwiftParser.WebApi.Repositories.Contracts;
using SwiftParser.WebApi.Services.Contracts;

namespace SwiftParser.WebApi.Controllers;

/// <summary>
/// Handles HTTP requests for SWIFT message processing.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SwiftController : ControllerBase
{
    private readonly ISwiftParserService _parserService;
    private readonly ISwiftRepository _repository;
    private readonly ILogger<SwiftController> _logger;

    public SwiftController(
        ISwiftParserService parserService,
        ISwiftRepository repository,
        ILogger<SwiftController> logger)
    {
        _parserService = parserService;
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Uploads a SWIFT MT103 file, parses its content, and saves it to the database.
    /// </summary>
    /// <param name="file">The .txt file containing the SWIFT message.</param>
    /// <returns>Returns the parsed object upon successful processing.</returns>
    /// <response code="200">If the file is processed and saved successfully.</response>
    /// <response code="400">If the file is empty or the SWIFT format is invalid.</response>
    /// <response code="500">If an unexpected server error occurs.</response>
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadSwiftFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            _logger.LogWarning("Upload attempt with an empty or null file.");
            return BadRequest(ErrorMessages.EmptyFile);
        }

        try
        {
            _logger.LogInformation("Processing file: {FileName}, Size: {Size} bytes.", file.FileName, file.Length);

            string rawContent;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                rawContent = await reader.ReadToEndAsync();
            }

            Mt103Message parsedMessage = _parserService.Parse(rawContent);
            _logger.LogInformation("File {FileName} parsed successfully. Ref: {Ref}", file.FileName, parsedMessage.Reference);

            await _repository.SaveMessageAsync(parsedMessage);
            _logger.LogInformation("Message Ref {Ref} saved to database.", parsedMessage.Reference);

            return Ok(new
            {
                Message = "File processed and saved successfully.",
                Data = parsedMessage
            });
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Validation error during SWIFT parsing.");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during file processing.");
            return StatusCode(500, ErrorMessages.InternalServerError);
        }
    }
}