using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
     public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri, 
            string AuthorizationToken = null,
            string AuthorizationMethod = "Bearer");
    }
}
