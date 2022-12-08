using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;


namespace MoneyPlus.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public CreateModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PayeeId"] = new SelectList(_context.Payee, "ID", "Name");

            //Transaction.Category = _context.Category.Where(c => c.ID == Transaction.CategoryID).FirstOrDefault();
            ViewData["CategoryId"] = new SelectList(_context.Category, "ID", "Name");

            var walletID = int.Parse(Request.Query["id"]);
            Wallet = _context.Wallet.Where(r => r.ID == walletID).FirstOrDefault();

            var typeId = int.Parse(Request.Query["type"]);

            Type = Transaction.GetType(typeId);

            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = default!;

        public Wallet Wallet { get; set; }

        public string Type { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid || _context.Transaction == null || Transaction == null)
              {
                  return Page();
              }*/

            var walletID = int.Parse(Request.Query["id"]);
            Wallet = _context.Wallet.Where(r => r.ID == walletID).FirstOrDefault();

            

            var typeId = int.Parse(Request.Query["type"]);
            if (typeId == 0) //add money
            {
                Wallet.Balance = Wallet.Balance + Transaction.Amount;
            }
            else // add expense
            {
                Wallet.Balance = Wallet.Balance - Transaction.Amount;
            }

            Transaction.Wallet = Wallet;
            Transaction.Type = typeId;

            _context.Transaction.Add(Transaction);
            await _context.SaveChangesAsync();

            //return RedirectToPage("../Wallets/Details",Wallet.ID);
            return RedirectToPage("../Wallets/Details", new { id = Wallet.ID });

        }
    }
}
