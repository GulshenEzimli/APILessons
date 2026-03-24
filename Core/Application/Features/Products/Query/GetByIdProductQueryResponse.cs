using Application.Dtos;

namespace Application.Features.Products.Query
{
    public class GetByIdProductQueryResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public BrandDto Brand { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
