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
    public class StorageLocationController : ControllerBase
    {
        private readonly BPKBContext _context;

        public StorageLocationController(BPKBContext context)
        {
            _context = context;
        }

        // GET: api/StorageLocation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ms_storage_location>>> Getms_storage_location()
        {
          if (_context.ms_storage_location == null)
          {
              return NotFound();
          }
            return await _context.ms_storage_location.ToListAsync();
        }

        // GET: api/StorageLocation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ms_storage_location>> Getms_storage_location(string id)
        {
          if (_context.ms_storage_location == null)
          {
              return NotFound();
          }
            var ms_storage_location = await _context.ms_storage_location.FindAsync(id);

            if (ms_storage_location == null)
            {
                return NotFound();
            }

            return ms_storage_location;
        }

        // PUT: api/StorageLocation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putms_storage_location(string id, ms_storage_location ms_storage_location)
        {
            if (id != ms_storage_location.location_id)
            {
                return BadRequest();
            }

            _context.Entry(ms_storage_location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ms_storage_locationExists(id))
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

        // POST: api/StorageLocation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ms_storage_location>> Postms_storage_location(ms_storage_location ms_storage_location)
        {
          if (_context.ms_storage_location == null)
          {
              return Problem("Entity set 'BPKBContext.ms_storage_location'  is null.");
          }
            _context.ms_storage_location.Add(ms_storage_location);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ms_storage_locationExists(ms_storage_location.location_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getms_storage_location", new { id = ms_storage_location.location_id }, ms_storage_location);
        }

        // DELETE: api/StorageLocation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletems_storage_location(string id)
        {
            if (_context.ms_storage_location == null)
            {
                return NotFound();
            }
            var ms_storage_location = await _context.ms_storage_location.FindAsync(id);
            if (ms_storage_location == null)
            {
                return NotFound();
            }

            _context.ms_storage_location.Remove(ms_storage_location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ms_storage_locationExists(string id)
        {
            return (_context.ms_storage_location?.Any(e => e.location_id == id)).GetValueOrDefault();
        }
    }
}
