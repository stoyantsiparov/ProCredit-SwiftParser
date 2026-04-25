using SwiftParser.WebApi.Models;

namespace SwiftParser.WebApi.Services;

public interface ISwiftParserService
{
    Mt103Message Parse(string rawContent);
}