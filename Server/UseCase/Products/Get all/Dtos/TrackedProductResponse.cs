namespace UseCase.Products.Get_all.Dtos;

public class TrackedProductResponse
{
    public required string ProductId { get; set; }
    public required string Name { get; set; }
    public required List<StoreResponse> StoreStatistics { get; set; }
}