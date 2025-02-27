namespace BCA.CarAuctionManagement.Api.Controllers.v1;

using System.Net.Mime;

using BCA.CarAuctionManagement.Api.Dto.v1;
using BCA.CarAuctionManagement.Api.Extensions;
using BCA.CarAuctionManagement.Api.Mappers;
using BCA.CarAuctionManagement.Domain.Requests.Commands;
using BCA.CarAuctionManagement.Domain.Requests.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

[Route("v1/users")]
[Produces(MediaTypeNames.Application.Json)]
public class UsersController(IMediator mediator) : BaseApiController
{
    private readonly IMediator mediator = mediator;

    [HttpPost,
     ProducesResponseType(StatusCodes.Status201Created),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(StatusCodes.Status409Conflict),
     ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] User user)
    {
        var command = new CreateUserCommand(user.Map());

        var result = await mediator.Send(command);

        return result.AsCreatedAtRouteResult(nameof(GetUserByIdAsync), new { id = result.ValueOrDefault });
    }

    [HttpGet("{id:Guid}", Name = nameof(GetUserByIdAsync)),
     ProducesResponseType(StatusCodes.Status200OK),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(StatusCodes.Status404NotFound),
     ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id)
    {
        var query = new GetUserByIdQuery(id);

        var result = await mediator.Send(query);

        return result.AsOkResult(result.ValueOrDefault.Map);
    }
}
