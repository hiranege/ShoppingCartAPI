using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.API.Entities
{
    public class ShoppingCartItem: BaseEntity
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required Guid ShopingCartId { get; set; }

    }
}
