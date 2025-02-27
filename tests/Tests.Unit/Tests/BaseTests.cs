namespace BCA.CarAuctionManagement.Tests.Unit.Tests;

using System.Diagnostics.CodeAnalysis;

using AutoFixture.AutoNSubstitute;

[ExcludeFromCodeCoverage]
public abstract class BaseTests
{
    protected readonly Fixture fixture;

    protected BaseTests()
    {
        fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        fixture.Customize(new AutoNSubstituteCustomization());
    }
}
