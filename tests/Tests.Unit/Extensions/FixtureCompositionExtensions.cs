namespace BCA.CarAuctionManagement.Tests.Unit.Extensions;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

public static class FixtureCompositionExtension
{
    public static FixtureCustomization<T> For<T>(this Fixture fixture) => new(fixture.Create<T>());

    public static FixtureCustomizationMany<T> ForMany<T>(this Fixture fixture, int count) => new(fixture.CreateMany<T>(count));

    public static void SetProperty<TSource, TProperty>(
        this TSource source,
        Expression<Func<TSource, TProperty>> prop,
        TProperty value)
    {
        var propertyInfo = (PropertyInfo)((MemberExpression)prop.Body).Member;
        propertyInfo.SetValue(source, value);
    }
}

public class FixtureCustomization<T>
{
    private readonly T instance;

    public FixtureCustomization(T instance)
    {
        this.instance = instance;
    }

    public FixtureCustomization<T> With<TProp>(Expression<Func<T, TProp>> expr, TProp value)
    {
        instance.SetProperty(expr, value);
        return this;
    }

    public T Create() => instance;
}

public class FixtureCustomizationMany<T>
{
    private readonly IEnumerable<T> instances;

    public FixtureCustomizationMany(IEnumerable<T> instances)
    {
        this.instances = instances;
    }

    public FixtureCustomizationMany<T> With<TProp>(Expression<Func<T, TProp>> expr, TProp value)
    {
        foreach (var instance in instances)
        {
            instance.SetProperty(expr, value);
        }

        return this;
    }

    public IEnumerable<T> Create() => instances;
}
