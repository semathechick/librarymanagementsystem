using Application.Features.Rezervations.Commands.Create;
using Application.Features.Rezervations.Commands.Delete;
using Application.Features.Rezervations.Commands.Update;
using Application.Features.Rezervations.Queries.GetById;
using Application.Features.Rezervations.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Books.Queries.GetBookByCategoryId;
using Application.Features.Rezervations.Queries.GetListReservationByMemberId;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RezervationsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRezervationCommand createRezervationCommand)
    {
        CreatedRezervationResponse response = await Mediator.Send(createRezervationCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRezervationCommand updateRezervationCommand)
    {
        UpdatedRezervationResponse response = await Mediator.Send(updateRezervationCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedRezervationResponse response = await Mediator.Send(new DeleteRezervationCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdRezervationResponse response = await Mediator.Send(new GetByIdRezervationQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListRezervationQuery getListRezervationQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListRezervationListItemDto> response = await Mediator.Send(getListRezervationQuery);
        return Ok(response);
    }
    [HttpGet("getreservationsbymemberid")]
    public async Task<IActionResult> GetListReservationByMemberId([FromQuery] PageRequest pageRequest, Guid memberId)
    {
        GetListReservationByMemberId query = new() { PageRequest = pageRequest, MemberId = memberId };
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}