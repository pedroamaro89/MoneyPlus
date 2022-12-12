using MoneyPlus.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace MoneyPlus.Services.Models
{
    public class Payee
    {

        public int ID { get; set; }

        public string Name { get; set; }
        public int? NIF { get; set; }
        public MoneyPlusUser User { get; set; }
        public string UserId { get; set; }

    }
}