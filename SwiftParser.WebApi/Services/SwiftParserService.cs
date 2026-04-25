using System.Text.RegularExpressions;
using SwiftParser.WebApi.Common;
using SwiftParser.WebApi.Models;
using SwiftParser.WebApi.Services.Contracts;

namespace SwiftParser.WebApi.Services;

public class SwiftParserService : ISwiftParserService
{
    /// <inheritdoc />
    public Mt103Message Parse(string rawContent)
    {
        var message = new Mt103Message();

        // Isolate Block {4:...} using Regex
        var block4Match = Regex.Match(rawContent, @"\{4:\r?\n?(.*?)\r?\n?-\}", RegexOptions.Singleline);
        if (!block4Match.Success)
        {
            throw new ArgumentException(ErrorMessages.MissingBlock4);
        }

        string block4Content = block4Match.Groups[1].Value;

        // Extract tags and values
        var tagPattern = @"^:([A-Z0-9]{2,3}):(?:\r?\n)?(.*?)(?=(?:^:[A-Z0-9]{2,3}:)|$)";
        var tagMatches = Regex.Matches(block4Content, tagPattern, RegexOptions.Multiline | RegexOptions.Singleline);

        foreach (Match match in tagMatches)
        {
            string tag = match.Groups[1].Value;
            string value = match.Groups[2].Value.Trim();

            switch (tag)
            {
                case "20":
                    message.Reference = value;
                    break;
                case "23B":
                    message.OperationCode = value;
                    break;
                case "32A":
                    if (value.Length >= 9)
                    {
                        message.ValueDate = value.Substring(0, 6);
                        message.Currency = value.Substring(6, 3);

                        string amountStr = value.Substring(9).Replace(",", ".");
                        if (decimal.TryParse(amountStr, out decimal amount))
                        {
                            message.Amount = amount;
                        }
                    }
                    break;
                case "50K":
                    message.OrderingCustomer = value;
                    break;
                case "59":
                    message.BeneficiaryCustomer = value;
                    break;
                case "70":
                    message.RemittanceInfo = value;
                    break;
            }
        }

        return message;
    }
}