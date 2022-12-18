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
    public class DetailsModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public DetailsModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        //public Category Category { get; set; }
        public Wallet Wallet { get; set; } = default!;
        public Transfer DestinationWallet { get; set; }
        public IList<Transaction> Transaction { get; set; }
        public IList<Transfer> Transfer { get; set; } = default!;
        public Transaction Trans { get; set; }
        public string Type { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_context.Transaction != null)
            {
                var walletID = int.Parse(Request.Query["id"]);
                Wallet = _context.Wallet.Where(r => r.ID == walletID).FirstOrDefault();
                Transfer = await _context.Transfer.Where(r => r.OriginWalletID == walletID | r.DestinationWalletID == walletID).ToListAsync();

                foreach (var item in Transfer)
                {
                    if (item.OriginWallet == null)
                    {
                        item.OriginWallet = _context.Wallet.Where(r => r.ID == item.OriginWalletID).FirstOrDefault();
                    }
                    else if (item.DestinationWallet == null)
                    {
                        item.DestinationWallet = _context.Wallet.Where(r => r.ID == item.DestinationWalletID).FirstOrDefault();
                    }
                }


                Transaction = await _context.Transaction
                    .Include(t => t.Payee)
                    .Include(t => t.Category)
                    .Include(t => t.SubCategory)
                    .Include(t => t.Asset)
                    .Where(t => t.WalletId == Wallet.ID).ToListAsync();

            }

            if (_context.Transfer != null)
            {
            }

            return Page();
        }
    }
}



