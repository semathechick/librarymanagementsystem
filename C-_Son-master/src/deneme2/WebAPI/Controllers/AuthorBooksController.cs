using Application.Features.AuthorBooks.Commands.Create;
using Application.Features.AuthorBooks.Commands.Delete;
using Application.Features.AuthorBooks.Commands.Update;
using Application.Features.AuthorBooks.Queries.GetById;
using Application.Features.AuthorBooks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorBooksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAuthorBookCommand createAuthorBookCommand)
    {
        CreatedAuthorBookResponse response = await Mediator.Send(createAuthorBookCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAuthorBookCommand updateAuthorBookCommand)
    {
        UpdatedAuthorBookResponse response = await Mediator.Send(updateAuthorBookCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedAuthorBookResponse response = await Mediator.Send(new DeleteAuthorBookCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAuthorBookResponse response = await Mediator.Send(new GetByIdAuthorBookQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAuthorBookQuery getListAuthorBookQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAuthorBookListItemDto> response = await Mediator.Send(getListAuthorBookQuery);
        return Ok(response);
    }
}