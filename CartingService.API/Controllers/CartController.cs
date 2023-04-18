using CartingService.Common.Entities;
using CartingService.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CartController(ILogger<CartController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("/cart", Name = "GetCartById")]
        public Cart GetCart(int id)
        {
            var cart = _unitOfWork.Cart.GetCart(id);
            return cart;
        }

        [HttpPost("/cart/add/{cartId}", Name = "AddItemToCart")]
        public ActionResult<Item> AddItemToCart(int cartId, Item item) 
        {
            var result = _unitOfWork.Cart.AddItemToCart(cartId, item);
            if (result)
            {
                return Ok(item);
            }
            return BadRequest();
        }

        [HttpDelete("/cart/{cartId}/remove/{itemId}", Name = "DeleteItem")]
        public ActionResult RemoveItemFromCart(int cartId, int itemId)
        {
            var result = _unitOfWork.Cart.RemoveItemFromCart(cartId, itemId);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("/cart/add", Name = "AddCart")]
        public ActionResult<Cart> AddCart(Cart cart) 
        { 
            var result = _unitOfWork.Cart.AddCart(cart);
            if (result)
            {
                return Ok(cart);
            }
            return BadRequest();
        }
    }
}