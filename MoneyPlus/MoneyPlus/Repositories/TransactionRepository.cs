using Microsoft.EntityFrameworkCore;

namespace MoneyPlus.Repositories
{
    public class TransactionRepository
    {
        private readonly MoneyPlus.Data.MoneyPlusContext _context;
        public TransactionRepository(MoneyPlus.Data.MoneyPlusContext context)
        {
            _context = context;
        }
        public async Task<List<MonthlyExpensesModel>> GetTransactionSummary(string userId, int month)
        {
            var result = from trans in _context.Transaction
                                  join wallet in _context.Wallet on trans.WalletId equals wallet.ID
                                  join category in _context.Category on trans.CategoryID equals category.ID
                                  join payee in _context.Payee on trans.PayeeId equals payee.ID
                               // join asset in _context.Asset on trans.AssetId equals asset.ID
                                where wallet.UserId == userId & trans.Type == 1 & trans.Date.Month == month
                                  select new MonthlyExpensesModel
                                  {
                                      Wallet = wallet.Name,
                                      Category = category.Name,
                                      Payee = payee.Name,
                                   //   Asset = asset.Name,
                                      Date = trans.Date,
                                      Amount = trans.Amount,
                                  };

            return await result.ToListAsync();
        }

        public class MonthlyExpensesModel
        {
            public string Wallet { get; set; }
            public string Category { get; set; }

            //public string SubCategory { get; set; }
            public string Payee { get; set; }

          //  public string Asset { get; set; }

            //public DateOnly Date { get; set; }
            public DateTime Date { get; set; }
            public double Amount { get; set; }
        }

    }
}
