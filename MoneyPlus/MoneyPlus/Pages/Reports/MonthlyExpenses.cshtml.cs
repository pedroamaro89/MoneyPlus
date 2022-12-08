using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Repositories;
using MoneyPlus.Services.Models;

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

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_context.Transaction != null)
            {
                var paramMonth = Request.Query["month"];

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

                monthExpenses = await _transactionRepository.GetTransactionSummary(userId, CurrentMonth);

                /* Transaction = await _context.Transaction
                 .Include(t => t.Payee)
                 .Include(t => t.Wallet).Where(r=> ).ToListAsync()*/

                /*var parameters = new List<object>();
                parameters.Add(new SqlParameter("userId", userId));
                parameters.Add(new SqlParameter("month", 12));*/

                /*IList<MonthlyExpensesModel> list = _context.Database.SqlQuery<MonthlyExpensesModel>("SELECT w.Name AS Wallet,  c.Name AS Category, p.Name AS Payee," +
                    " CONVERT(date, t.Date) AS Date, t.Amount" +
                    "FROM dbo.[Transaction]" +
                    "INNER JOIN Wallet w ON (w.ID = WalletId)" +
                    "INNER JOIN Category c ON (c.ID = CategoryID)" +
                    "INNER JOIN Payee p ON (p.ID = PayeeId)" +
                    "WHERE w.UserId= @userId" +
                    "AND Type = 1" +
                    "AND DATEPART(MONTH, t.Date) = @month", parameters.ToArray()).ToList();*/

                /* string query = "SELECT w.Name AS Wallet,  c.Name AS Category, p.Name AS Payee," +
                  " CONVERT(date, t.Date) AS Date, t.Amount" +
                  "FROM dbo.[Transaction]" +
                  "INNER JOIN Wallet w ON (w.ID = WalletId)" +
                  "INNER JOIN Category c ON (c.ID = CategoryID)" +
                  "INNER JOIN Payee p ON (p.ID = PayeeId)" +
                  "WHERE w.UserId=" + userId +
                  " AND Type = 1" +
                  "AND DATEPART(MONTH, t.Date) = 12";*/

                /*FormattableString query = $"SELECT * from wallet";
                var list1 = _context.Database.SqlQuery<MonthlyExpensesModel>(query).ToList();

                Task<List<CityViewModel>> result = from trans in _context.Transaction
                             join wallet in _context.Wallet on trans.WalletId equals wallet.ID
                             join category in _context.Category on wallet.CategoryID equals category.ID
                             join payee in _context.Payee on trans.PayeeId equals payee.ID
                             where wallet.UserId == userId & trans.Type == 1
                             select new MonthlyExpensesModel
                             {
                                 Wallet = wallet.Name,
                                 Category = category.Name,
                                 Payee = payee.Name,
                                 Date = trans.Date,
                                 Amount = trans.Amount,
                             };

                result= result.ToListAsync();*/

                // Transaction = await _context.Transaction.Where(r => r.UserId == userId).ToListAsync();
            }
        }

       /* public async Task<List<CityViewModel>> GetCitySummary(string cityName)
        {
            var result = from city in _ctx.Cities
                         join country in _ctx.Countries on city.CountryId equals country.Id
                         where city.Name == cityName
                         select new CityViewModel
                         {
                             CityName = city.Name,
                             CountryName = country.Name,
                         };

            return await result.ToListAsync();
        }*/
    }

    /*public class MonthlyExpensesModel
    {
        public string Wallet { get; set; }
        public string Category { get; set; }
        public string Payee { get; set; }

        //public DateOnly Date { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
    */
}

