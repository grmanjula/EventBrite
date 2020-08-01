﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CartAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;
        public CartController(ICartRepository repository)
        {
            _repository = repository;

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK) ]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _repository.GetCartAsync(id);
            return Ok(basket);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult>  Post([FromBody]Cart value)
        {
            var basket =  await _repository.UpdateCart(value);
            return Ok(basket);
        }
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            await _repository.DeleteCart(id);
        }
    }
}