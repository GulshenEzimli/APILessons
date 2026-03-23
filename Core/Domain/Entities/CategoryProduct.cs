using Domain.Interfaces;

namespace Domain.Entities
{
    public class CategoryProduct : IEntityBase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
