using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        //Below Http Client is from .Net frame work where in it opens browser like virtual browser
        //CustomHttpClient will call HttpClient behind the scenes to mimic the browser

        private readonly HttpClient _client;
       // private IHttpContextAccessor _httpContextAccessor;

        public CustomHttpClient()
        {
            _client = new HttpClient();

            //_httpContextAccessor = httpContextAccessor;

        }
        public async Task<string> GetStringAsync(string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer")
        {
            //Whenever you put Async you need to put await otherwise that line gets executed and moves on before the task is complete
            //The below line is equivalent to opening post man browser, enter uri, select get
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            //The below line is equivalent to clicking on send button
            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            var response = await _client.SendAsync(requestMessage);
            //there will be multiple things to read but reading only message content 
            return await response.Content.ReadAsStringAsync();
            //test
            }
        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            // a new StringContent must be created for each retry 
            // as it is disposed after each call

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");
            //  SetAuthorizationHeader(requestMessage);
            if (authorizationToken != null)
            {


                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);

            }


            //if (requestId != null)
            //{
            //    requestMessage.Headers.Add("x-requestid", requestId);
            //}

            var response = await _client.SendAsync(requestMessage);

            // raise exception if HttpResponseCode 500 
            // needed for circuit breaker to track fails

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;

        }

            public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
            {
                return await DoPostPutAsync(HttpMethod.Post, uri, item, authorizationToken, authorizationMethod);
            }

            public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
            {
                return await DoPostPutAsync(HttpMethod.Put, uri, item, authorizationToken, authorizationMethod);
            }
            public async Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
                // SetAuthorizationHeader(requestMessage);
                //code to add the authentication token to the request.
                //in postman there is a dropdown called authenticate. This is equivalent to it
                if (authorizationToken != null)
                {

                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);

                }

                return await _client.SendAsync(requestMessage);
            }

           
        }
    }


    

