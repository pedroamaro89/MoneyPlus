using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Transfers
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
            // ViewData["DestinationWalletID"] = new SelectList(_context.Wallet, "ID", "ID");

            int originWalletID = 0;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var paramWallet = Request.Query["id"];
            if (paramWallet.Count != 0)
            {
                originWalletID = int.Parse(paramWallet);
                OriginWallet = _context.Wallet.Where(r => r.ID == originWalletID).FirstOrDefault();
                ViewData["DestinationWalletID"] = new SelectList(_context.Set<Wallet>().Where(x => x.ID != originWalletID && x.UserId == userId), "ID", "Name");
            }
            else
            {
                ViewData["OriginWalletID"] = new SelectList(_context.Set<Wallet>().Where(x => x.UserId == userId), "ID", "Name");
                ViewData["DestinationWalletID"] = new SelectList(_context.Set<Wallet>().Where(x =>x.UserId == userId), "ID", "Name");
            }

            //  ViewData["OriginWalletID"] = new SelectList(_context.Wallet, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Transfer Transfer { get; set; } = default!;
        public Wallet OriginWallet { get; set; }
        public Wallet DestinationWallet { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            int originWalletID = 0;
            var paramWallet = Request.Query["id"];

            if (paramWallet.Count != 0)
            {
                originWalletID = int.Parse(paramWallet);
                OriginWallet = _context.Wallet.Where(r => r.ID == originWalletID).FirstOrDefault();
            } else
            {
                OriginWallet = _context.Wallet.Where(r => r.ID == Transfer.OriginWalletID).FirstOrDefault();
            }

            DestinationWallet = _context.Wallet.Where(r => r.ID == Transfer.DestinationWalletID).FirstOrDefault();


            OriginWallet.Balance = OriginWallet.Balance - Transfer.Amount;
            DestinationWallet.Balance = DestinationWallet.Balance + Transfer.Amount;
            /* if (!ModelState.IsValid || _context.Transfer == null || Transfer == null)
             {
                 return Page();
             }*/

            Transfer.OriginWallet = OriginWallet;
            Transfer.DestinationWallet = DestinationWallet;
            _context.Transfer.Add(Transfer);
            await _context.SaveChangesAsync();

            //return RedirectToPage("./Index");
            return RedirectToPage("../Transfers/Details", new { id = Transfer.ID });

        }
    }
}
