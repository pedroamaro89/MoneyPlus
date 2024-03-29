﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Transfers
{
    public class DetailsModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public DetailsModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public Transfer Transfer { get; set; } = default!;
        public Wallet OriginWallet { get; set; } = default!;
        public Wallet DestinationWallet { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {



            if (id == null || _context.Transfer == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfer.FirstOrDefaultAsync(m => m.ID == id);
            if (transfer == null)
            {
                return NotFound();
            }
            else 
            {
                Transfer = transfer;
               // var transferID = int.Parse(Request.Query["id"]);
                OriginWallet = _context.Wallet.Where(r => r.ID == Transfer.OriginWalletID).FirstOrDefault();
                DestinationWallet = _context.Wallet.Where(r => r.ID == Transfer.DestinationWalletID).FirstOrDefault();

            }
            return Page();
        }
    }
}
