using APBD_Task_6.Models;

namespace Zadanie5.Services;

public interface IWarehouseProcedureService
{
    Task<int> AddProductToWarehouseAsync(ProductWarehouse productWarehouse);
}