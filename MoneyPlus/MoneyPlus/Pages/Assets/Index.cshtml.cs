using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Assets
{
    public class IndexModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public IndexModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public IList<Asset> Asset { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Asset != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                Asset = await _context.Asset.Where(t => t.UserId == userId).ToListAsync();
            }
        }
    }
}

