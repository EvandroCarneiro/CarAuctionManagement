namespace BCA.CarAuctionManagement.Api.Controllers.v1;

using System.Net.Mime;

using BCA.CarAuctionManagement.Api.Dto.v1;
using BCA.CarAuctionManagement.Api.Extensions;
using BCA.CarAuctionManagement.Api.Mappers;
using BCA.CarAuctionManagement.Domain.Requests.Commands;
using BCA.CarAuctionManagement.Domain.Requests.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

[Route("v1/auctions")]
[Produces(MediaTypeNames.Application.Json)]
public class AuctionsController(IMediator mediator) : BaseApiController
{
    private readonly IMediator mediator = mediator;

    [HttpPost,
     ProducesResponseType(StatusCodes.Status201Created),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(StatusCodes.Status409Conflict),
     ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] Auction auction)
    {
        var command = new CreateAuctionCommand(auction.Map());

        var result = await mediator.Send(command);

        return result.AsCreatedAtRouteResult(nameof(GetAuctionByIdAsync), new { id = result.ValueOrDefault });
    }

    [HttpGet("{id:Guid}", Name = nameof(GetAuctionByIdAsync)),
     ProducesResponseType(StatusCodes.Status200OK),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(StatusCodes.Status404NotFound),
     ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAuctionByIdAsync([FromRoute] Guid id)
    {
        var query = new GetAuctionByIdQuery(id);

        var result = await mediator.Send(query);

        return result.AsOkResult(result.ValueOrDefault.Map);
    }

    [HttpPut("{id:Guid}/start"),
     ProducesResponseType(StatusCodes.Status201Created),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(StatusCodes.Status409Conflict),
     ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> StartAuctionAsync([FromRoute] Guid id)
    {
        var command = new StartAuctionCommand(id);

        var result = await mediator.Send(command);

        return result.AsOkResult();
    }

    [HttpPut("{id:Guid}/close"),
     ProducesResponseType(StatusCodes.Status201Created),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(StatusCodes.Status409Conflict),
     ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CloseAuctionAsync([FromRoute] Guid id)
    {
        var command = new CloseAuctionCommand(id);

        var result = await mediator.Send(command);

        return result.AsOkResult();
    }

    [HttpPost("{id:Guid}/bids"),
     ProducesResponseType(StatusCodes.Status201Created),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(StatusCodes.Status409Conflict),
     ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PlaceBidAsync([FromRoute] Guid id, [FromBody] Bid bid)
    {
        var command = new PlaceBidCommand(id, bid.Map());

        var result = await mediator.Send(command);

        return result.AsOkResult();
    }
}
