using Application.Features.CategoryBooks.Commands.Create;
using Application.Features.CategoryBooks.Commands.Delete;
using Application.Features.CategoryBooks.Commands.Update;
using Application.Features.CategoryBooks.Queries.GetById;
using Application.Features.CategoryBooks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryBooksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCategoryBookCommand createCategoryBookCommand)
    {
        CreatedCategoryBookResponse response = await Mediator.Send(createCategoryBookCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryBookCommand updateCategoryBookCommand)
    {
        UpdatedCategoryBookResponse response = await Mediator.Send(updateCategoryBookCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedCategoryBookResponse response = await Mediator.Send(new DeleteCategoryBookCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdCategoryBookResponse response = await Mediator.Send(new GetByIdCategoryBookQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCategoryBookQuery getListCategoryBookQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCategoryBookListItemDto> response = await Mediator.Send(getListCategoryBookQuery);
        return Ok(response);
    }
}