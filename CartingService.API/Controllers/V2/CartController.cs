using CartingService.Common.Entities;
using CartingService.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.API.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CartController(ILogger<CartController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Returns a list of Cart Items instead of Cart model.
        /// </summary>
        /// <param name="id"> Cart unique Key </param>
        /// <returns>Cart model (Cart key + list of Cart items)</returns>
        [MapToApiVersion("2.0")]
        [HttpGet("/cart", Name = "GetCartItemsByCartId")]
        public List<Item> GetCart(int id)
        {
            var cart = _unitOfWork.Cart.GetCart(id);
            return cart.Items;
        }

        /// <summary>
        /// Returns 200 if item was added to the cart. 
        /// If there was no cart for specified key – creates it. 
        /// Otherwise returns a corresponding HTTP code.
        /// </summary>
        /// <param name="cartId">Cart unique key</param>
        /// <param name="item">Cart Item model</param>
        /// <returns></returns>
        [MapToApiVersion("2.0")]
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

        /// <summary>
        /// Returns 200 if item was deleted, otherwise returns corresponding HTTP code.
        /// </summary>
        /// <param name="cartId">Cart unique key</param>
        /// <param name="itemId">Item Id</param>
        /// <returns>Corresponding HTTP code</returns>
        [MapToApiVersion("2.0")]
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

        /// <summary>
        /// Adding a new Cart
        /// </summary>
        /// <param name="cart">Cart model</param>
        /// <returns>Corresponding HTTP code</returns>
        [MapToApiVersion("2.0")]
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