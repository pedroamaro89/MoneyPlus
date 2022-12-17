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

namespace MoneyPlus.Pages.Reports
{
	public class YearsWithoutWorkModel : PageModel
	{
		private readonly MoneyPlus.Data.MoneyPlusContext _context;
		private readonly TransactionRepository _transactionRepository;
		private readonly UserRepository _userRepository;

		public YearsWithoutWorkModel(MoneyPlus.Data.MoneyPlusContext context, TransactionRepository transactionRepository, UserRepository userRepository)
		{
			_context = context;
			_transactionRepository = transactionRepository;
			_userRepository = userRepository;
		}

		//public IList<Transaction> Transaction { get;set; } = default!;
		public List<TransactionRepository.ExpensesByYearModel> yearExpenses { get; set; }

		public double Inflation { get; set; }
        public double ROI { get; set; }

		public double TotalBalance { get; set; }

		public double LastYearExpenses { get; set; }

		public double CostOfLiving { get; set; }

		public double YearsWithoutWorking { get; set; }

        public Dictionary<string, double>  total { get; set; }

		public async Task OnGetAsync()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			LastYearExpenses = _userRepository.GetLastYearExpensesByUser(userId);

			TotalBalance = _userRepository.GetTotalBalanceByUser(userId);

			var paramInflation = Request.Query["Inflation"];
			var paramROI = Request.Query["ROI"];

			if (paramInflation.Count != 0 && double.Parse(paramInflation) != 0)
			{
				Inflation = double.Parse(paramInflation)/100;
			} 
			else
			{
				Inflation = 1.0;
			}

			if (paramROI.Count != 0 && double.Parse(paramROI) != 0)
			{
				ROI = double.Parse(paramROI)/100;
			}
			else
			{
				ROI = 1.0;

			}

			CostOfLiving = (LastYearExpenses * Inflation/100) + LastYearExpenses;

			double AvailableBalance = 0;

			YearsWithoutWorking = 0;

			AvailableBalance = TotalBalance;

			while (AvailableBalance >= 0)
			{
				AvailableBalance = AvailableBalance - CostOfLiving;
				AvailableBalance = (AvailableBalance * ROI/100) + AvailableBalance;

				YearsWithoutWorking += 1;

			}


		}

	}




}

