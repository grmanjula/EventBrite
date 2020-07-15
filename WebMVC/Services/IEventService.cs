using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
     public interface IEventService
    {
        Task<Catalog> GetEventItemsAsync(int page, int size, int? location, int? type);

        Task <IEnumerable<SelectListItem>> GetEventTypeAsync();
        Task <IEnumerable<SelectListItem>> GetEventLocationAsync();

        
    }
}
