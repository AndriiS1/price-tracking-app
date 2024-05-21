using MongoDB.Bson;

namespace Domain.Models;

public class ProductStatistic
{
    public required ObjectId Id { get; set; }
    public required ObjectId TrackedProductId { get; set; }
    public required ObjectId StoreId { get; set; }
    public required decimal Price { get; set; }
    public required DateTime Date { get; set; }
}