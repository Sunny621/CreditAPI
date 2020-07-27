using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreditCard.Models;
using CreditDemo.Models;
using Microsoft.Extensions.Logging;

namespace CreditDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardItemController : ControllerBase
    {
        private readonly CreditCardContext _context;
        private readonly ILogger<CreditCardItemController> _logger;


        public CreditCardItemController(CreditCardContext context, ILogger<CreditCardItemController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/CreditCardItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCardItem>>> GetCreditCardItems()
        {
            return await _context.CreditCardItems.ToListAsync();
        }

        // GET: api/CreditCardItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditCardItem>> GetCreditCardItem(int id)
        {
            var creditCardItem = await _context.CreditCardItems.FindAsync(id);
            _logger.LogInformation($"Get credit card info for id: {id}");
            if (creditCardItem == null)
            {
                _logger.LogDebug($"No credit card info found for id: {id}");
                return NotFound();
            }
            return creditCardItem;
        }

        // PUT: api/CreditCardItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditCardItem(int id, CreditCardItem creditCardItem)
        {
            _logger.LogInformation($"Modify credit card info for id: {id}");

            if (!ModelState.IsValid)
            {
                _logger.LogDebug("Invalid data.");
                return BadRequest("Invalid data.");
            }

            if (id != creditCardItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(creditCardItem).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCardItemExists(id))
                {
                    _logger.LogDebug($"Can't find credit card info for id : {id}");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return NoContent();
        }

        // POST: api/CreditCardItem
        [HttpPost]
        public async Task<ActionResult<CreditCardItem>> PostCreditCardItem(CreditCardItem creditCardItem)
        {
            _logger.LogInformation("Create credit card info");
            if (!ModelState.IsValid)
            {
                _logger.LogDebug("Invalid data.");
                return BadRequest("Invalid data.");
            }
            _context.CreditCardItems.Add(creditCardItem);

            await _context.SaveChangesAsync();
            return NoContent();

        }

        // DELETE: api/CreditCardItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CreditCardItem>> DeleteCreditCardItem(int id)
        {
            _logger.LogInformation($"Delete credit card info for {id}");
            var creditCardItem = await _context.CreditCardItems.FindAsync(id);
            if (creditCardItem == null)
            {
                _logger.LogDebug($"Can't find credit card info for id : {id}");
                return NotFound();
            }

            _context.CreditCardItems.Remove(creditCardItem);
            await _context.SaveChangesAsync();

            return creditCardItem;
        }

        private bool CreditCardItemExists(int id)
        {
            return _context.CreditCardItems.Any(e => e.Id == id);
        }

    }
}
