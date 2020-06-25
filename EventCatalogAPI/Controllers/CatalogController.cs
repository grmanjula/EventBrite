using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using EventCatalogAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly EventContext _context;
        private readonly IConfiguration _config;

        public CatalogController(EventContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }
        //To verify in postman https://localhost:44315/api/Catalog\items
        //To Verify by Page Index https://localhost:44315/api/Catalog\items?page
        //We are using Async so we need to write Task as well
        [HttpGet("[action]")]
        public async Task<IActionResult> Items(
           [FromQuery] int pageIndex =0,
            [FromQuery] int pageSize =6)
        {
            var itemsCount = await _context.EventItems.LongCountAsync();
            var items = await   _context.EventItems
            .OrderBy(c => c.Name)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

            items = ChangeImageUrl(items);

            // items = ChangeImageUrl

            var model = new PaginatedItemsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = items.Count,
                Data = items,
                Count = itemsCount
            };
            return Ok(model);
        }

        private List<EventItem> ChangeImageUrl(List<EventItem> items)
        {
            items.ForEach(item => 
            item.ImageUrl = item.ImageUrl.Replace
            ("http://externaleventbaseurltobeplaced", 
            _config["ExternalCatalogBaseUrl"]));

            return items;
        }
    }
}