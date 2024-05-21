using Domain.Models;

namespace UseCase.Products.Get_all.Dtos;

public class StatisticResponse
{
    public required decimal Price { get; set; }
    public required DateTime Date { get; set; }

    public static StatisticResponse? FromDomain(ProductStatistic? product)
    {
        return product is null ? default : new StatisticResponse { Date = product.Date, Price = product.Price };
    }
}