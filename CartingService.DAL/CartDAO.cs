using CartingService.Common.Entities;
using CartingService.DAL.Interfaces;
using LiteDB;

namespace CartingService.DAL
{
    public class CartDAO : ICartDAO
    {
        private LiteDatabase context;

        public CartDAO(LiteDatabase database)
        {
            context = database;
        }

        public Cart GetCart(int cartId) 
        {
            var cart = GetAllCarts().Find(x => x.Id == cartId).FirstOrDefault();

            if (cart == null)
            {
                return new Cart();
            }
            else
            {
                return cart;
            }
        }

        public bool AddItemToCart(int cartId, Item item)
        {
            var cartSet = GetAllCarts();
            var cart = cartSet.Find(x => x.Id == cartId).FirstOrDefault();

            if (cart == null) 
            {
                return false;
            }
            else
            {
                cart.Items.Add(item);
                return cartSet.Update(cart);
            }
        }

        public bool RemoveItemFromCart(int cartId, int itemId)
        {
            var cartSet = GetAllCarts();
            var cart = cartSet.Find(x => x.Id == cartId).FirstOrDefault();

            if (cart == null)
            {
                return false;
            }
            else
            {
                var item = cart.Items.FirstOrDefault(x => x.Id == cartId);
                if (item == null)
                {
                    return false;
                }
                else
                {
                    cart.Items.Remove(item);
                    return cartSet.Update(cart);
                }
            }
        }

        private ILiteCollection<Cart> GetAllCarts() 
        {
            return context.GetCollection<Cart>("carts");
        }
    }
}
