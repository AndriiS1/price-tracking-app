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
        var getAllRelatedStatistic = await productStatistic.GetLatest(trackedProducts.Select(c => c.Id));

        var stores = await storeRepository.GetAllRelated(getAllRelatedStatistic.Select(c => c.StoreId));

        var response = trackedProducts.Select(trackedProduct =>
        {
            return new TrackedProductResponse
            {
                ProductId = trackedProduct.Id.ToString(),
                Name = trackedProduct.Name,
                StoreStatistics = getAllRelatedStatistic.Where(c => c.TrackedProductId == trackedProduct.Id).Select(
                    group => new StoreResponse
                    {
                        StoreId = group.StoreId.ToString(),
                        StoreName = stores.Find(c => c.Id == group.StoreId)?.Name,
                        StoreLastStatistic = StatisticResponse.FromDomain(group)
                    }).ToList()
            };
        });

        return new OkObjectResult(response);
    }
}