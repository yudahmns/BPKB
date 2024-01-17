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
    public class LoginController : ControllerBase
    {
        private readonly BPKBContext _context;

        public LoginController(BPKBContext context)
        {
            _context = context;
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
            
            var first = _context.ms_user.FirstOrDefault(x => x.user_name == ms_user.user_name & x.password == ms_user.password);
            if (first is not null && first.user_id != 0)
            {
                ms_user.is_active = true;
                return Ok(ms_user);
            }

            while (true)
            {
                try
                {
                    ms_user.user_id = new Random().NextInt64();
                    _context.ms_user.Add(ms_user);
                    await _context.SaveChangesAsync();
                    ms_user.is_active = true;
                    return Ok(ms_user);
                    // return CreatedAtAction("Getms_user", new { id = ms_user.user_id }, ms_user);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }


        private bool ms_userExists(long id)
        {
            return (_context.ms_user?.Any(e => e.user_id == id)).GetValueOrDefault();
        }

    }
}
