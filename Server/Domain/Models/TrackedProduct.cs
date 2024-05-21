using MongoDB.Bson;

namespace Domain.Models;

public class TrackedProduct
{
    public required ObjectId Id { get; set; }
    public required string Name { get; set; }
    public required int TotalSearchCount { get; set; }
}