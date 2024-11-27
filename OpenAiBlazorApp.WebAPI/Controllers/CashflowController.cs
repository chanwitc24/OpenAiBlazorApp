using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.WebAPI.Models;
using OpenAiBlazorApp.WebAPI.Services;

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
        public ActionResult<List<Cashflow>> GetV1() =>
            _cashflowService.Get();

        [HttpGet, MapToApiVersion("2.0")]
        public ActionResult<List<Cashflow>> GetV2() =>
            _cashflowService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCashflow")]
        public ActionResult<Cashflow> Get(string id)
        {
            var cashflow = _cashflowService.Get(id);
            if (cashflow == null)
            {
                return NotFound();
            }
            return cashflow;
        }

        [HttpPost]
        public ActionResult<Cashflow> Create(Cashflow cashflow)
        {
            _cashflowService.Create(cashflow);
            return CreatedAtRoute("GetCashflow", new { id = cashflow.Id!.ToString() }, cashflow);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Cashflow cashflowIn)
        {
            var cashflow = _cashflowService.Get(id);
            if (cashflow == null)
            {
                return NotFound();
            }
            _cashflowService.Update(id, cashflowIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var cashflow = _cashflowService.Get(id);
            if (cashflow == null)
            {
                return NotFound();
            }
            _cashflowService.Remove(cashflow.Id!);
            return NoContent();
        }
    }
}








