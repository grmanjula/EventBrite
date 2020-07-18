using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class CatalogIndexViewModel
    { 
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<SelectListItem> EventItems { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
        public List<EventItem> CatalogItems { get; internal set; }

        public int? LocationFilterApplied { get; set; }
        public int? TypesFilterApplied { get; set; }
    }
}
