namespace BCA.CarAuctionManagement.Domain.Common.Mappers;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Mapper
{
    public static TOut Map<TIn, TOut>(TIn source, Func<TIn, TOut> mapFunction)
        where TIn : class
        where TOut : class
        => source is null ? null : mapFunction(source);

    public static IEnumerable<TOut> Map<TIn, TOut>(IEnumerable<TIn> source, Func<TIn, TOut> mapFunction)
        where TIn : class
        where TOut : class
        => source is null || !source.Any() ? [] : source.Select(x => Map(x, mapFunction)).ToList();
}
