using Microsoft.AspNetCore.Identity;
using MoneyPlus.Areas.Identity.Data;
using MoneyPlus.Services.Interfaces;

namespace MoneyPlus.Services.Models
{
    public class Wallet : IWallet
    {
        public int ID { get; set; } 
        public MoneyPlusUser User { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryID { get; set; }
        public double Amount { get; set; }
    }
}