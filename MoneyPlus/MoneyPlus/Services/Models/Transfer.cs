namespace MoneyPlus.Services.Models
{
    public class Transfer 
    {
        public int ID { get; set; }
        public Wallet OriginWallet { get; set; }
        public int OriginWalletID { get; set; }
        public Wallet DestinationWallet { get; set; }
        public int DestinationWalletID { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

    }
}
