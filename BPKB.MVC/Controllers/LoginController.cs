using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPKB.MVC.Contexts;
using BPKB.MVC.Tables;
using System.Text.Json;
using System.Text;

namespace BPKB.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly BPKBContext _context;
        private readonly HttpClient _client;

        public LoginController(BPKBContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        // GET: User/Create
        public IActionResult Index()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("user_id,user_name,password,is_active")] ms_user ms_user)
        {
            var loginPostUri = "https://localhost:7271/api/Login";
            var content = new StringContent(JsonSerializer.Serialize(ms_user),Encoding.UTF8,System.Net.Mime.MediaTypeNames.Application.Json);
            using var x = await _client.PostAsync(loginPostUri, content);
            using var xx = await x.Content.ReadAsStreamAsync();
            var xxx = JsonSerializer.Deserialize<BPKB.MVC.Tables.ms_user>(xx);
            if (xxx is null || !xxx.is_active)
            {
                return View();
            }

            if (xxx.is_active)
            {
                HttpContext.Session.SetString(nameof(xxx.user_name), xxx.user_name ?? string.Empty);
                return RedirectToAction("Index", "BPKB");
            }

            return View();

        }

    }
}
