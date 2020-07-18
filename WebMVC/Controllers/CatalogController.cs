using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IEventService _service;

        public CatalogController(IEventService service)
        {
            _service = service;

        }
        public async Task<IActionResult> Index(int? page, int? locationFilterApplied, int? typeFilterApplied)
        {
            var itemsPerPage = 10;

            var catalog = await _service.GetEventItemsAsync(page ?? 0, itemsPerPage, locationFilterApplied, typeFilterApplied);

            var vm = new CatalogIndexViewModel
            {
                CatalogItems = catalog.Data,
                Locations = await _service.GetEventLocationAsync(),
                Types = await _service.GetEventTypeAsync(),
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsPerPage,
                    TotalItems = catalog.Count,
                    TotalPages = (int)Math.Ceiling((decimal)(catalog.Count) / itemsPerPage)
                },

                LocationFilterApplied = locationFilterApplied ?? 0,
                TypesFilterApplied = typeFilterApplied,


            };

            return View(vm);
        }
            

        }

    }
