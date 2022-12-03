using Microsoft.Build.Framework;

namespace MoneyPlus.Services.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public Wallet Wallet { get; set; }
        [Required]

        public int WalletId { get; set; }
        [Required]

        public Payee Payee { get; set; }
        public int PayeeId { get; set; }

        [Required]

        public DateTime Date { get; set; }
        public string Description { get; set; }
        [Required]

        public double Amount { get; set; }

        public int Type { get; set; } //0 - reforço // 1 - despesa
    }
}
