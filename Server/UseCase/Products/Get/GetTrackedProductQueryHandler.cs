using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UseCase.Products.Get.Dtos;

namespace UseCase.Products.Get;

public class GetTrackedProductQueryHandler(
    IStoreRepository storeRepository,
    ITrackedProductRepository trackedProductRepository,
    IProductStatisticRepository productStatistic)
    : IRequestHandler<GetTrackedProductQuery, IActionResult>
{
    public async Task<IActionResult> Handle(GetTrackedProductQuery query, CancellationToken cancellationToken)
    {
        var trackedProduct = await trackedProductRepository.Get(ObjectId.Parse(query.Id));

        if (trackedProduct == null)
        {
            return new NotFoundObjectResult("Tracked product is not found.");
        }

        var stores = await storeRepository.GetAll();
        var statistics = await productStatistic.GetAll(trackedProduct.Id);
        var groupedStatistic = statistics.GroupBy(c => c.StoreId).ToDictionary(c => c.Key, g => g.ToList());
        return new OkObjectResult(new TrackedProductResponse
        {
            Id = trackedProduct.Id.ToString(),
            Name = trackedProduct.Name,
            StoreStatistics = groupedStatistic.Select(group =>
            {
                return new StoreResponse()
                {
                    Id = group.Key.ToString(),
                    Name = stores.Find(c => c.Id == group.Key)!.Name,
                    Statistic = group.Value.Select(statistic => new StatisticResponse()
                    {
                        Date = statistic.Date,
                        Price = statistic.Price
                    }).ToList()
                };
            }).ToList()
        });
    }
}