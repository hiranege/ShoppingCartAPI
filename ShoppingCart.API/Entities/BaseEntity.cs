using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
