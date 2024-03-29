﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Transfers
{
    public class DeleteModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public DeleteModel(MoneyPlus.Data.MoneyPlusContext context)
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

            var transfer = await _context.Transfer.FirstOrDefaultAsync(m => m.ID == id);

            if (transfer == null)
            {
                return NotFound();
            }
            else 
            {
                Transfer = transfer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Transfer == null)
            {
                return NotFound();
            }
            var transfer = await _context.Transfer.FindAsync(id);

            if (transfer != null)
            {
                Transfer = transfer;
                _context.Transfer.Remove(Transfer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
