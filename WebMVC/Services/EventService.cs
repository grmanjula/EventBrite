using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class EventService : IEventService
    {
        private readonly string _baseurl;
        private readonly IHttpClient _client;

        public EventService(IConfiguration config,IHttpClient client)
        {
            _baseurl = $"{config["CatalogUrl"]}/api/catalog/";
            _client = client;


        }
        public async Task<Catalog> GetEventItemsAsync(int page, int size, int? location, int? type)
        {
            var eventItemsUri = ApiPaths.Catalog.GetAllEventItems(_baseurl, page, location, type, size);
            var datastring = await _client.GetStringAsync(eventItemsUri);
            return JsonConvert.DeserializeObject<Catalog>(datastring);
        }

        public  async Task<IEnumerable<SelectListItem>> GetEventLocationAsync()
        {
            var locationUri = ApiPaths.Catalog.GetAllLocations(_baseurl);
            var dataString =  await _client.GetStringAsync(locationUri);
            var items = new List<SelectListItem>
             {
                 new SelectListItem
                 {
                     Value = null,
                     Text = "All",
                     Selected = true

                 }
             };
            var locations = JArray.Parse(dataString);
            foreach(var location in locations)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = location.Value<string>("id"),
                        Text = location.Value<string>("location")
                    });
               
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetEventTypeAsync()
        {
            var typeUri = ApiPaths.Catalog.GetAllTypes(_baseurl);
            var datastring = await  _client.GetStringAsync(typeUri);
            var items = new List<SelectListItem>
             {
                 new SelectListItem
                 {
                     Value = null,
                     Text = "All",
                     Selected = true

                 }
             };
            var types = JArray.Parse(datastring);
            foreach (var type in types)
            {
                types.Add(
                    new SelectListItem
                    {
                        Value = type.Value<string>("id"),
                        Text = type.Value<string>("brand")
                    });

            }
            return items;
        }
    }
}
