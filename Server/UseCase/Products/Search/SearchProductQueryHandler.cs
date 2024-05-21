using System.Text.RegularExpressions;
using Domain.Models;
using Domain.Repositories;
using HtmlAgilityPack;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using UseCase.Products.Search.Dtos;

namespace UseCase.Products.Search;

public class SearchProductQueryHandler(
    IStoreRepository storeRepository,
    ITrackedProductRepository trackedProductRepository,
    IProductStatisticRepository productStatistic)
    : IRequestHandler<SearchProductQuery, IActionResult>
{
    private const string PricePattern = @"\d+[\.,]\d+";

    public async Task<IActionResult> Handle(SearchProductQuery query, CancellationToken cancellationToken)
    {
        var stores = await storeRepository.GetAll();
        var regex = new Regex(PricePattern);

        var responses = stores.Select(store =>
        {
            var searchUrl = store.SearchUrl + query.ProductName;

            var web = new HtmlWeb();
            var doc = web.Load(searchUrl);

            var priceNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'price')]");

            if (priceNodes == null)
            {
                return null;
            }

            var prices = priceNodes.Select(n =>
                {
                    var priceText = regex.Match(n.InnerText).Value;

                    if (priceText.IsNullOrEmpty())
                    {
                        return 0;
                    }

                    var price = decimal.Parse(priceText.Replace(".", ","));
                    return Math.Round(price, 2);
                })
                .Where(x => x != 0)
                .ToList();

            return ToSearchResponse(prices, store, query.ProductName);
        }).ToList();

        var withoutNulls = responses.Where(response => response is not null);
        await HandleTrackedProduct(query.ProductName, withoutNulls!);
        return new OkObjectResult(withoutNulls!);
    }

    private async Task HandleTrackedProduct(string productName, IEnumerable<SearchResponse> searchResponses)
    {
        var tryGetTrackedProduct = await trackedProductRepository.GetByName(productName);

        if (tryGetTrackedProduct is not null)
        {
            await trackedProductRepository.Increment(tryGetTrackedProduct.Id, 1);
        }
        else
        {
            var productId = ObjectId.GenerateNewId();
            await trackedProductRepository.Create(new TrackedProduct
                { Id = productId, Name = productName, TotalSearchCount = 1 });
            await productStatistic.CreateMany(searchResponses.Select(response => new ProductStatistic()
            {
                Date = DateTime.UtcNow,
                Id = ObjectId.GenerateNewId(),
                Price = response.Average,
                StoreId = ObjectId.Parse(response.StoreId),
                TrackedProductId = productId
            }));
        }
    }

    private static SearchResponse ToSearchResponse(IReadOnlyCollection<decimal> prices, Store store, string search)
    {
        return new SearchResponse
        {
            StoreId = store.Id.ToString(),
            StoreName = store.Name,
            MaxPrice = prices.Max(),
            MinPrice = prices.Min(),
            Average = prices.Average(),
            SearchUrl = store.SearchUrl + search
        };
    }
}