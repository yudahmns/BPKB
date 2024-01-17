using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BPKB.API.Contexts;
using BPKB.API.Tables;

namespace BPKB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BPKBController : ControllerBase
    {
        private readonly BPKBContext _context;

        public BPKBController(BPKBContext context)
        {
            _context = context;
        }

        // GET: api/BPKB
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tr_bpkb>>> Gettr_bpkb()
        {
          if (_context.tr_bpkb == null)
          {
              return NotFound();
          }
            return await _context.tr_bpkb.ToListAsync();
        }

        // GET: api/BPKB/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tr_bpkb>> Gettr_bpkb(string id)
        {
          if (_context.tr_bpkb == null)
          {
              return NotFound();
          }
            var tr_bpkb = await _context.tr_bpkb.FindAsync(id);

            if (tr_bpkb == null)
            {
                return NotFound();
            }

            return tr_bpkb;
        }

        // PUT: api/BPKB/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttr_bpkb(string id, tr_bpkb tr_bpkb)
        {
            if (id != tr_bpkb.agreement_number)
            {
                return BadRequest();
            }

            _context.Entry(tr_bpkb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tr_bpkbExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BPKB
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tr_bpkb>> Posttr_bpkb(tr_bpkb tr_bpkb)
        {
          if (_context.tr_bpkb == null)
          {
              return Problem("Entity set 'BPKBContext.tr_bpkb'  is null.");
          }
            _context.tr_bpkb.Add(tr_bpkb);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tr_bpkbExists(tr_bpkb.agreement_number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Gettr_bpkb", new { id = tr_bpkb.agreement_number }, tr_bpkb);
        }

        // DELETE: api/BPKB/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletetr_bpkb(string id)
        {
            if (_context.tr_bpkb == null)
            {
                return NotFound();
            }
            var tr_bpkb = await _context.tr_bpkb.FindAsync(id);
            if (tr_bpkb == null)
            {
                return NotFound();
            }

            _context.tr_bpkb.Remove(tr_bpkb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tr_bpkbExists(string id)
        {
            return (_context.tr_bpkb?.Any(e => e.agreement_number == id)).GetValueOrDefault();
        }
    }
}
