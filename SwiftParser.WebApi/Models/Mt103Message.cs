namespace SwiftParser.WebApi.Models;

/// <summary>
/// Represents a structured SWIFT MT103 message parsed from a raw text file.
/// </summary>
public class Mt103Message
{
    /// <summary>Sender's Reference (Tag :20:)</summary>
    public string Reference { get; set; } = string.Empty;

    /// <summary>Bank Operation Code (Tag :23B:)</summary>
    public string OperationCode { get; set; } = string.Empty;

    /// <summary>Date of the transaction in YYMMDD format (From Tag :32A:)</summary>
    public string ValueDate { get; set; } = string.Empty;

    /// <summary>3-letter currency code (From Tag :32A:)</summary>
    public string Currency { get; set; } = string.Empty;

    /// <summary>Transaction amount (From Tag :32A:)</summary>
    public decimal Amount { get; set; }

    /// <summary>Ordering Customer details (Tag :50K:)</summary>
    public string OrderingCustomer { get; set; } = string.Empty;

    /// <summary>Beneficiary Customer details (Tag :59:)</summary>
    public string BeneficiaryCustomer { get; set; } = string.Empty;

    /// <summary>Remittance Information (Tag :70:)</summary>
    public string RemittanceInfo { get; set; } = string.Empty;
}