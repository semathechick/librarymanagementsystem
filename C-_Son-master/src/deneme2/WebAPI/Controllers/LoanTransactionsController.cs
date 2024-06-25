using Application.Features.LoanTransactions.Commands.Create;
using Application.Features.LoanTransactions.Commands.Delete;
using Application.Features.LoanTransactions.Commands.Update;
using Application.Features.LoanTransactions.Queries.GetById;
using Application.Features.LoanTransactions.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoanTransactionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLoanTransactionCommand createLoanTransactionCommand)
    {
        CreatedLoanTransactionResponse response = await Mediator.Send(createLoanTransactionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLoanTransactionCommand updateLoanTransactionCommand)
    {
        UpdatedLoanTransactionResponse response = await Mediator.Send(updateLoanTransactionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLoanTransactionResponse response = await Mediator.Send(new DeleteLoanTransactionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLoanTransactionResponse response = await Mediator.Send(new GetByIdLoanTransactionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLoanTransactionQuery getListLoanTransactionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLoanTransactionListItemDto> response = await Mediator.Send(getListLoanTransactionQuery);
        return Ok(response);
    }
}