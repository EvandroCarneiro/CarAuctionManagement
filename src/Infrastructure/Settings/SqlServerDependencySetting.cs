namespace BCA.CarAuctionManagement.Infrastructure.Settings;

internal record SqlServerDependencySetting : DependencySetting
{
    public override string Name => Constants.Dependencies.SqlServer;

    public string ConnectionString { get; set; }
}
