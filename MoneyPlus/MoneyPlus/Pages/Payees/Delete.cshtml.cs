using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Payees
{
    public class DeleteModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public DeleteModel(MoneyPlus.Data.MoneyPlusContext context)
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

            var payee = await _context.Payee.FirstOrDefaultAsync(m => m.ID == id);

            if (payee == null)
            {
                return NotFound();
            }
            else 
            {
                Payee = payee;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Payee == null)
            {
                return NotFound();
            }
            var payee = await _context.Payee.FindAsync(id);

            if (payee != null)
            {
                Payee = payee;
                _context.Payee.Remove(Payee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
