﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Transfers
{
    public class IndexModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public IndexModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

        public IList<Transfer> Transfer { get;set; } = default!;
        public Transfer TransferW { get;set; }   
        public async Task OnGetAsync()
        {

            if (_context.Transfer != null)
            {
                /*Transfer = await _context.Transfer
                .Include(t => t.DestinationWallet)
                .Include(t => t.OriginWallet).ToListAsync();*/


                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Transfer = await _context.Transfer
                .Include(t => t.DestinationWallet)
                .Include(t => t.OriginWallet).Where(t => t.OriginWallet.UserId == userId && t.DestinationWallet.UserId == userId).ToListAsync();

                //Wallet = await _context.Wallet.Where(r => r.UserId == userId).ToListAsync();


            }
        }
    }
}
