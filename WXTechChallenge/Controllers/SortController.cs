using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WXTechChallenge.Dtos.Response;
using WXTechChallenge.Enums;
using WXTechChallenge.Services.Interfaces;

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
        public async Task<ActionResult<List<ProductDto>>> GetUser(SortOption sortOption)
        {
            return await _sortService.GetProducts(sortOption, "545d6d04-b34b-42f0-acd4-b445aa52119d").ConfigureAwait(false);
        }
    }
}
