using System.Data.SqlClient;
using APBD_Task_6.Models;

namespace Zadanie5.Services;

public class WarehouseProcedureService : IWarehouseProcedureService
{
    private readonly IConfiguration _configuration;

    public WarehouseProcedureService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<int> AddProductToWarehouseAsync(ProductWarehouse productWarehouse)
    {
        int idProductWarehouse = 0;
        var connectionString = _configuration.GetConnectionString("Databse");
        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand("AddProductToWarehouse", connection);

        var transaction = (SqlTransaction)await connection.BeginTransactionAsync();

        cmd.Transaction = transaction;

        try
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            
            cmd.Parameters.AddWithValue("IdWarehouse", productWarehouse.IdWarehouse);
            cmd.Parameters.AddWithValue("IdProduct", productWarehouse.IdProduct);
            cmd.Parameters.AddWithValue("Amount", productWarehouse.Amount);
            cmd.Parameters.AddWithValue("CreatedAt", productWarehouse.CreatedAt);

            await connection.OpenAsync();
            int rowsChanged = await cmd.ExecuteNonQueryAsync();
            if (rowsChanged < 1) throw new Exception();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception();
        }
        using var reader = await cmd.ExecuteReaderAsync();

        await reader.ReadAsync();
         idProductWarehouse = int.Parse(reader["IdProductWarehouse"].ToString());

        await reader.CloseAsync();

        await connection.CloseAsync();

        return idProductWarehouse;
    }
}