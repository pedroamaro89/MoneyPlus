using Microsoft.AspNetCore.Identity;
using MoneyPlus.Areas.Identity.Data;
using MoneyPlus.Services.Models;

namespace MoneyPlus.Services.Interfaces
{
    public interface IWallet
    {
        public int ID { get; set; }
        public MoneyPlusUser User { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

    }
}
