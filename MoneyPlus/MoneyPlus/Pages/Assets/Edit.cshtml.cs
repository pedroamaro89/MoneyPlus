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
using MoneyPlus.Services;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Assets
{
    public class EditModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public EditModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Asset == null)
            {
                return NotFound();
            }

            var asset =  await _context.Asset.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null)
            {
                return NotFound();
            }
            Asset = asset;
           //["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Attach(Asset).State = EntityState.Modified;
            Asset.UserId = userId;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(Asset.ID))
                {
                    return NotFound();
                }
                else
                {
                    Logger.WriteLog("Error editing Assets");
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AssetExists(int id)
        {
          return (_context.Asset?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
