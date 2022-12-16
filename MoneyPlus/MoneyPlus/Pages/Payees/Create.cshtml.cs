using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Payees
{
    public class CreateModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public CreateModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Payee Payee { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid || _context.Payee == null || Payee == null)
              {
                  return Page();
              }*/
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Payee.UserId = userId;
            _context.Payee.Add(Payee);
            await _context.SaveChangesAsync();

			return RedirectToPage("../Payees/Details", new { id = Payee.ID });

		}
	}
}
