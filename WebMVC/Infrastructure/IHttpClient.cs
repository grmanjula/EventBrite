using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
     public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri, 
            string AuthorizationToken = null,
            string AuthorizationMethod = "Bearer");

        //post is used when anyone is posting something
        Task<HttpResponseMessage> PostAsync<T>(string uri,
            T item, 
            string authorizationToken = null, 
            string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> DeleteAsync(string uri, 
            string authorizationToken = null, 
            string authorizationMethod = "Bearer");

        //similar to post but put is used when editing something
        //For ex: when editing cart
        Task<HttpResponseMessage> PutAsync<T>(string uri, 
            T item,
            string authorizationToken = null, 
            string authorizationMethod = "Bearer");
    }
}

