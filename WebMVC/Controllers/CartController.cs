using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using WebMVC.Services;
using WebMVC.Models;
using WebMVC.Models.CartModels;
using Polly.CircuitBreaker;
using WebMVC.Services;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        
        private readonly ICartService _cartService;
        //M
        //  private readonly ICatalogService _catalogService;
        private readonly IEventService _eventservice;
        private readonly IIdentityService<ApplicationUser> _identityService;
        //M
        //public CartController(IIdentityService<ApplicationUser> identityService, ICartService cartService, ICatalogService catalogService)
        public CartController(IIdentityService<ApplicationUser> identityService, ICartService cartService, IEventService eventService)

        {
            _identityService = identityService;
            _cartService = cartService;

            //M
            //_catalogService = catalogService;
            _eventservice = eventService;



        }
        public IActionResult  Index()
        {
            //try
            //{

            //    var user = _identityService.Get(HttpContext.User);
            //    var cart = await _cartService.GetCart(user);


            //    return View();
            //}
            //catch (BrokenCircuitException)
            //{
            //    // Catch error when CartApi is in circuit-opened mode                 
            //    HandleBrokenCircuitException();
            //}

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<string, int> quantities, string action)
        {
            if (action == "[ Checkout ]")
            {
                return RedirectToAction("Create", "Order");
            }


            try
            {
                var user = _identityService.Get(HttpContext.User);
                var basket = await _cartService.SetQuantities(user, quantities);
                var vm = await _cartService.UpdateCart(basket);

            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in open circuit  mode                 
                HandleBrokenCircuitException();
            }

            return View();

        }
            public async Task<IActionResult> AddToCart(EventItem productDetails)
          //  public async Task<IActionResult> AddToCart(CatalogItem productDetails)
        {
            try
            {
               // if (productDetails.Id! > 0)
                    if (productDetails.ID > 0)
                {
                    var user = _identityService.Get(HttpContext.User);
                    var product = new CartItem()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Quantity = 1,
                        ProductName = productDetails.Name,
                       // PictureUrl = productDetails.PictureUrl,
                       PictureUrl = productDetails.ImageUrl,
                        UnitPrice = productDetails.Price,
                        ProductId = productDetails.ID.ToString()
                    };
                    await _cartService.AddItemToCart(user, product);
                }
                return RedirectToAction("Index", "Catalog");
            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in circuit-opened mode                 
                HandleBrokenCircuitException();
            }

            return RedirectToAction("Index", "Catalog");

        }
       

        private void HandleBrokenCircuitException()
        {
            TempData["BasketInoperativeMsg"] = "cart Service is inoperative, please try later on. (Business Msg Due to Circuit-Breaker)";
        }

    }
}