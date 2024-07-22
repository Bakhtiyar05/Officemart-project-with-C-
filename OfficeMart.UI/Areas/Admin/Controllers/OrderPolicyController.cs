using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Domain.Models.AppDbContext;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderPolicyController : Controller
    {
        private readonly OfficeMartContext _context;
        public OrderPolicyController(OfficeMartContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(_context.OrderPolicy.FirstOrDefault());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(OrderPolicy orderPolicy)
        {
            if (orderPolicy == null) return NotFound();
            var orderPolicyDB = _context.OrderPolicy.FirstOrDefault();
            orderPolicyDB.Policy = orderPolicy.Policy;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

