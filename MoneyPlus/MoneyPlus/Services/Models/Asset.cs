using MoneyPlus.Areas.Identity.Data;

namespace MoneyPlus.Services.Models
{
    public class Asset
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public MoneyPlusUser User { get; set; }
        public string UserId { get; set; }

    }
}
