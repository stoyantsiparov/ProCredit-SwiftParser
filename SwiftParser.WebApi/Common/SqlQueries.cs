namespace SwiftParser.WebApi.Common;

/// <summary>
/// Centralized repository for raw SQL queries to prevent magic strings in the data access layer.
/// </summary>
public static class SqlQueries
{
    public const string CreateSwiftMessagesTable = @"
            CREATE TABLE IF NOT EXISTS SwiftMessages (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Reference TEXT,
                OperationCode TEXT,
                ValueDate TEXT,
                Currency TEXT,
                Amount DECIMAL,
                OrderingCustomer TEXT,
                BeneficiaryCustomer TEXT,
                RemittanceInfo TEXT,
                CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
            );";

    public const string InsertSwiftMessage = @"
            INSERT INTO SwiftMessages (
                Reference, OperationCode, ValueDate, Currency, Amount, 
                OrderingCustomer, BeneficiaryCustomer, RemittanceInfo
            ) VALUES (
                @Reference, @OperationCode, @ValueDate, @Currency, @Amount, 
                @OrderingCustomer, @BeneficiaryCustomer, @RemittanceInfo
            );";
}