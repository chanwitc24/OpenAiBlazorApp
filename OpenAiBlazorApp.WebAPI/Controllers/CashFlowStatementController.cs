using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.WebAPI.Models;
using OpenAiBlazorApp.WebAPI.Services;

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

    [HttpGet()]    
    public ActionResult<List<CashFlowStatement>> GetV1() =>
        _cashFlowStatementService.Get();
    [HttpGet, MapToApiVersion("2.0")]
    public ActionResult<List<CashFlowStatement>> GetV2() =>
        _cashFlowStatementService.Get();

    [HttpGet("{id:length(24)}", Name = "GetCashFlowStatement")]
    public ActionResult<CashFlowStatement> Get(string id)
    {
        var cashFlowStatement = _cashFlowStatementService.Get(id);
        if (cashFlowStatement == null)
        {
            return NotFound();
        }
        return cashFlowStatement;
    }
    [HttpPost]
    public ActionResult<CashFlowStatement> Create(CashFlowStatement cashFlowStatement)
    {
        _cashFlowStatementService.Create(cashFlowStatement);
        return CreatedAtRoute("GetCashFlowStatement", new { id = cashFlowStatement.Id!.ToString() }, cashFlowStatement);
    }
    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, CashFlowStatement cashFlowStatementIn)
    {
        var cashFlowStatement = _cashFlowStatementService.Get(id);
        if (cashFlowStatement == null)
        {
            return NotFound();
        }
        _cashFlowStatementService.Update(id, cashFlowStatementIn);
        return NoContent();
    }
    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var cashFlowStatement = _cashFlowStatementService.Get(id);
        if (cashFlowStatement == null)
        {
            return NotFound();
        }
        _cashFlowStatementService.Remove(cashFlowStatement.Id!);
        return NoContent();
    }
}
