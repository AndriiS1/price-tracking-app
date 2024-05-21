namespace UseCase.Products.Get_all.Dtos;

public class StoreResponse
{
    public required string StoreId { get; set; }
    public required string? StoreName { get; set; }
    public List<StatisticResponse> StoreStatistic { get; set; } = [];
}