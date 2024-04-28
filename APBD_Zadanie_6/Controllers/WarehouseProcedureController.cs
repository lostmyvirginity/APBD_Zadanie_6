using APBD_Task_6.Models;
using Microsoft.AspNetCore.Mvc;

namespace Zadanie5.Controllers;

[Route("api/warehouse2")]
[ApiController]
public class WarehouseProcedureController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddProductToWarehouse(ProductWarehouse productWarehouse)
    {
        return Ok();
    }
}