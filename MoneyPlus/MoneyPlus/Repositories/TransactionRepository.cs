using Microsoft.EntityFrameworkCore;
using MoneyPlus.Pages.Reports;
using MoneyPlus.Services.Models;
using System.Linq;

namespace MoneyPlus.Repositories
{
    public class TransactionRepository
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;
        public TransactionRepository(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }
        public async Task<List<MonthlyExpensesModel>> GetTransactionSummary(string userId, int month, int filtCat, int filtAsset, int filtPayee)
        {

			var result = from trans in _context.Transaction
                         join wallet in _context.Wallet on trans.WalletId equals wallet.ID
                         join category in _context.Category on trans.CategoryID equals category.ID
                         join subcategory in _context.SubCategory on trans.SubCategoryID equals subcategory.ID
                         join payee in _context.Payee on trans.PayeeId equals payee.ID
						 join asset in _context.Asset on trans.AssetId equals asset.ID into as1
						 from a in as1.DefaultIfEmpty()
						 
						 where wallet.UserId == userId & trans.Type == 1 & trans.Date.Month == month
						 
						 select new MonthlyExpensesModel
            {
                Wallet = wallet.Name,
				CategoryId = category.ID,
				Category = category.Name,
				SubCategory = subcategory.Name,
				PayeeId = payee.ID,
				Payee = payee.Name,
                AssetId = a.ID,
				Asset = a.Name,
				Date = trans.Date,
                Amount = trans.Amount,
            };

			if (filtAsset != 0)
			{
				result = result.Where(c => c.AssetId == filtAsset);
			}
			if (filtCat != 0)
			{
				result = result.Where(c => c.CategoryId == filtCat);
			}

			if (filtPayee != 0)
			{
				result = result.Where(c => c.PayeeId == filtPayee);
			}

			return await result.ToListAsync();
        }


		public async Task<List<ExpensesByYearModel>> GetTransactionByYearSummary(string userId)
		{

			var result = from trans in _context.Transaction
						 join wallet in _context.Wallet on trans.WalletId equals wallet.ID
						 join category in _context.Category on trans.CategoryID equals category.ID
						 where wallet.UserId == userId & trans.Type == 1

                         select new ExpensesByYearModel
						 {
							 Category = category.Name,
							 Year = trans.Date.Year.ToString(),
							 Amount = trans.Amount,
						 };

            result = result.GroupBy(t => new { t.Category, t.Year })
					.Select(grp => new ExpensesByYearModel { Category = grp.First().Category, Year = grp.First().Year, Amount = grp.Sum(t => t.Amount) });


            return await result.ToListAsync();
		}

		public async Task<List<CompleteReportModel>> GetCompleteReport(string userId, int year)
		{

			var result = from trans in _context.Transaction
						 join wallet in _context.Wallet on trans.WalletId equals wallet.ID
						 join category in _context.Category on trans.CategoryID equals category.ID
						 join subcategory in _context.SubCategory on trans.SubCategoryID equals subcategory.ID
						 where wallet.UserId == userId & trans.Type == 1 & trans.Date.Year == year

						 select new CompleteReportModel
						 {
							 Category = category.Name,
							 SubCategory = subcategory.Name,	
							 Year = trans.Date.Year.ToString(),
							 Month = trans.Date.Month.ToString(),
							 Amount = trans.Amount,

						 };

			result = result.GroupBy(t => new { t.Category, t.SubCategory, t.Month })
					.Select(grp => new CompleteReportModel { Category = grp.First().Category, SubCategory = grp.First().SubCategory, Month = grp.First().Month, Amount = grp.Sum(t => t.Amount) });


			return await result.ToListAsync();
		}


		[Keyless]
		public class MonthlyExpensesModel
        {
            public string Wallet { get; set; }

			public int? CategoryId { get; set; }
			public string Category { get; set; }

            public string SubCategory { get; set; }
			public int? PayeeId { get; set; }
			public string Payee { get; set; }

			public int? AssetId { get; set; }
			public string Asset { get; set; }

            //public DateOnly Date { get; set; }
            public DateTime Date { get; set; }
            public double Amount { get; set; }
        }

		
		public class ExpensesByYearModel
		{
			public string Category { get; set; }
			public string Year { get; set; }
			public double Amount { get; set; }
		}


		public class CompleteReportModel
		{
			public string Category { get; set; }
			public string SubCategory { get; set; }

			public string Year { get; set; }
			public string Month { get; set; }

			public double Amount { get; set; }
		}
	}
}
