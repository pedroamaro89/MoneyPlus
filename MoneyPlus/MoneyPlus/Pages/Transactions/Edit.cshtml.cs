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
            ViewData["PayeeId"] = new SelectList(_context.Payee, "ID", "Name");
            ViewData["WalletId"] = new SelectList(_context.Wallet, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.



        public Wallet Wallet { get; set; }
        public string Type { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }
    var typeId = int.Parse(Request.Query["type"]);
            var walletID = int.Parse(Request.Query["id"]);
            Wallet = _context.Wallet.Where(r => r.ID == walletID).FirstOrDefault();*/

            //_context.Attach(Transaction).State = EntityState.Modified;

            var walletID = int.Parse(Request.Query["id"]);
            Wallet = _context.Wallet.Where(r => r.ID == walletID).FirstOrDefault();


            if (Transaction.Type == 0)
            {
                Wallet.Balance = Wallet.Balance + Transaction.Amount;

            }
            else
            {
                Wallet.Balance = Wallet.Balance - Transaction.Amount;
            }

            Transaction.Wallet = Wallet;    

            _context.Transaction.Add(Transaction);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }



            private bool TransactionExists(int id)
        {
            return (_context.Transaction?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
