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
    public class UserController : ControllerBase
    {
        private readonly BPKBContext _context;

        public UserController(BPKBContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ms_user>>> Getms_user()
        {
          if (_context.ms_user == null)
          {
              return NotFound();
          }
            return await _context.ms_user.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ms_user>> Getms_user(long id)
        {
          if (_context.ms_user == null)
          {
              return NotFound();
          }
            var ms_user = await _context.ms_user.FindAsync(id);

            if (ms_user == null)
            {
                return NotFound();
            }

            return ms_user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putms_user(long id, ms_user ms_user)
        {
            if (id != ms_user.user_id)
            {
                return BadRequest();
            }

            _context.Entry(ms_user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ms_userExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ms_user>> Postms_user(ms_user ms_user)
        {
          if (_context.ms_user == null)
          {
              return Problem("Entity set 'BPKBContext.ms_user'  is null.");
          }
            _context.ms_user.Add(ms_user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ms_userExists(ms_user.user_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getms_user", new { id = ms_user.user_id }, ms_user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletems_user(long id)
        {
            if (_context.ms_user == null)
            {
                return NotFound();
            }
            var ms_user = await _context.ms_user.FindAsync(id);
            if (ms_user == null)
            {
                return NotFound();
            }

            _context.ms_user.Remove(ms_user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ms_userExists(long id)
        {
            return (_context.ms_user?.Any(e => e.user_id == id)).GetValueOrDefault();
        }
    }
}
