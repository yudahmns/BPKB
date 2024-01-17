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
    public class BPKBController : Controller
    {
        private readonly BPKBContext _context;

        public BPKBController(BPKBContext context)
        {
            _context = context;
        }

        // GET: BPKB
        public async Task<IActionResult> Index()
        {
            var bPKBContext = _context.tr_bpkb.Include(t => t.location);
            return View(await bPKBContext.ToListAsync());
        }

        // GET: BPKB/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.tr_bpkb == null)
            {
                return NotFound();
            }

            var tr_bpkb = await _context.tr_bpkb
                .Include(t => t.location)
                .FirstOrDefaultAsync(m => m.agreement_number == id);
            if (tr_bpkb == null)
            {
                return NotFound();
            }

            return View(tr_bpkb);
        }

        public IActionResult IndexStorageLocation()
        {
            return RedirectToAction("Index", "StorageLocation");
        }

        // GET: BPKB/Create
        public IActionResult Create()
        {
            ViewData["location_id"] = new SelectList(_context.ms_storage_location, "location_id", "location_id");
            return View();
        }

        // POST: BPKB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("agreement_number,bpkb_no,branch_id,bpkb_date,faktur_no,faktur_date,location_id,police_no,bpkb_date_in,created_by,created_on,last_updated_by,last_updated_on")] tr_bpkb tr_bpkb)
        {
            if (ModelState.IsValid)
            {
                tr_bpkb.created_by = HttpContext.Session.GetString(nameof(BPKB.MVC.Tables.ms_user.user_name));
                tr_bpkb.created_on = DateTime.Now;
                _context.Add(tr_bpkb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["location_id"] = new SelectList(_context.ms_storage_location, "location_id", "location_id", tr_bpkb.location_id);
            return View(tr_bpkb);
        }

        // GET: BPKB/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.tr_bpkb == null)
            {
                return NotFound();
            }

            var tr_bpkb = await _context.tr_bpkb.FindAsync(id);
            if (tr_bpkb == null)
            {
                return NotFound();
            }
            ViewData["location_id"] = new SelectList(_context.ms_storage_location, "location_id", "location_id", tr_bpkb.location_id);
            return View(tr_bpkb);
        }

        // POST: BPKB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("agreement_number,bpkb_no,branch_id,bpkb_date,faktur_no,faktur_date,location_id,police_no,bpkb_date_in,created_by,created_on,last_updated_by,last_updated_on")] tr_bpkb tr_bpkb)
        {
            if (id != tr_bpkb.agreement_number)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tr_bpkb.last_updated_by = HttpContext.Session.GetString(nameof(BPKB.MVC.Tables.ms_user.user_name));
                    tr_bpkb.last_updated_on = DateTime.Now;
                    _context.Update(tr_bpkb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tr_bpkbExists(tr_bpkb.agreement_number))
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
            ViewData["location_id"] = new SelectList(_context.ms_storage_location, "location_id", "location_id", tr_bpkb.location_id);
            return View(tr_bpkb);
        }

        // GET: BPKB/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.tr_bpkb == null)
            {
                return NotFound();
            }

            var tr_bpkb = await _context.tr_bpkb
                .Include(t => t.location)
                .FirstOrDefaultAsync(m => m.agreement_number == id);
            if (tr_bpkb == null)
            {
                return NotFound();
            }

            return View(tr_bpkb);
        }

        // POST: BPKB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.tr_bpkb == null)
            {
                return Problem("Entity set 'BPKBContext.tr_bpkb'  is null.");
            }
            var tr_bpkb = await _context.tr_bpkb.FindAsync(id);
            if (tr_bpkb != null)
            {
                _context.tr_bpkb.Remove(tr_bpkb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tr_bpkbExists(string id)
        {
          return (_context.tr_bpkb?.Any(e => e.agreement_number == id)).GetValueOrDefault();
        }
    }
}
