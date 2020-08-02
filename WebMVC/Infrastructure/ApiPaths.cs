using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public static class ApiPaths
    {
        public static class Catalog
        {
            public static string GetAllTypes(string baseUri)
            {
                return $"{baseUri}EventTypes";
            }
            public static string GetAllLocations(string baseUri)
            {
                return $"{baseUri}EventLocations";
            }
            public static string GetAllEventItems(string baseUri, int page, int? location, int? type, int take)
            {
                var filterQs = string.Empty;
                if (location.HasValue || type.HasValue)
                {
                    var locationQs = (location.HasValue) ? location.Value.ToString() : null;
                    var typeQs = (type.HasValue) ? type.ToString() : null;
                    filterQs = $"/type/{typeQs}/location/{locationQs}";

                }
                return $"{baseUri}items{filterQs}?pageIndex={page}&pageSize={take}";
            }
        }
        public static class Basket
        {
            public static string GetBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }

            public static string UpdateBasket(string baseUri)
            {
                return baseUri;
            }

            public static string CleanBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }
        }

    }
}
