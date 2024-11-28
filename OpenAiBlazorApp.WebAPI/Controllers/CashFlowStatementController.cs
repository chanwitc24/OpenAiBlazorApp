using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.Application.Services;
using OpenAiBlazorApp.Core.Entities;

namespace OpenAiBlazorApp.WebAPI.Controllers;
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CashFlowStatementController : ControllerBase
{
    private readonly CashFlowStatementService _cashFlowStatementService;

    public CashFlowStatementController(CashFlowStatementService cashFlowStatementService)
    {
        _cashFlowStatementService = cashFlowStatementService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CashFlowStatement>>> GetV1()
    {
        var cashFlowStatements = await _cashFlowStatementService.GetAllCashFlowStatementsAsync();
        return Ok(cashFlowStatements);
    }

    [HttpGet, MapToApiVersion("2.0")]
    public async Task<ActionResult<List<CashFlowStatement>>> GetV2()
    {
        var cashFlowStatements = await _cashFlowStatementService.GetAllCashFlowStatementsAsync();
        return Ok(cashFlowStatements);
    }

    [HttpGet("{id:length(24)}", Name = "GetCashFlowStatement")]
    public async Task<ActionResult<CashFlowStatement>> Get(string id)
    {
        var cashFlowStatement = await _cashFlowStatementService.GetCashFlowStatementByIdAsync(id);
        if (cashFlowStatement == null)
        {
            return NotFound();
        }
        return Ok(cashFlowStatement);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CashFlowStatement cashFlowStatement)
    {
        await _cashFlowStatementService.AddCashFlowStatementAsync(cashFlowStatement);
        return CreatedAtRoute("GetCashFlowStatement", new { id = cashFlowStatement.Id!.ToString() }, cashFlowStatement);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<ActionResult> Update(string id, CashFlowStatement cashFlowStatementIn)
    {
        if (id != cashFlowStatementIn.Id)
        {
            return BadRequest();
        }
        await _cashFlowStatementService.UpdateCashFlowStatementAsync(cashFlowStatementIn);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _cashFlowStatementService.DeleteCashFlowStatementAsync(id);
        return NoContent();
    }
}
