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
    public class UserController : Controller
    {
        private readonly BPKBContext _context;

        public UserController(BPKBContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
              return _context.ms_user != null ? 
                          View(await _context.ms_user.ToListAsync()) :
                          Problem("Entity set 'BPKBContext.ms_user'  is null.");
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ms_user == null)
            {
                return NotFound();
            }

            var ms_user = await _context.ms_user
                .FirstOrDefaultAsync(m => m.user_id == id);
            if (ms_user == null)
            {
                return NotFound();
            }

            return View(ms_user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("user_id,user_name,password,is_active")] ms_user ms_user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ms_user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ms_user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.ms_user == null)
            {
                return NotFound();
            }

            var ms_user = await _context.ms_user.FindAsync(id);
            if (ms_user == null)
            {
                return NotFound();
            }
            return View(ms_user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("user_id,user_name,password,is_active")] ms_user ms_user)
        {
            if (id != ms_user.user_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ms_user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ms_userExists(ms_user.user_id))
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
            return View(ms_user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.ms_user == null)
            {
                return NotFound();
            }

            var ms_user = await _context.ms_user
                .FirstOrDefaultAsync(m => m.user_id == id);
            if (ms_user == null)
            {
                return NotFound();
            }

            return View(ms_user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.ms_user == null)
            {
                return Problem("Entity set 'BPKBContext.ms_user'  is null.");
            }
            var ms_user = await _context.ms_user.FindAsync(id);
            if (ms_user != null)
            {
                _context.ms_user.Remove(ms_user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ms_userExists(long id)
        {
          return (_context.ms_user?.Any(e => e.user_id == id)).GetValueOrDefault();
        }
    }
}
