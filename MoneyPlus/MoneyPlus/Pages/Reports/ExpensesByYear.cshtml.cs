using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Repositories;
using MoneyPlus.Services.Models;
using static MoneyPlus.Repositories.TransactionRepository;
//using Dapper;

namespace MoneyPlus.Pages.Reports
{
	public class ExpensesByYearModel : PageModel
	{
		private readonly MoneyPlus.Data.MoneyPlusContext _context;
		private readonly TransactionRepository _transactionRepository;

		public ExpensesByYearModel(MoneyPlus.Data.MoneyPlusContext context, TransactionRepository transactionRepository)
		{
			_context = context;
			_transactionRepository = transactionRepository;
		}

		//public IList<Transaction> Transaction { get;set; } = default!;
		public List<TransactionRepository.ExpensesByYearModel> yearExpenses { get; set; }

		public List<string> years { get; set; }

		public List<string> catgs { get; set; }

		public Dictionary<string, double>  total { get; set; }

		public async Task OnGetAsync()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (_context.Transaction != null)
			{
				yearExpenses = await _transactionRepository.GetTransactionByYearSummary(userId);
			}

			

			total = yearExpenses.GroupBy(z => z.Year).ToDictionary(z => z.Key, z => z.Sum(f => f.Amount)); 

			var distYears = yearExpenses.DistinctBy(y => y.Year).ToList();

			var distCategories = yearExpenses.DistinctBy(y => y.Category).ToList();

			years = new List<string>();

			foreach (var item in distYears)
			{
				years.Add(item.Year);
			}

			catgs = new List<string>();

			foreach (var item in distCategories)
			{
				catgs.Add(item.Category);
			}
		}

	}




}

