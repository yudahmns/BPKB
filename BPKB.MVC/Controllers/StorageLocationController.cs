using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPKB.MVC.Contexts;
using BPKB.MVC.Tables;

namespace BPKB.MVC.Controllers
{
    public class StorageLocationController : Controller
    {
        private readonly BPKBContext _context;

        public StorageLocationController(BPKBContext context)
        {
            _context = context;
        }

        // GET: StorageLocation
        public async Task<IActionResult> Index()
        {
              return _context.ms_storage_location != null ? 
                          View(await _context.ms_storage_location.ToListAsync()) :
                          Problem("Entity set 'BPKBContext.ms_storage_location'  is null.");
        }

        // GET: StorageLocation/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ms_storage_location == null)
            {
                return NotFound();
            }

            var ms_storage_location = await _context.ms_storage_location
                .FirstOrDefaultAsync(m => m.location_id == id);
            if (ms_storage_location == null)
            {
                return NotFound();
            }

            return View(ms_storage_location);
        }

        public IActionResult IndexBPKB()
        {
            return RedirectToAction("Index", "BPKB");
        }

        // GET: StorageLocation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StorageLocation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("location_id,location_name")] ms_storage_location ms_storage_location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ms_storage_location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ms_storage_location);
        }

        // GET: StorageLocation/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ms_storage_location == null)
            {
                return NotFound();
            }

            var ms_storage_location = await _context.ms_storage_location.FindAsync(id);
            if (ms_storage_location == null)
            {
                return NotFound();
            }
            return View(ms_storage_location);
        }

        // POST: StorageLocation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("location_id,location_name")] ms_storage_location ms_storage_location)
        {
            if (id != ms_storage_location.location_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ms_storage_location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ms_storage_locationExists(ms_storage_location.location_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ms_storage_location);
        }

        // GET: StorageLocation/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ms_storage_location == null)
            {
                return NotFound();
            }

            var ms_storage_location = await _context.ms_storage_location
                .FirstOrDefaultAsync(m => m.location_id == id);
            if (ms_storage_location == null)
            {
                return NotFound();
            }

            return View(ms_storage_location);
        }

        // POST: StorageLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ms_storage_location == null)
            {
                return Problem("Entity set 'BPKBContext.ms_storage_location'  is null.");
            }
            var ms_storage_location = await _context.ms_storage_location.FindAsync(id);
            if (ms_storage_location != null)
            {
                _context.ms_storage_location.Remove(ms_storage_location);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ms_storage_locationExists(string id)
        {
          return (_context.ms_storage_location?.Any(e => e.location_id == id)).GetValueOrDefault();
        }
    }
}
