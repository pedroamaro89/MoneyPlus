using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Transactions
{
    public class EditModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public EditModel(MoneyPlus.Data.MoneyPlusContext context)
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
            Transaction = transaction;
            oldAmount = Transaction.Amount;
            ViewData["PayeeId"] = new SelectList(_context.Payee, "ID", "Name");
            ViewData["WalletId"] = new SelectList(_context.Wallet, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.



        public Wallet Wallet { get; set; }
        public string Type { get; set; }

        [BindProperty]
        public double oldAmount { get; set;}

        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            } */

            Transaction.Wallet = _context.Wallet.Where(r => r.ID == Transaction.WalletId).FirstOrDefault();

            //var oldTrans = _context.Transaction.Where(r => r.ID == Transaction.ID).FirstOrDefault();

            

            var amountDiference = Transaction.Amount - oldAmount;

            if (Transaction.Type == 0)
            {
                Transaction.Wallet.Balance = Transaction.Wallet.Balance + amountDiference;

            }
            else
            {
                Transaction.Wallet.Balance = Transaction.Wallet.Balance - amountDiference;
            }

            //Transaction.Wallet = Wallet;    

            //_context.Transaction.Add(Transaction);
            _context.Attach(Transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();


           // return RedirectToPage("./Index");
            return RedirectToPage("../Transactions/Details", new { id = Transaction.ID });

        }

        private bool TransactionExists(int id)
        {
            return (_context.Transaction?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
