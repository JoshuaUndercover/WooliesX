using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WXTechChallenge.Common.Dtos.Request;
using WXTechChallenge.Common.Services.Interfaces;

namespace WXTechChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrolleyTotalController : ControllerBase
    {
        private readonly ITrolleyTotalService _trolleyTotalService;

        public TrolleyTotalController(ITrolleyTotalService trolleyTotalService)
        {
            _trolleyTotalService = trolleyTotalService;
        }

        [HttpPost]
        public async Task<IActionResult> Calculate([FromBody] TrolleyRequest trolleyRequest)
        {
            try
            {
                return Ok(await _trolleyTotalService.GetTotal(trolleyRequest).ConfigureAwait(false));
            }
            catch (Exception e)
            {
                return BadRequest("An error has occurred. " + e.Message);
            }
        }
    }
}
