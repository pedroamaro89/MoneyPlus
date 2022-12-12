﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Pages.Assets
{
    public class DetailsModel : PageModel
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;

        public DetailsModel(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }

      public Asset Asset { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Asset == null)
            {
                return NotFound();
            }

            var asset = await _context.Asset.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null)
            {
                return NotFound();
            }
            else 
            {
                Asset = asset;
            }
            return Page();
        }
    }
}
