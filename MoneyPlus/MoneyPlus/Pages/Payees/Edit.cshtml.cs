using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Payees
{
    public class EditModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public EditModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Payee Payee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Payee == null)
            {
                return NotFound();
            }

            var payee =  await _context.Payee.FirstOrDefaultAsync(m => m.ID == id);
            if (payee == null)
            {
                return NotFound();
            }
            Payee = payee;
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Payee.UserId = userId;
            _context.Attach(Payee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayeeExists(Payee.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PayeeExists(int id)
        {
          return (_context.Payee?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
