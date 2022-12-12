using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.SubCategories
{
    public class DetailsModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public DetailsModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

      public SubCategory SubCategory { get; set; } = default!;
      

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubCategory == null)
            {
                return NotFound();
            }

            var subcategory = await _context.SubCategory.FirstOrDefaultAsync(m => m.ID == id);
            if (subcategory == null)
            {
                return NotFound();
            }
            else
            {
                SubCategory = subcategory;
                SubCategory.Category = _context.Category.Where(r => r.ID == SubCategory.CategoryId).FirstOrDefault();
            }
            return Page();
        }
    }
}
