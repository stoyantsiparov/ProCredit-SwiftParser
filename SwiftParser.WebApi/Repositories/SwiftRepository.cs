using Microsoft.Data.Sqlite;
using SwiftParser.WebApi.Common;
using SwiftParser.WebApi.Models;
using SwiftParser.WebApi.Repositories.Contracts;

namespace SwiftParser.WebApi.Repositories;

public class SwiftRepository : ISwiftRepository
{
    private readonly string _connectionString;

    public SwiftRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? "Data Source=bank_data.db";
    }

    /// <inheritdoc />
    public async Task InitializeDatabaseAsync()
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = SqlQueries.CreateSwiftMessagesTable;

        await command.ExecuteNonQueryAsync();
    }

    /// <inheritdoc />
    public async Task SaveMessageAsync(Mt103Message message)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = SqlQueries.InsertSwiftMessage;

        command.Parameters.AddWithValue("@Reference", message.Reference);
        command.Parameters.AddWithValue("@OperationCode", message.OperationCode);
        command.Parameters.AddWithValue("@ValueDate", message.ValueDate);
        command.Parameters.AddWithValue("@Currency", message.Currency);
        command.Parameters.AddWithValue("@Amount", message.Amount);
        command.Parameters.AddWithValue("@OrderingCustomer", message.OrderingCustomer);
        command.Parameters.AddWithValue("@BeneficiaryCustomer", message.BeneficiaryCustomer);
        command.Parameters.AddWithValue("@RemittanceInfo", message.RemittanceInfo);

        await command.ExecuteNonQueryAsync();
    }
}