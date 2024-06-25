using Application.Features.BookPublishers.Commands.Create;
using Application.Features.BookPublishers.Commands.Delete;
using Application.Features.BookPublishers.Commands.Update;
using Application.Features.BookPublishers.Queries.GetById;
using Application.Features.BookPublishers.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookPublishersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBookPublisherCommand createBookPublisherCommand)
    {
        CreatedBookPublisherResponse response = await Mediator.Send(createBookPublisherCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBookPublisherCommand updateBookPublisherCommand)
    {
        UpdatedBookPublisherResponse response = await Mediator.Send(updateBookPublisherCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedBookPublisherResponse response = await Mediator.Send(new DeleteBookPublisherCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBookPublisherResponse response = await Mediator.Send(new GetByIdBookPublisherQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBookPublisherQuery getListBookPublisherQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBookPublisherListItemDto> response = await Mediator.Send(getListBookPublisherQuery);
        return Ok(response);
    }
}