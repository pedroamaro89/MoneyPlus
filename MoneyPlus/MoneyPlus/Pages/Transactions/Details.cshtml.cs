using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Transactions
{
    public class DetailsModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public DetailsModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

      public Transaction Transaction { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction.FirstOrDefaultAsync(m => m.ID == id);
            if (transaction == null)
            {
                return NotFound();
            }
            else 
            {
                Transaction = transaction;
                Transaction.Category = _context.Category.Where(r => r.ID == Transaction.CategoryID).FirstOrDefault();
                Transaction.Wallet = _context.Wallet.Where(r => r.ID == Transaction.WalletId).FirstOrDefault();
                Transaction.Payee = _context.Payee.Where(r => r.ID == Transaction.PayeeId).FirstOrDefault();
                Transaction.SubCategory = _context.SubCategory.Where(r => r.ID == Transaction.SubCategoryID).FirstOrDefault();
                Transaction.Asset = _context.Asset.Where(r => r.ID == Transaction.AssetId).FirstOrDefault();
            }





            return Page();
        }
    }
}
