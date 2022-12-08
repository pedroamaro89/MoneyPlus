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
    public class DeleteModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public DeleteModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        [BindProperty]
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
                Transaction.Wallet = _context.Wallet.Where(r => r.ID == Transaction.WalletId).FirstOrDefault();
                Transaction.Payee = _context.Payee.Where(r => r.ID == Transaction.PayeeId).FirstOrDefault();
            }
            return Page();
        }
        public Wallet Wallet { get; set; }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            


            //Wallet.Balance = Wallet.Balance - Transaction.Amount;

            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }
            var transaction = await _context.Transaction.FindAsync(id);

            if (transaction != null)
            {
                Transaction = transaction;
                Transaction.Wallet = _context.Wallet.Where(r => r.ID == Transaction.WalletId).FirstOrDefault();
                //Transaction.Wallet.Balance = Transaction.Wallet.Balance - Transaction.Amount;

                if (Transaction.Type == 0)
                {
                    Transaction.Wallet.Balance = Transaction.Wallet.Balance - Transaction.Amount;

                }
                else
                {
                    Transaction.Wallet.Balance = Transaction.Wallet.Balance + Transaction.Amount;
                }

                _context.Transaction.Remove(Transaction);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
