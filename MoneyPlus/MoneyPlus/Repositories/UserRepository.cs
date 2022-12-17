using Microsoft.EntityFrameworkCore;

namespace MoneyPlus.Repositories
{
	public class UserRepository
	{
		private readonly MoneyPlus.Data.MoneyPlusContext _context;


		public UserRepository(MoneyPlus.Data.MoneyPlusContext context)
		{
			_context = context;
		}

		public double GetLastYearExpensesByUser(string userID)
		{
			var expenses = _context.Transaction.Include(t => t.Wallet).Where(t => t.Wallet.UserId == userID && t.Type == 1).Sum(t => t.Amount);

			return expenses;
		}

		public double GetTotalBalanceByUser(string userID)
		{
			var balance = _context.Wallet.Where(t => t.UserId == userID).Sum(t => t.Balance);
			return balance;
		}
	}
}
