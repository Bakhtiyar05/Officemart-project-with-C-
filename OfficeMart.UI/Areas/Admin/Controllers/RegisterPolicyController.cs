using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Domain.Models.AppDbContext;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RegisterPolicyController : Controller
    {
        private readonly OfficeMartContext _context;
        public RegisterPolicyController(OfficeMartContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(_context.RegisterPolicy.FirstOrDefault());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegisterPolicy registerPolicy)
        {
            if (registerPolicy == null) return NotFound();
            var registerPolicyDB = _context.RegisterPolicy.FirstOrDefault();
            registerPolicyDB.Policy = registerPolicy.Policy;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

