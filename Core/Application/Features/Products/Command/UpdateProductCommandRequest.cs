using MediatR;

namespace Application.Features.Products.Command
{
    public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public decimal Discount { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
        public List<int> Categories { get; set; }
    }
}
