using System.Text.RegularExpressions;
using SwiftParser.WebApi.Models;

namespace SwiftParser.WebApi.Services;

public class SwiftParserService : ISwiftParserService
{
    public Mt103Message Parse(string rawContent)
    {
        var message = new Mt103Message();

        // 1. Изолираме само блока {4:...}
        // Singleline опцията позволява на точката (.) да хваща и нови редове
        var block4Match = Regex.Match(rawContent, @"\{4:\r?\n?(.*?)\r?\n?-\}", RegexOptions.Singleline);
        if (!block4Match.Success)
        {
            throw new ArgumentException("Невалидно SWIFT съобщение: Липсва блок {4}.");
        }

        string block4Content = block4Match.Groups[1].Value;

        // 2. Извличаме всички тагове и техните стойности
        // Обяснение на Regex-а:
        // ^:([A-Z0-9]{2,3}):  -> Търси начало на ред (^), двоеточие, 2 до 3 букви/цифри и пак двоеточие.
        // (?:\r?\n)?(.*?)     -> Хваща стойността след тага (дори и да е на нови редове).
        // (?=(?:^:[A-Z0-9]{2,3}:)|$) -> Спира, когато види следващ таг на нов ред ИЛИ края на текста (Lookahead).
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
                    // Формат по стандарт: YYMMDD (6) + Валута (3) + Сума
                    // Пример: 160217EUR540,00
                    if (value.Length >= 9)
                    {
                        message.ValueDate = value.Substring(0, 6);
                        message.Currency = value.Substring(6, 3);

                        // Заместваме запетаята с точка, за да можем да парснем към decimal
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