using CalcCDB.Domain.Entities;
using CalcCDB.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalcCDB.Appication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CDBController : ControllerBase
    {
        private readonly ICDBService icalcService;

        public CDBController(ICDBService cDBService)
        {
            icalcService = cDBService;
        }

        [HttpGet]
        public async Task<IActionResult> CalCdb(decimal value, int months)
        {
            if (value <= 0)
                return BadRequest("O valor investido não pode ser menor ou igual a 0");

            if (months <= 0)
                return BadRequest("Os meses devem ser maior que 0");

            if (months > 12)
                return BadRequest("Os meses não podem ser maiores que 12");

           return Ok(await icalcService.CalCdb(value, months));
        }
    }
}
