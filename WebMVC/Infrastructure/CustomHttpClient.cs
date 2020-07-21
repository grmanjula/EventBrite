using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        //Below Http Client is from .Net frame work where in it opens browser like virtual browser
        //CustomeHttpClient will call HttpClient behind the scenes to mimic the browser

        private readonly HttpClient _client;

        public CustomHttpClient()
        {
            _client = new HttpClient();
        }
        public async Task<string> GetStringAsync(string uri, 
            string AuthorizationToken = null, 
            string AuthorizationMethod = "Bearer")
        {
            //Whenever you put Async you need to put await otherwise that line gets executed and moves on before the task is complete
            //The below line is equivalent to opening post man browser, enter uri, select get
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            //The below line is equivalent to clicking on send button
            var response = await _client.SendAsync(requestMessage);
            //there will be multiple things to read but reading only message content 
            return await response.Content.ReadAsStringAsync();
            //test

        }
    }
}
