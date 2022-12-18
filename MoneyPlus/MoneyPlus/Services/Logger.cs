namespace MoneyPlus.Services
{
	public class Logger
	{
		public static void WriteLog (string message)
		{
			string logPath = @"log.txt";

			using (StreamWriter writer = new StreamWriter(logPath, true))
			{
				writer.WriteLine($"{DateTime.Now}: {message}");
			}
		}
	}
}
