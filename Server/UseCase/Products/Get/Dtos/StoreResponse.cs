namespace UseCase.Products.Get.Dtos;

public class StoreResponse
{
    public required string Id { get; set; }
    public required string? Name { get; set; }
    public required List<StatisticResponse> Statistic { get; set; } = [];
}