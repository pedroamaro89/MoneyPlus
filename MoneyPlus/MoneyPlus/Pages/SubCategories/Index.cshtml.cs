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
    public class IndexModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public IndexModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public IList<SubCategory> SubCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.SubCategory != null)
            {
                /*  SubCategory = await _context.SubCategory
                  .Include(s => s.Category).ToListAsync();*/

                SubCategory = await _context.SubCategory.ToListAsync();
            }
        }
    }
}
