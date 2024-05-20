using MongoDB.Bson;

namespace Domain.Models;

public class Store
{
    public required ObjectId Id { get; set; }
    public required string Name { get; set; }
    public required string BaseUrl { get; set; }
    public required string SearchUrl { get; set; }
}