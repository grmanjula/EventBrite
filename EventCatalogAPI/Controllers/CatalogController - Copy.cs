using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly EventContext _context;

        public CatalogController(EventContext context)
        {
            _context = context;

        }
        //To verify in postman https://localhost:44315/api/Catalog\items
        //To Verify by Page Index https://localhost:44315/api/Catalog\items?pageIndex=2&pageSize=5
        //We are using Async so we need to write Task as well
        [HttpGet("[action]")]
        public async Task<IActionResult> Items(
           [FromQuery] int pageIndex =0,
            [FromQuery] int pageSize =6)
        {
            var items = await _context.EventItems
            .OrderBy(c => c.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

            return Ok(items);
        }
    }
}