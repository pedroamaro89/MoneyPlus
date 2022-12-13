using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public IndexModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        public IList<SubCategory> SubCategory { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Category != null)
            {
                //Subcategories.Category = _context.Category.Where(r => r.ID == Subcategories.CategoryId).FirstOrDefault();

                Category = await _context.Category.ToListAsync();
            }

            if (_context.SubCategory != null)
            {
                /*  SubCategory = await _context.SubCategory
                  .Include(s => s.Category).ToListAsync();*/

                SubCategory = await _context.SubCategory.ToListAsync();
            }
        }
    }
}
