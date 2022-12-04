using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Transfers
{
    public class EditModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public EditModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Transfer Transfer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Transfer == null)
            {
                return NotFound();
            }

            var transfer =  await _context.Transfer.FirstOrDefaultAsync(m => m.ID == id);
            if (transfer == null)
            {
                return NotFound();
            }
            Transfer = transfer;
           ViewData["DestinationWalletID"] = new SelectList(_context.Wallet, "ID", "ID");
           ViewData["OriginWalletID"] = new SelectList(_context.Wallet, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Transfer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransferExists(Transfer.ID))
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

        private bool TransferExists(int id)
        {
          return (_context.Transfer?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
