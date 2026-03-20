using MediatR;

namespace Application.Features.Products.Command
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>   
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public decimal Discount { get; set; }
    }
}
