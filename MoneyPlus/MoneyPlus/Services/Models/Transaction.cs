using Microsoft.Build.Framework;
using NuGet.ContentModel;
using System.Transactions;

namespace MoneyPlus.Services.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public Wallet Wallet { get; set; }
        [Required]

        public int WalletId { get; set; }
        
        [Required]
        public Category Category { get; set; }

        public int CategoryID { get; set; }

        [Required]
        public SubCategory SubCategory { get; set; }

        public int SubCategoryID { get; set; }

        [Required]

        public Payee Payee { get; set; }
        public int PayeeId { get; set; }

        [Required]

        public DateTime Date { get; set; }
        public string? Description { get; set; }
        [Required]

        public double Amount { get; set; }

        public int Type { get; set; } //0 - reforço // 1 - despesa

        public Asset Asset { get; set; }
        public int? AssetId { get; set; }


        public static string GetType(int idType)
        {
            string type = "";
            if (idType == 0)
            {
                type = "Add Money";
            }
            else
            {
                type = "Add Expense";
            }
            return type;
        }
    }

   
}
