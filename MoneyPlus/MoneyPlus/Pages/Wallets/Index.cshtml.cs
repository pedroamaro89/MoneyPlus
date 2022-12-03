using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Wallets
{
    public class IndexModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public IndexModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public IList<Wallet> Wallet { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Wallet != null)
            {
                Wallet = await _context.Wallet
                .Include(w => w.Category)
                .Include(w => w.User).ToListAsync();
            }
        }
    }
}
