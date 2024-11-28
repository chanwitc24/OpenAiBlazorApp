using Microsoft.AspNetCore.Mvc;
using OpenAiBlazorApp.Application.Services;
using OpenAiBlazorApp.Core.Entities;

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
        public async Task<ActionResult<List<MonthlyAmount>>> GetV1()
        {
            var monthlyAmounts = await _monthlyAmountService.GetAllMonthlyAmountsAsync();
            return Ok(monthlyAmounts);
        }

        [HttpGet, MapToApiVersion("2.0")]
        public async Task<ActionResult<List<MonthlyAmount>>> GetV2()
        {
            var monthlyAmounts = await _monthlyAmountService.GetAllMonthlyAmountsAsync();
            return Ok(monthlyAmounts);
        }

        [HttpGet("{id:length(24)}", Name = "GetMonthlyAmount")]
        public async Task<ActionResult<MonthlyAmount>> Get(string id)
        {
            var monthlyAmount = await _monthlyAmountService.GetMonthlyAmountByIdAsync(id);
            if (monthlyAmount == null)
            {
                return NotFound();
            }
            return Ok(monthlyAmount);
        }

        [HttpPost]
        public async Task<ActionResult> Create(MonthlyAmount monthlyAmount)
        {
            await _monthlyAmountService.AddMonthlyAmountAsync(monthlyAmount);
            return CreatedAtRoute("GetMonthlyAmount", new { id = monthlyAmount.Id!.ToString() }, monthlyAmount);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, MonthlyAmount monthlyAmountIn)
        {
            if (id != monthlyAmountIn.Id)
            {
                return BadRequest();
            }
            await _monthlyAmountService.UpdateMonthlyAmountAsync(monthlyAmountIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _monthlyAmountService.DeleteMonthlyAmountAsync(id);
            return NoContent();
        }
    }
}









