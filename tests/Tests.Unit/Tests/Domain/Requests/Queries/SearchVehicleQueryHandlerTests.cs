namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Queries;

using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Base;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Domain.Requests.Queries;

using NSubstitute;

public class SearchVehicleQueryHandlerTests : BaseTests
{
    private readonly IVehicleRepository vehicleRepository;
    private readonly SearchVehicleQueryHandler handler;

    public SearchVehicleQueryHandlerTests()
    {
        vehicleRepository = fixture.Freeze<IVehicleRepository>();
        handler = fixture.Create<SearchVehicleQueryHandler>();
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldReturnResultOkWithPagedModel()
    {
        // Arrange
        var request = fixture.Create<SearchVehicleQuery>();

        var pagedModel = fixture.Create<PagedResult<Vehicle>>();

        vehicleRepository.SearchAsync(
            request.PagedFilter, 
            request.VehicleTypes, 
            request.Manufacturers, 
            request.Models, 
            request.Years)
            .Returns(pagedModel);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(pagedModel);
        await vehicleRepository.Received(1).SearchAsync(
            request.PagedFilter,
            request.VehicleTypes,
            request.Manufacturers,
            request.Models,
            request.Years);
    }
}
