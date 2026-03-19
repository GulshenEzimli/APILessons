using Domain.Common;

namespace Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<CategoryProduct> CategoryProducts { get; set; }
        public ICollection<Category> ChildCategories { get; set; }

    }
}
