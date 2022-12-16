using MoneyPlus.Areas.Identity.Data;

namespace MoneyPlus.Services.Models
{
	public class EmailLogs
	{
		public int ID { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string EmailFrom { get; set; }
		public string EmailTo { get; set; }
		public DateTime Date { get; set; }
		

	}
}
