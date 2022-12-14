using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.SubCategories
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public CreateModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Category, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public SubCategory SubCategory { get; set; } = default!;
        public Category Category { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /* if (!ModelState.IsValid || _context.SubCategory == null || SubCategory == null)
               {
                   return Page();
               }*/

            var categoryID = int.Parse(Request.Query["id"]);
           // Category = _context.Category.Where(r => r.ID == categoryID).FirstOrDefault();

            SubCategory.CategoryId = categoryID;
            _context.SubCategory.Add(SubCategory);
            await _context.SaveChangesAsync();

			return RedirectToPage("../Subcategories/Details", new { id = SubCategory.ID });

		}
	}
}
