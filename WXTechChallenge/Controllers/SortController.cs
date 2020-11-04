using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WXTechChallenge.Common.Dtos.Response;
using WXTechChallenge.Common.Enums;
using WXTechChallenge.Common.Services.Interfaces;

namespace WXTechChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly ISortService _sortService;

        public SortController(ISortService sortService)
        {
            _sortService = sortService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts(SortOption sortOption)
        {
            try
            {
                return Ok(await _sortService.GetProducts(sortOption).ConfigureAwait(false));
            }
            catch (Exception e)
            {
                return BadRequest("An error has occurred. " + e.Message);
            }
        }
    }
}
