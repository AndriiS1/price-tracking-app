﻿namespace UseCase.Products.Get_all.Dtos;

public class StoreResponse
{
    public required string Id { get; set; }
    public required string? Name { get; set; }
    public required StatisticResponse? LastStatistic { get; set; }
}