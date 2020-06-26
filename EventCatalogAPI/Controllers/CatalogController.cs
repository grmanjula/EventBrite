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
                //KAL CODE itemsCount.Result
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

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventTypes()
        {
            var types = await _context.EventTypes.ToListAsync();
            return Ok(types);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventLocations()
        {
            var locations = await _context.EventTypes.ToListAsync();
            return Ok(locations);
        }

        [HttpGet("[action]/type/{EventTypeId}/location/{EventLocationId}")]
        public async Task<IActionResult> Items(

            int? eventTypeId,
            int? eventLocationID,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 6)
        {

            //Type casting to IQueryable from Entitiy Frame work will make it build the query not to run it.
            //Without that it will execute the query and with millions of records getting DBSet and filtering out is very inefficient
            var query = (IQueryable<EventItem>)_context.EventItems;

            if (eventTypeId.HasValue)
            {
                query = query.Where(c => c.EventTypeId == eventTypeId);
            }

            //We don't write the below code as it will always return a empty set
            //_context.EventItems.Where(c=> 
            //c.EventTypeId == eventTypeId 
            //&& c.EventLocationId == eventLocationID)

            if (eventLocationID.HasValue)
            {
                query = query.Where(c => c.EventLocationId == eventLocationID);
            }

            var itemsCount = query.LongCountAsync();
            var items = await query
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
                Count = itemsCount.Result
            };
            return Ok(model);

        }
            

            
    }
}