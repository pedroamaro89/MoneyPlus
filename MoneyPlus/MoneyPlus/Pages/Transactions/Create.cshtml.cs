using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["PayeeId"] = new SelectList(_context.Payee.Where(x => x.UserId == userId), "ID", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "ID", "Name");
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "ID", "Name");
            ViewData["AssetId"] = new SelectList(_context.Asset.Where(x => x.UserId == userId), "ID", "Name");

            int walletID = 0, typeId = 0;

            var paramWallet = Request.Query["id"];
            if (paramWallet.Count != 0)
            {
                walletID = int.Parse(paramWallet);

                Wallet = _context.Wallet.Where(r => r.ID == walletID).FirstOrDefault();
            }
            else
            {
                ViewData["WalletID"] = new SelectList(_context.Set<Wallet>().Where(x => x.UserId == userId), "ID", "Name");
            }

            var paramType = Request.Query["type"];
            if (paramType.Count != 0)
            {
                typeId = int.Parse(paramType);

                Type = Transaction.GetType(typeId);
            }
            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = default!;

        public Wallet Wallet { get; set; }

        public string Type { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
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

            if (Transaction.AssetId==0) 
            {
                Transaction.AssetId = null;
            }

            Transaction.Wallet = Wallet;
            Transaction.Type = typeId;

            _context.Transaction.Add(Transaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Transactions/Details", new { id = Transaction.ID });

        }


	
	}
}
