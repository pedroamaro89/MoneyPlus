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

namespace MoneyPlus.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public IndexModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Transaction != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Transaction = await _context.Transaction
                .Include(t => t.Payee)
                .Include(t => t.Category)
                .Include(t => t.SubCategory)
                .Include(t => t.Wallet).Where(t => t.Wallet.UserId == userId).ToListAsync();
            }
        }
    }
}
