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
            /* if (id == null || _context.Wallet == null)
             {
                 return NotFound();
             }

             var wallet = await _context.Wallet.FirstOrDefaultAsync(m => m.ID == id);
             if (wallet == null)
             {
                 return NotFound();
             }
             else 
             {
                 Wallet = wallet;
             }*/

            if (_context.Transaction != null)
            {
                var walletID = int.Parse(Request.Query["id"]);
                Wallet = _context.Wallet.Where(r => r.ID == walletID).FirstOrDefault();
                Transfer = await _context.Transfer.Where(r => r.OriginWalletID == walletID | r.DestinationWalletID == walletID).ToListAsync();
                // DestinationWallet = _context.Transfer.Where(r => r.DestinationWalletID == DestinationWallet.).FirstOrDefault();
                //Wallet.Category = _context.Category.Where(r => r.ID == walletID).FirstOrDefault();

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

                Transaction = await _context.Transaction.Where(r => r.WalletId == Wallet.ID).ToListAsync();

                foreach (var item in Transaction)
                {
                    if (item.Payee == null)
                    {
                        item.Payee = _context.Payee.Where(r => r.ID == item.PayeeId).FirstOrDefault();
                    }

                    //Type = Trans.GetType(item.Type);
                   
                }


                //var typeId = int.Parse(Request.Query["type"]);

                

            }

            if (_context.Transfer != null)
            {
            }

            return Page();
        }
    }
}



