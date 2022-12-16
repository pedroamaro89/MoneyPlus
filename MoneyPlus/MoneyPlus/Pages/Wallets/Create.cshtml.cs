using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyPlus.Areas.Identity.Data;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Wallets
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public CreateModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID", "Name");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Wallet Wallet { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            /*ModelState.Clear();
                      TryValidateModel(Wallet);
            TryValidateModel(Wallet.Category);*/


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Wallet.UserId = userId;

            /* if (!ModelState.IsValid || _context.Wallet == null || Wallet == null)
             {
                 return Page();
             }*/

            _context.Wallet.Add(Wallet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

	}
}
