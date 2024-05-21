using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCase.Products.Get_all.Dtos;

namespace UseCase.Products.Get_all;

public class GetTrackedProductsQueryHandler(
    IStoreRepository storeRepository,
    ITrackedProductRepository trackedProductRepository,
    IProductStatisticRepository productStatistic)
    : IRequestHandler<GetTrackedProductsQuery, IActionResult>
{
    public async Task<IActionResult> Handle(GetTrackedProductsQuery query, CancellationToken cancellationToken)
    {
        var trackedProducts = await trackedProductRepository.Get(query.Page, query.Size);
        var getAllRelatedStatistic = await productStatistic.GetAllRelated(trackedProducts.Select(c => c.Id));

        var groupedStatistic = getAllRelatedStatistic.GroupBy(c => c.StoreId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var stores = await storeRepository.GetAllRelated(groupedStatistic.Select(c => c.Key));

        var response = trackedProducts.Select(trackedProduct =>
        {
            return new TrackedProductResponse
            {
                ProductId = trackedProduct.Id.ToString(),
                Name = trackedProduct.Name,
                StoreStatistics = groupedStatistic.Select(group => new StoreResponse
                {
                    StoreId = group.Key.ToString(),
                    StoreName = stores.Find(c => c.Id == group.Key)?.Name,
                    StoreStatistic = group.Value.Select(c => new StatisticResponse { Date = c.Date, Price = c.Price })
                        .ToList()
                }).ToList()
            };
        });

        return new OkObjectResult(response);
    }
}