namespace BCA.CarAuctionManagement.Api.Controllers.v1;

using System.Net.Mime;

using BCA.CarAuctionManagement.Api.Dto.v1;
using BCA.CarAuctionManagement.Api.Dto.v1.Enums;
using BCA.CarAuctionManagement.Api.Extensions;
using BCA.CarAuctionManagement.Api.Mappers;
using BCA.CarAuctionManagement.Api.Mappers.Base;
using BCA.CarAuctionManagement.Domain.Models.Base;
using BCA.CarAuctionManagement.Domain.Requests.Commands;
using BCA.CarAuctionManagement.Domain.Requests.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

[Route("v1/vehicles")]
[Produces(MediaTypeNames.Application.Json)]
public class VehiclesController(IMediator mediator) : BaseApiController
{
    private readonly IMediator mediator = mediator;

    [HttpPost,
     ProducesResponseType(StatusCodes.Status201Created),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(StatusCodes.Status409Conflict),
     ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] Vehicle vehicle)
    {
        var command = new CreateVehicleCommand(vehicle.Map());

        var result = await mediator.Send(command);

        return result.AsCreatedAtRouteResult(nameof(GetVehicleByIdAsync), new { id = result.ValueOrDefault });
    }

    [HttpGet("{id:Guid}", Name = nameof(GetVehicleByIdAsync)),
     ProducesResponseType(typeof(Vehicle), StatusCodes.Status200OK),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(StatusCodes.Status404NotFound),
     ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetVehicleByIdAsync([FromRoute] Guid id)
    {
        var query = new GetVehicleByIdQuery(id);

        var result = await mediator.Send(query);

        return result.AsOkResult(result.ValueOrDefault.Map);
    }

    [HttpGet,
     ProducesResponseType(typeof(Dto.PagedResult<Vehicle>), StatusCodes.Status200OK),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchAsync(
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] IEnumerable<VehicleType> vehicleTypes,
        [FromQuery] IEnumerable<string> models,
        [FromQuery] IEnumerable<string> manufacturers,
        [FromQuery] IEnumerable<int> years)
    {
        var query = new SearchVehicleQuery(
            new PagedFilter(pageNumber, pageSize),
            vehicleTypes.Select(x => x.Map()),
            manufacturers,
            models,
            years);

        var result = await mediator.Send(query);

        return result.AsOkResult(() => result.ValueOrDefault.Map(vehicles => vehicles.Map()));
    }
}
