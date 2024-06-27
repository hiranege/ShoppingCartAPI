namespace ShoppingCart.API.Entities
{
    public class UserShoppingCart:BaseEntity
    {
        public required string UserId { get; set; }
        public virtual List<ShoppingCartItem>? Items { get; set; }
    }

}
