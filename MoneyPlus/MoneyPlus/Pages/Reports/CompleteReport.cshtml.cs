using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
	public class CompleteReport : PageModel
	{
		private readonly MoneyPlus.Data.MoneyPlusContext _context;
		private readonly TransactionRepository _transactionRepository;

		public CompleteReport(MoneyPlus.Data.MoneyPlusContext context, TransactionRepository transactionRepository)
		{
			_context = context;
			_transactionRepository = transactionRepository;
		}


		//public IList<Transaction> Transaction { get;set; } = default!;
		public List<CompleteReportModel> completeReport { get; set; }

		public List<string> months { get; set; }

		public List<string> catgs { get; set; }
		public List<string> subcatgs { get; set; }
		public List<string> years { get; set; }
		public List<string> catsandsubcats { get; set; }


		public Dictionary<string, double>  total { get; set; }

		public async Task OnGetAsync()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (_context.Transaction != null)
			{
				completeReport = await _transactionRepository.GetCompleteReport(userId);
			}

			total = completeReport.GroupBy(z => z.Month).ToDictionary(z => z.Key, z => z.Sum(f => f.Amount));

			var distMonths = completeReport.DistinctBy(y => y.Month).ToList();

			var distYears = completeReport.DistinctBy(y => y.Year).ToList();

			var distCategories = completeReport.DistinctBy(y => y.Category).ToList();

			var distSubCategories = completeReport.DistinctBy(y => y.SubCategory).ToList();


			years = new List<string>();

			foreach (var item in distYears)
			{
				years.Add(item.Year);
			}
			months = new List<string>();

			foreach (var item in distMonths)
			{
				months.Add(item.Month);
			}

			catgs = new List<string>();

			foreach (var item in distCategories)
			{
				catgs.Add(item.Category);
			}
			subcatgs = new List<string>();

			foreach (var item in distSubCategories)
			{
				subcatgs.Add(item.SubCategory);
			}

			for (int i = 0; i < distCategories.Count; i++)
			{
				for (int j = 0; j < distSubCategories.Count; j++)
				{
					catsandsubcats.Add(distCategories[i].Category);

					while (distSubCategories[j].Category == distCategories[i].Category)
					{
						catsandsubcats.Add(subcatgs[j]);
					}
				}
			}
		}

		}




	}

