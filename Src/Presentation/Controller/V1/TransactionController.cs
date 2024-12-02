using Asp.Versioning;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Transaction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controller.V1;

[ApiVersion("1")]
public class TransactionController : ApiController
{
    public TransactionController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<Responses.TransactionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Transactions([FromQuery] Query.GetTransactionQuery request)
    {
        var result = await sender.Send(request);

        if (!result.IsSuccess)
            HandlerFailure(result);
        return Ok(result);
    }

    [HttpGet("{TransactionId:Guid}")]
    [ProducesResponseType(typeof(Result<Responses.TransactionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Transactions(Guid TransactionId)
    {
        var result = await sender.Send(new Query.GetTransactionByIdQuery(TransactionId));

        if (!result.IsSuccess)
            HandlerFailure(result);
        return Ok(result);
    }

    //[HttpPost("AppointmentTransaction")]
    //[ProducesResponseType(typeof(Result<Responses.CreateTransactionResponse>), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> Transactions(Command.CreateAppointmentTransactionCommand request)
    //{
    //    var result = await sender.Send(request);

    //    if (!result.IsSuccess)
    //        HandlerFailure(result);
    //    return Ok(result);
    //}

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.CreateTransactionResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBalance(Command.CreateAddBalanceTransactionCommand request)
    {
        var result = await sender.Send(request);

        if (!result.IsSuccess)
            HandlerFailure(result);
        return Ok(result);
    }


    [HttpPut("{TransactionId:Guid}")]
    [ProducesResponseType(typeof(Result<Responses.TransactionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Transactions([FromRoute] Guid TransactionId, Command.UpdateTransactionCommand request)
    {
        var updateTransaction = request with { Id = TransactionId };
        var result = await sender.Send(updateTransaction);

        if (!result.IsSuccess)
            HandlerFailure(result);
        return Ok(result);
    }
}
