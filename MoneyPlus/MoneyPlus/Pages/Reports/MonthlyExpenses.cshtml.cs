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
//using Dapper;

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

		//public IList<Transaction> Transaction { get;set; } = default!;
		public List<TransactionRepository.MonthlyExpensesModel> monthExpenses { get; set; }

		public int CurrentMonth { get; set; }
		public int PrevMonth { get; set; }
		public int NextMonth { get; set; }

		[BindProperty]
		public int CategoryId { get; set; }

		public int AssetId { get; set; }
		public int PayeeId { get; set; }


		/*public IActionResult OnGetFilter()
        {
            var catId = CategoryId;
			var paramMonth = Request.Query["cat"];

			return Page();
        }*/

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

				/*var parameters = new List<object>();
				parameters.Add(new SqlParameter("userId", userId));
				parameters.Add(new SqlParameter("month", 12));

				IList<MonthlyExpensesModel> list = _context.Database.SqlQueryRaw<MonthlyExpensesModel>("SELECT w.Name AS Wallet,  c.Name AS Category, p.Name AS Payee," +
					" CONVERT(date, t.Date) AS Date, t.Amount" +
					"FROM dbo.[Transaction]" +
					"INNER JOIN Wallet w ON (w.ID = WalletId)" +
					"INNER JOIN Category c ON (c.ID = CategoryID)" +
					"INNER JOIN Payee p ON (p.ID = PayeeId)" +
					"WHERE w.UserId= @userId" +
					"AND Type = 1" +
					"AND DATEPART(MONTH, t.Date) = @month", parameters.ToArray()).ToList();

				using (var con = new SqlConnection(ConfigurationManager.Con))
				{
					return con.Query<Movie>("SELECT * FROM Movies");
				}
                */

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

				/*if (paramCategory.Count != 0)
				{*/
				monthExpenses = await _transactionRepository.GetTransactionSummary(userId, CurrentMonth, cat, asset, payee);
				
			}
		}

	
	}



}

