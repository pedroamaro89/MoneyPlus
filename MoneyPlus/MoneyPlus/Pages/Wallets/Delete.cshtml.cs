using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Wallets
{
    public class DeleteModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public DeleteModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Wallet Wallet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Wallet == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallet.FirstOrDefaultAsync(m => m.ID == id);

            if (wallet == null)
            {
                return NotFound();
            }
            else 
            {
                Wallet = wallet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Wallet == null)
            {
                return NotFound();
            }
            var wallet = await _context.Wallet.FindAsync(id);

            if (wallet != null)
            {
                Wallet = wallet;
                _context.Wallet.Remove(Wallet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
