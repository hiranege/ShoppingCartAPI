using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Entities;
using ShoppingCart.API.Exceptions;
using ShoppingCart.API.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("user/{userId}/totalamount")]
        public async Task<IActionResult> GetCartTotalAmount(string userId)
        {
            try
            {
                var totalAmount = await _shoppingCartService.GetShoppingCartAmountAsync(userId);
                return Ok(totalAmount);
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, ex.Message);
            }
           
        }

        [HttpPost("AddItemsToCart/{userId}")]
        public async Task<IActionResult> AddItemsToCart(string userId, [FromBody] List<string> items)
        {
            try
            {
                var updatedCart = await _shoppingCartService.AddItemToShoppingCartAsync(userId, items);
                return Ok(updatedCart);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, ex.Message);
            }
        }
    }
}
