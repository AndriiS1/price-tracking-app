namespace UseCase.Products.Search.Dtos;

public class SearchResponse
{
    public required string StoreId { get; set; }
    public required string StoreName { get; set; }
    public required decimal MinPrice { get; set; }
    public required decimal MaxPrice { get; set; }
    public required decimal Average { get; set; }
    public required string SearchUrl { get; set; }
}