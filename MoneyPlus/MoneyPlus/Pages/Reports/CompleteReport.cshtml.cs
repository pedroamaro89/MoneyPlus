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

		//public List<int> months { get; set; }
		public Dictionary<int, string> months { get; set; }

		public List<string> catgs { get; set; }
		public List<string> subcatgs { get; set; }
	
		public int CurrentYear { get; set; }
		public int PrevYear { get; set; }
		public int NextYear { get; set; }

		//public double allYearSum { get; set; }	

		public List<KeyValuePair<int, string>> catsAndSubcats { get; set; }

        public Dictionary<string, double> total { get; set; }
		public Dictionary<string, double> totalcat { get; set; }
		public Dictionary<string, double> totalsubcat { get; set; }


		public async Task OnGetAsync()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (_context.Transaction != null)
			{
				var paramYear = Request.Query["CurrentYear"];

				if (paramYear.Count != 0)
				{
					CurrentYear = int.Parse(paramYear);
				}
				else
				{
					CurrentYear = System.DateTime.Now.Year;
				}

				PrevYear = CurrentYear - 1;
				NextYear = CurrentYear + 1;


				completeReport = await _transactionRepository.GetCompleteReport(userId, CurrentYear);
			}

			catsAndSubcats = new List<KeyValuePair<int, string>>();

			total = completeReport.GroupBy(z => z.Month).ToDictionary(z => z.Key, z => z.Sum(f => f.Amount));
			totalcat = completeReport.GroupBy(z => z.Category).ToDictionary(z => z.Key, z => z.Sum(f => f.Amount));
			totalsubcat = completeReport.GroupBy(z => z.SubCategory).ToDictionary(z => z.Key, z => z.Sum(f => f.Amount));
			var distMonths = completeReport.DistinctBy(y => y.Month).ToList();

			var distYears = completeReport.DistinctBy(y => y.Year).ToList();

			var distCategories = completeReport.DistinctBy(y => y.Category).ToList();

			var distSubCategories = completeReport.DistinctBy(y => y.SubCategory).ToList();


			/*years = new List<string>();

			foreach (var item in distYears)
			{
				years.Add(item.Year);
			}*/


			//months = new List<int>();
			months = new Dictionary<int, string>
			{
				{ 1, "Jan" },
				{ 2, "Feb" },
				{ 3, "Mar" },
				{ 4, "Apr" },
				{ 5, "May" },
				{ 6, "Jun" },
				{ 7, "Jul" },
				{ 8, "Aug" },
				{ 9, "Sep" },
				{ 10, "Oct" },
				{ 11, "Nov" },
				{ 12, "Dec" }
			};

			/*foreach (var item in distMonths)
			{
				months.Add(int.Parse(item.Month));
			}*/

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


            for (int i = 0; i < catgs.Count; i++)
			{
				catsAndSubcats.Add(new KeyValuePair<int, string>(0, catgs[i]));


				for (int j = 0; j < distSubCategories.Count; j++)
				{
					if (distSubCategories[j].Category == catgs[i])
						catsAndSubcats.Add(new KeyValuePair<int, string>(1, subcatgs[j]));

				}
			}





		}




	}
}

