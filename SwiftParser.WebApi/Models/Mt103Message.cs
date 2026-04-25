namespace SwiftParser.WebApi.Models;

public class Mt103Message
{
    public string Reference { get; set; } = string.Empty;
    public string OperationCode { get; set; } = string.Empty;

    public string ValueDate { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal Amount { get; set; }

    public string OrderingCustomer { get; set; } = string.Empty;
    public string BeneficiaryCustomer { get; set; } = string.Empty;
    public string RemittanceInfo { get; set; } = string.Empty;
}