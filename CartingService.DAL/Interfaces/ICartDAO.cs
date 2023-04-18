using CartingService.Common.Entities;

namespace CartingService.DAL.Interfaces
{
    public interface ICartDAO
    {
        Cart GetCart(int id);
        bool AddItemToCart(int cartId, Item item);
        bool RemoveItemFromCart(int cartId, int itemId);
        public bool AddCart(Cart cart);
    }
}
