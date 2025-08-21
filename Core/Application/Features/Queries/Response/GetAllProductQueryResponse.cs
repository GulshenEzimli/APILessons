namespace Application.Features.Queries.Response
{
	public class GetAllProductQueryResponse
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
	}
}
