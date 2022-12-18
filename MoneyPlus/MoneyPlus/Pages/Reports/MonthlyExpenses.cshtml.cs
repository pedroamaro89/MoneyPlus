using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Repositories;
using MoneyPlus.Services.Models;
using static MoneyPlus.Repositories.TransactionRepository;

namespace MoneyPlus.Pages.Reports
{
	public class IndexModel : PageModel
	{
		private readonly MoneyPlus.Data.MoneyPlusContext _context;
		private readonly TransactionRepository _transactionRepository;

		public IndexModel(MoneyPlus.Data.MoneyPlusContext context, TransactionRepository transactionRepository)
		{
			_context = context;
			_transactionRepository = transactionRepository;
		}
		public List<TransactionRepository.MonthlyExpensesModel> monthExpenses { get; set; }

		public int CurrentMonth { get; set; }
		public int PrevMonth { get; set; }
		public int NextMonth { get; set; }

		[BindProperty]
		public int CategoryId { get; set; }

		public int AssetId { get; set; }
		public int PayeeId { get; set; }

		public async Task OnGetAsync()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			ViewData["CategoryId"] = new SelectList(_context.Category, "ID", "Name");
			ViewData["AssetId"] = new SelectList(_context.Asset, "ID", "Name");
			ViewData["PayeeId"] = new SelectList(_context.Payee, "ID", "Name");

			if (_context.Transaction != null)
			{
				var paramMonth = Request.Query["CurrentMonth"];

				if (paramMonth.Count != 0)
				{
					CurrentMonth = int.Parse(paramMonth);
				}
				else
				{
					CurrentMonth = System.DateTime.Now.Month;
				}

				PrevMonth = CurrentMonth - 1;
				NextMonth = CurrentMonth + 1;

				var paramCategory = Request.Query["CategoryId"];
				var paramAsset = Request.Query["AssetId"];
				var paramPayee = Request.Query["PayeeId"];

				int cat = 0, asset = 0, payee = 0;

				if (paramCategory.Count != 0 && int.Parse(paramCategory) != 0)
				{
					cat = int.Parse(paramCategory);
					CategoryId = cat;
				}

				if (paramAsset.Count != 0 && int.Parse(paramAsset) != 0)
				{
					asset = int.Parse(paramAsset);
					AssetId = asset;
				}

				if (paramPayee.Count != 0 && int.Parse(paramPayee) != 0)
				{
					payee = int.Parse(paramPayee);
					PayeeId = payee;
				}
				monthExpenses = await _transactionRepository.GetTransactionSummary(userId, CurrentMonth, cat, asset, payee);	
			}
		}
	}
}

