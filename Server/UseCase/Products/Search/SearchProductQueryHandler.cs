using System.Text.RegularExpressions;
using Domain.Repositories;
using HtmlAgilityPack;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UseCase.Products.Search;

public class SearchProductQueryHandler(
    IStoreRepository storeRepository)
    : IRequestHandler<SearchProductQuery, IActionResult>
{
    public async Task<IActionResult> Handle(SearchProductQuery query, CancellationToken cancellationToken)
    {
        // Launch a new browser instance

        var url = "https://www.atbmarket.com/sch?page=1&lang=uk&location=1154&query=%D1%8F%D0%B9%D1%86%D1%8F";

        // Load the HTML document from the URL
        var web = new HtmlWeb();
        var doc = web.Load(url);

        var priceNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'product-price')]");

        // List to store the parsed prices
        var prices = new List<decimal>();

        if (priceNodes != null)
        {
            // Iterate through each price node
            foreach (var node in priceNodes)
            {
                var pattern = @"\d+\.\d+";
                var regex = new Regex(pattern);
                prices.Add(decimal.Parse(regex.Matches(node.InnerText)[0].Value.Replace(".", ",")));
            }
        }

        return new OkObjectResult(prices);
    }
}