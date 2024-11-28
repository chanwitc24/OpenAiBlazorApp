using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.Application.Services;
using OpenAiBlazorApp.Core.Entities;

namespace OpenAiBlazorApp.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CashflowController : ControllerBase
    {
        private readonly CashflowService _cashflowService;

        public CashflowController(CashflowService cashflowService)
        {
            _cashflowService = cashflowService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cashflow>>> GetV1()
        {
            var cashflows = await _cashflowService.GetAllCashflowsAsync();
            return Ok(cashflows);
        }

        [HttpGet, MapToApiVersion("2.0")]
        public async Task<ActionResult<List<Cashflow>>> GetV2()
        {
            var cashflows = await _cashflowService.GetAllCashflowsAsync();
            return Ok(cashflows);
        }

        [HttpGet("{id:length(24)}", Name = "GetCashflow")]
        public async Task<ActionResult<Cashflow>> Get(string id)
        {
            var cashflow = await _cashflowService.GetCashflowByIdAsync(id);
            if (cashflow == null)
            {
                return NotFound();
            }
            return Ok(cashflow);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Cashflow cashflow)
        {
            await _cashflowService.AddCashflowAsync(cashflow);
            return CreatedAtRoute("GetCashflow", new { id = cashflow.Id!.ToString() }, cashflow);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, Cashflow cashflowIn)
        {
            if (id != cashflowIn.Id)
            {
                return BadRequest();
            }
            await _cashflowService.UpdateCashflowAsync(cashflowIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _cashflowService.DeleteCashflowAsync(id);
            return NoContent();
        }
    }
}








