using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.WebAPI.Models;
using OpenAiBlazorApp.WebAPI.Services;

namespace OpenAiBlazorApp.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MonthlyAmountController : ControllerBase
    {
        private readonly MonthlyAmountService _monthlyAmountService;

        public MonthlyAmountController(MonthlyAmountService monthlyAmountService)
        {
            _monthlyAmountService = monthlyAmountService;
        }

        [HttpGet]
        public ActionResult<List<MonthlyAmount>> GetV1() =>
            _monthlyAmountService.Get();

        [HttpGet, MapToApiVersion("2.0")]
        public ActionResult<List<MonthlyAmount>> GetV2() =>
            _monthlyAmountService.Get();

        [HttpGet("{id:length(24)}", Name = "GetMonthlyAmount")]
        public ActionResult<MonthlyAmount> Get(string id)
        {
            var monthlyAmount = _monthlyAmountService.Get(id);
            if (monthlyAmount == null)
            {
                return NotFound();
            }
            return monthlyAmount;
        }

        [HttpPost]
        public ActionResult<MonthlyAmount> Create(MonthlyAmount monthlyAmount)
        {
            _monthlyAmountService.Create(monthlyAmount);
            return CreatedAtRoute("GetMonthlyAmount", new { id = monthlyAmount.Id!.ToString() }, monthlyAmount);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, MonthlyAmount monthlyAmountIn)
        {
            var monthlyAmount = _monthlyAmountService.Get(id);
            if (monthlyAmount == null)
            {
                return NotFound();
            }
            _monthlyAmountService.Update(id, monthlyAmountIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var monthlyAmount = _monthlyAmountService.Get(id);
            if (monthlyAmount == null)
            {
                return NotFound();
            }
            _monthlyAmountService.Remove(monthlyAmount.Id!);
            return NoContent();
        }
    }
}









